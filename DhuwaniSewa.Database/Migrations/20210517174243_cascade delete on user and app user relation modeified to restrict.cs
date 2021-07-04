using Microsoft.EntityFrameworkCore.Migrations;

namespace DhuwaniSewa.Database.Migrations
{
    public partial class cascadedeleteonuserandappuserrelationmodeifiedtorestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspUser_ApplicationUser",
                table: "AppUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalDetail",
                table: "PersonalDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyDetail",
                table: "CompanyDetail");

            migrationBuilder.RenameTable(
                name: "PersonalDetail",
                newName: "PersonalDetails");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "CompanyDetail",
                newName: "CompanyDetails");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalDetail_AppUserId",
                table: "PersonalDetails",
                newName: "IX_PersonalDetails_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_AppUserId",
                table: "Employees",
                newName: "IX_Employees_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_AppUserId",
                table: "Customers",
                newName: "IX_Customers_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyDetail_AppUserId",
                table: "CompanyDetails",
                newName: "IX_CompanyDetails_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalDetails",
                table: "PersonalDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyDetails",
                table: "CompanyDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspUser_ApplicationUser",
                table: "AppUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspUser_ApplicationUser",
                table: "AppUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalDetails",
                table: "PersonalDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyDetails",
                table: "CompanyDetails");

            migrationBuilder.RenameTable(
                name: "PersonalDetails",
                newName: "PersonalDetail");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "CompanyDetails",
                newName: "CompanyDetail");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalDetails_AppUserId",
                table: "PersonalDetail",
                newName: "IX_PersonalDetail_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_AppUserId",
                table: "Employee",
                newName: "IX_Employee_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_AppUserId",
                table: "Customer",
                newName: "IX_Customer_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyDetails_AppUserId",
                table: "CompanyDetail",
                newName: "IX_CompanyDetail_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalDetail",
                table: "PersonalDetail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyDetail",
                table: "CompanyDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspUser_ApplicationUser",
                table: "AppUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
