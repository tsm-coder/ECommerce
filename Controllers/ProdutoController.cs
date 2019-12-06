using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    //todo método de controlador retorna um ActionResult ou um IActionResult
    public class ProdutoController : Controller
    {
        public ActionResult Visualizar()
        {
            return new ContentResult() { Content = "<h3>Teste<h3>", ContentType = "text/html" };

        }
    }
}
