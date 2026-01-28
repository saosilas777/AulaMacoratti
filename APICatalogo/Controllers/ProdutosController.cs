using APICatalogo.Data;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories;
using AutoMapper;
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
		private readonly IMapper _mapper;
		public ProdutosController(IUnitOfWork uof,IMapper mapper)
		{
			_uof = uof;
			_mapper = mapper;
		}

		[HttpGet("GetAll")]
		public ActionResult<IEnumerable<ProdutoDTO>> GetProdutos()
		{
			var produtosDb = _uof.produtoRepositorio.GetAll();
			if (produtosDb is null) throw new Exception("Requisição sem sucesso");
			var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtosDb);
			return Ok(produtosDTO);

		}
		[HttpGet("{id:min(1)}", Name = "ObterProduto")]
		public ActionResult<ProdutoDTO> GetProduto(int id)
		{
			var produtoDb = _uof.produtoRepositorio.Get(p => p.ProdutoId == id);
			if (produtoDb is null) throw new Exception("Requisição sem sucesso");
			var produto = _mapper.Map<ProdutoDTO>(produtoDb);	
			return Ok(produto);

		}
		[HttpGet("{categoryId}", Name = "ObterProdutoPorCategoria")]
		public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoria(int categoryId)
		{
			var produtosPorCategoria = _uof.produtoRepositorio.GetProdutosPorCategoria(categoryId).ToList();

			var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtosPorCategoria);
			
			return Ok(produtosDTO);
		}

		[HttpPost("Create")]
		public ActionResult<ProdutoDTO> Create(ProdutoDTO produtoDTO)
		{
			if (produtoDTO is null)
				return BadRequest();
			var produto = _mapper.Map<Produto>(produtoDTO);
			_uof.produtoRepositorio.Create(produto);
			_uof.Commit();
			return new CreatedAtRouteResult("ObterProduto", new { id = produtoDTO.ProdutoId }, produtoDTO);
		}

		[HttpPut("{id:int}")]
		public ActionResult<ProdutoDTO> Update(int id, ProdutoDTO produtoDTO)
		{
			if (id != produtoDTO.ProdutoId) throw new Exception("Requisição sem sucesso");
			var produto = _mapper.Map<Produto>(produtoDTO);
			_uof.produtoRepositorio.Update(produto);
			_uof.Commit();
			return Ok(produto);
		}
		[HttpDelete("{id}")]
		public ActionResult<ProdutoDTO> Delete(int id)
		{
			var produto = _uof.produtoRepositorio.Get(p => p.ProdutoId == id);
			if (produto is null) throw new Exception("Requisição sem sucesso");
			_uof.produtoRepositorio.Delete(produto);
			_uof.Commit();

			return _mapper.Map<ProdutoDTO>(produto);
		}

		
	}
}
