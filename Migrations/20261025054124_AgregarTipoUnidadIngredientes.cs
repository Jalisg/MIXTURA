using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIXTURA.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTipoUnidadIngredientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoUnidad",
                table: "Ingredientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoUnidad",
                table: "Ingredientes");
        }
    }
}
