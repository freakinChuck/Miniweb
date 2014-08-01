using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Admin.Links
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Storage.DataStorage.LoadStorage().DataSet.Benutzer.Any(b => b.Benutzername.ToLower() == User.Identity.Name.ToLower()
                && b.Linkadmin))
            {
                Response.Redirect("~/Admin/Default.aspx");
            }
            linksRepeater.DataSource = Storage.DataStorage.LoadStorage().DataSet.LinkEintrag.OrderBy(s => s.Titel);
            linksRepeater.DataBind();
        }
        protected void entfernenButton_Command(object sender, CommandEventArgs e)
        {
            string link = e.CommandArgument.ToString().ToLower();
            var storage = Storage.DataStorage.LoadStorage();
            storage.DataSet.LinkEintrag.RemoveLinkEintragRow(storage.DataSet.LinkEintrag.First(l => l.Link.ToLower() == link));
            storage.Save();
            Response.Redirect("Default.aspx");
        }

        protected void speichernButton_Click(object sender, EventArgs e)
        {
            var storage = Storage.DataStorage.LoadStorage();
            bool isvalid = true;
            if (string.IsNullOrEmpty(titelTextBox.Text))
            {
                titelTextBox.CssClass = "invalidInput";
                isvalid = false;
            }
            else
            {
                titelTextBox.CssClass = string.Empty;
            }
            if (string.IsNullOrEmpty(linkTextBox.Text))
            {
                linkTextBox.CssClass = "invalidInput";
                isvalid = false;
            }
            else
            {
                linkTextBox.CssClass = string.Empty;
            }
            string link = linkTextBox.Text.ToLower();
            if (!link.StartsWith("http"))
	        {
		        link = string.Format("http://{0}", link);
	        }
            if (storage.DataSet.LinkEintrag.Any(l => l.Link.ToLower() == link.ToLower()))
            {
                isvalid = false;
                errorLiteral.Text = "Dieser Link ist bereits vorhanden.";
            }

            if (isvalid)
            {
                var row = storage.DataSet.LinkEintrag.NewLinkEintragRow();
                row.Beschreibung = bechreibungTextBox.Text;
                row.Titel = titelTextBox.Text;
                row.Link = link;
                storage.DataSet.LinkEintrag.AddLinkEintragRow(row);
                storage.Save();
                Response.Redirect("Default.aspx");
            }

        }
    }
}