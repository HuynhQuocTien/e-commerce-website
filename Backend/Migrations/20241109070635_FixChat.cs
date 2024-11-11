using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_website.Migrations
{
    /// <inheritdoc />
    public partial class FixChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4557893f-1f56-4b6f-bb3b-caefd62c8c49"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cc750292-d405-4564-b348-33e2c5c048d0", "AQAAAAIAAYagAAAAEPS/eYTYfr9Wua7pk17lFECrLrIZUPuSq9dV0U9SN3AuPgYPrHYCNOBLO5CVPuTuIA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4557893f-1f56-4b6f-bb3b-caefd62c8c49"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3f8b8923-cb45-438d-9081-f83d4c21138b", "AQAAAAIAAYagAAAAENoDzuNTx+FBAel7PiIrxXGMj47Y4ZbbbCc8m/i/4+mihRkw7ppLmSMGO4tzWhD8iw==" });
        }
    }
}
