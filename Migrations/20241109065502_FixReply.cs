using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_website.Migrations
{
    /// <inheritdoc />
    public partial class FixReply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chats_AspNetUsers_senderId",
                table: "chats");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4557893f-1f56-4b6f-bb3b-caefd62c8c49"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3f8b8923-cb45-438d-9081-f83d4c21138b", "AQAAAAIAAYagAAAAENoDzuNTx+FBAel7PiIrxXGMj47Y4ZbbbCc8m/i/4+mihRkw7ppLmSMGO4tzWhD8iw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_chats_AspNetUsers_senderId",
                table: "chats",
                column: "senderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chats_AspNetUsers_senderId",
                table: "chats");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4557893f-1f56-4b6f-bb3b-caefd62c8c49"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4ba3ec6d-eed2-4968-91d9-fe8d768879b9", "AQAAAAIAAYagAAAAECnsILXEf1M07t11jTJZZM7GCukfEpIt/yrBB63sAhn/vWYIWOpX0in6Jj6gbG8Daw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_chats_AspNetUsers_senderId",
                table: "chats",
                column: "senderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
