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
                    if(!string.IsNullOrEmpty(ReturnUrl)){
                        return RedirecttoLocal(ReturnUrl);   
                    }
                    else{
                        return RedirectToAction("Index","Home");
                    }
                }
                foreach(var Error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,Error.Description);
                }
            }
            return View(model);
        }
        private IActionResult RedirecttoLocal(string ReturnUrl = null){
            if(Url.IsLocalUrl(ReturnUrl)){
                return Redirect(ReturnUrl);
            }
            else{
                return RedirectToAction("Index","Home");
            }
        }
    }
}