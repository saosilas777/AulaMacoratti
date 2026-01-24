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
		private readonly IProdutoRepositorio _repository;
		public ProdutosController(IProdutoRepositorio repository)
		{
			_repository = repository;
		}

		[HttpGet("GetAll")]
		public ActionResult<IEnumerable<Produto>> GetAllProdutos()
		{
			var products = _repository.GetAll();
			if (products is null) throw new Exception("Requisição sem sucesso");
			return Ok(products);

		}

		[HttpGet("{id:min(1)}", Name = "ObterProduto")]
		public ActionResult<Produto> GetByIdAsync(int id)
		{

			Produto? product = _repository.Get(p => p.ProdutoId == id);
			if (product is null) throw new Exception("Requisição sem sucesso");
			return product;

		}

		[HttpPost("Create")]
		public ActionResult Create(Produto produto)
		{
			if (produto == null)
				return BadRequest();

			_repository.Create(produto);
			return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
		}

		[HttpPut("{id:int}")]
		public ActionResult Update(int id, Produto produto)
		{
			if (id != produto.ProdutoId) throw new Exception("Requisição sem sucesso");

			_repository.Update(produto);
			return Ok(produto);
		}
		[HttpDelete("{id}")]
		public ActionResult<Produto> Delete(int id)
		{
			var produto = _repository.Get(p => p.ProdutoId == id);
			if (produto is null) throw new Exception("Requisição sem sucesso");
			return _repository.Delete(produto);
		}

		[HttpGet("{categoryId}", Name = "ObterProdutoPorCategoria")]
		public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int categoryId)
		{
			return _repository.GetProdutosPorCategoria(categoryId).ToList();
		}
	}
}
