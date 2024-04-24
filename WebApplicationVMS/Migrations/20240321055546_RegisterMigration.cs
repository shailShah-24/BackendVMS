using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationVMS.Migrations
{
    /// <inheritdoc />
    public partial class RegisterMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "department",
                table: "Login",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Login",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "f_name",
                table: "Login",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "l_name",
                table: "Login",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone_no",
                table: "Login",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "department",
                table: "Login");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Login");

            migrationBuilder.DropColumn(
                name: "f_name",
                table: "Login");

            migrationBuilder.DropColumn(
                name: "l_name",
                table: "Login");

            migrationBuilder.DropColumn(
                name: "phone_no",
                table: "Login");
        }
    }
}
