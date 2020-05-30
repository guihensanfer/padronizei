using Microsoft.EntityFrameworkCore.Migrations;

namespace Padronizei.Migrations
{
    public partial class AlteracaoColaborador2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Colaborador");

            migrationBuilder.AddColumn<int>(
                name: "Departamento",
                table: "Colaborador",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_Departamento",
                table: "Colaborador",
                column: "Departamento");

            migrationBuilder.AddForeignKey(
                name: "FK_Colaborador_Departamento_Departamento",
                table: "Colaborador",
                column: "Departamento",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaborador_Departamento_Departamento",
                table: "Colaborador");

            migrationBuilder.DropIndex(
                name: "IX_Colaborador_Departamento",
                table: "Colaborador");

            migrationBuilder.DropColumn(
                name: "Departamento",
                table: "Colaborador");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Colaborador",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
