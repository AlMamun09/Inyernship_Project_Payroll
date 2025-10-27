using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentSystemFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BankAccountNumber",
                table: "Employees",
                newName: "PaymentSystem");

            migrationBuilder.AddColumn<string>(
                name: "AccountHolderName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAndBranchName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobilenumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountHolderName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BankAndBranchName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Mobilenumber",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "PaymentSystem",
                table: "Employees",
                newName: "BankAccountNumber");
        }
    }
}
