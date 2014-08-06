using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MinismuriWeb
{
    public class AnmeldungslisteVersendenTask : ScheduledTask
    {
        private static AnmeldungslisteVersendenTask instance;
        public static AnmeldungslisteVersendenTask Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AnmeldungslisteVersendenTask();
                }
                return instance;
            }
        }
        private AnmeldungslisteVersendenTask()
        {

        }
        protected override int MinutesToWaitBetwenTaskRun
        {
            get
            {
                return 6 * 60; // alle 6 Stunden genügt
            }
        }
        protected override void Execute()
        {
            var storage = Storage.EventStorage.LoadStorage();

            foreach (var ev in storage.DataSet.Event.Where(e => !e.AbschlussemailGesendet && e.AnmeldefristEnde < DateTime.Now.Date))
            {
                ev.AbschlussemailGesendet = true;
                EmailHelper email = new EmailHelper();
                email.To.Add(ev.VerantwortlicherEmail);
                email.Subject = string.Format("Anmeldestand {0} nach Ablauf der Anmeldefrist", ev.Name);
                email.Body= string.Format(@"Guten Tag

Im Anhang finden Sie die Liste der Anmeldungen per {0:dd.MM.yyyy HH:mm} (Anmeldeschluss) für den Event {1}.
Sie erhalten dieses Email, da Sie als Eventverantwortliche/r hinterlegt sind.

Freundliche Grüsse
Leitungsteam Ministranten Muri", DateTime.Now, ev.Name);
                
                Stream stream = new MemoryStream();

                ExcelLibrary.Anmeldungsliste(ev.Id, stream);
                stream.Position = 0;
                email.Attatchments.Add(new System.Net.Mail.Attachment(stream, string.Format("Anmedestand {1} {0:yyyy-MM-dd_HH-mm}.xlsx", DateTime.Now, ev.Name)));
                email.Send();

            }

            storage.Save();
        }

        protected override void OnError(Exception e, out bool exceptionHandled)
        {
            exceptionHandled = true;
        }
    }
}