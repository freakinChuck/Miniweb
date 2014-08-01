using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Pages.Links
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            linksRepeater.DataSource = Storage.DataStorage.LoadStorage().DataSet.LinkEintrag.OrderBy(s => s.Titel);
            linksRepeater.DataBind();
        }
        
            
    }
}