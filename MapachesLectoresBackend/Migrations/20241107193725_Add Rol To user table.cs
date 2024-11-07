using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapachesLectoresBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddRolTousertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "Role",
                table: "user",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "user");
        }
    }
}
