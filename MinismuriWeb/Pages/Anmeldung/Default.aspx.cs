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
            storage.Save();

            EmailHelper mail = new EmailHelper(new MailAddress(emailadresse));
            mail.Subject = string.Format("Bestätigung {0}", EventName);
            mail.Body = string.Format(@"Guten Tag

Hiermit bestätigen wir Ihre {0} für den Event ""{1}"".
Name: {2}
Bemerkung: {3}

Freundliche Grüsse
Leitungsteam Ministranten Muri
",                  
            row.IstAnmeldung ? "Anmeldung" : "Abmeldung",
            EventName,
            row.Name,
            row.Bemerkung
                );

            mail.Send();

            Server.Transfer("Success.aspx");
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