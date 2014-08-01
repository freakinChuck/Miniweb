using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace MinismuriWeb.Pages.Bildergalerie
{
    public partial class Default : System.Web.UI.Page
    {
        public string Galerie
        {
            get
            {
                return Request["Galerie"];
            }
        }
        public string GalerieRootFolderInternal
        {
            get { return Server.MapPath(string.Format("~{0}", GalerieRootFolder)); }
        }
        public string GalerieRootFolder
        {
            get { return "/UploadData/Bildergalerie"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            downloadLink.Visible = User.Identity.IsAuthenticated;
            downloadLink.HRef = string.Format("Download.aspx?Galerie={0}", Galerie);

            var rootFolder = new DirectoryInfo(GalerieRootFolderInternal);
            List<DirectoryInfo> directories = rootFolder.GetDirectories().OrderByDescending(d => d.CreationTime).ToList();
            var selectedGalerie = directories.Where(d => d.Name == Galerie).FirstOrDefault();
            if (selectedGalerie != null)
            {
                GalerieDisplayer galerie = LoadControl("GalerieDisplayer.ascx") as GalerieDisplayer;

                galerie.SetGalerie(GalerieRootFolder, selectedGalerie);

                galerieItemsPlaceholder.Controls.Add(galerie);

                linkDivs.Visible = true;
            }
            else
            {
                foreach (var galerie in directories.Where(d => IsFreigegeben(d.Name)))
                {
                    var galerieDisplayFile = galerie.GetFiles().FirstOrDefault();
                    if (galerieDisplayFile != null)
	                {
		                GalerieItem item = LoadControl("GalerieItem.ascx") as GalerieItem;
                        item.SetData(GalerieRootFolder, galerie.Name, galerieDisplayFile.Name);
                        galerieItemsPlaceholder.Controls.Add(item);
	                }
                }

                linkDivs.Visible = false;

            }
        }

        private bool IsFreigegeben(string galerieName)
        {
            var storage = Storage.DataStorage.LoadStorage();
            return storage.DataSet.FreigegebeneGalerien.Any(g => g.Name.ToLower() == galerieName.ToLower());
        }
    }
}