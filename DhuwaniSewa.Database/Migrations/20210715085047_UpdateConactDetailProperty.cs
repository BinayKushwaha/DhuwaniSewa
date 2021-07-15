using Microsoft.EntityFrameworkCore.Migrations;

namespace DhuwaniSewa.Database.Migrations
{
    public partial class UpdateConactDetailProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "ContactPerson");

            migrationBuilder.DropColumn(
                name: "MobileNumberConfirmed",
                table: "ContactPerson");

            migrationBuilder.RenameColumn(
                name: "PersionId",
                table: "ContactPerson",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactPerson_PersionId",
                table: "ContactPerson",
                newName: "IX_ContactPerson_PersonId");

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "ContactDetail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MobileNumberConfirmed",
                table: "ContactDetail",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "ContactDetail");

            migrationBuilder.DropColumn(
                name: "MobileNumberConfirmed",
                table: "ContactDetail");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "ContactPerson",
                newName: "PersionId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactPerson_PersonId",
                table: "ContactPerson",
                newName: "IX_ContactPerson_PersionId");

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "ContactPerson",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MobileNumberConfirmed",
                table: "ContactPerson",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
