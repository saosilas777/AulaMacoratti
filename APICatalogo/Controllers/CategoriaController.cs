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

		//private readonly ICategoriaRepositorio _repository;
		private readonly IUnitOfWork _uof;
		private readonly IConfiguration _config;
		private readonly ILogger _logger;
		public CategoriaController(IUnitOfWork uof, IConfiguration config, ILogger<CategoriaController> logger)
		{
			_uof = uof;
			_config = config;
			_logger = logger;
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

		//[HttpGet("produtos")]
		//public ActionResult<IEnumerable<Categoria>> GetCategorysAndProducts()
		//{
		//	var categoriasProdutos = _repository.GetCategorysAndProducts();
		//	return Ok(categoriasProdutos);
		//}


		[HttpGet]
		[ServiceFilter(typeof(ApiLogginFilter))]
		public ActionResult<IEnumerable<Categoria>> GetCategorias()
		{
			return Ok(_uof.categoriaRepositorio.GetAll());
		}

		[HttpGet("{id:int}", Name = "ObterCategoria")]
		public ActionResult<Categoria> GetCategoria(int id)
		{
			Categoria category = _uof.categoriaRepositorio.Get(c => c.CategoriaId == id);
			if (category == null) throw new Exception("Requisição sem sucesso");
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
			var _categoria = _uof.categoriaRepositorio.Create(category);
			_uof.Commit();
			return new CreatedAtRouteResult("ObterProduto", new { id = _categoria.CategoriaId }, _categoria);
		}

		[HttpPut("{id:int}")]
		public ActionResult UpdateCategoria(int id, Categoria categoria)
		{
			var _categoria = _uof.categoriaRepositorio.Update(categoria);
			_uof.Commit();
			return Ok(_categoria);
		}

		[HttpDelete("{id}")]
		public ActionResult<Categoria> DeleteCategoria(int id)
		{
			var categoria = _uof.categoriaRepositorio.Get(c => c.CategoriaId == id);
			if (categoria == null) throw new Exception("Requisição sem sucesso");
			_uof.categoriaRepositorio.Delete(categoria);
			_uof.Commit();
			return Ok(categoria);
		}


	}
}

