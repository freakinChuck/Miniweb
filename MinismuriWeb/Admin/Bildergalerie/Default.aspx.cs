using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;

namespace MinismuriWeb.Admin.Bildergalerie
{
    public partial class Default : System.Web.UI.Page
    {
        public bool IsUploader
        {
            get
            {
                return Storage.DataStorage.LoadStorage().DataSet.Benutzer.Any(b => b.Benutzername.ToLower() == User.Identity.Name.ToLower()
                    && b.BildergalerieUploader);
            }
        }
        public bool IsFreigeber
        {
            get
            {
                return Storage.DataStorage.LoadStorage().DataSet.Benutzer.Any(b => b.Benutzername.ToLower() == User.Identity.Name.ToLower()
                    && b.BildergalerieFreigeber);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsUploader && !IsFreigeber)
            {
                Response.Redirect("~/Admin/Default.aspx");
            }
            neueGalerieFieldset.Visible = IsUploader;
            keineBerechtigungUploadSpan.Visible = !IsUploader;
            keineBerechtigungFreigabeSpan.Visible = !IsFreigeber;
            LoadBestehendeGalerienRepeater();
            LoadFreizugebendeGalerienRepeater();
        }

        private void LoadBestehendeGalerienRepeater()
        {
            var storage = Storage.DataStorage.LoadStorage();
            string folderPath = Server.MapPath("~/UploadData/Bildergalerie/");

            var info = new DirectoryInfo(folderPath);

            bestehendeGalerienRepeater.DataSource = info.GetDirectories()
                .OrderByDescending(d => d.CreationTime)
                .Select(i => new { Name = i.Name })
                .Where(n => IsFreigegeben(n.Name))
                .Select(x => new {
                    Name = x.Name,
                    Freigeber = storage.DataSet.FreigegebeneGalerien.Where(g => g.Name.ToLower() == x.Name.ToLower()).Select(g => g.FreigeberUsername).FirstOrDefault()
                })
                .ToList();
            bestehendeGalerienRepeater.DataBind();
        }

        private void LoadFreizugebendeGalerienRepeater()
        {
            //TODO: Anpassen
            //bestehendeGalerienRepeater

            string folderPath = Server.MapPath("~/UploadData/Bildergalerie/");

            var info = new DirectoryInfo(folderPath);

            freizugebendeGalerienRepeater.DataSource = info.GetDirectories()
                .OrderByDescending(d => d.CreationTime)
                .Select(i => new { Name = i.Name })
                .Where(n => !IsFreigegeben(n.Name))
                .ToList();
            freizugebendeGalerienRepeater.DataBind();
        }


        protected void galerieFreigebenButton_Command(object sender, CommandEventArgs e)
        {
            string name = e.CommandArgument.ToString();
            var storage = Storage.DataStorage.LoadStorage();
            if (!storage.DataSet.FreigegebeneGalerien.Any(r => r.Name.ToLower() == name.ToLower()))
            {
                storage.DataSet.FreigegebeneGalerien.AddFreigegebeneGalerienRow(name, User.Identity.Name);
                storage.Save();
            }

            LoadBestehendeGalerienRepeater();
            LoadFreizugebendeGalerienRepeater();
        }
        protected void galerieFreigabeEntziehenButton_Command(object sender, CommandEventArgs e)
        {
            string name = e.CommandArgument.ToString();
            var storage = Storage.DataStorage.LoadStorage();
            if (storage.DataSet.FreigegebeneGalerien.Any(r => r.Name.ToLower() == name.ToLower()))
            {
                var row = storage.DataSet.FreigegebeneGalerien.FirstOrDefault(r => r.Name.ToLower() == name.ToLower());
                storage.DataSet.FreigegebeneGalerien.RemoveFreigegebeneGalerienRow(row);
                storage.Save();
            }

            LoadBestehendeGalerienRepeater();
            LoadFreizugebendeGalerienRepeater();
        }
        

        protected void galerieLoeschenButton_Command(object sender, CommandEventArgs e)
        {
            string name = e.CommandArgument.ToString();
            string folderPath = Server.MapPath("~/UploadData/Bildergalerie/");

            string galeriePath = Path.Combine(folderPath, name);
            Directory.Delete(galeriePath, true);

            LoadBestehendeGalerienRepeater();
            LoadFreizugebendeGalerienRepeater();
        }

        private bool IsFreigegeben(string galerieName)
        {
            var storage = Storage.DataStorage.LoadStorage();
            return storage.DataSet.FreigegebeneGalerien.Any(g => g.Name.ToLower() == galerieName.ToLower());
        }
    }
}