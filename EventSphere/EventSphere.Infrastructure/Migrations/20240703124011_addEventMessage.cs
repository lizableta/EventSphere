﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addEventMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Event",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Event");
        }
    }
}
