using Microsoft.EntityFrameworkCore.Migrations;

namespace PostOffice.DAL.Migrations.Migrations
{
    public partial class BagType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BagType",
                table: "Bag",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BagType",
                table: "Bag");
        }
    }
}
