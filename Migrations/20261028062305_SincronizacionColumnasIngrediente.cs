using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIXTURA.Migrations
{
    /// <inheritdoc />
    public partial class SincronizacionColumnasIngrediente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleReceta_Ingredientes_IngredienteId",
                table: "DetalleReceta");

            migrationBuilder.DropForeignKey(
                name: "FK_DetalleReceta_Recetas_RecetaId",
                table: "DetalleReceta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleReceta",
                table: "DetalleReceta");

            migrationBuilder.RenameTable(
                name: "DetalleReceta",
                newName: "DetallesReceta");

            migrationBuilder.RenameColumn(
                name: "Presentacion",
                table: "Ingredientes",
                newName: "CantidadPresentacion");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "DetallesReceta",
                newName: "CantidadRequerida");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleReceta_RecetaId",
                table: "DetallesReceta",
                newName: "IX_DetallesReceta_RecetaId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleReceta_IngredienteId",
                table: "DetallesReceta",
                newName: "IX_DetallesReceta_IngredienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesReceta",
                table: "DetallesReceta",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesReceta_Ingredientes_IngredienteId",
                table: "DetallesReceta",
                column: "IngredienteId",
                principalTable: "Ingredientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesReceta_Recetas_RecetaId",
                table: "DetallesReceta",
                column: "RecetaId",
                principalTable: "Recetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesReceta_Ingredientes_IngredienteId",
                table: "DetallesReceta");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesReceta_Recetas_RecetaId",
                table: "DetallesReceta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesReceta",
                table: "DetallesReceta");

            migrationBuilder.RenameTable(
                name: "DetallesReceta",
                newName: "DetalleReceta");

            migrationBuilder.RenameColumn(
                name: "CantidadPresentacion",
                table: "Ingredientes",
                newName: "Presentacion");

            migrationBuilder.RenameColumn(
                name: "CantidadRequerida",
                table: "DetalleReceta",
                newName: "Cantidad");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesReceta_RecetaId",
                table: "DetalleReceta",
                newName: "IX_DetalleReceta_RecetaId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesReceta_IngredienteId",
                table: "DetalleReceta",
                newName: "IX_DetalleReceta_IngredienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleReceta",
                table: "DetalleReceta",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleReceta_Ingredientes_IngredienteId",
                table: "DetalleReceta",
                column: "IngredienteId",
                principalTable: "Ingredientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleReceta_Recetas_RecetaId",
                table: "DetalleReceta",
                column: "RecetaId",
                principalTable: "Recetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
