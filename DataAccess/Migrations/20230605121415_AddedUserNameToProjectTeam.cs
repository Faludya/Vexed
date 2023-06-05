using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vexed.Migrations
{
    public partial class AddedUserNameToProjectTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ProjectTeams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ProjectTeams");
        }
    }
}
