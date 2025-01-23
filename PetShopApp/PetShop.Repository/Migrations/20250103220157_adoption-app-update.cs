using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class adoptionappupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sumOfAdoptionFee",
                table: "AdoptionApplications",
                newName: "SumOfAdoptionFee");

            migrationBuilder.RenameColumn(
                name: "isValid",
                table: "AdoptionApplications",
                newName: "IsValid");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApplicationDate",
                table: "AdoptionApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationDate",
                table: "AdoptionApplications");

            migrationBuilder.RenameColumn(
                name: "SumOfAdoptionFee",
                table: "AdoptionApplications",
                newName: "sumOfAdoptionFee");

            migrationBuilder.RenameColumn(
                name: "IsValid",
                table: "AdoptionApplications",
                newName: "isValid");
        }
    }
}
