using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTestTask.Migrations
{
    /// <inheritdoc />
    public partial class RenamedTimestampToCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "JournalItem",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "JournalItem",
                newName: "Timestamp");
        }
    }
}
