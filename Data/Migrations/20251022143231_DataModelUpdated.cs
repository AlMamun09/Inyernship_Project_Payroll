using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SlipId",
                table: "SalarySlips",
                newName: "SalarySlipId");

            migrationBuilder.RenameColumn(
                name: "ADType",
                table: "AllowanceDeductions",
                newName: "AllowanceDeductionType");

            migrationBuilder.RenameColumn(
                name: "ADName",
                table: "AllowanceDeductions",
                newName: "AllowanceDeductionName");

            migrationBuilder.RenameColumn(
                name: "ADId",
                table: "AllowanceDeductions",
                newName: "AllowanceDeductionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalarySlipId",
                table: "SalarySlips",
                newName: "SlipId");

            migrationBuilder.RenameColumn(
                name: "AllowanceDeductionType",
                table: "AllowanceDeductions",
                newName: "ADType");

            migrationBuilder.RenameColumn(
                name: "AllowanceDeductionName",
                table: "AllowanceDeductions",
                newName: "ADName");

            migrationBuilder.RenameColumn(
                name: "AllowanceDeductionId",
                table: "AllowanceDeductions",
                newName: "ADId");
        }
    }
}
