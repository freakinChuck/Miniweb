using MinismuriWeb.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MinismuriWeb.Pages.Anmeldung
{
    public partial class Default : System.Web.UI.Page
    {
        private string EventHash
        {
            get { return Request["Event"]; }
        }

        private string EventId
        {
            get
            {
                try
                {
                    return new Guid(Convert.FromBase64String(EventHash)).ToString();
                }
                catch { return string.Empty; }
            }
        }

        public string EventName
        {
            get 
            {
                return string.Empty + (EventHash == "1" ? "Hallo" : string.Empty);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var storage = EventStorage.LoadStorage();
            var row = storage.DataSet.Event.FirstOrDefault(r => r.Id == EventId);

            if (row == null)
            {
                anmeldungDiv.Visible = false;
                nichtExistentDiv.Visible = true;
                anmeldefristAbgelaufenDiv.Visible = false;
            }
            else
            {
                anmeldungDiv.Visible = true;
                nichtExistentDiv.Visible = false;
                anmeldefristAbgelaufenDiv.Visible = false;
            
                if (row.AnmeldefristEnde.Date < DateTime.Now.Date)
                {
                    anmeldefristAbgelaufenDiv.Visible = true;
                }
            }
        }
    }
}