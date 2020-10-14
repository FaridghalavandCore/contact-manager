using Microsoft.EntityFrameworkCore.Migrations;

namespace Context.Migrations
{
    public partial class Change_PasswordInContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Contacts");
        }
    }
}
