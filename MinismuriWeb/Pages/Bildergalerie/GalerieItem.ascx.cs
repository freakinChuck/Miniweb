using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Pages.Bildergalerie
{
    public partial class GalerieItem : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public string GalerieRootFolder { get; set; }
        public string GalerieName { get; set; }
        public string GalerieDisplayItemName { get; set; }
        internal void SetData(string galerieRootFolder, string galerie, string galerieDisplayItemName)
        {
            GalerieRootFolder = galerieRootFolder;
            GalerieName = galerie;
            GalerieDisplayItemName = galerieDisplayItemName;
        }
    }
}