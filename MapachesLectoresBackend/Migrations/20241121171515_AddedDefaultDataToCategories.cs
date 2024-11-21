using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MapachesLectoresBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultDataToCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "Id", "CreatedAt", "Description", "ItemUuid", "UpdatedAt" },
                values: new object[,]
                {
                    { 1u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Terror", "123e4567-e89b-12d3-a456-426614174000", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romance", "123e4567-e89b-12d3-a456-426614174001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suspense", "123e4567-e89b-12d3-a456-426614174002", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Policiaca", "123e4567-e89b-12d3-a456-426614174003", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ciencia Ficción", "123e4567-e89b-12d3-a456-426614174004", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fantasía", "123e4567-e89b-12d3-a456-426614174005", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aventuras", "123e4567-e89b-12d3-a456-426614174006", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Misterio", "123e4567-e89b-12d3-a456-426614174007", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drama", "123e4567-e89b-12d3-a456-426614174008", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comedia", "123e4567-e89b-12d3-a456-426614174009", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Historia", "123e4567-e89b-12d3-a456-426614174010", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Biografía", "123e4567-e89b-12d3-a456-426614174011", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Autoayuda", "123e4567-e89b-12d3-a456-426614174012", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cocina", "123e4567-e89b-12d3-a456-426614174013", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15u, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Viajes", "123e4567-e89b-12d3-a456-426614174014", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 1u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 2u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 3u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 4u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 5u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 6u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 7u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 8u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 9u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 10u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 11u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 12u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 13u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 14u);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "Id",
                keyValue: 15u);
        }
    }
}
