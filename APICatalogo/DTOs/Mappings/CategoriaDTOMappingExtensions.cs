using APICatalogo.Models;


namespace APICatalogo.DTOs.Mappings
{
	public static class CategoriaDTOMappingExtensions
	{
		public static CategoriaDTO? ToCategoriaDTO(this Categoria categoria)
		{
			var categoriaDTO = new CategoriaDTO
			{
				CategoriaId = categoria.CategoriaId,
				Nome = categoria.Nome,
				ImagemUrl = categoria.ImagemUrl

			};
			return categoriaDTO;
		}
		public static Categoria? ToCategoria(this CategoriaDTO categoriaDTO)
		{
			var categoria = new Categoria
			{
				CategoriaId = categoriaDTO.CategoriaId,
				Nome = categoriaDTO.Nome,
				ImagemUrl = categoriaDTO.ImagemUrl

			};
			return categoria;
		}
		public static IEnumerable<CategoriaDTO>? ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
		{
			return categorias.Select(categoria => new CategoriaDTO
			{
				CategoriaId = categoria.CategoriaId,
				Nome = categoria.Nome,
				ImagemUrl = categoria.ImagemUrl
			}).ToList();
		}

	}
}
