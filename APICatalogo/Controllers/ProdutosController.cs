using APICatalogo.Data;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ProdutosController : ControllerBase
	{
		private readonly AppDbContext _context;
		public ProdutosController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Produto>> GetAll()
		{
			
			try
			{
				var products = _context.Produtos.AsNoTracking().ToList();
				if(products is null)
				{
					return NotFound("Products not found");
				}
				return products;

			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}
			

		}

		[HttpGet("{id:min(1)}", Name = "ObterProduto")]
		public async Task<ActionResult<Produto>> GetByIdAsync(int id)
		{

			try
			{
				Produto?  product = await _context.Produtos.Include(x => x.Categoria).AsNoTracking().FirstOrDefaultAsync(x => x.ProdutoId == id);
				if (product is null)
				{
					return NotFound("Product not found");
				}
				return  product;

			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}


		}

		[HttpPost("Create")]
		public ActionResult Create(Produto produto)
		{
			if (produto == null)
				return BadRequest();

			_context.Produtos.Add(produto);
			_context.SaveChanges();
			return new CreatedAtRouteResult("ObterProduto",new { id = produto.ProdutoId},produto); 
		}

		[HttpPut("{id:int}")]
		public ActionResult Update(int id, Produto produto)
		{
			if(id != produto.ProdutoId)
			{
				return BadRequest();
			}
			_context.Entry(produto).State = EntityState.Modified;
			_context.SaveChanges();
			return Ok(produto);
		}
		[HttpDelete("{id}")]
		public ActionResult<Produto> Delete(int id)
		{

			try
			{
				Produto? product = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
				if (product is null)
				{
					return NotFound("Product not found");
				}
				_context.Remove(product);
				_context.SaveChanges();
				return Ok(product);

			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}


		}
	}
}
