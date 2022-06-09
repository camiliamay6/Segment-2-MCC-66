using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class update_education_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Universities_UniversityId",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_UniversityId",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "Educations");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_University_Id",
                table: "Educations",
                column: "University_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Universities_University_Id",
                table: "Educations",
                column: "University_Id",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Universities_University_Id",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_University_Id",
                table: "Educations");

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "Educations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_UniversityId",
                table: "Educations",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Universities_UniversityId",
                table: "Educations",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
