using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteApi1.Models;
using TesteApi1.Models.Data;

namespace TesteApi1.Controllers
{
       [ApiController] 
       [Route("v1/Product")]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [Route("")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAsync()
        {
            return await _dataContext.Products.Include(x => x.Categors).ToListAsync();
        }
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> PostItens(Product model)
        {
            if(ModelState.IsValid)
            {
                _dataContext.Add(model);
                await _dataContext.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [Route("{Id:int}")]
        public async Task<ActionResult<Product>> GetOneItem(int Id)
        {
            var produto = await _dataContext.Products.Include(x => x.Categors).FirstOrDefaultAsync(x => x.Id == Id);
            if(produto == null)
            {
                return BadRequest();
            }
            return produto;
        }

        [Route("delete/{Id:int}")]
        public async Task<ActionResult<IEnumerable<Product>>> DeleteItem(int Id)
        {
            var produto = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if(produto == null)
            {
                return BadRequest();
            }
            _dataContext.Products.Remove(produto);
            await _dataContext.SaveChangesAsync();
            return await _dataContext.Products.Include(x => x.Categors).ToListAsync();
        }
    }
}