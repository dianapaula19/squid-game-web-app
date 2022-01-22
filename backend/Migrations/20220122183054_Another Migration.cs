using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class AnotherMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FrontManVIP");

            migrationBuilder.DropTable(
                name: "FrontMen");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Players",
                newName: "PlayerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Guards",
                newName: "GuardId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Todos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserForeignKey",
                table: "Players",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserForeignKey",
                table: "Guards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUserVIPs",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VIPId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserVIPs", x => new { x.ApplicationUserId, x.VIPId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserVIPs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserVIPs_VIPS_VIPId",
                        column: x => x.VIPId,
                        principalTable: "VIPS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_ApplicationUserId",
                table: "Todos",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_ApplicationUserForeignKey",
                table: "Players",
                column: "ApplicationUserForeignKey",
                unique: true,
                filter: "[ApplicationUserForeignKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Guards_ApplicationUserForeignKey",
                table: "Guards",
                column: "ApplicationUserForeignKey",
                unique: true,
                filter: "[ApplicationUserForeignKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserVIPs_VIPId",
                table: "ApplicationUserVIPs",
                column: "VIPId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guards_AspNetUsers_ApplicationUserForeignKey",
                table: "Guards",
                column: "ApplicationUserForeignKey",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_AspNetUsers_ApplicationUserForeignKey",
                table: "Players",
                column: "ApplicationUserForeignKey",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_AspNetUsers_ApplicationUserId",
                table: "Todos",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guards_AspNetUsers_ApplicationUserForeignKey",
                table: "Guards");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_AspNetUsers_ApplicationUserForeignKey",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_AspNetUsers_ApplicationUserId",
                table: "Todos");

            migrationBuilder.DropTable(
                name: "ApplicationUserVIPs");

            migrationBuilder.DropIndex(
                name: "IX_Todos_ApplicationUserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Players_ApplicationUserForeignKey",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Guards_ApplicationUserForeignKey",
                table: "Guards");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "ApplicationUserForeignKey",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ApplicationUserForeignKey",
                table: "Guards");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Players",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "GuardId",
                table: "Guards",
                newName: "UserId");

            migrationBuilder.CreateTable(
                name: "FrontMen",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrontMen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FrontManVIP",
                columns: table => new
                {
                    FrontMenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VIPsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrontManVIP", x => new { x.FrontMenId, x.VIPsId });
                    table.ForeignKey(
                        name: "FK_FrontManVIP_FrontMen_FrontMenId",
                        column: x => x.FrontMenId,
                        principalTable: "FrontMen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FrontManVIP_VIPS_VIPsId",
                        column: x => x.VIPsId,
                        principalTable: "VIPS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FrontManVIP_VIPsId",
                table: "FrontManVIP",
                column: "VIPsId");
        }
    }
}
