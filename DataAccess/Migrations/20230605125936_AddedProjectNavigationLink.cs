using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vexed.Migrations
{
    public partial class AddedProjectNavigationLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeams_ProjectId",
                table: "ProjectTeams",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeams_Projects_ProjectId",
                table: "ProjectTeams",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeams_Projects_ProjectId",
                table: "ProjectTeams");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTeams_ProjectId",
                table: "ProjectTeams");
        }
    }
}
