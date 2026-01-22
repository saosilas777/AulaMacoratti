using APICatalogo.Data;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace APICatalogo.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CategoriaController : Controller
	{

		private readonly ICategoriaRepositorio _repository;
		private readonly IConfiguration _config;
		private readonly ILogger _logger;
		public CategoriaController(ICategoriaRepositorio repository, IConfiguration config, ILogger<CategoriaController> logger)
		{
			_config = config;
			_logger = logger;
			_repository = repository;
		}

		[HttpGet("LerArquivodeconfiguracao")]
		public string GetValues()
		{
			var value1 = _config["chave1"];
			var value2 = _config["chave2"];
			var secao1 = _config["secao1:chave2"];

			return $"Chave1 =  {value1}\n Chave2 = {value2} \n Seção1 => chave2 = {secao1}";

		}

		[HttpGet("FromService/{nome}")]
		public ActionResult<string> GetSaudacaoFromService([FromServices] IService meuServico, string nome)
		{
			return meuServico.Saudacao(nome);
		}


		[HttpGet("SemFromService/{nome}")]
		public ActionResult<string> GetSaudacaoSemFromService(IService meuServico, string nome)
		{
			return meuServico.Saudacao(nome);
		}

		[HttpGet("produtos")]
		public ActionResult<IEnumerable<Categoria>> GetCategorysAndProducts()
		{
			var categoriasProdutos = _repository.GetCategorysAndProducts();
			return Ok(categoriasProdutos);
		}


		[HttpGet]
		[ServiceFilter(typeof(ApiLogginFilter))]
		public ActionResult<IEnumerable<Categoria>> GetCategorias()
		{
			IEnumerable<Categoria> categorias = _repository.GetCategorias();
			return Ok(categorias);
		}

		[HttpGet("{id:int}", Name = "ObterCategoria")]
		public ActionResult<Categoria> GetCategoria(int id)
		{
			Categoria category = _repository.GetCategoria(id);
			return Ok(category);

		}
		[HttpGet("UsandoMiddleware")]
		public ActionResult<Categoria> GetByIdMiddleware()
		{
			throw new Exception("exceção ao retornar a categoria");
		}

		[HttpPost("Create")]
		public ActionResult CreateCategoria(Categoria category)
		{
			var _categoria = _repository.CreateCategoria(category);
			return new CreatedAtRouteResult("ObterProduto", new { id = _categoria.CategoriaId }, _categoria);
		}

		[HttpPut("{id:int}")]
		public ActionResult UpdateCategoria(int id, Categoria categoria)
		{
			var _categoria = _repository.UpdateCategoria(categoria);
			return Ok(_categoria);
		}

		[HttpDelete("{id}")]
		public ActionResult<Categoria> DeleteCategoria(int id)
		{
			var categoria = _repository.DeleteCategoria(id);
			return Ok(categoria);
		}


	}
}

