using MinismuriWeb.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Admin.EventAnmeldung
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs ea)
        {
            var storage = EventStorage.LoadStorage();
            var offene = storage.DataSet.Event.Where(e => e.AnmeldefristEnde >= DateTime.Now.Date).OrderBy(e => e.AnmeldefristEnde).ToList();
            var nichtOffene = storage.DataSet.Event.Where(e => e.AnmeldefristEnde < DateTime.Now.Date).OrderByDescending(e => e.AnmeldefristEnde).ToList();
            openEventRepeater.DataSource = offene;
            openEventRepeater.DataBind();
            closedEventRepeater.DataSource = nichtOffene;
            closedEventRepeater.DataBind();
        }

        protected void newEventLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Detail.aspx");
        }
    }
}