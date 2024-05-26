using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Domain.Migrations
{
    /// <inheritdoc />
    public partial class update_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Job_Category");

            migrationBuilder.DropTable(
                name: "Job_Location");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Category_CompanyId",
                table: "Job",
                column: "CompanyId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Location_CompanyId",
                table: "Job",
                column: "CompanyId",
                principalTable: "Location",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Category_CompanyId",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_Location_CompanyId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Job");

            migrationBuilder.CreateTable(
                name: "Job_Category",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Category", x => new { x.JobId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_Job_Category_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Category_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Job_Location",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Location", x => new { x.JobId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_Job_Location_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Location_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_Category_CategoryId",
                table: "Job_Category",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Location_LocationId",
                table: "Job_Location",
                column: "LocationId");
        }
    }
}
