using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiEsp32Master.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "People",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_GroupId",
                table: "People",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Groups_GroupId",
                table: "People",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Groups_GroupId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_GroupId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "People");
        }
    }
}
