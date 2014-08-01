using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Admin.Benutzer
{
    public partial class Default : System.Web.UI.Page
    {
        private bool IsBenutzeradmin
        {
            get
            {
                var storage = Storage.DataStorage.LoadStorage();
                return storage.DataSet.Benutzer.Any(b => b.Benutzername.ToLower() == User.Identity.Name.ToLower() && b.Benutzeradmin);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadUsers();
            newBenutzerLinkButton.Visible = IsBenutzeradmin;
        }

        private void LoadUsers()
        {
            var data = Storage.DataStorage.LoadStorage().DataSet.Benutzer
                .Select(x => new { Benutzername = x.Benutzername, Aktiv = x.Aktiv, Ersteller = x.Ersteller })
                .OrderByDescending(b => b.Aktiv).ThenBy(b => b.Benutzername).AsEnumerable();
            if (!IsBenutzeradmin)
            {
                data = data.Where(d => d.Benutzername.ToLower() == User.Identity.Name.ToLower());
            }
            userRepeater.DataSource = data;
            userRepeater.DataBind();
        }

        protected void newBenutzerLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Detail.aspx");
        }
    }
}