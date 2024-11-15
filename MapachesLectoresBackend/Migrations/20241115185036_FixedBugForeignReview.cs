using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapachesLectoresBackend.Migrations
{
    /// <inheritdoc />
    public partial class FixedBugForeignReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_review_user_BookId",
                table: "review");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "review",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "review",
                type: "varchar(999)",
                maxLength: 999,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.CreateIndex(
                name: "IX_review_UserId",
                table: "review",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_review_user_UserId",
                table: "review",
                column: "UserId",
                principalTable: "user",
                principalColumn: "ItemUuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_review_user_UserId",
                table: "review");

            migrationBuilder.DropIndex(
                name: "IX_review_UserId",
                table: "review");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "review",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "review",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(999)",
                oldMaxLength: 999)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "review",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_review_user_BookId",
                table: "review",
                column: "BookId",
                principalTable: "user",
                principalColumn: "ItemUuid");
        }
    }
}
