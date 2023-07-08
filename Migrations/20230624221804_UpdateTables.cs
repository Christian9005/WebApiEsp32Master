using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiEsp32Master.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Groups_GroupId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_People_PersonId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_GroupId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_PersonId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Tags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_GroupId",
                table: "Tags",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PersonId",
                table: "Tags",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Groups_GroupId",
                table: "Tags",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_People_PersonId",
                table: "Tags",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id");
        }
    }
}
