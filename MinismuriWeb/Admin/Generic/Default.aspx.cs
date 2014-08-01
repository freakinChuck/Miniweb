using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Admin.Generic
{
    public partial class Default : System.Web.UI.Page
    {
        public string EditId
        {
            get
            {
                //if (string.IsNullOrWhiteSpace(Request["Edit"]))
                //{
                //    return 0;
                //}
                //return int.Parse(Request["Edit"]);
                return Request["Edit"];
            }
        }
        public string ParentId
        {
            get
            {
                //if (string.IsNullOrWhiteSpace(Request["Parent"]))
                //{
                //    return 0;
                //}
                //return int.Parse(Request["Parent"]);
                return Request["Parent"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var editId = EditId;

                var storage = Storage.GenericPageStorage.LoadStorage();

                var row = storage.DataSet.Pages.FirstOrDefault(p => p.Id == editId);

                if (row != null)
                {
                    contentTextBox.Text = Server.HtmlDecode(row.Content);
                    titelTextBox.Text = row.Title;
                }
            }
        }

        protected void contentTextBox_HtmlEditorExtender_ImageUploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            string fileGuid = Guid.NewGuid().ToString();

            string pathToMap = "~/UploadData/Generic/" + fileGuid + e.FileName;
            string fullpath = Server.MapPath(pathToMap);
            contentTextBox_HtmlEditorExtender.AjaxFileUpload.SaveAs(fullpath);

            try
            {
                var img = System.Drawing.Image.FromFile(fullpath);
                img.ResizeAndSave(1000, 1000, fullpath);
            }
            catch (Exception)
            {
                
            }

            e.PostedUrl = Page.ResolveUrl(pathToMap);
        }

        protected void speichernButton_Click(object sender, EventArgs e)
        {
            errorLiteral.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(titelTextBox.Text) || string.IsNullOrWhiteSpace(contentTextBox.Text))
            {
                errorLiteral.Text = "Bitte füllen Sie beide Felder aus.";
            }

            if (string.IsNullOrEmpty(errorLiteral.Text))
            {
                var titel = titelTextBox.Text;
                var content = contentTextBox.Text;

                var parent = ParentId;
                var editId = EditId;

                var storage = Storage.GenericPageStorage.LoadStorage();

                var row = storage.DataSet.Pages.FirstOrDefault(p => p.Id == editId);
                if (row == null)
                {
                    row = storage.DataSet.Pages.NewPagesRow();
                    row.Id = Guid.NewGuid().ToString();

                    try
                    {
                        row.Ordnungszahl = storage.DataSet.Pages.Max(r => r.Ordnungszahl) + 1;
                    }
                    catch (Exception)
                    {
                        row.Ordnungszahl = 1;
                    }
                    row.ParentId = parent;
                    row.Published = false;
                    storage.DataSet.Pages.AddPagesRow(row);
                }


                row.Aenderungsdatum = Time.Now;
                row.Aenderungsbenutzer = User.Identity.Name;
                row.Title = titel;
                row.Content = content;

                storage.Save();
                RedirectToAdmin();
            }

        }

        private void RedirectToAdmin()
        {
            Response.Redirect("~/Admin/Default.aspx");
        }
    }
}