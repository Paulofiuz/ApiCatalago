using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalago.Migrations
{
    public partial class PopulaProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Inserir produtos com as categorias previamente criadas
            migrationBuilder.Sql(@"
                INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId)
                VALUES
                ('Cerveja Skol', 'Cerveja Skol Lata 350ml', 3.50, 'cerveja_skol.jpg', 100, '2025-03-26', 
                    (SELECT CategoriaId FROM Categorias WHERE Nome = 'Bebidas')),
                ('Refrigerante Coca-Cola', 'Refrigerante Coca-Cola Lata 350ml', 4.00, 'coca_cola.jpg', 150, '2025-03-26', 
                    (SELECT CategoriaId FROM Categorias WHERE Nome = 'Bebidas')),
                ('Sanduíche de Frango', 'Sanduíche de frango grelhado no pão de leite', 12.00, 'sanduiche_frango.jpg', 50, '2025-03-26', 
                    (SELECT CategoriaId FROM Categorias WHERE Nome = 'Lanches')),
                ('Hambúrguer', 'Hambúrguer tradicional com queijo e carne de primeira', 15.00, 'hamburguer_tradicional.jpg', 30, '2025-03-26', 
                    (SELECT CategoriaId FROM Categorias WHERE Nome = 'Lanches')),
                ('Torta de Limão', 'Torta de limão cremosa com base crocante', 20.00, 'torta_limao.jpg', 40, '2025-03-26', 
                    (SELECT CategoriaId FROM Categorias WHERE Nome = 'Sobremesas')),
                ('Pudim de Leite', 'Pudim de leite condensado com calda de caramelo', 18.00, 'pudim_leite.jpg', 50, '2025-03-26', 
                    (SELECT CategoriaId FROM Categorias WHERE Nome = 'Sobremesas')),
                ('Espaguete à Carbonara', 'Espaguete com molho carbonara cremoso', 25.00, 'espaguete_carbonara.jpg', 60, '2025-03-26', 
                    (SELECT CategoriaId FROM Categorias WHERE Nome = 'Massas')),
                ('Lasanha de Carne', 'Lasanha de carne com queijo e molho vermelho', 30.00, 'lasanha_carne.jpg', 40, '2025-03-26', 
                    (SELECT CategoriaId FROM Categorias WHERE Nome = 'Massas'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remover os produtos inseridos
            migrationBuilder.Sql("DELETE FROM Produtos WHERE Nome IN ('Cerveja Skol', 'Refrigerante Coca-Cola', 'Sanduíche de Frango', 'Hambúrguer', 'Torta de Limão', 'Pudim de Leite', 'Espaguete à Carbonara', 'Lasanha de Carne')");
        }
    }
}
