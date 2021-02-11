using ejerciocioMonolegal.Models;
using ejerciocioMonolegal.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ejerciocioMonolegal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly FacturaService _facturaService ;
        private readonly MailData _mailData;
        // GET: api/<FacturasController>
        public FacturaController(FacturaService facturaService, MailData mailData) 
        {
            this._facturaService = facturaService;
            this._mailData = mailData;
        }

        [HttpGet]
        public IEnumerable<Factura> Get()
        {
            
            return _facturaService.Get();
        }

        // GET api/<FacturasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FacturasController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        [HttpPost("enviarCorreo")]
        public void EnviarCorreo() 
        {
            List<Factura> listaFacturas = _facturaService.Get();
            foreach (var factura in listaFacturas)
            {
                ProcesarEnviarCorreo(factura);
            }
            
        }

        [HttpPost("enviarCorreo/{id}")]
        public void EnviarCorreo(string id)
        {
            
            Factura factura = _facturaService.Get(id);
            if (factura!=null && factura.Correo!=null)
                ProcesarEnviarCorreo(factura);
            

        }

        // PUT api/<FacturasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FacturasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //
        public void ProcesarEnviarCorreo(Factura factura) 
        {
            if (factura.Estado != MailData.Desactivado) 
            {
                EmailMessage message = new EmailMessage();
                message.Sender = new MailboxAddress("Monolegal Facturación", _mailData.Sender);
                message.Reciever = new MailboxAddress(factura.Cliente, factura.Correo);
                if (factura.Estado == MailData.PrimerR)
                {
                    message.Subject = _mailData.SegundoRecordatorioSubject;
                    message.Content = _mailData.SegundoRecordatorioContent;
                    factura.Estado = MailData.SegundoR;
                }
                else if (factura.Estado == MailData.SegundoR)
                {
                    message.Subject = _mailData.DesactivacionSubject;
                    message.Content = _mailData.DesactivacionContent;
                    factura.Estado = MailData.Desactivado;
                }
                RealizarEnvio(message, factura);
            }
            

            
        }
        private void RealizarEnvio(EmailMessage message, Factura factura) 
        {
            var mimeMessage = message.CreateMessage(message);
            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_mailData.SmtpServer,
                _mailData.Port, true);
                smtpClient.Authenticate(_mailData.UserName,
                _mailData.Password);
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
            }
            _facturaService.Update(factura.Id, factura);
        }
    }
}
