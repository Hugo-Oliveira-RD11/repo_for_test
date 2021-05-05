using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteApi1.Models;
using TesteApi1.Models.Data;

namespace TesteApi1.Controllers
{
    [ApiController]
    [Route("v1/Categories")]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public CategoryController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetItem()
        {
            return await _dataContext.Categories.ToListAsync();

        }
        [Route("")]
        [HttpPost]
        public async Task<ActionResult<Category>> PostItens(Category model)
        {
            if(ModelState.IsValid)
            {
                _dataContext.Add(model);
                await _dataContext.SaveChangesAsync();
                return model;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Deu pau!!");
                return BadRequest(ModelState);

            }
        }

        [HttpGet]
        [Route("{Id:int}")]
        public async Task<ActionResult<Category>> GetOneItem(int Id)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if(category == null)
            {
                return BadRequest();
            }
            else 
            {
                return category;
            }
        }

        [HttpGet]
        [Route("Delete/{Id:int}")]
        public async Task<ActionResult<IEnumerable<Category>>> DeleteItens(int Id)
        {
            var delete = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if(delete == null)
            {
                return BadRequest();
            }
            _dataContext.Categories.Remove(delete);
            await _dataContext.SaveChangesAsync();

            return await _dataContext.Categories.ToListAsync();
        }

    }
}