using Microsoft.EntityFrameworkCore.Migrations;

namespace DhuwaniSewa.Database.Migrations
{
    public partial class updatetablestructurerelatedwithappUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceSeeker_UserId",
                table: "ServiceSeeker");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProvider_UserId",
                table: "ServiceProvider");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDetails_AppUserId",
                table: "PersonalDetails");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_CompanyDetails_AppUserId",
                table: "CompanyDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSeeker_UserId",
                table: "ServiceSeeker",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProvider_UserId",
                table: "ServiceProvider",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_AppUserId",
                table: "PersonalDetails",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDetails_AppUserId",
                table: "CompanyDetails",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceSeeker_UserId",
                table: "ServiceSeeker");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProvider_UserId",
                table: "ServiceProvider");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDetails_AppUserId",
                table: "PersonalDetails");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_CompanyDetails_AppUserId",
                table: "CompanyDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSeeker_UserId",
                table: "ServiceSeeker",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProvider_UserId",
                table: "ServiceProvider",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_AppUserId",
                table: "PersonalDetails",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDetails_AppUserId",
                table: "CompanyDetails",
                column: "AppUserId",
                unique: true);
        }
    }
}
