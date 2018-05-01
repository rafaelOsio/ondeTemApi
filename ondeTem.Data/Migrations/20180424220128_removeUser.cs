using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ondeTem.Data.Migrations
{
    public partial class removeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_estabelecimentos_users_UserId",
                table: "estabelecimentos");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropIndex(
                name: "IX_estabelecimentos_UserId",
                table: "estabelecimentos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "estabelecimentos");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "estabelecimentos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "estabelecimentos",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "estabelecimentos",
                type: "longblob",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_estabelecimentos_Email",
                table: "estabelecimentos",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_estabelecimentos_Email",
                table: "estabelecimentos");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "estabelecimentos");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "estabelecimentos");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "estabelecimentos");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "estabelecimentos",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    IsAdmin = table.Column<bool>(nullable: true, defaultValue: false),
                    PasswordHash = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_estabelecimentos_UserId",
                table: "estabelecimentos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_estabelecimentos_users_UserId",
                table: "estabelecimentos",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
