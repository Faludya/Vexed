using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vexed.Migrations
{
    public partial class AddedSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Function = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalWorkedHours = table.Column<float>(type: "real", nullable: false),
                    TotalWorkedDays = table.Column<int>(type: "int", nullable: false),
                    GrossSalary = table.Column<float>(type: "real", nullable: false),
                    SocialInsuranceTax = table.Column<float>(type: "real", nullable: false),
                    SocialInsuranceValue = table.Column<float>(type: "real", nullable: false),
                    HealthInsuranceTax = table.Column<float>(type: "real", nullable: false),
                    HealthInsuranceValue = table.Column<float>(type: "real", nullable: false),
                    PersonalIncomeTax = table.Column<float>(type: "real", nullable: false),
                    PersonalIncomeValue = table.Column<float>(type: "real", nullable: false),
                    WorkInsuranceTax = table.Column<float>(type: "real", nullable: false),
                    WorkInsuranceValue = table.Column<float>(type: "real", nullable: false),
                    NetSalary = table.Column<float>(type: "real", nullable: false),
                    MealTicketValue = table.Column<int>(type: "int", nullable: false),
                    MealTicketTotal = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Salaries");
        }
    }
}
