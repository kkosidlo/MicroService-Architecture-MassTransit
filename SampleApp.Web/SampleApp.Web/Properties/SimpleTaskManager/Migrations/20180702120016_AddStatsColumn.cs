using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleTaskManager.Migrations
{
    public partial class AddStatsColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NrOfTimesTaskCreated",
                table: "Assignment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NrOfTimesTaskCreated",
                table: "Assignment");
        }
    }
}
