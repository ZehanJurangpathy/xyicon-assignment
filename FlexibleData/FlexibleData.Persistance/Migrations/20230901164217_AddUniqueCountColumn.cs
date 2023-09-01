using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexibleData.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueCountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqueCount",
                table: "Statistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueCount",
                table: "Statistics");
        }
    }
}
