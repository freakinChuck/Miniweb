using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Pages.Aktuelles
{
    public partial class Default : System.Web.UI.Page
    {
        public List<dynamic> TerminListe
        {
            get
            {
                return Storage.DataStorage.LoadStorage().DataSet.Aktuelles.Where(r => r.ZeigenAb <= Time.Now && r.ZeigenBis >= Time.Now).OrderBy(r => r.ZeigenBis).Select(r => new { Title = r.Titel, Monat = r.Monat, Tag = r.Tag }).Cast<dynamic>().ToList();
                //return new List<dynamic>() { 
                //    new {
                //        Title = "Miniweekend", Monat = "September", Tag="7/8"
                //    }, 
                //    new {
                //        Title = "Minimorgen", Monat = "Oktober", Tag="30"
                //    },
                //    new {
                //        Title = "Irgendwas :-)", Monat = "Novermber", Tag="13"
                //    },
                //    new {
                //        Title = "Weihnachten", Monat = "Dezember", Tag="24"
                //    }
                //};
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            terminRepeater.DataSource = TerminListe;
            terminRepeater.DataBind();
        }
    }
}