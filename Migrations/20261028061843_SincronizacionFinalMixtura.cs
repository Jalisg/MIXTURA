using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIXTURA.Migrations
{
    /// <inheritdoc />
    public partial class SincronizacionFinalMixtura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesRecetas_Ingredientes_IngredienteId",
                table: "DetallesRecetas");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesRecetas_Recetas_RecetaId",
                table: "DetallesRecetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesRecetas",
                table: "DetallesRecetas");

            migrationBuilder.RenameTable(
                name: "DetallesRecetas",
                newName: "DetalleReceta");

            migrationBuilder.RenameColumn(
                name: "CantidadRequerida",
                table: "DetalleReceta",
                newName: "Cantidad");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesRecetas_RecetaId",
                table: "DetalleReceta",
                newName: "IX_DetalleReceta_RecetaId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesRecetas_IngredienteId",
                table: "DetalleReceta",
                newName: "IX_DetalleReceta_IngredienteId");

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                table: "Ingredientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Presentacion",
                table: "Ingredientes",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Ingredientes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "DetallesRecetas");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "DetallesRecetas",
                newName: "CantidadRequerida");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleReceta_RecetaId",
                table: "DetallesRecetas",
                newName: "IX_DetallesRecetas_RecetaId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleReceta_IngredienteId",
                table: "DetallesRecetas",
                newName: "IX_DetallesRecetas_IngredienteId");

            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                table: "Ingredientes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "Presentacion",
                table: "Ingredientes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Ingredientes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesRecetas",
                table: "DetallesRecetas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesRecetas_Ingredientes_IngredienteId",
                table: "DetallesRecetas",
                column: "IngredienteId",
                principalTable: "Ingredientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesRecetas_Recetas_RecetaId",
                table: "DetallesRecetas",
                column: "RecetaId",
                principalTable: "Recetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
