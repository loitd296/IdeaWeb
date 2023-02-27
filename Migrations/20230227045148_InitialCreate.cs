using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeaWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Deleted_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CloseDateAcedamic",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CloseDatePostIdea = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloseDateAcedamic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    gmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    departmentIdid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Departments_departmentIdid",
                        column: x => x.departmentIdid,
                        principalTable: "Departments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Ideas",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    like_Count = table.Column<int>(type: "int", nullable: true),
                    dislike_Count = table.Column<int>(type: "int", nullable: true),
                    date_Upload = table.Column<DateTime>(type: "datetime2", nullable: true),
                    categoryIdId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    userIdid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CloseDateAcedamicId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ideas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ideas_Category_categoryIdId",
                        column: x => x.categoryIdId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ideas_CloseDateAcedamic_CloseDateAcedamicId",
                        column: x => x.CloseDateAcedamicId,
                        principalTable: "CloseDateAcedamic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ideas_User_userIdid",
                        column: x => x.userIdid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userIdid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    roleIdid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_roleIdid",
                        column: x => x.roleIdid,
                        principalTable: "Roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UserRoles_User_userIdid",
                        column: x => x.userIdid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date_Upload = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ideasIdid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    userIdid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Ideas_ideasIdid",
                        column: x => x.ideasIdid,
                        principalTable: "Ideas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Comment_User_userIdid",
                        column: x => x.userIdid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date_Upload = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ideasIdid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Ideas_ideasIdid",
                        column: x => x.ideasIdid,
                        principalTable: "Ideas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDidea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dislike = table.Column<int>(type: "int", nullable: false),
                    like = table.Column<int>(type: "int", nullable: false),
                    ideasIdid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    userIdid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_Ideas_ideasIdid",
                        column: x => x.ideasIdid,
                        principalTable: "Ideas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Rating_User_userIdid",
                        column: x => x.userIdid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ideasIdid",
                table: "Comment",
                column: "ideasIdid");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_userIdid",
                table: "Comment",
                column: "userIdid");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ideasIdid",
                table: "Documents",
                column: "ideasIdid");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_categoryIdId",
                table: "Ideas",
                column: "categoryIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_CloseDateAcedamicId",
                table: "Ideas",
                column: "CloseDateAcedamicId");

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_userIdid",
                table: "Ideas",
                column: "userIdid");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ideasIdid",
                table: "Rating",
                column: "ideasIdid");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_userIdid",
                table: "Rating",
                column: "userIdid");

            migrationBuilder.CreateIndex(
                name: "IX_User_departmentIdid",
                table: "User",
                column: "departmentIdid");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_roleIdid",
                table: "UserRoles",
                column: "roleIdid");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_userIdid",
                table: "UserRoles",
                column: "userIdid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Ideas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "CloseDateAcedamic");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
