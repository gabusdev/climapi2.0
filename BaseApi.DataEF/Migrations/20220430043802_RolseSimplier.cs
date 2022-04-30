using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Climapi.DataEF.Migrations
{
    public partial class RolseSimplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DalyRequests", "MonthlyRequests", "Name", "NormalizedName", "WeeklyRequests" },
                values: new object[] { "10029d49-b521-4457-8f5e-50067a32a7da", "fedb4101-4511-46af-b936-d63f860fbd95", 20, 300, "User", "USER", 100 });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DalyRequests", "MonthlyRequests", "Name", "NormalizedName", "WeeklyRequests" },
                values: new object[] { "7cae2b84-b159-4be9-a284-1350506afa56", "662a0c5d-4263-43bc-8498-003cca92f25f", 40, 600, "UserPro", "USERPRO", 200 });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DalyRequests", "MonthlyRequests", "Name", "NormalizedName", "WeeklyRequests" },
                values: new object[] { "b4fbe100-ea25-4e07-a484-d97d6fd3bb53", "8175932e-a2ca-4849-ab29-9cc815e3cac5", 2147483647, 2147483647, "Admin", "ADMIN", 2147483647 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10029d49-b521-4457-8f5e-50067a32a7da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cae2b84-b159-4be9-a284-1350506afa56");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4fbe100-ea25-4e07-a484-d97d6fd3bb53");

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
        }
    }
}
