using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_channelsDB",
                table: "channelsDB");

            migrationBuilder.RenameTable(
                name: "channelsDB",
                newName: "Channels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Channels",
                table: "Channels",
                column: "id");

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "id", "description", "name" },
                values: new object[] { new Guid("a4a0fa99-a258-4bae-aef4-b272d1af5808"), "Canal dedicado a dotnet core", "DotnetCore" });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "id", "description", "name" },
                values: new object[] { new Guid("67c39dc7-847a-4f13-8de1-cd5d7f29d9b7"), "Canal dedicado a Angular", "Angular" });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "id", "description", "name" },
                values: new object[] { new Guid("7b57afbf-9505-4367-b7b4-fc1edb2a303d"), "Canal dedicado a ReactJs", "ReactJs" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Channels",
                table: "Channels");

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "id",
                keyValue: new Guid("67c39dc7-847a-4f13-8de1-cd5d7f29d9b7"));

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "id",
                keyValue: new Guid("7b57afbf-9505-4367-b7b4-fc1edb2a303d"));

            migrationBuilder.DeleteData(
                table: "Channels",
                keyColumn: "id",
                keyValue: new Guid("a4a0fa99-a258-4bae-aef4-b272d1af5808"));

            migrationBuilder.RenameTable(
                name: "Channels",
                newName: "channelsDB");

            migrationBuilder.AddPrimaryKey(
                name: "PK_channelsDB",
                table: "channelsDB",
                column: "id");
        }
    }
}
