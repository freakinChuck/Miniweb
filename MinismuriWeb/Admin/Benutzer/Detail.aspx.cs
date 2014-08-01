using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Admin.Benutzer
{
    public partial class Detail : System.Web.UI.Page
    {
        private bool IsBenutzeradmin
        {
            get 
            {
                var storage = Storage.DataStorage.LoadStorage();
                return storage.DataSet.Benutzer.Any(b => b.Benutzername.ToLower() == User.Identity.Name.ToLower() && b.Benutzeradmin);
            }
        }
        public bool IsEdit
        {
            get
            {
                return (Request["Edit"] ?? string.Empty).ToLower() == bool.TrueString.ToLower();
            }
        }
        public string Benutzername
        {
            get
            {
                return Request["User"];
            }
        }

        public bool IsNew
        {
            get { return string.IsNullOrEmpty(Benutzername); }
        }

        public bool Aktiv { get; set; }
        public string Ersteller { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Benutzername))
            {
                if (!IsBenutzeradmin)
                {
                    Response.Redirect("Default.aspx");
                }
            }
            else if(!IsPostBack)
            {
                deleteLinkButton.Visible = User.Identity.Name.ToLower() != Benutzername.ToLower();
                var user = Storage.DataStorage.LoadStorage().DataSet.Benutzer.FirstOrDefault(x => x.Benutzername.ToLower() == Benutzername.ToLower());

                if (!IsBenutzeradmin)
                {
                    if (user.Benutzername.ToLower() != User.Identity.Name.ToLower())
                    {
                        Response.Redirect("Default.aspx");                        
                    }
                }

                if (user != null)
                {
                    Aktiv = user.Aktiv;
                    Ersteller = user.Ersteller;

                    benutzeradminDisplayCheckBox.Checked = user.Benutzeradmin;
                    blogschreiberDisplayCheckBox.Checked = user.Blogschreiber;
                    bildergalerieFreigeberDisplayCheckBox.Checked = user.BildergalerieFreigeber;
                    bildergalerieUploaderDisplayCheckBox.Checked = user.BildergalerieUploader;
                    gaestebuchAdminDisplayCheckBox.Checked = user.Gaestebuchmoderator;
                    linkadminDisplayCheckBox.Checked = user.Linkadmin;
                    terminadminDisplayCheckBox.Checked = user.Terminadmin;
                    videoadminDisplayCheckBox.Checked = user.Videoadministrator;
                    seitenadminDisplayCheckBox.Checked = user.Seitenadmin;
                    eventDisplayCheckBox.Checked = user.Eventadmin;
                    
                    benutzeradminEditCheckBox.Checked = user.Benutzeradmin;
                    blogschreiberEditCheckBox.Checked = user.Blogschreiber;
                    bildergalerieFreigeberEditCheckBox.Checked = user.BildergalerieFreigeber;
                    bildergalerieUploaderEditCheckBox.Checked = user.BildergalerieUploader;
                    gaestebuchAdminEditCheckBox.Checked = user.Gaestebuchmoderator;
                    linkadminEditCheckBox.Checked = user.Linkadmin;
                    terminadminEditCheckBox.Checked = user.Terminadmin;
                    videoadminEditCheckBox.Checked = user.Videoadministrator;
                    seitenadminEditCheckBox.Checked = user.Seitenadmin;
                    eventEditCheckBox.Checked = user.Eventadmin;

                }
                aktivCheckBox.Checked = Aktiv;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            detailDiv.Visible = !IsEdit && !IsNew;
            editDiv.Visible = IsEdit && !IsNew;
            newDiv.Visible = IsNew;
            rollenEditDiv.Visible = IsBenutzeradmin;
            aktivCheckBox.Enabled = IsBenutzeradmin;
        }

        protected void deleteLinkButton_Click(object sender, EventArgs e)
        {
            var storage = Storage.DataStorage.LoadStorage();
            storage.DataSet.Benutzer.RemoveBenutzerRow(storage.DataSet.Benutzer.First(b => b.Benutzername.ToLower() == Benutzername.ToLower()));
            storage.Save();
            Response.Redirect("Default.aspx");

        }

        protected void editLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Detail.aspx?User=" + Benutzername + "&Edit=True");
        }

        protected void speichernLinkButton_Click(object sender, EventArgs e)
        {
            var storage = Storage.DataStorage.LoadStorage();
            var row = storage.DataSet.Benutzer.First(b => b.Benutzername.ToLower() == Benutzername.ToLower());
            row.Aktiv = aktivCheckBox.Checked;
            if (!string.IsNullOrEmpty(passwortTextBox.Text))
            {
                row.PasswordHash = Crypto.GetHash(passwortTextBox.Text);
            }

            if (IsBenutzeradmin)
            {
                row.Benutzeradmin = benutzeradminEditCheckBox.Checked;
                row.Blogschreiber = blogschreiberEditCheckBox.Checked;
                row.BildergalerieFreigeber = bildergalerieFreigeberEditCheckBox.Checked;
                row.BildergalerieUploader = bildergalerieUploaderEditCheckBox.Checked;
                row.Gaestebuchmoderator = gaestebuchAdminEditCheckBox.Checked;
                row.Linkadmin = linkadminEditCheckBox.Checked;
                row.Terminadmin = terminadminEditCheckBox.Checked;
                row.Videoadministrator = videoadminEditCheckBox.Checked;
                row.Seitenadmin = seitenadminEditCheckBox.Checked;
                row.Eventadmin = eventEditCheckBox.Checked;
            }

            storage.Save();
            Response.Redirect("Default.aspx");
        }

        protected void editAbbrechenLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Detail.aspx?User=" + Benutzername);
        }

        protected void newAbbrechenLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void newSpeichernLinkButton_Click(object sender, EventArgs e)
        {
            var storage = Storage.DataStorage.LoadStorage();
            if (string.IsNullOrEmpty(newBenutzernameTextBox.Text))
            {
                errorLiteral.Text = "Der Benutzername darf nicht leer sein.";
                return;
            }
            if (string.IsNullOrEmpty(newPasswordTextBox.Text))
            {
                errorLiteral.Text = "Das Passwort darf nicht leer sein.";
                return;
            }
            if (storage.DataSet.Benutzer.Any(b => b.Benutzername.ToLower() == newBenutzernameTextBox.Text.ToLower()))
            {
                errorLiteral.Text = "Der Benutzername ist bereits vorhanden.";
                return;
            }

            var row = storage.DataSet.Benutzer.NewBenutzerRow();
            row.Benutzername = newBenutzernameTextBox.Text;
            row.Aktiv = newAktivCheckBox.Checked;
            row.PasswordHash = Crypto.GetHash(newPasswordTextBox.Text);
            row.Ersteller = User.Identity.Name;
            row.Benutzeradmin = benutzeradminNewCheckBox.Checked;
            row.Blogschreiber = blogschreiberNewCheckBox.Checked;
            row.BildergalerieFreigeber = bildergalerieFreigeberNewCheckBox.Checked;
            row.BildergalerieUploader = bildergalerieUploaderNewCheckBox.Checked;
            row.Gaestebuchmoderator = gaestebuchAdminNewCheckBox.Checked;
            row.Linkadmin = linkadminNewCheckBox.Checked;
            row.Terminadmin = terminadminNewCheckBox.Checked;
            row.Videoadministrator = videoadminNewCheckBox.Checked;
            row.Seitenadmin = seitenadminNewCheckBox.Checked;
            row.Eventadmin = eventNewCheckBox.Checked;
            storage.DataSet.Benutzer.AddBenutzerRow(row);
            storage.Save();
            Response.Redirect("Default.aspx");
        }
    }
}