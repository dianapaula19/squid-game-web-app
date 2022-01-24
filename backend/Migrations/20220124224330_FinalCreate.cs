using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class FinalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserVIPs_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserVIPs");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_AspNetUsers_ApplicationUserId",
                table: "Todos");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserVIPs",
                table: "ApplicationUserVIPs");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Todos",
                newName: "ApplicationUserForeignKey");

            migrationBuilder.RenameIndex(
                name: "IX_Todos_ApplicationUserId",
                table: "Todos",
                newName: "IX_Todos_ApplicationUserForeignKey");

            migrationBuilder.AddColumn<Guid>(
                name: "GuardInfoForeignKey",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerInfoForeignKey",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ApplicationUserVIPs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ApplicationUserVIPs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserVIPs",
                table: "ApplicationUserVIPs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserVIPs_ApplicationUserId",
                table: "ApplicationUserVIPs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserVIPs_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserVIPs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_AspNetUsers_ApplicationUserForeignKey",
                table: "Todos",
                column: "ApplicationUserForeignKey",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserVIPs_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserVIPs");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_AspNetUsers_ApplicationUserForeignKey",
                table: "Todos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserVIPs",
                table: "ApplicationUserVIPs");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserVIPs_ApplicationUserId",
                table: "ApplicationUserVIPs");

            migrationBuilder.DropColumn(
                name: "GuardInfoForeignKey",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlayerInfoForeignKey",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ApplicationUserVIPs");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserForeignKey",
                table: "Todos",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Todos_ApplicationUserForeignKey",
                table: "Todos",
                newName: "IX_Todos_ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ApplicationUserVIPs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserVIPs",
                table: "ApplicationUserVIPs",
                columns: new[] { "ApplicationUserId", "VIPId" });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserVIPs_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserVIPs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_AspNetUsers_ApplicationUserId",
                table: "Todos",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
