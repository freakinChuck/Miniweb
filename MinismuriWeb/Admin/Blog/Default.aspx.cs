using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Admin.Blog
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Storage.DataStorage.LoadStorage().DataSet.Benutzer.Any(b => b.Benutzername.ToLower() == User.Identity.Name.ToLower() 
                && b.Blogschreiber))
            {
                Response.Redirect("~/Admin/Default.aspx");
            }
            if (eintragRepeater.Items.Count <= 0)
            {
                LoadEintraege();                
            }
        }

        private void LoadEintraege()
        {
            var storage = Storage.DataStorage.LoadStorage();
            eintragRepeater.DataSource = storage.DataSet.BlogEntry.OrderByDescending(b => b.Datum);
            eintragRepeater.DataBind();
        }

        protected void saveNewEntryButton_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            string titel = titelTextBox.Text;
            string content = contentTextBox.Text;
            bool startseiteneintrag = startseiteneintragCheckBox.Checked;

            if (string.IsNullOrEmpty(titel))
            {
                titelTextBox.CssClass = "invalidInput";
                isValid = false;
            }
            else
            {
                titelTextBox.CssClass = string.Empty;
            }

            if (string.IsNullOrEmpty(content))
            {
                contentContainer.Attributes["class"] = "inputBox invalidInput";
                isValid = false;
            }
            else
            {
                contentContainer.Attributes["class"] = "inputBox";
            }

            string filename = string.Empty;

            if (bildUpload.HasFile)
            {
                var extension = Path.GetExtension(bildUpload.FileName).ToLower();
                if (extension != ".jpg")
	            {
                    isValid = false;
                    errorLiteral.Text = "Das Bild muss vom Format '.jpg' sein.";
	            }
                else
                {
                    filename = string.Format("{0}.jpg", Guid.NewGuid());
                    var stream = bildUpload.FileContent;
                    var image = System.Drawing.Image.FromStream(stream);
                    image.ResizeAndSave(1000, 1000, Server.MapPath(string.Format("~/UploadData/Blogbilder/{0}", filename)));
                }
            }

            if (isValid)
            {
                var storage = Storage.DataStorage.LoadStorage();
                var row = storage.DataSet.BlogEntry.NewBlogEntryRow();
                row.Content = content;
                row.Datum = Time.Now;
                row.Ersteller = User.Identity.Name;
                row.Id = Guid.NewGuid().ToString();
                row.Startseite = startseiteneintrag;
                row.Titel = titel;
                row.ImagePath = filename;
                storage.DataSet.BlogEntry.AddBlogEntryRow(row);
                storage.Save();
                Response.Redirect("Default.aspx");
            }

        }

        protected void loeschenButton_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            var storage = Storage.DataStorage.LoadStorage();
            if (storage.DataSet.BlogEntry.Any(r => r.Id == id))
            {
                var row = storage.DataSet.BlogEntry.FirstOrDefault(r => r.Id == id);
                string imagePath = row.ImagePath;

                storage.DataSet.BlogEntry.RemoveBlogEntryRow(row);
                storage.Save();

                if (!string.IsNullOrEmpty(imagePath))
                {
                    try
                    {
                        File.Delete(Server.MapPath(string.Format("~/UploadData/Blogbilder/{0}", imagePath))); 
                    }
                    catch
                    {

                    }
                }

            }
            Response.Redirect("Default.aspx");
        }
    }
}