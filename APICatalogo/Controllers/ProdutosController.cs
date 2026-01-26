using APICatalogo.Data;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ProdutosController : Controller
	{
		private readonly IUnitOfWork _uof;
		public ProdutosController(IUnitOfWork uof)
		{
			_uof = uof;
		}

		[HttpGet("GetAll")]
		public ActionResult<IEnumerable<Produto>> GetProdutos()
		{
			var products = _uof.produtoRepositorio.GetAll();
			if (products is null) throw new Exception("Requisição sem sucesso");
			return Ok(products);

		}
		[HttpGet("{id:min(1)}", Name = "ObterProduto")]
		public ActionResult<Produto> GetProduto(int id)
		{

			Produto? product = _uof.produtoRepositorio.Get(p => p.ProdutoId == id);
			if (product is null) throw new Exception("Requisição sem sucesso");
			return product;

		}
		[HttpGet("{categoryId}", Name = "ObterProdutoPorCategoria")]
		public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int categoryId)
		{
			return _uof.produtoRepositorio.GetProdutosPorCategoria(categoryId).ToList();
		}

		[HttpPost("Create")]
		public ActionResult Create(Produto produto)
		{
			if (produto == null)
				return BadRequest();

			_uof.produtoRepositorio.Create(produto);
			_uof.Commit();
			return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
		}

		[HttpPut("{id:int}")]
		public ActionResult Update(int id, Produto produto)
		{
			if (id != produto.ProdutoId) throw new Exception("Requisição sem sucesso");

			_uof.produtoRepositorio.Update(produto);
			_uof.Commit();
			return Ok(produto);
		}
		[HttpDelete("{id}")]
		public ActionResult<Produto> Delete(int id)
		{
			var produto = _uof.produtoRepositorio.Get(p => p.ProdutoId == id);
			if (produto is null) throw new Exception("Requisição sem sucesso");
			_uof.produtoRepositorio.Delete(produto);
			_uof.Commit();
			return produto;
		}

		
	}
}
