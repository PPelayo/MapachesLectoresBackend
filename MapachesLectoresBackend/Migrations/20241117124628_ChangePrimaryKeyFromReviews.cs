using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapachesLectoresBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangePrimaryKeyFromReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_review",
                table: "review");

            // migrationBuilder.DropIndex(
            //     name: "IX_review_BookId",
            //     table: "review");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "review");

            migrationBuilder.AddPrimaryKey(
                name: "PK_review",
                table: "review",
                columns: new[] { "BookId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_review",
                table: "review");

            migrationBuilder.AddColumn<uint>(
                name: "Id",
                table: "review",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_review",
                table: "review",
                column: "Id");

            // migrationBuilder.CreateIndex(
            //     name: "IX_review_BookId",
            //     table: "review",
            //     column: "BookId");
        }
    }
}
