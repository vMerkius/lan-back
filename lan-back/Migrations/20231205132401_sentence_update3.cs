using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lan_back.Migrations
{
    /// <inheritdoc />
    public partial class sentence_update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Sentences",
                newName: "Translated");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Translated",
                table: "Sentences",
                newName: "Content");
        }
    }
}
