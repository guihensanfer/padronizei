using Microsoft.EntityFrameworkCore.Migrations;

namespace Padronizei.Migrations
{
    public partial class alteracaoTeste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_OrganizacaoId",
                table: "Departamento",
                column: "OrganizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_DepartamentoId",
                table: "Colaborador",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colaborador_Departamento_DepartamentoId",
                table: "Colaborador",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departamento_Organizacao_OrganizacaoId",
                table: "Departamento",
                column: "OrganizacaoId",
                principalTable: "Organizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaborador_Departamento_DepartamentoId",
                table: "Colaborador");

            migrationBuilder.DropForeignKey(
                name: "FK_Departamento_Organizacao_OrganizacaoId",
                table: "Departamento");

            migrationBuilder.DropIndex(
                name: "IX_Departamento_OrganizacaoId",
                table: "Departamento");

            migrationBuilder.DropIndex(
                name: "IX_Colaborador_DepartamentoId",
                table: "Colaborador");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Colaborador");

            migrationBuilder.AddColumn<int>(
                name: "Departamento",
                table: "Colaborador",
                type: "int",
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
    }
}
