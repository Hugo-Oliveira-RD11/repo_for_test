using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using test2.Models;
using test2.Data;

namespace test2.Controllers{
    public class UsersController : Controller{
        private readonly Test2Context _context;

        public UsersController(Test2Context context){
            _context=context;
        }
        public IActionResult Index(){
            return View();
        }
        public IActionResult Login(){
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,NomeR,NomeU,Email,senha")] CreateUser user){
            if(ModelState.IsValid){
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        public async Task<IActionResult> MyUser(int? id){
            if(id == null){
                return NotFound();
            }
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if(user ==null){
                return NotFound();
            }
            return View(user);
        }
 
    }
}