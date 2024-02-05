using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChatAppDatabaseLibraryy.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersTbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTbs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionsTb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromId = table.Column<int>(type: "int", nullable: false),
                    ToId = table.Column<int>(type: "int", nullable: false),
                    FromConnectedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToConnectedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SofDeleteFrom = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SoftDeleteTo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionsTb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectionsTb_UsersTbs_FromId",
                        column: x => x.FromId,
                        principalTable: "UsersTbs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConnectionsTb_UsersTbs_ToId",
                        column: x => x.ToId,
                        principalTable: "UsersTbs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupAndUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAndUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupAndUser_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupAndUser_UsersTbs_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersTbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    ToId = table.Column<int>(type: "int", nullable: false),
                    FromId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_UsersTbs_FromId",
                        column: x => x.FromId,
                        principalTable: "UsersTbs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_UsersTbs_ToId",
                        column: x => x.ToId,
                        principalTable: "UsersTbs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Status_UsersTbs_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersTbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UsersTbs",
                columns: new[] { "Id", "Bio", "Gmail", "ImagePath", "Password" },
                values: new object[,]
                {
                    { 1, "I am John", "John", "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png", "1234" },
                    { 2, "I am BlackBoy", "BlackBoy", "\\Images\\3135715.png", "1234" },
                    { 3, "I am Cavanshir", "Cavanshir", "\\Images\\download (1).jpeg", "1234" },
                    { 4, "I am Qudret", "Qudret", "\\Images\\photo-little-brunet-boy-wear-260nw-2030792027.webp", "1234" },
                    { 5, "I am Michail", "Michail", "\\Images\\3135715.png", "1234" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Title", "UserId", "VideoPath" },
                values: new object[,]
                {
                    { 1, null, 2, "\\Database\\Statues\\video1.mp4" },
                    { 2, null, 2, "\\Database\\Statues\\video2.mp4" },
                    { 3, null, 2, "\\Database\\Statues\\video3.mp4" },
                    { 4, null, 2, "\\Database\\Statues\\video4.mp4" },
                    { 5, null, 2, "\\Database\\Statues\\video5.mp4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionsTb_FromId",
                table: "ConnectionsTb",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionsTb_ToId",
                table: "ConnectionsTb",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAndUser_GroupId",
                table: "GroupAndUser",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAndUser_UserId",
                table: "GroupAndUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_FromId",
                table: "Message",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ToId",
                table: "Message",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_UserId",
                table: "Status",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTbs_Gmail",
                table: "UsersTbs",
                column: "Gmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionsTb");

            migrationBuilder.DropTable(
                name: "GroupAndUser");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "UsersTbs");
        }
    }
}
