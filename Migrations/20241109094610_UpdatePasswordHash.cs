using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_website.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4557893f-1f56-4b6f-bb3b-caefd62c8c49"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "abc18d67-70fe-4050-9a38-261a3579015c", "AQAAAAIAAYagAAAAEFxrS0eF13pXYRqecvfwaNKGuNKn1euDQ5KLSP+iXoktQ8B6WGLMMUi48+zBIqGaSg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4557893f-1f56-4b6f-bb3b-caefd62c8c49"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cc750292-d405-4564-b348-33e2c5c048d0", "AQAAAAIAAYagAAAAEPS/eYTYfr9Wua7pk17lFECrLrIZUPuSq9dV0U9SN3AuPgYPrHYCNOBLO5CVPuTuIA==" });
        }
    }
}
