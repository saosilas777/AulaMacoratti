using APICatalogo.Data;
using APICatalogo.Filters;
using APICatalogo.Models;
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
		private readonly AppDbContext _context;
		private readonly IConfiguration _config;
		private readonly ILogger _logger;
		public CategoriaController(AppDbContext context, IConfiguration config, ILogger<CategoriaController> logger)
		{
			_context = context;
			_config = config;
			_logger = logger;
		}

		[HttpGet("LerArquivodeconfiguracao")]
		public string  GetValues()
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
			return _context.Categorias.Include(x => x.Produtos).AsNoTracking().ToList();
		}


		[HttpGet]
		[ServiceFilter(typeof(ApiLogginFilter))]
		public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
		{

			try
			{
				_logger.LogInformation("===========GET api/categorias==================");

				var category = await _context.Categorias.AsNoTracking().ToListAsync();
				if (category is null)
				{
					_logger.LogInformation("===========GET api/categorias NOT FOUND===========");
					return NotFound("Category not found");
				}
				return category;

			}
			catch (Exception)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao processar a solicitação");
			}


		}

		[HttpGet("{id:int}", Name="ObterCategoria")]
		public ActionResult<Categoria> GetById(int id)
		{

			try
			{
				Categoria? category = _context.Categorias.AsNoTracking().FirstOrDefault(x => x.CategoriaId == id);
				if (category is null)
				{
					return NotFound("Category not found");
				}
				return category;

			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}


		}
		[HttpGet("UsandoMiddleware")]
		public ActionResult<Categoria> GetByIdMiddleware()
		{
			throw new Exception("exceção ao retornar a categoria");
		}

		[HttpPost("Create")]
		public ActionResult Create(Categoria category)
		{
			if (category == null)
				return BadRequest();

			_context.Categorias.Add(category);
			_context.SaveChanges();
			return new CreatedAtRouteResult("ObterProduto", new { id = category.CategoriaId }, category);
		}

		[HttpPut("{id:int}")]
		public ActionResult Update(int id, Categoria category)
		{
			if (id != category.CategoriaId)
			{
				return BadRequest();
			}
			_context.Entry(category).State = EntityState.Modified;
			_context.SaveChanges();
			return Ok(category);
		}

		[HttpDelete("{id}")]
		public ActionResult<Categoria> Delete(int id)
		{

			try
			{
				Categoria? category = _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);
				if (category is null)
				{
					return NotFound("Product not found");
				}
				_context.Remove(category);
				_context.SaveChanges();
				return Ok(category);

			}
			catch (Exception e)
			{

				throw new Exception(e.Message);
			}


		}
	}
}
