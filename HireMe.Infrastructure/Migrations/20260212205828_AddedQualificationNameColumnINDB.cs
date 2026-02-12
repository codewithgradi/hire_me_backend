using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HireMe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedQualificationNameColumnINDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QualificationName",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "342e5217-5403-4877-bfaa-4a1d3ae52ea7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da401b7e4",
                column: "ConcurrencyStamp",
                value: "f2d1da63-5de2-4919-a054-167c66be0c10");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QualificationName",
                table: "UserProfiles");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "583c5072-0e61-435a-b639-a051e4f189b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da401b7e4",
                column: "ConcurrencyStamp",
                value: "f5c3dda3-7ff2-4dbd-81e7-bb42b6d97b28");
        }
    }
}
