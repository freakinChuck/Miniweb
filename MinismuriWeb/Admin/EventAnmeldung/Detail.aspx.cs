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
            eventNameTextBox.BorderColor = Color.Empty;
            anmeldefristTextBox.BorderColor = Color.Empty;

            displayDiv.Visible = !IsEdit && !IsNew;
            editDiv.Visible = IsEdit || IsNew;

            var storage = EventStorage.LoadStorage();
            var row = storage.DataSet.Event.FirstOrDefault(r => r.Id == EventId);
            if (row != null)
            {
                Eventname = row.Name;
                Anmeldeschluss = row.AnmeldefristEnde;
                Beschreibung = row.Beschreibung;

                if (IsEdit)
                {
                    eventNameTextBox.Text = Eventname;
                    anmeldefristTextBox.Text = Anmeldeschluss.ToShortDateString();
                    beschreibungTextBox.Text = Beschreibung;
                }
            }
        }

        protected void speichernButton_Click(object sender, EventArgs e)
        {
            DateTime anmeldefrist = DateTime.Now;
            if (string.IsNullOrWhiteSpace(eventNameTextBox.Text))
            {
                eventNameTextBox.BorderColor = System.Drawing.Color.Red;
            }
            try
            {
                anmeldefrist = DateTime.Parse(anmeldefristTextBox.Text);
            }
            catch
            {
                anmeldefristTextBox.BorderColor = Color.Red;
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
                storage.DataSet.Event.AddEventRow(row);
                storage.Save();
            }
            else
            {
                var storage = EventStorage.LoadStorage();
                var row = storage.DataSet.Event.FirstOrDefault(r => r.Id == EventId);
                row.Name = eventNameTextBox.Text;
                row.AnmeldefristEnde = anmeldefrist;
                row.Beschreibung = beschreibungTextBox.Text;
                storage.Save();
            }
            Response.Redirect("Default.aspx");
        }

        protected void loeschenLinkButton_Click(object sender, EventArgs e)
        {
            var storage = EventStorage.LoadStorage();
            var row = storage.DataSet.Event.FirstOrDefault(r => r.Id == EventId);
            storage.DataSet.Event.RemoveEventRow(row);
            storage.Save();
            Response.Redirect("Default.aspx");

        }
    }
}