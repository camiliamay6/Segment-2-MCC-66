using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class remove_primary_key_account_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Accounts_NIK",
                table: "AccountRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles");

            migrationBuilder.DropIndex(
                name: "IX_AccountRoles_Role_id",
                table: "AccountRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AccountRoles");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "AccountRoles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles",
                columns: new[] { "Role_id", "NIK" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Accounts_NIK",
                table: "AccountRoles",
                column: "NIK",
                principalTable: "Accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Accounts_NIK",
                table: "AccountRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "AccountRoles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AccountRoles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_Role_id",
                table: "AccountRoles",
                column: "Role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Accounts_NIK",
                table: "AccountRoles",
                column: "NIK",
                principalTable: "Accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
