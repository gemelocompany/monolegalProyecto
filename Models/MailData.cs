using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
namespace ejerciocioMonolegal.Services
{
    public class MailData
    {
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SegundoRecordatorioSubject { get; set; }
        public string SegundoRecordatorioContent { get; set; }
        public string DesactivacionSubject { get; set; }
        public string DesactivacionContent { get; set; }
        public const string PrimerR = "primerrecordatorio";
        public const string SegundoR = "segundorecordatorio";
        public const string Desactivado= "desactivado";
    }
}
