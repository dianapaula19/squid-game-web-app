using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class FinalV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserRole",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserRole",
                table: "AspNetUsers");
        }
    }
}
