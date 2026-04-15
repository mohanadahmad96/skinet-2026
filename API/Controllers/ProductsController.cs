using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
{


    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        var spec = new ProductSpecification(brand, type, sort);
        var products = await repo.ListAsync(spec);
        return Ok (products);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new ProductBrandSpecification();
        var brands = await repo.ListAsync(spec);
        return Ok(brands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new ProductTypesSpecification();
        var types = await repo.ListAsync(spec);
        return Ok(types);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        await repo.AddAsync(product);

        if(await repo.SaveChangesAsync())
        {
            return CreatedAtAction("GetProduct",new {id = product.Id}, product);
        }

     
        return BadRequest("Problem Creating Product");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id)
            return BadRequest("Cannot update this product");

        if (!await ProductExists(id))
            return NotFound();

        repo.Update(product);

        if(await repo.SaveChangesAsync())
        {
          return NoContent();
        }

       return BadRequest("Problem Update Product"); 

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        repo.Delete(product);

        if(await repo.SaveChangesAsync())
        {
          return NoContent();
        }

       return BadRequest("Problem Delete Product"); 

    }

  
   private async Task<bool> ProductExists(int id)
    {
        return await repo.ExistsAsync(id);
    }
}