using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationVMS.Migrations
{
    /// <inheritdoc />
    public partial class loginidfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Visitor_LoginId",
                table: "Visitor",
                column: "LoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_Login_LoginId",
                table: "Visitor",
                column: "LoginId",
                principalTable: "Login",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_Login_LoginId",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_LoginId",
                table: "Visitor");
        }
    }
}
