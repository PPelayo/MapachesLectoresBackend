using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MapachesLectoresBackend.Migrations
{
    /// <inheritdoc />
    public partial class NewCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "category",
                keyColumn: "Id",
                keyValue: 4u,
                column: "Description",
                value: "Trhiller");

            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "Id", "CreatedAt", "Description", "ItemUuid", "UpdatedAt" },
                values: new object[,]
                {
                    { 16u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dark Romance", "123e4567-e89b-12d3-a456-426614174015", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juegos", "123e4567-e89b-12d3-a456-426614174016", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 16u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 17u);

            migrationBuilder.UpdateData(
                table: "category",
                keyColumn: "Id",
                keyValue: 4u,
                column: "Description",
                value: "Policiaca");
        }
    }
}
