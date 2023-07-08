using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiEsp32Master.Migrations
{
    /// <inheritdoc />
    public partial class UdTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tasks",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CodeNumber",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                name: "IX_Tasks_GroupId",
                table: "Tasks",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PersonId",
                table: "Tasks",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TagId",
                table: "Tasks",
                column: "TagId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Groups_GroupId",
                table: "Tasks",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_People_PersonId",
                table: "Tasks",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Tags_TagId",
                table: "Tasks",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Groups_GroupId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_People_PersonId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Groups_GroupId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_People_PersonId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Tags_TagId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_GroupId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_PersonId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TagId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tags_GroupId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_PersonId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CodeNumber",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tasks",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
