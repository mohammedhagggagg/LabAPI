using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Day01.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1ldkfaa58702m",
                column: "ConcurrencyStamp",
                value: "90f289ec-ebdf-4131-b3db-d787eaa93e19");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1ldkfaa58702m",
                column: "ConcurrencyStamp",
                value: "3a253e5e-ba0e-4dd3-a66c-421a6673b325");
        }
    }
}
