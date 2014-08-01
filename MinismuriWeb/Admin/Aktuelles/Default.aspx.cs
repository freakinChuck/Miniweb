using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Admin.Aktuelles
{
    public partial class Default : System.Web.UI.Page
    {
        public List<dynamic> TerminListe
        {
            get
            {
                return Storage.DataStorage.LoadStorage().DataSet.Aktuelles.Where(r => r.ZeigenBis >= Time.Now).OrderBy(r => r.ZeigenBis).Cast<dynamic>().ToList();
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
            if (!Storage.DataStorage.LoadStorage().DataSet.Benutzer.Any(b => b.Benutzername.ToLower() == User.Identity.Name.ToLower()
                && b.Terminadmin))
            {
                Response.Redirect("~/Admin/Default.aspx");
            }

            terminRepeater.DataSource = TerminListe;
            terminRepeater.DataBind();


            //

        }

        protected void entfernenButton_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            var storage = Storage.DataStorage.LoadStorage();
            storage.DataSet.Aktuelles.RemoveAktuellesRow(storage.DataSet.Aktuelles.Where(r => r.Id == id).FirstOrDefault());
            storage.Save();
            Response.Redirect(Request.Url.PathAndQuery);
        }

        protected void speichernButton_Click(object sender, EventArgs e)
        {
            bool istValid = true;
            string titel = titelTextBox.Text;
            string tag = tagTextBox.Text;
            string monat = monatTextBox.Text;
            DateTime zeigenAb;
            DateTime zeigenBis;

            if (!DateTime.TryParse(zeigenAbTextBox.Text, out zeigenAb))
            {
                zeigenAbTextBox.CssClass = "invalidInput";
                istValid = false;
            }
            if (!DateTime.TryParse(zeigenBisTextBox.Text, out zeigenBis))
            {
                zeigenBisTextBox.CssClass = "invalidInput";
                istValid = false;                
            }

            if (string.IsNullOrWhiteSpace(titel))
            {
                istValid = false;
                titelTextBox.CssClass = "invalidInput";
            }
            else
            {
                titelTextBox.CssClass = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(tag))
            {
                istValid = false;
                tagTextBox.CssClass = "invalidInput";
            }
            else
            {
                tagTextBox.CssClass = "";
            }
            if (string.IsNullOrWhiteSpace(monat))
            {
                istValid = false;
                monatTextBox.CssClass = "invalidInput";
            }
            else
            {
                monatTextBox.CssClass = "";
            }

            if (istValid)
            {
                var storage = Storage.DataStorage.LoadStorage();
                var data = storage.DataSet.Aktuelles.NewAktuellesRow();

                data.Titel = titel;
                data.Tag = tag;
                data.Monat = monat;
                data.ZeigenAb = zeigenAb;
                data.ZeigenBis = zeigenBis;
                data.Id = Guid.NewGuid().ToString();

                storage.DataSet.Aktuelles.AddAktuellesRow(data);
                storage.Save();

                Response.Redirect("Default.aspx");

            }
        }
    }
}