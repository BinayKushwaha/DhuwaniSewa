using Microsoft.EntityFrameworkCore.Migrations;

namespace DhuwaniSewa.Database.Migrations
{
    public partial class updatedocumentnumbertoregistrationnumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "DocumentDetail",
                newName: "RegistrationNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationNumber",
                table: "DocumentDetail",
                newName: "Number");
        }
    }
}
