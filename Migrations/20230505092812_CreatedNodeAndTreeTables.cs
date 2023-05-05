using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTestTask.Migrations
{
    /// <inheritdoc />
    public partial class CreatedNodeAndTreeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tree",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tree", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Node",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreeID = table.Column<int>(type: "int", nullable: false),
                    ParentNodeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Node", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Node_Node_ParentNodeID",
                        column: x => x.ParentNodeID,
                        principalTable: "Node",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Node_Tree_TreeID",
                        column: x => x.TreeID,
                        principalTable: "Tree",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Node_ParentNodeID",
                table: "Node",
                column: "ParentNodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Node_TreeID",
                table: "Node",
                column: "TreeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Node");

            migrationBuilder.DropTable(
                name: "Tree");
        }
    }
}
