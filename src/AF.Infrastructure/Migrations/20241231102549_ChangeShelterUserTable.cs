﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AF.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeShelterUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShelterUsers_Id",
                table: "ShelterUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShelterUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ShelterUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ShelterUsers_Id",
                table: "ShelterUsers",
                column: "Id",
                unique: true);
        }
    }
}
