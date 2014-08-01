using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Pages.Gaestebuch
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var storage = Storage.DataStorage.LoadStorage();
            gaestebuchEintraegeRepeater.DataSource = storage.DataSet.GaestebuchEintrag.OrderByDescending(g => g.Datum).ToList();
            gaestebuchEintraegeRepeater.DataBind();
        }
    }
}