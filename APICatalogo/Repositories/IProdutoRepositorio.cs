using APICatalogo.Models;

namespace APICatalogo.Repositories
{
	public interface IProdutoRepositorio : IRepository<Produto>
	{
		//IEnumerable<Categoria> GetCategorias();
		//IEnumerable<Categoria> GetCategorysAndProducts();
		//Categoria? GetCategoria(int id);
		//Categoria CreateCategoria(Categoria categoria);
		//Categoria UpdateCategoria(Categoria categoria);
		//Categoria DeleteCategoria(int id);
		IEnumerable<Produto> GetProdutosPorCategoria(int id);
	}
}
