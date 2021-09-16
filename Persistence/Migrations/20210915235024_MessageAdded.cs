using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class MessageAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "id",
                keyValue: new Guid("514ffdf6-dafa-416d-8adf-e5d81989408a"));

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "id",
                keyValue: new Guid("b2f24e07-e9d5-4b84-929d-be3a952ac0c0"));

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "id",
                keyValue: new Guid("d3b8e593-cfb1-4718-a5b7-268a223a5ba4"));

            migrationBuilder.AddColumn<int>(
                name: "ChannelType",
                table: "Channels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SenderId = table.Column<string>(type: "TEXT", nullable: true),
                    ChannelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MessageType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "id", "ChannelType", "description", "name" },
                values: new object[] { new Guid("4d002bc1-bbd6-41a2-ba82-a1714ba790bc"), 0, "Canal dedicado a dotnet core", "DotnetCore" });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "id", "ChannelType", "description", "name" },
                values: new object[] { new Guid("e6c7449b-4531-473c-86df-06015218db96"), 0, "Canal dedicado a Angular", "Angular" });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "id", "ChannelType", "description", "name" },
                values: new object[] { new Guid("8782d30c-ec7b-4719-b36a-11f081aea832"), 0, "Canal dedicado a ReactJs", "ReactJs" });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChannelId",
                table: "Messages",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "id",
                keyValue: new Guid("4d002bc1-bbd6-41a2-ba82-a1714ba790bc"));

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "id",
                keyValue: new Guid("8782d30c-ec7b-4719-b36a-11f081aea832"));

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "id",
                keyValue: new Guid("e6c7449b-4531-473c-86df-06015218db96"));

            migrationBuilder.DropColumn(
                name: "ChannelType",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "id", "description", "name" },
                values: new object[] { new Guid("514ffdf6-dafa-416d-8adf-e5d81989408a"), "Canal dedicado a dotnet core", "DotnetCore" });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "id", "description", "name" },
                values: new object[] { new Guid("d3b8e593-cfb1-4718-a5b7-268a223a5ba4"), "Canal dedicado a Angular", "Angular" });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "id", "description", "name" },
                values: new object[] { new Guid("b2f24e07-e9d5-4b84-929d-be3a952ac0c0"), "Canal dedicado a ReactJs", "ReactJs" });
        }
    }
}
