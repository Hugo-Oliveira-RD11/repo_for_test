using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TestAuth.Models;
using Microsoft.AspNetCore.Authorization;
using TestAuth.Models.AccountViewModel;
using Microsoft.AspNetCore.Authentication;

namespace TestAuth.Controllers
{
    public class AccountController : Controller
    {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
       _userManager = userManager;
       _signInManager = signInManager; 
    } 

        [AllowAnonymous]
        public IActionResult Register(string ReturnUrl = null){

            ViewData["ReturnUrl"]=ReturnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string ReturnUrl){

            if(ModelState.IsValid){
                var user = new ApplicationUser{
                    UserName = model.Email,
                    Email = model.Email
                }; 
                var result = await _userManager.CreateAsync(user,model.Password);
                
                if(result.Succeeded){
                    return RedirecttoLocal(ReturnUrl);   

                }
                foreach(var Error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,Error.Description);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string ReturnUrl = null){
            ViewData["ReturnUrl"]= ReturnUrl;

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl = null){
            
            if(ModelState.IsValid){
                ViewData["ReturnUrl"]=ReturnUrl;
                var result = await _signInManager.PasswordSignInAsync(model.Email,model.PassWord,model.Rememberme,lockoutOnFailure: false);
                if(result.Succeeded){
                    return RedirecttoLocal(ReturnUrl);
                }
            }else{
                ModelState.AddModelError(string.Empty, "Tentativa de login invalida");
            }
            return View(model);
        }
        private IActionResult RedirecttoLocal(string ReturnUrl = null){
            if(!string.IsNullOrEmpty(ReturnUrl)){
                if(Url.IsLocalUrl(ReturnUrl)){
                    return Redirect(ReturnUrl);
                }
                else{
                    return RedirectToAction("Index","Home");
                }
            }
            else{
                return RedirectToAction("Index", "Home");
            }
        }
    }
}