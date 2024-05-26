﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Domain.Migrations
{
    /// <inheritdoc />
    public partial class update_15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobType",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobType",
                table: "Job");
        }
    }
}
