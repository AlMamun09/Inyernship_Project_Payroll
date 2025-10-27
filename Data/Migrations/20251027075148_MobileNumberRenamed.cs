using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class MobileNumberRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mobilenumber",
                table: "Employees",
                newName: "MobileNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                table: "Employees",
                newName: "Mobilenumber");
        }
    }
}
