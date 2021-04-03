using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectReporter.Modules.UsersService.Storage.Migrations
{
    public partial class MoveGroupsFromDepartmentToFaculty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicGroups_Departments_DepartmentId",
                table: "AcademicGroups");

            migrationBuilder.DropIndex(
                name: "IX_AcademicGroups_DepartmentId",
                table: "AcademicGroups");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AcademicGroups");

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "AcademicGroups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicGroups_FacultyId",
                table: "AcademicGroups",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicGroups_Faculties_FacultyId",
                table: "AcademicGroups",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicGroups_Faculties_FacultyId",
                table: "AcademicGroups");

            migrationBuilder.DropIndex(
                name: "IX_AcademicGroups_FacultyId",
                table: "AcademicGroups");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "AcademicGroups");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "AcademicGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicGroups_DepartmentId",
                table: "AcademicGroups",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicGroups_Departments_DepartmentId",
                table: "AcademicGroups",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
