using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Climapi.DataEF.Migrations
{
    public partial class WithNewRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueryRecords_AspNetUsers_UserId",
                table: "QueryRecords");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53dd6905-b863-4e78-8c3f-d96ec469ec7f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c724b7c-5f22-4ac9-b15c-5b3f56f7c27c");

            migrationBuilder.AddColumn<int>(
                name: "DalyRequests",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyRequests",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeeklyRequests",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppRoleAppUser",
                columns: table => new
                {
                    AppUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserAppRolesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleAppUser", x => new { x.AppUsersId, x.UserAppRolesId });
                    table.ForeignKey(
                        name: "FK_AppRoleAppUser_AspNetRoles_UserAppRolesId",
                        column: x => x.UserAppRolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppRoleAppUser_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DalyRequests", "MonthlyRequests", "Name", "NormalizedName", "WeeklyRequests" },
                values: new object[] { "5f11a36a-39fa-4999-a258-8a9bbe2d018d", "263513ca-7c4f-4624-9f54-27d144445eb5", 20, 300, "User", "USER", 100 });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DalyRequests", "MonthlyRequests", "Name", "NormalizedName", "WeeklyRequests" },
                values: new object[] { "a3c015a3-0d23-4a0d-9c50-89fd0a7117a8", "07289b89-b591-4ee1-b43d-7f7b862d97d0", 2147483647, 2147483647, "Admin", "ADMIN", 2147483647 });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DalyRequests", "MonthlyRequests", "Name", "NormalizedName", "WeeklyRequests" },
                values: new object[] { "e006fb01-f8da-4725-8dde-289213d2fe9d", "50f4497d-f2e6-4841-ab3a-be492f905329", 40, 600, "UserPro", "USERPRO", 200 });

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleAppUser_UserAppRolesId",
                table: "AppRoleAppUser",
                column: "UserAppRolesId");

            migrationBuilder.AddForeignKey(
                name: "fk_user",
                table: "QueryRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user",
                table: "QueryRecords");

            migrationBuilder.DropTable(
                name: "AppRoleAppUser");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f11a36a-39fa-4999-a258-8a9bbe2d018d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3c015a3-0d23-4a0d-9c50-89fd0a7117a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e006fb01-f8da-4725-8dde-289213d2fe9d");

            migrationBuilder.DropColumn(
                name: "DalyRequests",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "MonthlyRequests",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "WeeklyRequests",
                table: "AspNetRoles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "53dd6905-b863-4e78-8c3f-d96ec469ec7f", "a6963fcd-2cef-43af-aefb-0ad6e66c8fd8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c724b7c-5f22-4ac9-b15c-5b3f56f7c27c", "80664b32-eee4-41a6-bb87-6dbe248e8304", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_QueryRecords_AspNetUsers_UserId",
                table: "QueryRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
