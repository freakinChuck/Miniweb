using ExcelExportHelper;
using MinismuriWeb.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MinismuriWeb
{
    public static class ExcelLibrary
    {
        public static void Anmeldungsliste(string eventId, Stream stream)
        {
            ExcelWorkbook<EventStorageDataSet.AnmeldungRow> workbook = new ExcelWorkbook<EventStorageDataSet.AnmeldungRow>();
            var anmeldungen = workbook.AddWorksheet("Anmeldungen", r => r.Where(x => x.IstAnmeldung));
            anmeldungen.AutoAdjustAllColumns = true;
            anmeldungen.AddColumn("Name", x => x.Name);
            anmeldungen.AddColumn("Email", x => x.Emailadresse);
            anmeldungen.AddColumn("Bemerkung", x => x.Bemerkung);
            anmeldungen.AddColumn("AnmeldeZeitpunkt", x => x.Zeitpunkt.ToString("dd.MM.yyyy HH:mm"));


            var abmeldungen = workbook.AddWorksheet("Abmeldungen", r => r.Where(x => !x.IstAnmeldung));
            abmeldungen.AutoAdjustAllColumns = true;
            abmeldungen.AddColumn("Name", x => x.Name);
            abmeldungen.AddColumn("Email", x => x.Emailadresse);
            abmeldungen.AddColumn("Bemerkung", x => x.Bemerkung);
            abmeldungen.AddColumn("AnmeldeZeitpunkt", x => x.Zeitpunkt.ToString("dd.MM.yyyy HH:mm"));


            var storage = EventStorage.LoadStorage();
            var export = workbook.Export(storage.DataSet.Anmeldung.Where(a => a.EventId == eventId));

            export.SaveAs(stream);

        }
    }
}