using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ECommerce.Libraries.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato contato) 
        {
            //smtp: servidor que envia a mensagem 
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("X@gmail.com", "sua_senha");
            smtp.EnableSsl = true;

            string corpoMsg = string.Format("<h2>Contato - Ecommerce</h2>" +
                "<b>Nome: </b> {0} </br>" +
                "<b>E-mail: </b> {1} </br>" +
                "<b>Texto: </b> {2} </br>" +
                "Não responda este e-mail. Envio automático.", contato.Nome, contato.Email, contato.Texto);

            //MailMessage serve para construir a mensagem 
            MailMessage mensagem = new MailMessage();

            mensagem.From = new MailAddress("X@gmail.com");
            mensagem.To.Add("X@gmail.com");
            //mensagem.To.Add("Y@gmail.com");
            //mensagem.To.Add("Z@gmail.com");
            mensagem.Subject = "Contato Ecommerce - E-mail: "  + contato.Email;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Envio da mensagem
            smtp.Send(mensagem);

            //Se o google negar o acesso:
            //https://myaccount.google.com/lesssecureapps?pli=1

        }
    }
}
