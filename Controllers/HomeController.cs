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
using ECommerce.Repositories;
using ECommerce.Repositories.Contracts;
using ECommerce.Libraries.Login;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {

        private IClienteRepository _clienteRepository;
        private INewsletterRepository _newsletterRepository;
        private LoginCliente _loginCliente;

        public HomeController(IClienteRepository clienteRepository, INewsletterRepository newsletterRepository, LoginCliente loginCliente)
        {
            _clienteRepository = clienteRepository;
            _newsletterRepository = newsletterRepository;
            _loginCliente = loginCliente;
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
                _newsletterRepository.Cadastrar(newsletter);

                TempData["MSG_S"] = "E-mail cadastrado! Agora você vai receber promoções especiais no seu e-mail! Fique atento as novidades!";

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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Login([FromForm] Cliente cliente)
        {
            Cliente clienteDB = _clienteRepository.Login(cliente.Email, cliente.Senha);

            if(clienteDB != null)
            {
                _loginCliente.Login(clienteDB);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha digitados.";
                return View(); 
            }
        }

        public IActionResult Painel()
        {
            Cliente cliente = _loginCliente.GetCliente();
            if (cliente != null)
            {
                return new ContentResult() { Content = "Usuário " + cliente.id + ". E-mail: " + cliente.Email + " - Idade: " + DateTime.Now.AddYears(-cliente.Nascimento.Year).ToString("yyyy") + ". Logado!" };
            }
            else
            {
                return new ContentResult() { Content = "Acesso negado." };
            }
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
                _clienteRepository.Cadastrar(cliente);

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