using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        public bool ShowAdminLink
        {
            get { return !Request.Url.LocalPath.ToLower().Contains("/Admin/Default.aspx".ToLower()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/");
        }
    }
}