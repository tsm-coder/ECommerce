using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Libraries.Email;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ECommerce.Database;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {

        private ECommerceContext _banco;

        public HomeController(ECommerceContext banco)
        {
            _banco = banco;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm]NewsletterEmail newsletter)
        {
            if (ModelState.IsValid)
            {
                _banco.NewsletterEmails.Add(newsletter);
                _banco.SaveChanges();

                TempData["MSG_S"] = "E-mail cadastrado! Agora você vai receber promoções especiais no seu e-mail! Fique atento as novidades!";
                //TODO - A mensagem ainda não aparece após o cadastro do e-mail

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
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

        [HttpGet]
        public IActionResult CadastroClientes()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroClientes([FromForm] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _banco.Add(cliente);
                _banco.SaveChanges();

                //TODO - A mensagem ainda não aparece após o cadastro do cliente
                TempData["MSG_S"] = "Cadastro realizado com sucesso!";

                //TODO - Implementar outros redirecionamentos como Painel ou Carrinho de Compras
                return RedirectToAction(nameof(CadastroClientes));
            }
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}