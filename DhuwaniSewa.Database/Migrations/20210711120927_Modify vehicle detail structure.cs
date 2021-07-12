using Microsoft.EntityFrameworkCore.Migrations;

namespace DhuwaniSewa.Database.Migrations
{
    public partial class Modifyvehicledetailstructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfVehicle",
                table: "ServiceProviderVehicleDetail");

            migrationBuilder.AddColumn<int>(
                name: "MaxWeight",
                table: "VehicleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "VehicleDetail",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeightUnit",
                table: "VehicleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WheelType",
                table: "VehicleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxWeight",
                table: "VehicleDetail");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "VehicleDetail");

            migrationBuilder.DropColumn(
                name: "WeightUnit",
                table: "VehicleDetail");

            migrationBuilder.DropColumn(
                name: "WheelType",
                table: "VehicleDetail");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVehicle",
                table: "ServiceProviderVehicleDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
