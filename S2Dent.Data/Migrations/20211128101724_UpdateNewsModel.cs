using Microsoft.EntityFrameworkCore.Migrations;

namespace S2Dent.Data.Migrations
{
    public partial class UpdateNewsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "News",
                newName: "TitleInEnglish");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "News",
                newName: "DescriptionInEnglish");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionInBulgarian",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleInBulgarian",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionInBulgarian",
                table: "News");

            migrationBuilder.DropColumn(
                name: "TitleInBulgarian",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "TitleInEnglish",
                table: "News",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "DescriptionInEnglish",
                table: "News",
                newName: "Description");
        }
    }
}
