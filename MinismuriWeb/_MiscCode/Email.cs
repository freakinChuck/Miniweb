using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace MinismuriWeb
{
    /// <summary>
    /// Mit dieser Klasse lassen sich emails versenden
    /// </summary>
    public class EmailHelper
    {
        private static string mailserver;
        private static int mailport;
        private static bool enableSSL;

        /// <summary>
        /// Die Adresse, von der Aus die Email versendet wird
        /// </summary>
        public static string FromAdress
        {
            get { return ConfigurationManager.AppSettings["EmailFromAdress"]; }
        }

        /// <summary>
        /// Die Adresse, von der Aus die Email versendet wird
        /// </summary>
        public static string FromDisplay
        {
            get { return ConfigurationManager.AppSettings["EmailFromDisplay"]; }
        }

        static EmailHelper()
        {
            mailserver = ConfigurationManager.AppSettings["EmailServer"];
            mailport = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
            enableSSL = (ConfigurationManager.AppSettings["EmailEnableSSL"] ?? string.Empty).ToLower() == bool.TrueString.ToLower();
         }

        private MailMessage mail;

        /// <summary>
        /// Der Konstruktor
        /// </summary>
        public EmailHelper()
        {
         
            mail = new MailMessage();
            this.From = EmailHelper.FromAdress;
        }
        /// <summary>
        /// der Konstruktor
        /// </summary>
        /// <param name="to">die Empfängeradresse</param>
        public EmailHelper(MailAddress to)
        {
            mail = new MailMessage(new MailAddress(EmailHelper.FromAdress, EmailHelper.FromAdress), to);
        }
        /// <summary>
        /// der Konstruktor
        /// </summary>
        /// <param name="to">die Empfängeradresse</param>
        /// <param name="subject">die Betreffzeile</param>
        /// <param name="body">der Inhalt der eMail</param>
        public EmailHelper(MailAddress to, string subject, string body)
        {
            mail = new MailMessage(new MailAddress(EmailHelper.FromAdress, EmailHelper.FromDisplay), to);
            mail.Subject = subject;
            mail.Body = body;
        }

        /// <summary>
        /// die Betrefzeile
        /// </summary>
        public string Subject
        {
            get { return mail.Subject; }
            set { mail.Subject = value; }
        }
        /// <summary>
        /// der Inhalt der eMail
        /// </summary>
        public string Body
        {
            get { return mail.Body; }
            set { mail.Body = value; }
        }
        /// <summary>
        /// die Absenderadresse
        /// </summary>
        public string From
        {
            get { return mail.From.Address; }
            set 
            {
                mail.From = new MailAddress(value);
            }
        }
        /// <summary>
        /// Die Empfängeradressen
        /// </summary>
        public MailAddressCollection To
        {
            get 
            {
                return mail.To; 
            }
        }
        /// <summary>
        /// Die Empfängeradressen der CC
        /// </summary>
        public MailAddressCollection CC
        {
            get
            {
                return mail.CC;
            }
        }
        /// <summary>
        /// Die Empfängeradressen der BCC
        /// </summary>
        public MailAddressCollection Bcc
        {
            get
            {
                return mail.Bcc;
            }
        }
        /// <summary>
        /// versendet das eMail
        /// </summary>
        public void Send()
        {
            SmtpClient client = new SmtpClient();
            client.Host = mailserver;
            client.Port = mailport;
            client.EnableSsl = enableSSL;

			if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailUsername"]) && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailPasswort"]))
            {
				NetworkCredential credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUsername"], ConfigurationManager.AppSettings["EmailPasswort"]);
                client.Credentials = credentials;
            }

			//REM: Solange auf DEV-Umgebung gearbeitet wird, bitte einkommentieren, um falsche Mails gegen Aussen zu verhindern.
			//     Später #IF DEBUG verwenden!
			#region lokale Email-Versendungsabfragen

			string mailbodyToAdd = string.Empty;
			if (mail.To.Count > 0)
			{
				mailbodyToAdd += "\nTo:";
				foreach (MailAddress item in mail.To)
				{
					mailbodyToAdd += " " + item.Address;
				}
			}
			if (mail.CC.Count > 0)
			{
				mailbodyToAdd += "\nCC:";
				foreach (MailAddress item in mail.CC)
				{
					mailbodyToAdd += " " + item.Address;
				}
			}
			if (mail.Bcc.Count > 0)
			{
				mailbodyToAdd += "\nBCC:";
				foreach (MailAddress item in mail.Bcc)
				{
					mailbodyToAdd += " " + item.Address;
				}
			}

            mail.Body = string.Format("{0}\n\n{1}", mailbodyToAdd, mail.Body);

			mail.To.Clear();
			mail.CC.Clear();
			mail.Bcc.Clear();
            mail.To.Add("freakinChuck@gmail.com");

            #endregion

            client.Send(mail);

        }

		/// <summary>
		/// Die Anhänge der eMail
		/// </summary>
		public AttachmentCollection Attatchments
		{
			get { return mail.Attachments; }
		}
		/// <summary>
		/// die Priorität des Emails
		/// </summary>
		public MailPriority Priority
		{
			get { return mail.Priority; }
			set { mail.Priority = value; }
		}
		/// <summary>
		/// Angabe, ob eine Bestätigung gesendet werden soll
		/// </summary>
		public DeliveryNotificationOptions DeliveryNotificationOptions
		{
			get { return mail.DeliveryNotificationOptions; }
			set { mail.DeliveryNotificationOptions = value; }
		}
    }
}
