using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapachesLectoresBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreatedReviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_user_ItemUuid",
                table: "user",
                column: "ItemUuid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_book_ItemUuid",
                table: "book",
                column: "ItemUuid");

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BookId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GeneralRating = table.Column<uint>(type: "int unsigned", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    ItemUuid = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_review_book_BookId",
                        column: x => x.BookId,
                        principalTable: "book",
                        principalColumn: "ItemUuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_review_user_BookId",
                        column: x => x.BookId,
                        principalTable: "user",
                        principalColumn: "ItemUuid");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_review_BookId",
                table: "review",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_review_ItemUuid",
                table: "review",
                column: "ItemUuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_user_ItemUuid",
                table: "user");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_book_ItemUuid",
                table: "book");
        }
    }
}
