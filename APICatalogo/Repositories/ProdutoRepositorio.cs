using APICatalogo.Data;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
	public class ProdutoRepositorio : Repository<Produto>, IProdutoRepositorio
	{
		public ProdutoRepositorio(AppDbContext context) : base(context) { }

		public IEnumerable<Produto> GetProdutosPorCategoria(int id)
		{
			return GetAll().Where(c => c.CategoriaId == id);
		}
	}
}
