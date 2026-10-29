using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIXTURA.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPrivacidadRecetas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Energia",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "ManoDeObra",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "Mermas",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "Transporte",
                table: "Recetas");

            migrationBuilder.RenameColumn(
                name: "Unidades",
                table: "Recetas",
                newName: "UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "Molde",
                table: "Recetas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "EsPublica",
                table: "Recetas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_UsuarioId",
                table: "Recetas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_Usuarios_UsuarioId",
                table: "Recetas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_Usuarios_UsuarioId",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_UsuarioId",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "EsPublica",
                table: "Recetas");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Recetas",
                newName: "Unidades");

            migrationBuilder.AlterColumn<string>(
                name: "Molde",
                table: "Recetas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Energia",
                table: "Recetas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ManoDeObra",
                table: "Recetas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Mermas",
                table: "Recetas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Transporte",
                table: "Recetas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
