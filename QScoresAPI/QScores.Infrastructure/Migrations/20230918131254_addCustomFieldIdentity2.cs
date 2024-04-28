using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QScores.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCustomFieldIdentity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "AspNetUsers",
                newName: "GroupID");

            migrationBuilder.AddColumn<int>(
                name: "RecID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "GroupID",
                table: "AspNetUsers",
                newName: "GroupId");
        }
    }
}
