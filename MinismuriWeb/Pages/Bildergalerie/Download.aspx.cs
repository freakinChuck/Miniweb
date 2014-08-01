using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ionic.Zip;
using System.IO;

namespace MinismuriWeb.Pages.Bildergalerie
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string galerie = Request["Galerie"];
            
            
            Response.Clear();
            Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}.zip", galerie));
            
            MemoryStream stream = new MemoryStream();

            using (ZipFile zip = new ZipFile())
            {
                foreach (string fileName in Directory.GetFiles(Server.MapPath(string.Format("~/UploadData/Bildergalerie/{0}", galerie))))
	            {
                    zip.AddFile(fileName, "/");
	            }
                zip.Save(Response.OutputStream);
            }

            //Response.ContentType = "application/zip";
            Response.Flush();
        }
    }
}