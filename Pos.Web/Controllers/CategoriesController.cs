using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pos.Web.Data;
using Pos.Web.Models;

namespace Pos.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly PosDbContext _dbcontext;

        // access posDbContext through constructor (parameter)
        public CategoriesController(PosDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        // GET method: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await _dbcontext.Categories.Include(c => c.Products).ToListAsync();
        }


        // GET method by Id: api/Categories/1
        [HttpGet]
        [Route("Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _dbcontext.Categories.Include(c => c.Products).FirstOrDefaultAsync(x => x.CategoryId == id);

            if (category == null)
            {
                return NotFound($"The category is Not Found for id - {id}");
            }
            return Ok(category);
        }




        // POST method: api/Products
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            _dbcontext.Categories.Add(category);
            await _dbcontext.SaveChangesAsync();
            return Ok("Created Successfully");
        }


        //update method
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _dbcontext.Categories.Update(category);
            _dbcontext.SaveChanges();
            return NoContent();

        }


        //delete method
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (id == 0)
            {
                return BadRequest("Id should be provided");
            }
            var category = await _dbcontext.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound($"The category is Not Found for id - {id}");
            }
            _dbcontext.Categories.Remove(category);
            await _dbcontext.SaveChangesAsync();
            return NoContent();
        }




    }
}
