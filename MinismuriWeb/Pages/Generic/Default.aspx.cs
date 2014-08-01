using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Pages.Generic
{
    public partial class Default : System.Web.UI.Page
    {
        public string Titel { get; set; }
        public string Content { get; set; }
        public DateTime Datum { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string id= Request["id"];

            var storage = Storage.GenericPageStorage.LoadStorage();
            var row = storage.DataSet.Pages.FirstOrDefault(r => r.Id == id);
            if (row != null && (row.Published || User.Identity.IsAuthenticated))
            {
                Titel = row.Title;
                Content = row.Content;
                Datum = row.Aenderungsdatum;
            }
        }
    }
}