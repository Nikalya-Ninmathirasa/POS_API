using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pos.Web.Data;
using Pos.Web.Models;
using System;

namespace Pos.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PosDbContext _dbcontext;

        // access posDbContext through constructor (parameter)
        public ProductsController(PosDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        // GET method: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _dbcontext.Products.Include(p => p.Category).ToListAsync();
        }

        // GET method by Id: api/Products/1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _dbcontext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound($"The category is Not Found for id - {id}");
            }

            return Ok(product);
        }


        // POST method: api/Products
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _dbcontext.Products.Add(product);
            await _dbcontext.SaveChangesAsync();
            return Ok("Created Successfully");
        }

        //Update method
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _dbcontext.Products.Update(product);
            _dbcontext.SaveChanges();
            return NoContent();
        }

        //delete method
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            if (id == 0)
            {
                return BadRequest("Id should be provided");
            }
            var product = await _dbcontext.Products.FindAsync(id);
            
            if (product == null)
            {
                return NotFound($"The category is Not Found for id - {id}");
            }

            _dbcontext.Products.Remove(product);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }


        // total calculate
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("calculate-total")]
        public async Task<ActionResult<decimal>> CalculateTotal(POSRequest request)
        {
            if (request?.Items == null || !request.Items.Any())
            {
                return BadRequest("No items provided.");
            }

            double total = 0;

            foreach (var item in request.Items)
            {
                var product = await _dbcontext.Products.FindAsync(item.ProductId);
                if (product == null)
                {
                    return NotFound($"Product with ID {item.ProductId} not found.");
                }

                total = total + product.Price * item.Quantity;
            }

            return Ok("Total  is : " + total);
        }


    }

}


