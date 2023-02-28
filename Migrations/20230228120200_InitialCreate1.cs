using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeaWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Ideas_ideasIdid",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Ideas_ideasIdid",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Ideas_ideasIdid",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Departments_departmentIdid",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_roleIdid",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_User_userIdid",
                table: "UserRoles");

            migrationBuilder.DropTable(
                name: "Ideas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRole");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Document");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "ideasIdid",
                table: "Rating",
                newName: "ideaIdid");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_ideasIdid",
                table: "Rating",
                newName: "IX_Rating_ideaIdid");

            migrationBuilder.RenameColumn(
                name: "ideasIdid",
                table: "Comment",
                newName: "ideaIdid");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ideasIdid",
                table: "Comment",
                newName: "IX_Comment_ideaIdid");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_userIdid",
                table: "UserRole",
                newName: "IX_UserRole_userIdid");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_roleIdid",
                table: "UserRole",
                newName: "IX_UserRole_roleIdid");

            migrationBuilder.RenameColumn(
                name: "ideasIdid",
                table: "Document",
                newName: "ideaIdid");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_ideasIdid",
                table: "Document",
                newName: "IX_Document_ideaIdid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Idea",
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
                    table.PrimaryKey("PK_Idea", x => x.id);
                    table.ForeignKey(
                        name: "FK_Idea_Category_categoryIdId",
                        column: x => x.categoryIdId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Idea_CloseDateAcedamic_CloseDateAcedamicId",
                        column: x => x.CloseDateAcedamicId,
                        principalTable: "CloseDateAcedamic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Idea_User_userIdid",
                        column: x => x.userIdid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Idea_categoryIdId",
                table: "Idea",
                column: "categoryIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Idea_CloseDateAcedamicId",
                table: "Idea",
                column: "CloseDateAcedamicId");

            migrationBuilder.CreateIndex(
                name: "IX_Idea_userIdid",
                table: "Idea",
                column: "userIdid");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Idea_ideaIdid",
                table: "Comment",
                column: "ideaIdid",
                principalTable: "Idea",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Idea_ideaIdid",
                table: "Document",
                column: "ideaIdid",
                principalTable: "Idea",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Idea_ideaIdid",
                table: "Rating",
                column: "ideaIdid",
                principalTable: "Idea",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Department_departmentIdid",
                table: "User",
                column: "departmentIdid",
                principalTable: "Department",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_roleIdid",
                table: "UserRole",
                column: "roleIdid",
                principalTable: "Role",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_userIdid",
                table: "UserRole",
                column: "userIdid",
                principalTable: "User",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Idea_ideaIdid",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Idea_ideaIdid",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Idea_ideaIdid",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Department_departmentIdid",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_roleIdid",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_userIdid",
                table: "UserRole");

            migrationBuilder.DropTable(
                name: "Idea");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "Documents");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Departments");

            migrationBuilder.RenameColumn(
                name: "ideaIdid",
                table: "Rating",
                newName: "ideasIdid");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_ideaIdid",
                table: "Rating",
                newName: "IX_Rating_ideasIdid");

            migrationBuilder.RenameColumn(
                name: "ideaIdid",
                table: "Comment",
                newName: "ideasIdid");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ideaIdid",
                table: "Comment",
                newName: "IX_Comment_ideasIdid");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_userIdid",
                table: "UserRoles",
                newName: "IX_UserRoles_userIdid");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_roleIdid",
                table: "UserRoles",
                newName: "IX_UserRoles_roleIdid");

            migrationBuilder.RenameColumn(
                name: "ideaIdid",
                table: "Documents",
                newName: "ideasIdid");

            migrationBuilder.RenameIndex(
                name: "IX_Document_ideaIdid",
                table: "Documents",
                newName: "IX_Documents_ideasIdid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Ideas",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    categoryIdId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    userIdid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CloseDateAcedamicId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    date_Upload = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dislike_Count = table.Column<int>(type: "int", nullable: true),
                    like_Count = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Ideas_ideasIdid",
                table: "Comment",
                column: "ideasIdid",
                principalTable: "Ideas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Ideas_ideasIdid",
                table: "Documents",
                column: "ideasIdid",
                principalTable: "Ideas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Ideas_ideasIdid",
                table: "Rating",
                column: "ideasIdid",
                principalTable: "Ideas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Departments_departmentIdid",
                table: "User",
                column: "departmentIdid",
                principalTable: "Departments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_roleIdid",
                table: "UserRoles",
                column: "roleIdid",
                principalTable: "Roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_User_userIdid",
                table: "UserRoles",
                column: "userIdid",
                principalTable: "User",
                principalColumn: "id");
        }
    }
}
