using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Libraries.Email;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public  IActionResult Contato()
        {
            return View();
        }

        public IActionResult ContatoAcao()
        {
            try
            {
                Contato contato = new Contato();
            
                contato.Nome = HttpContext.Request.Form["nome"];
                contato.Email = HttpContext.Request.Form["email"];
                contato.Texto = HttpContext.Request.Form["texto"];

                var listaDeMensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool ehValido = Validator.TryValidateObject(contato, contexto, listaDeMensagens, true);

                if (ehValido)
                {
                    ContatoEmail.EnviarContatoPorEmail(contato);

                    ViewData["MSG_S"] = "Agradecemos o contato. Sua mensagem será respondida em breve!";

                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var texto in listaDeMensagens)
                    {
                        sb.Append(texto.ErrorMessage + "<br/>");                       
                    }
                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }

            }
            catch (Exception e)
            {
                ViewData["MSG_E"] = "Oops! Ocorreu um erro. Tente novamente mais tarde.";
                

                //TODO - Implementar log
            }
            return View("Contato");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CadastroClientes()
        {
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}