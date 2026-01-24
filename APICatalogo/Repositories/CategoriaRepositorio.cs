using APICatalogo.Data;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
	public class CategoriaRepositorio : Repository<Categoria>, ICategoriaRepositorio
	{
		public CategoriaRepositorio(AppDbContext context) : base(context)
		{
		}
	}
}
