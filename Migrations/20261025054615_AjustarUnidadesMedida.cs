using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIXTURA.Migrations
{
    /// <inheritdoc />
    public partial class AjustarUnidadesMedida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoUnidad",
                table: "Ingredientes",
                newName: "UnidadMedida");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnidadMedida",
                table: "Ingredientes",
                newName: "TipoUnidad");
        }
    }
}
