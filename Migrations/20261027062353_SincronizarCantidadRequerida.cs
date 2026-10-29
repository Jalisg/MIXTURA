using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIXTURA.Migrations
{
    /// <inheritdoc />
    public partial class SincronizarCantidadRequerida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadBase",
                table: "DetallesRecetas");

            migrationBuilder.RenameColumn(
                name: "CostoInsumo",
                table: "DetallesRecetas",
                newName: "CantidadRequerida");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CantidadRequerida",
                table: "DetallesRecetas",
                newName: "CostoInsumo");

            migrationBuilder.AddColumn<decimal>(
                name: "CantidadBase",
                table: "DetallesRecetas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
