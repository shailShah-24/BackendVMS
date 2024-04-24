using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationVMS.Migrations
{
    /// <inheritdoc />
    public partial class loginid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginId",
                table: "Visitor",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Visitor");
        }
    }
}
