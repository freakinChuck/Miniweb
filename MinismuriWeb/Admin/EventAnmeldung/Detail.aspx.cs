using ExcelExportHelper;
using MinismuriWeb.Storage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Admin.EventAnmeldung
{
    public partial class Detail : System.Web.UI.Page
    {
        private bool IsEventadmin
        {
            get
            {
                var storage = Storage.DataStorage.LoadStorage();
                return storage.DataSet.Benutzer.Any(b => b.Benutzername.ToLower() == User.Identity.Name.ToLower() && b.Eventadmin);
            }
        }
        public bool IsEdit
        {
            get
            {
                return (Request["Edit"] ?? string.Empty).ToLower() == bool.TrueString.ToLower();
            }
        }
        public string EventId
        {
            get
            {
                return Request["Event"];
            }
        }

        public string Eventname
        {
            get;
            set;
        }

        public bool IsNew
        {
            get { return string.IsNullOrEmpty(EventId); }
        }

        public DateTime Anmeldeschluss { get; set; }
        public string Beschreibung { get; set; }
        public string VerantwortlicherMail { get; set; }

        public string LinkUrl
        {
            get
            {
                return string.Format("{0}/Pages/Anmeldung/Default.aspx?Event={1}",
                    string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority),
                    Server.UrlEncode(Convert.ToBase64String(Guid.Parse(EventId).ToByteArray()))
                    );
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadZusatzinformation();                
            }

            eventNameTextBox.BorderColor = Color.Empty;
            anmeldefristTextBox.BorderColor = Color.Empty;
            verantwortlicherTextBox.BorderColor = Color.Empty;

            displayDiv.Visible = !IsEdit && !IsNew;
            editDiv.Visible = IsEdit || IsNew;
            if (!IsPostBack || !IsEdit)
            {
                var storage = EventStorage.LoadStorage();
                var row = storage.DataSet.Event.FirstOrDefault(r => r.Id == EventId);

                if (row != null)
                {
                    Eventname = row.Name;
                    Anmeldeschluss = row.AnmeldefristEnde;
                    Beschreibung = row.Beschreibung;
                    VerantwortlicherMail = row.VerantwortlicherEmail;

                    var antworten = storage.DataSet.Anmeldung.Where(a => a.EventId == EventId).Select(x => new { Name = x.Name, Email = x.Emailadresse, Zeitpunkt = x.Zeitpunkt, Bemerkung = x.Bemerkung, IsAnmeldung = x.IstAnmeldung }).ToList();
                    anmeldungenRepeater.DataSource = antworten.Where(a => a.IsAnmeldung).OrderByDescending(a => a.Zeitpunkt).ToList();
                    abmeldungenRepeater.DataSource = antworten.Where(a => !a.IsAnmeldung).OrderByDescending(a => a.Zeitpunkt).ToList();
                    anmeldungenRepeater.DataBind();
                    abmeldungenRepeater.DataBind();

                    if (IsEdit)
                    {
                        eventNameTextBox.Text = Eventname;
                        anmeldefristTextBox.Text = Anmeldeschluss.ToShortDateString();
                        beschreibungTextBox.Text = Beschreibung;
                    }
                }   
            }
        }

        private void LoadZusatzinformation()
        {
            var storage = EventStorage.LoadStorage();
            var dataTmp = storage.DataSet.Zusatzinformation
                .Where(z => z.EventId == EventId)
                .OrderBy(z => z.Ordnungszahl).ToList();
            var data = dataTmp.Select(z =>
                    new
                    {
                        Feldname = z.Feldname,
                        Typ = GetText((EventStorage.Antworttyp)z.Typ),
                        Id = z.Id,
                        Ordnungzahl = z.Ordnungszahl,
                        IsFirst = z.Ordnungszahl == dataTmp.Min(x => x.Ordnungszahl),
                        IsLast = z.Ordnungszahl == dataTmp.Max(x => x.Ordnungszahl)
                    }
                 )
                .ToList();
            zusatzfelderRepeater.DataSource = data;
            zusatzfelderRepeater.DataBind();
        }

        private string GetText(EventStorage.Antworttyp antworttyp)
        {
            switch (antworttyp)
            {
                case EventStorage.Antworttyp.Text:
                    return "Freitext";
                case EventStorage.Antworttyp.Checkbox:
                    return "Ja / Nein";
            }
            return string.Empty;
        }

        protected void speichernButton_Click(object sender, EventArgs e)
        {
            bool isInvalid = false;
            DateTime anmeldefrist = DateTime.Now;
            if (string.IsNullOrWhiteSpace(eventNameTextBox.Text))
            {
                eventNameTextBox.BorderColor = System.Drawing.Color.Red;
                isInvalid = false;
            }

            try
            {
                new System.Net.Mail.MailAddress(verantwortlicherTextBox.Text);
            }
            catch
            {
                verantwortlicherTextBox.BorderColor = Color.Red;
                isInvalid = true;
            }

            try
            {
                anmeldefrist = DateTime.Parse(anmeldefristTextBox.Text);
            }
            catch
            {
                anmeldefristTextBox.BorderColor = Color.Red;
                isInvalid = true;
            }


            if (isInvalid)
            {
                return;
            }

            if (IsNew)
            {
                var storage = EventStorage.LoadStorage();
                var row = storage.DataSet.Event.NewEventRow();

                row.Id = Guid.NewGuid().ToString();
                row.Name = eventNameTextBox.Text;
                row.AnmeldefristEnde = anmeldefrist;
                row.Beschreibung = beschreibungTextBox.Text;
                row.VerantwortlicherEmail = verantwortlicherTextBox.Text;
                row.AbschlussemailGesendet = false;
                storage.DataSet.Event.AddEventRow(row);
                storage.Save();
                Response.Redirect("Default.aspx");
            }
            else
            {
                var storage = EventStorage.LoadStorage();
                var row = storage.DataSet.Event.FirstOrDefault(r => r.Id == EventId);
                row.Name = eventNameTextBox.Text;
                row.AnmeldefristEnde = anmeldefrist;
                row.Beschreibung = beschreibungTextBox.Text;
                row.VerantwortlicherEmail = verantwortlicherTextBox.Text;
                storage.Save();
                Response.Redirect(string.Format("Detail.aspx?Event={0}", EventId));
            }
        }

        protected void loeschenLinkButton_Click(object sender, EventArgs e)
        {
            var storage = EventStorage.LoadStorage();
            var row = storage.DataSet.Event.FirstOrDefault(r => r.Id == EventId);
            storage.DataSet.Event.RemoveEventRow(row);
            storage.Save();
            Response.Redirect("Default.aspx");

        }

        protected void exportButton_Click(object sender, EventArgs e)
        {
            string file = Server.MapPath(string.Format("~/TemporaryData/{0}.xlsx", Guid.NewGuid()));
            if (!System.IO.Directory.Exists(Server.MapPath("~/TemporaryData/")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/TemporaryData/"));
            }

            var stream = new System.IO.FileStream(file, System.IO.FileMode.OpenOrCreate);

            ExcelLibrary.Anmeldungsliste(EventId, stream);
            stream.Close();
            stream.Dispose();
            //Response.ContentType = "application/pdf";
            Response.ContentType = "application/vnd.ms-excel";

            string name = Storage.EventStorage.LoadStorage().DataSet.Event.First(ev => ev.Id == EventId).Name;

            Response.AppendHeader("Content-Disposition", string.Format("attachment; filename=\"Anmeldestand {1} {0:yyyy-MM-dd_HH-mm}.xlsx\"", DateTime.Now, name));

            Response.TransmitFile(file);

            Response.End();
        }

        protected void onceUpImageButton_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            var storage = EventStorage.LoadStorage();
            var row = storage.DataSet.Zusatzinformation.Single(r => r.Id == id);
            var rowToChange = storage.DataSet.Zusatzinformation
                .Where(r => r.EventId == EventId)
                .OrderByDescending(r => r.Ordnungszahl)
                .First(r => r.Ordnungszahl < row.Ordnungszahl);
            int tmp = row.Ordnungszahl;
            row.Ordnungszahl = rowToChange.Ordnungszahl;
            rowToChange.Ordnungszahl = tmp;
            storage.Save();

            LoadZusatzinformation();
        }

        protected void onceDownImageButton_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            var storage = EventStorage.LoadStorage();
            var row = storage.DataSet.Zusatzinformation.Single(r => r.Id == id);
            var rowToChange = storage.DataSet.Zusatzinformation
                .Where(r => r.EventId == EventId)
                .OrderBy(r => r.Ordnungszahl)
                .First(r => r.Ordnungszahl > row.Ordnungszahl);
            int tmp = row.Ordnungszahl;
            row.Ordnungszahl = rowToChange.Ordnungszahl;
            rowToChange.Ordnungszahl = tmp;
            storage.Save();

            LoadZusatzinformation();
        }

        protected void deleteImageButton_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            var storage = EventStorage.LoadStorage();
            var row = storage.DataSet.Zusatzinformation.Single(r => r.Id == id);
            storage.DataSet.Zusatzinformation.RemoveZusatzinformationRow(row);
            storage.Save();

            LoadZusatzinformation();
        }

        protected void speichernLinkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(feldnameTextBox.Text))
            {
                return;
            }
            var storage = EventStorage.LoadStorage();

            var row = storage.DataSet.Zusatzinformation.NewZusatzinformationRow();

            row.EventId = EventId;
            row.Feldname = feldnameTextBox.Text;
            row.Id = Guid.NewGuid().ToString();
            row.Ordnungszahl = 0;
            try { row.Ordnungszahl = storage.DataSet.Zusatzinformation.Where(ev => ev.EventId == EventId).Max(x => x.Ordnungszahl) + 1; }
            catch { }
            row.Typ = int.Parse(feldtypDropDownList.SelectedValue);

            storage.DataSet.Zusatzinformation.AddZusatzinformationRow(row);
            storage.Save();

            feldnameTextBox.Text = string.Empty;
            LoadZusatzinformation();
        }
    }
}