using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Pages.Videos
{
    public partial class Deafult : System.Web.UI.Page
    {

        public string SelectedVideo 
        { 
            get
            {
                return Request["VideoUrl"];
            }
        }
        public List<KeyValuePair<string, string>> AlleVideos { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            Storage.DataStorage data = Storage.DataStorage.LoadStorage();
            AlleVideos = data.DataSet.Video.Where(v => !string.IsNullOrEmpty(v.Freigeber)).OrderBy(v => v.Ordnungszahl).Select(s => new KeyValuePair<string, string>(s.Anzeigename, s.FileName)).ToList();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            alleFilmeRepeater.DataSource = AlleVideos;
            alleFilmeRepeater.DataBind();
        }
    }
}