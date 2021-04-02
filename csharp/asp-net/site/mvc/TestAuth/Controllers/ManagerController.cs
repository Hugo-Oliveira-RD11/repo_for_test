using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestAuth.Models;
using TestAuth.Models.ManagerViewModel;

namespace TestAuth.Controllers
{
    public class ManagerController : Controller{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ManagerController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager){
            _userManager= userManager;
            _signInManager = signInManager;

        }
        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> Index(){
                
            var user = await _userManager.GetUserAsync(User);

            if(user == null){
                throw new ApplicationException($"Não foi possivel carregar o usuario com o ID '{_userManager.GetUserId(User)}'");
            }
            var model = new IndexViewModel{
                Username = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessege = StatusMessage
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model){
            if(!ModelState.IsValid){
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            if(user == null){
                throw new ApplicationException($"Não foi possivel carregar o usuario com o ID '{_userManager.GetUserId(User)}'");
            }
            if(user.Email != model.Email){
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if(!setEmailResult.Succeeded){
                    throw new ApplicationException($"Error inesperado ao atribuir um email para o usuario com o ID'{user.Id}'");
                }
            }

            if(user.PhoneNumber != model.Phone){
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.Phone);
                if(!setPhoneResult.Succeeded){
                    throw new ApplicationException($"Error inesperado ao atribuir um telefone para o usuario com o ID'{user.Id}'");
                }
            }
            StatusMessage = "Seu perfil foi atualizado!";
            return RedirectToAction("Index","Home");
        }
    }
    
}