using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Buurtapp.Migrations
{
    public partial class ReportTypesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Post_Post",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_User_Reporter",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_Post",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Post",
                table: "Reports");

            migrationBuilder.RenameTable(
                name: "Reports",
                newName: "Report");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_Reporter",
                table: "Report",
                newName: "IX_Report_Reporter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Report",
                table: "Report",
                column: "ReportId");

            migrationBuilder.CreateTable(
                name: "ReportedComment",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportedComment", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_ReportedComment_Comment_Comment",
                        column: x => x.Comment,
                        principalTable: "Comment",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportedComment_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportedPost",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Post = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportedPost", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_ReportedPost_Post_Post",
                        column: x => x.Post,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportedPost_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportedSolution",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Solution = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportedSolution", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_ReportedSolution_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportedSolution_Solution_Solution",
                        column: x => x.Solution,
                        principalTable: "Solution",
                        principalColumn: "SolutionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportedComment_Comment",
                table: "ReportedComment",
                column: "Comment");

            migrationBuilder.CreateIndex(
                name: "IX_ReportedPost_Post",
                table: "ReportedPost",
                column: "Post");

            migrationBuilder.CreateIndex(
                name: "IX_ReportedSolution_Solution",
                table: "ReportedSolution",
                column: "Solution");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_User_Reporter",
                table: "Report",
                column: "Reporter",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_User_Reporter",
                table: "Report");

            migrationBuilder.DropTable(
                name: "ReportedComment");

            migrationBuilder.DropTable(
                name: "ReportedPost");

            migrationBuilder.DropTable(
                name: "ReportedSolution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Report",
                table: "Report");

            migrationBuilder.RenameTable(
                name: "Report",
                newName: "Reports");

            migrationBuilder.RenameIndex(
                name: "IX_Report_Reporter",
                table: "Reports",
                newName: "IX_Reports_Reporter");

            migrationBuilder.AddColumn<int>(
                name: "Post",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Post",
                table: "Reports",
                column: "Post");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Post_Post",
                table: "Reports",
                column: "Post",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_User_Reporter",
                table: "Reports",
                column: "Reporter",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
