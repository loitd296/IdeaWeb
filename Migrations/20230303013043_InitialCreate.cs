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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CloseDatePostIdea = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloseDateAcedamic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flag = table.Column<int>(type: "int", nullable: false),
                    departmentIdid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Department_departmentIdid",
                        column: x => x.departmentIdid,
                        principalTable: "Department",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Idea",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    like_Count = table.Column<int>(type: "int", nullable: true),
                    dislike_Count = table.Column<int>(type: "int", nullable: true),
                    date_Upload = table.Column<DateTime>(type: "datetime2", nullable: true),
                    categoryIdId = table.Column<int>(type: "int", nullable: true),
                    userIdid = table.Column<int>(type: "int", nullable: true),
                    CloseDateAcedamicId = table.Column<int>(type: "int", nullable: true)
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
                name: "UserRole",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userIdid = table.Column<int>(type: "int", nullable: true),
                    roleIdid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_roleIdid",
                        column: x => x.roleIdid,
                        principalTable: "Role",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UserRole_User_userIdid",
                        column: x => x.userIdid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_Upload = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ideaIdid = table.Column<int>(type: "int", nullable: true),
                    userIdid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Idea_ideaIdid",
                        column: x => x.ideaIdid,
                        principalTable: "Idea",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Comment_User_userIdid",
                        column: x => x.userIdid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_Upload = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ideaIdid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_Idea_ideaIdid",
                        column: x => x.ideaIdid,
                        principalTable: "Idea",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDidea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dislike = table.Column<int>(type: "int", nullable: false),
                    like = table.Column<int>(type: "int", nullable: false),
                    ideaIdid = table.Column<int>(type: "int", nullable: true),
                    userIdid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_Idea_ideaIdid",
                        column: x => x.ideaIdid,
                        principalTable: "Idea",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Rating_User_userIdid",
                        column: x => x.userIdid,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ideaIdid",
                table: "Comment",
                column: "ideaIdid");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_userIdid",
                table: "Comment",
                column: "userIdid");

            migrationBuilder.CreateIndex(
                name: "IX_Document_ideaIdid",
                table: "Document",
                column: "ideaIdid");

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

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ideaIdid",
                table: "Rating",
                column: "ideaIdid");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_userIdid",
                table: "Rating",
                column: "userIdid");

            migrationBuilder.CreateIndex(
                name: "IX_User_departmentIdid",
                table: "User",
                column: "departmentIdid");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_roleIdid",
                table: "UserRole",
                column: "roleIdid");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_userIdid",
                table: "UserRole",
                column: "userIdid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Idea");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "CloseDateAcedamic");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
