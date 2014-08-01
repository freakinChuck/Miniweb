using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Pages.Gaestebuch
{
    public partial class NeuerEintrag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void speichernButton_Click(object sender, EventArgs e)
        {
            bool istValid = true;

            if (!noBot.IsValid())
            {
                errorLiteral.Text = "Unser Anti-Spam System hat uns ein verdächtiges Verhalten gemeldet. Falls Sie ein Mensch sind, möchten wir uns für jegliche Unannehmlichkeiten entschuldigen.";
                istValid = false;
            }

            string name = nameTextBox.Text;
            string email = string.Empty; // emailTextBox.Text;
            string nachricht = nachtichtTextBox.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                istValid = false;
                nameTextBox.CssClass = "invalidInput";
            }
            else
            {
                nameTextBox.CssClass = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(nachricht))
            {
                istValid = false;
                nachtichtTextBox.CssClass = "invalidInput";
            }
            else
            {
                nachtichtTextBox.CssClass = "";
            }

            if (istValid)
            {
                var storage = Storage.DataStorage.LoadStorage();
                var data = storage.DataSet.GaestebuchEintrag.NewGaestebuchEintragRow();
                
                data.Datum = Time.Now;
                data.Name = name;
                data.Email = email;
                data.Nachricht = nachricht;
                try
                {
                    data.Id = storage.DataSet.GaestebuchEintrag.Max(r => r.Id) + 1;
                }
                catch
                {
                    data.Id = 1;
                }

                storage.DataSet.GaestebuchEintrag.AddGaestebuchEintragRow(data);
                storage.Save();

                Response.Redirect("Default.aspx");

            }
        }

    }
}