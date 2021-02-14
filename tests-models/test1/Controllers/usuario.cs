using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using test1.Models;
using test1.Models.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
namespace test1.Controllers
{
    public class usuarios : Controller
    {
        private AcessoModels db= new AcessoModels();
        public IActionResult Cadastro(){
            var usuario = new Usuarios{ nome="test"};
            return View(usuario);
        }
        [HttpPost]
        public IActionResult Cadastro(Usuarios user){
            return View(user);
        }

        public IActionResult List(){
            return View(db.cadastro.ToListAsync());
        }

    }
}