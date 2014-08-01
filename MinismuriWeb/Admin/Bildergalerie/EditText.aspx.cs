using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Admin.Bildergalerie
{
    public partial class EditText : System.Web.UI.Page
    {
        public string Galerie
        {
            get { return Request["Galerie"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var storage = Storage.DataStorage.LoadStorage();
                var entry = storage.DataSet.BildergalerieBegleittext.FirstOrDefault(t => t.GalerieName.ToLower() == Galerie.ToLower());
                if (entry != null)
                {
                    contentTextBox.Text = entry.Inhalt;
                }
            }
        }

        protected void speichernButton_Click(object sender, EventArgs e)
        {
            var storage = Storage.DataStorage.LoadStorage();
            var entry = storage.DataSet.BildergalerieBegleittext.FirstOrDefault(t => t.GalerieName.ToLower() == Galerie.ToLower());
            if (entry == null)
            {
                entry = storage.DataSet.BildergalerieBegleittext.NewBildergalerieBegleittextRow();
                storage.DataSet.BildergalerieBegleittext.AddBildergalerieBegleittextRow(entry);
                entry.GalerieName = Galerie;
            }
            entry.Inhalt = contentTextBox.Text;
            
            storage.Save();

            ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
        }
    }
}