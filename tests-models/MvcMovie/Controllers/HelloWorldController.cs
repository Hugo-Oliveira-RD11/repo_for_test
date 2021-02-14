using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller{
        public IActionResult Index(){
            return View();
        }
        public IActionResult Welcome(string nome,int numtimes){
            ViewData["mensagem"]="ola " + nome;
            ViewData["NumTimes"]=numtimes;
            return View();
        }
    }
}