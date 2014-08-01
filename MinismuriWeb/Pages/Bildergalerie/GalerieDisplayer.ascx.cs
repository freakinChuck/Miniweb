using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Pages.Bildergalerie
{
    public partial class GalerieDisplayer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        public string Begleittext
        {
            get
            {
                if (string.IsNullOrEmpty(GalerieName))
                {
                    return string.Empty;
                }
                var storage = Storage.DataStorage.LoadStorage();
                var entry = storage.DataSet.BildergalerieBegleittext.FirstOrDefault(t => t.GalerieName.ToLower() == GalerieName.ToLower());
                return entry != null ? entry.Inhalt : string.Empty;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            entfernenContainer.Visible = Page.User.Identity.IsAuthenticated;

            if (entfernenContainer.Visible)
            {
                var storage = Storage.DataStorage.LoadStorage();
                var freigegeben = storage.DataSet.FreigegebeneGalerien.Any(g => g.Name.ToLower() == GalerieName.ToLower());
                var userHasRights = storage.DataSet.Benutzer.Any(b => b.Benutzername.ToLower() == Page.User.Identity.Name.ToLower() && (b.BildergalerieFreigeber || b.BildergalerieUploader));
                entfernenContainer.Visible = !freigegeben && userHasRights;
            }
        }

        public string GalerieRootFolder { get; set; }
        public string GalerieName { get; set; }

        public int ImageCount { get; set; }

        public List<KeyValuePair<string, string>> ImageLinks { get; set; }


        internal void SetGalerie(string galerieRootFolder, System.IO.DirectoryInfo selectedGalerie)
        {
            GalerieRootFolder = galerieRootFolder;
            GalerieName = selectedGalerie.Name;
            var images = selectedGalerie.GetFiles().Where(f => f.Extension.ToLower() == ".jpg").ToList();
            ImageCount = images.Count;
            ImageLinks = images.Select(f => 
                new KeyValuePair<string, string>(
                    string.Format("{0}/{1}/{2}", GalerieRootFolder, GalerieName, f.Name),
                    string.Format("{0}/{1}/thumb/{2}", GalerieRootFolder, GalerieName, f.Name)
                )
            ).ToList();
        }

        protected void entfernenLinkButton_Click(object sender, EventArgs e)
        {
            int index;
            if (int.TryParse(deleteHiddenField.Value, out index))
            {
                var imageLink = ImageLinks[index];
                try
                {
                    System.IO.File.Delete(Server.MapPath(string.Format("~{0}", imageLink.Key)));
                    System.IO.File.Delete(Server.MapPath(string.Format("~{0}", imageLink.Value)));
                }
                catch
                {
                    
                }
            }
            Response.Redirect(Request.Url.PathAndQuery);
        }
    }
}