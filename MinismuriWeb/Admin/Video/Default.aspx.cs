using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Admin.Video
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Storage.DataStorage.LoadStorage().DataSet.Benutzer.Any(b => b.Benutzername.ToLower() == User.Identity.Name.ToLower()
                && b.Videoadministrator))
            {
                Response.Redirect("~/Admin/Default.aspx");
            }
            if (videosRepeater.Items.Count <= 0)
            {
                LoadRepeater();                
            }
        }

        private void LoadRepeater()
        {
            var storage = Storage.DataStorage.LoadStorage();
            videosRepeater.DataSource = storage.DataSet.Video.Select(v => new
            {
                Name = v.Anzeigename,
                Freigeber = v.Freigeber,
                FileName = v.FileName,
                Freigegeben = !string.IsNullOrEmpty(v.Freigeber),
                Ordnungszahl = v.Ordnungszahl,
                IsFirst = v.Ordnungszahl == storage.DataSet.Video.Min(x => x.Ordnungszahl),
                IsLast = v.Ordnungszahl == storage.DataSet.Video.Max(x => x.Ordnungszahl)
            }).OrderBy(x => x.Ordnungszahl).ToList();
            videosRepeater.DataBind();
        }

        protected void videoFreigabeEntziehenButton_Command(object sender, CommandEventArgs e)
        {
            var videoUrl = e.CommandArgument.ToString().ToLower();
            var storage = Storage.DataStorage.LoadStorage();
            var row = storage.DataSet.Video.First(v => v.FileName.ToLower() == videoUrl);
            row.Freigeber = string.Empty;
            storage.Save();
            Response.Redirect("Default.aspx");
        }
        protected void videoFreigebenLinkButton_Command(object sender, CommandEventArgs e)
        {
            var videoUrl = e.CommandArgument.ToString().ToLower();
            var storage = Storage.DataStorage.LoadStorage();
            var row = storage.DataSet.Video.First(v => v.FileName.ToLower() == videoUrl);
            row.Freigeber = User.Identity.Name;
            storage.Save();
            Response.Redirect("Default.aspx");
        }
        protected void videoLoeschenButton_Command(object sender, CommandEventArgs e)
        {
            var videoUrl = e.CommandArgument.ToString().ToLower();
            var storage = Storage.DataStorage.LoadStorage();
            var row = storage.DataSet.Video.First(v => v.FileName.ToLower() == videoUrl);
            storage.DataSet.Video.RemoveVideoRow(row);
            storage.Save();
            Response.Redirect("Default.aspx");
        }

        protected void speichernButton_Click(object sender, EventArgs e)
        {
            var storage = Storage.DataStorage.LoadStorage();
            bool isvalid = true;
            if (string.IsNullOrEmpty(anzeigenameTextBox.Text))
            {
                anzeigenameTextBox.CssClass = "invalidInput";
                isvalid = false;
            }
            else
            {
                anzeigenameTextBox.CssClass = string.Empty;
            }
            if (string.IsNullOrEmpty(dateinameTextBox.Text))
            {
                dateinameTextBox.CssClass = "invalidInput";
                isvalid = false;
            }
            else
            {
                dateinameTextBox.CssClass = string.Empty;
            }

            if (isvalid)
            {
                var row = storage.DataSet.Video.NewVideoRow();
                row.FileName = dateinameTextBox.Text;
                row.Anzeigename = anzeigenameTextBox.Text;
                try
                {
                    row.Ordnungszahl = storage.DataSet.Video.Max(v => v.Ordnungszahl) + 1;
                }
                catch (Exception)
                {
                    row.Ordnungszahl = 1; 
                }
                storage.DataSet.Video.AddVideoRow(row);
                storage.Save();
                Response.Redirect("Default.aspx");
            }

        }

        protected void downImageButton_Command(object sender, CommandEventArgs e)
        {
            var videoUrl = e.CommandArgument.ToString().ToLower();
            var storage = Storage.DataStorage.LoadStorage();
            var row = storage.DataSet.Video.First(v => v.FileName.ToLower() == videoUrl);

            int lowerOz = storage.DataSet.Video.Where(v => v.Ordnungszahl > row.Ordnungszahl).Min(v => v.Ordnungszahl);
            var lowerRow = storage.DataSet.Video.First(v => v.Ordnungszahl == lowerOz);
            lowerRow.Ordnungszahl = row.Ordnungszahl;
            row.Ordnungszahl = lowerOz;

            storage.Save();
            Response.Redirect("Default.aspx");
        }

        protected void upImageButton_Command(object sender, CommandEventArgs e)
        {
            var videoUrl = e.CommandArgument.ToString().ToLower();
            var storage = Storage.DataStorage.LoadStorage();
            var row = storage.DataSet.Video.First(v => v.FileName.ToLower() == videoUrl);

            int higherOz = storage.DataSet.Video.Where(v => v.Ordnungszahl < row.Ordnungszahl).Max(v => v.Ordnungszahl);
            var higherRow = storage.DataSet.Video.First(v => v.Ordnungszahl == higherOz);
            higherRow.Ordnungszahl = row.Ordnungszahl;
            row.Ordnungszahl = higherOz;

            storage.Save();
            Response.Redirect("Default.aspx");
        }

    }
}