using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalago.Migrations
{
    /// <inheritdoc />
    public partial class PopilarCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Bebidas', 'bebidas.jpg')");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Lanches', 'lanches.jpg')");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Sobremesas', 'sobremesas.jpg')");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Massas', 'massas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
             mb.Sql("DELETE FROM Categorias WHERE Nome IN ('Bebidas', 'Lanches', 'Sobremesas', 'Massas')");

        }
    }
}
