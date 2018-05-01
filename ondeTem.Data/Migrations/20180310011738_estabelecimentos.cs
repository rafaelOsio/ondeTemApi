using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ondeTem.Data.Migrations
{
    public partial class estabelecimentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estabelecimentos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Bairro = table.Column<string>(type: "varchar(65)", maxLength: 65, nullable: false),
                    Complemento = table.Column<string>(type: "varchar(65)", maxLength: 65, nullable: false),
                    Desativado = table.Column<bool>(type: "bool", nullable: true, defaultValue: false),
                    Latitude = table.Column<decimal>(type: "decimal(65, 30)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(65, 30)", nullable: false),
                    MensagemParaClientes = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: true),
                    Nome = table.Column<string>(type: "varchar(65)", maxLength: 65, nullable: false),
                    Numero = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Rua = table.Column<string>(type: "varchar(65)", maxLength: 65, nullable: false),
                    TelefonePrincipal = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    TelefoneSecundario = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estabelecimentos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estabelecimentos");
        }
    }
}
