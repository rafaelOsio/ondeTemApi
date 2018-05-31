using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ondeTem.Data.Migrations
{
    public partial class sprint1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataHoraCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Nome = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "estabelecimentos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Bairro = table.Column<string>(type: "varchar(65)", maxLength: 65, nullable: true),
                    CaminhoImage = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Complemento = table.Column<string>(type: "varchar(65)", maxLength: 65, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    Desativado = table.Column<bool>(type: "bool", nullable: true, defaultValue: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    Latitude = table.Column<decimal>(type: "decimal(65, 30)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(65, 30)", nullable: true),
                    MensagemParaClientes = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true),
                    Nome = table.Column<string>(type: "varchar(65)", maxLength: 65, nullable: true),
                    Numero = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "longblob", nullable: false),
                    Rua = table.Column<string>(type: "varchar(65)", maxLength: 65, nullable: true),
                    TelefonePrincipal = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true),
                    TelefoneSecundario = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true),
                    isComplete = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estabelecimentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Acessos = table.Column<int>(type: "int", nullable: false),
                    CaminhoImage = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    CategoriaId = table.Column<long>(type: "bigint", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    EstabelecimentoId = table.Column<long>(type: "bigint", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_produtos_categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_produtos_estabelecimentos_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "estabelecimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_estabelecimentos_Email",
                table: "estabelecimentos",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_produtos_CategoriaId",
                table: "produtos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_produtos_EstabelecimentoId",
                table: "produtos",
                column: "EstabelecimentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "produtos");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "estabelecimentos");
        }
    }
}
