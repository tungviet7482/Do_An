using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Domain.Migrations
{
    /// <inheritdoc />
    public partial class update_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Job");

            migrationBuilder.AddColumn<decimal>(
                name: "MaxSalary",
                table: "Job",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinSalary",
                table: "Job",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxSalary",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "MinSalary",
                table: "Job");

            migrationBuilder.AddColumn<string>(
                name: "Salary",
                table: "Job",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
