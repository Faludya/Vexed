using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vexed.Migrations
{
    public partial class AddedTotalHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "TimeCards",
                newName: "TotalHours");

            migrationBuilder.AddColumn<float>(
                name: "DailyHours",
                table: "TimeCards",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<string>(
                name: "AttachmentUrl",
                table: "Qualifications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyHours",
                table: "TimeCards");

            migrationBuilder.RenameColumn(
                name: "TotalHours",
                table: "TimeCards",
                newName: "Quantity");

            migrationBuilder.AlterColumn<string>(
                name: "AttachmentUrl",
                table: "Qualifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
