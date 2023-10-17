using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lan_back.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Replies_ReplyId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ReplyId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReplyId",
                table: "Reports");

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "Replies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ReportId",
                table: "Replies",
                column: "ReportId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Reports_ReportId",
                table: "Replies",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Reports_ReportId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_ReportId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Replies");

            migrationBuilder.AddColumn<int>(
                name: "ReplyId",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReplyId",
                table: "Reports",
                column: "ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Replies_ReplyId",
                table: "Reports",
                column: "ReplyId",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
