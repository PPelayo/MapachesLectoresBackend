using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapachesLectoresBackend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    ItemUuid = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    ItemUuid = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "publisher",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    ItemUuid = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publisher", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", maxLength: 99999, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    ItemUuid = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Synopsis = table.Column<string>(type: "TEXT", maxLength: 99999, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PublishedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CoverUrl = table.Column<string>(type: "TEXT", maxLength: 99999, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumberOfPages = table.Column<uint>(type: "int unsigned", nullable: false),
                    PublisherId = table.Column<uint>(type: "int unsigned", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    ItemUuid = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_book_publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "books_authors",
                columns: table => new
                {
                    BookId = table.Column<uint>(type: "int unsigned", nullable: false),
                    AuthorId = table.Column<uint>(type: "int unsigned", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    ItemUuid = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books_authors", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_books_authors_author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_books_authors_book_BookId",
                        column: x => x.BookId,
                        principalTable: "book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "books_categories",
                columns: table => new
                {
                    BookId = table.Column<uint>(type: "int unsigned", nullable: false),
                    CategoryId = table.Column<uint>(type: "int unsigned", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME(3)", nullable: false, defaultValueSql: "NOW(3)"),
                    ItemUuid = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books_categories", x => new { x.BookId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_books_categories_book_BookId",
                        column: x => x.BookId,
                        principalTable: "book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_books_categories_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_author_ItemUuid",
                table: "author",
                column: "ItemUuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_book_ItemUuid",
                table: "book",
                column: "ItemUuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_book_PublisherId",
                table: "book",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_books_authors_AuthorId",
                table: "books_authors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_books_authors_ItemUuid",
                table: "books_authors",
                column: "ItemUuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_books_categories_CategoryId",
                table: "books_categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_books_categories_ItemUuid",
                table: "books_categories",
                column: "ItemUuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_ItemUuid",
                table: "category",
                column: "ItemUuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_publisher_ItemUuid",
                table: "publisher",
                column: "ItemUuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_ItemUuid",
                table: "user",
                column: "ItemUuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books_authors");

            migrationBuilder.DropTable(
                name: "books_categories");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "publisher");
        }
    }
}
