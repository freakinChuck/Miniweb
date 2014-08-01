using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Pages.Admin.Gaestebuch
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var storage = Storage.DataStorage.LoadStorage();

            if (!storage.DataSet.Benutzer.Any(b => b.Benutzername.ToLower() == User.Identity.Name.ToLower()
                && b.Gaestebuchmoderator))
            {
                Response.Redirect("~/Admin/Default.aspx");
            }
            
            gaestebuchEintraegeRepeater.DataSource = storage.DataSet.GaestebuchEintrag.OrderByDescending(g => g.Datum).ToList();
            gaestebuchEintraegeRepeater.DataBind();
        }

        protected void entfernenButton_Command(object sender, CommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            var storage = Storage.DataStorage.LoadStorage();
            storage.DataSet.GaestebuchEintrag.RemoveGaestebuchEintragRow(storage.DataSet.GaestebuchEintrag.Where(r => r.Id == id).FirstOrDefault());
            storage.Save();
            Response.Redirect(Request.Url.PathAndQuery);
        }
    }
}