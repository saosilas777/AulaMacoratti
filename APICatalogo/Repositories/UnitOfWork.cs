using APICatalogo.Data;

namespace APICatalogo.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private IProdutoRepositorio? _produtoRepo;

		private ICategoriaRepositorio? _categoriaRepo;

		public AppDbContext _context;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}
		public IProdutoRepositorio produtoRepositorio
		{
			get { return _produtoRepo = _produtoRepo ?? new ProdutoRepositorio(_context); }
		}
		public ICategoriaRepositorio categoriaRepositorio
		{
			get { return _categoriaRepo = _categoriaRepo ?? new CategoriaRepositorio(_context); }
		}

		public void Commit()
		{
			_context.SaveChanges();
		}
		public void Dispose()
		{
			_context?.Dispose();
		}
	}
}
