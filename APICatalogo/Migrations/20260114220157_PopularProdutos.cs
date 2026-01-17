using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopularProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {   
			mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
                "Values('Coca-cola diet','Refrigerante de Cola','5.45','cocacola.jpg','10',GETDATE(),1)");
			mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
				"Values('Lanche de Atum','Lanche de Atum com maionese','10.25','atum.jpg','5',GETDATE(),2)");
			mb.Sql("Insert into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId)" +
				"Values('Pudim','Pudim de leite','6.00','pudim.jpg','8',GETDATE(),3)");

		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
			mb.Sql("Delete from Produtos");
		}
    }
}
