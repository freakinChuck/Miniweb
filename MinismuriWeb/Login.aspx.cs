using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            errorLiteral.Text = string.Empty;
        }

        protected void loginLinkButton_Click(object sender, EventArgs e)
        {
            string username = benutzernameTextBox.Text;
            string password = passwordTextBox.Text;

            if (DoLogin(username, password))
            {
                FormsAuthentication.RedirectFromLoginPage(username, true);
            }
            errorLiteral.Text = "Login fehlgeschlagen";
        }

        private bool DoLogin(string username, string password)
        {
            var storage = Storage.DataStorage.LoadStorage();
            string hash = Crypto.GetHash(password);

            if (!storage.DataSet.Benutzer.Any())
            {
                storage.DataSet.Benutzer.AddBenutzerRow(username, hash, "ADMIN", true, true, true, true, true, true, true, true, true, true, true);
                storage.Save();
            }

            if (storage.DataSet.Benutzer.Any(r => r.Aktiv && r.Benutzername.ToLower() == username.ToLower() && r.PasswordHash == hash))
            {
                return true;
            }
            return false;
        }
    }
}