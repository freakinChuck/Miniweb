using MinismuriWeb.Storage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Pages.Anmeldung
{
    public partial class Default : System.Web.UI.Page
    {
        private string EventHash
        {
            get { return Request["Event"]; }
        }

        private string EventId
        {
            get
            {
                try
                {
                    return new Guid(Convert.FromBase64String(EventHash)).ToString();
                }
                catch { return string.Empty; }
            }
        }

        public string EventName
        {
            get;
            set;
        }
        public DateTime Anmeldeschluss { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadZusazuinfoRepeater();    
            }

		    emailTextBox.BorderColor = Color.Empty;
		    nameTextBox.BorderColor = Color.Empty;

            var storage = EventStorage.LoadStorage();
            var row = storage.DataSet.Event.FirstOrDefault(r => r.Id == EventId);

            if (row == null)
            {
                anmeldungDiv.Visible = false;
                nichtExistentDiv.Visible = true;
                anmeldefristAbgelaufenDiv.Visible = false;
            }
            else
            {
                EventName = row.Name;
                beschreibungText.InnerText = row.Beschreibung;
                Anmeldeschluss = row.AnmeldefristEnde;
                anmeldungDiv.Visible = true;
                nichtExistentDiv.Visible = false;
                anmeldefristAbgelaufenDiv.Visible = false;
            
                if (row.AnmeldefristEnde.Date < DateTime.Now.Date)
                {
                    anmeldefristAbgelaufenDiv.Visible = true;
                }
            }
        }

        private void LoadZusazuinfoRepeater()
        {
            var storage = EventStorage.LoadStorage();
            var data = storage.DataSet.Zusatzinformation.Where(z => z.EventId == EventId).OrderBy(e => e.Ordnungszahl).Select(e => new { Feldname = e.Feldname, Typ = e.Typ, Id = e.Id }).ToList();
            zusatzInfoRepeater.DataSource = data;
            zusatzInfoRepeater.DataBind();
        }

        protected void speichernButton_Click(object sender, EventArgs e)
        {
            bool isInvalid = false;


            if (!noBot.IsValid())
            {
                genericErrorLiteral.Text = "Unser Anti-Spam System hat uns ein verdächtiges Verhalten gemeldet. Falls Sie ein Mensch sind, möchten wir uns für jegliche Unannehmlichkeiten entschuldigen.";
                isInvalid = true;
            }
            
            string name = nameTextBox.Text;
            string emailadresse = emailTextBox.Text;
            string bemerkung = bemerkungTextBox.Text;

            if (!IsValidMail(emailadresse))
	        {
                isInvalid = true;
		        emailTextBox.BorderColor = Color.Red;
	        }

            if (string.IsNullOrWhiteSpace(name))
	        {
		        isInvalid = true;
		        nameTextBox.BorderColor = Color.Red;
	        }

            if (isInvalid)
	        {
		        return;
	        }

            EventStorage storage = EventStorage.LoadStorage();
            var row = storage.DataSet.Anmeldung.NewAnmeldungRow();
            row.Id = Guid.NewGuid().ToString();
            row.EventId = EventId;
            row.IstAnmeldung = anmeldungRadioButton.Checked;
            row.Emailadresse = emailadresse;
            row.Name = name;
            row.Bemerkung = bemerkung;
            row.Zeitpunkt = DateTime.Now;
            storage.DataSet.Anmeldung.AddAnmeldungRow(row);

            List<KeyValuePair<string, string>> zusatzinfo = new List<KeyValuePair<string, string>>();

            foreach (RepeaterItem item in zusatzInfoRepeater.Items)
            {
                string id = ((HiddenField)item.FindControl("idHiddenField")).Value;
                string feldname = ((HiddenField)item.FindControl("feldnameHiddenField")).Value;
                int typ = int.Parse(((HiddenField)item.FindControl("typHiddenField")).Value);
                string text = ((TextBox)item.FindControl("freitextTextField")).Text;
                bool check = ((CheckBox)item.FindControl("janeinCheckBox")).Checked;

                var infoRow = storage.DataSet.Zusatzantwort.NewZusatzantwortRow();

                string antwort = typ == (int)EventStorage.Antworttyp.Text ? text : check ? "Ja" : "Nein";
                infoRow.AnmeldungId = row.Id;
                infoRow.Antwort = antwort;
                infoRow.Id = Guid.NewGuid().ToString();
                infoRow.ZusatzinfoId = id;

                zusatzinfo.Add(new KeyValuePair<string, string>(feldname, antwort));

                storage.DataSet.Zusatzantwort.AddZusatzantwortRow(infoRow);
            }

            storage.Save();

            string zusatzinfoText = string.Empty;

            foreach (var item in zusatzinfo)
	        {
		        zusatzinfoText = string.Format("{0}{1}: {2}\n", zusatzinfoText, item.Key, item.Value);
	        }

            EmailHelper mail = new EmailHelper(new MailAddress(emailadresse));
            mail.Subject = string.Format("Bestätigung {0}", EventName);
            mail.Body = string.Format(@"Guten Tag

Hiermit bestätigen wir Ihre {0} für den Event ""{1}"".
Name: {2}
{4}Bemerkung: {3}

Freundliche Grüsse
Leitungsteam Ministranten Muri
",                  
            row.IstAnmeldung ? "Anmeldung" : "Abmeldung",
            EventName,
            row.Name,
            row.Bemerkung,
            zusatzinfoText
                );

            mail.Send();

            Server.Transfer(string.Format("Success.aspx?Name={0}&Anmeldung={1}", row.Name, row.IstAnmeldung));
        }

        public bool IsValidMail(string emailaddress)
        {
            if (string.IsNullOrWhiteSpace(emailaddress))
            {
                return false;
            }
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}