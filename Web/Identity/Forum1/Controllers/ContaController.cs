using System;
using System.Threading.Tasks;
using Forum1.Models;
using Forum1.Models.ContaViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum1.Controllers
{
    public class ContaController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ContaController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
          _signInManager = signInManager;  
          _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Registrar(string ReturnUrl = null){
            if(_signInManager.IsSignedIn(User)){
                return RedirectToAction("Index","Home");
            }
            ViewData["ReturnUrl"] = ReturnUrl;
            return View(); 

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar(RegistrarViewModel model,string ReturnUrl = null){
            if(ModelState.IsValid){
                var user = new ApplicationUser{
                    UserName = model.UserName, 
                    Email = model.Email 
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded){
                    ViewData["ReturnUrl"] =  ReturnUrl;

                    var result2 = await _signInManager.PasswordSignInAsync(model.UserName,model.Password,model.Remenberme,lockoutOnFailure:false);

                    if(result2.Succeeded){
                        return ReturnToLocalUrl(ReturnUrl); 
                    }

                    foreach(var Error in result.Errors){

                        ModelState.AddModelError(string.Empty,"tentativa de login invalida");

                    }
                }

                foreach(var Error in result.Errors){

                    ModelState.AddModelError(string.Empty,"tentativa de login invalida");

                }

            }
            Console.WriteLine("fudeu!");
            return View(model);
        }

        public async Task<IActionResult> Login(string ReturnUrl = null){

            if(_signInManager.IsSignedIn(User)){
                return RedirectToAction("Index","Home");
            }

            ViewData["ReturnUrl"] = ReturnUrl;
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme); 
            return View();

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model,string ReturnUrl = null){
            if(ModelState.IsValid){
               ViewData["ReturnUrl"] = ReturnUrl;

               var result3 = await _signInManager.PasswordSignInAsync(model.UserName,model.Password,model.Remenberme,lockoutOnFailure:false);
               
               if(result3.Succeeded)
               {
                   return ReturnToLocalUrl(ReturnUrl);
               }  
            }
            else{
                ModelState.AddModelError(string.Empty,"tentativa de login invalida");
            }
            
            Console.WriteLine("fudeu!");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(){

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index","Home");
        }


        private IActionResult ReturnToLocalUrl(string ReturnUrl)
        {
            if (!string.IsNullOrEmpty(ReturnUrl))
            { if (Url.IsLocalUrl(ReturnUrl))
                {
                     return Redirect(ReturnUrl);
                }
                else {
                    return RedirectToAction("Index", "Home"); 
                }
            } else
            { 
                return RedirectToAction("Index", "Home");
            } 
        }
    }
}