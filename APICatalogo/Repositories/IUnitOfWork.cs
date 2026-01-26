namespace APICatalogo.Repositories
{
	public interface IUnitOfWork
	{
		IProdutoRepositorio produtoRepositorio { get; }
		ICategoriaRepositorio categoriaRepositorio { get; }
		void Commit();
	}
}
