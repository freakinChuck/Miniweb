using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Pages.Blog
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (eintragRepeater.Items.Count <= 0)
            {
                LoadEintraege();
            }
        }

        private void LoadEintraege()
        {
            var storage = Storage.DataStorage.LoadStorage();
            eintragRepeater.DataSource = storage.DataSet.BlogEntry.OrderByDescending(b => b.Datum);
            eintragRepeater.DataBind();
        }
    }
}