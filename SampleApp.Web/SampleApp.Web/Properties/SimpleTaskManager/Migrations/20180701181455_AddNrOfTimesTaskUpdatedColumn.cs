using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleTaskManager.Migrations
{
    public partial class AddNrOfTimesTaskUpdatedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NrOfTimesTaskUpdated",
                table: "Assignment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NrOfTimesTaskUpdated",
                table: "Assignment");
        }
    }
}
