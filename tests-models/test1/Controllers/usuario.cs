using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace test1.Controllers
{
    public class usuarios : Controller
    {
        public IActionResult Cadastro(){
            return View();
        }

    }
}