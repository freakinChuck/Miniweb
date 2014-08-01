using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Time.Now.ToString();
            var storage = Storage.DataStorage.LoadStorage();
            var entrys = storage.DataSet.BlogEntry.Where(b => b.Startseite).ToList();
            var display = entrys.Count > 0 ? entrys.OrderByDescending(x => x.Datum).First() : null;
                
            welcomeTextLiteral.Text = display != null ? display.Content : string.Empty;
            var imgPath = display != null ? display.ImagePath : string.Empty;
            if (!string.IsNullOrEmpty(imgPath))
	        {
		        titelImage.ImageUrl = string.Format("/UploadData/Blogbilder/{0}", imgPath);
	        }
        }
    }
}