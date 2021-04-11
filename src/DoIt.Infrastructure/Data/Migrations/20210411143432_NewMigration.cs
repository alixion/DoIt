using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoIt.Infrastructure.Data.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "to_do_lists",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_to_do_lists", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "to_do_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    done = table.Column<bool>(type: "boolean", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    to_do_list_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_to_do_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_to_do_item_to_do_lists_to_do_list_id",
                        column: x => x.to_do_list_id,
                        principalTable: "to_do_lists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_to_do_item_title",
                table: "to_do_item",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_to_do_item_to_do_list_id",
                table: "to_do_item",
                column: "to_do_list_id");

            migrationBuilder.CreateIndex(
                name: "ix_to_do_lists_title",
                table: "to_do_lists",
                column: "title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "to_do_item");

            migrationBuilder.DropTable(
                name: "to_do_lists");
        }
    }
}
