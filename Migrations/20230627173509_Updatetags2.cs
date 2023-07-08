using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiEsp32Master.Migrations
{
    /// <inheritdoc />
    public partial class Updatetags2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomId",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomId",
                table: "Tags");
        }
    }
}
