using APICatalogo.Data;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
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
		#region OthersMethods
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
		[HttpGet("UsandoMiddleware")]
		public ActionResult<Categoria> GetByIdMiddleware()
		{
			throw new Exception("exceção ao retornar a categoria");
		}

		//[HttpGet("produtos")]
		//public ActionResult<IEnumerable<Categoria>> GetCategorysAndProducts()
		//{
		//	var categoriasProdutos = _repository.GetCategorysAndProducts();
		//	return Ok(categoriasProdutos);
		//}

		#endregion

		[HttpGet]
		[ServiceFilter(typeof(ApiLogginFilter))]
		public ActionResult<IEnumerable<CategoriaDTO>> GetCategorias()
		{
			var categorysDb = _uof.categoriaRepositorio.GetAll();
			if (categorysDb is null) return NotFound();
			var categoriasDTO = categorysDb.ToCategoriaDTOList();
			return Ok(categoriasDTO);
		}

		[HttpGet("{id:int}", Name = "ObterCategoria")]
		public ActionResult<CategoriaDTO> GetCategoria(int id)
		{
			var category = _uof.categoriaRepositorio.Get(c => c.CategoriaId == id);
			if (category == null) throw new Exception("Requisição sem sucesso");

			var categoriaDTO = category.ToCategoriaDTO();
			return Ok(categoriaDTO);

		}
		

		[HttpPost("Create")]
		public ActionResult<CategoriaDTO> CreateCategoria(CategoriaDTO categoryDTO)
		{
			if (categoryDTO is null) throw new Exception("Requisição sem sucesso.");

			var categoria = categoryDTO.ToCategoria();
			var categoryDb = _uof.categoriaRepositorio.Create(categoria);
			_uof.Commit();
			return new CreatedAtRouteResult("ObterProduto", new { id = categoryDb.CategoriaId }, categoryDb);
		}

		[HttpPut("{id:int}")]
		public ActionResult<CategoriaDTO> UpdateCategoria(int id, CategoriaDTO categoryDTO)
		{
			if (categoryDTO.CategoriaId != id) throw new Exception("Requisição sem sucesso.");
			var categoria = categoryDTO.ToCategoria();
			_uof.categoriaRepositorio.Update(categoria);
			_uof.Commit();
			return Ok(categoryDTO);
		}

		[HttpDelete("{id}")]
		public ActionResult<CategoriaDTO> DeleteCategoria(int id)
		{
			var categoryDb = _uof.categoriaRepositorio.Get(c => c.CategoriaId == id);

			if (categoryDb == null) throw new Exception("Requisição sem sucesso");
			_uof.categoriaRepositorio.Delete(categoryDb);
			_uof.Commit();
			var categoriaDTO = CategoriaDTOMappingExtensions.ToCategoriaDTO(categoryDb);
			
			return Ok(categoriaDTO);
		}


	}
}

