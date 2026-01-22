using APICatalogo.Data;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
	public class CategoriaRepositorio : ICategoriaRepositorio
	{
		private readonly AppDbContext _context;

		public CategoriaRepositorio(AppDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Categoria> GetCategorias()
		{
			var categorias = _context.Categorias.ToList();
			if(categorias is null) throw new ArgumentNullException(nameof(categorias));
			return categorias;
		}
		public Categoria? GetCategoria(int id)
		{
			var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);
			if (categoria is null) throw new ArgumentNullException(nameof(categoria));
			return categoria;
		}
		public Categoria CreateCategoria(Categoria categoria)
		{
			if (categoria is null) throw new ArgumentNullException(nameof(categoria));
			_context.Categorias.Add(categoria);
			_context.SaveChanges();
			return categoria;
		}
		public Categoria UpdateCategoria(Categoria categoria)
		{
			if(categoria is null) throw new ArgumentNullException(nameof(categoria));
			_context.Entry(categoria).State = EntityState.Modified;
			_context.SaveChanges();
			return categoria;
		}
		public Categoria DeleteCategoria(int id)
		{
			Categoria? category = _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);
			if (category is null) throw new ArgumentNullException(nameof(category));
			_context.Remove(category);
			_context.SaveChanges();
			return category;
		}
		public IEnumerable<Categoria> GetCategorysAndProducts()
		{
			var categorias = _context.Categorias.Include(p => p.Produtos).ToList();
			if(categorias is null) throw new ArgumentNullException(nameof(categorias));
			return categorias;
		}


	}
}
