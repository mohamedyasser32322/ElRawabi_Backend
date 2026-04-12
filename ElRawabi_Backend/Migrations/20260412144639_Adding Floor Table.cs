using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElRawabi_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddingFloorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Buildings_BuildingId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "FloorNumber",
                table: "Apartments",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "BuildingId",
                table: "Apartments",
                newName: "FloorId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_BuildingId",
                table: "Apartments",
                newName: "IX_Apartments_FloorId");

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BuildingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Floors_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Floors_BuildingId",
                table: "Floors",
                column: "BuildingId");

            // ✅ الجزء الجديد: حل مشكلة التعارض (Conflict)
            // 1. إنشاء دور افتراضي (رقم 0) لكل عمارة موجودة حالياً
            migrationBuilder.Sql("INSERT INTO Floors (FloorNumber, CreatedAt, LastUpdatedAt, IsDeleted, BuildingId) SELECT 0, GETUTCDATE(), GETUTCDATE(), 0, Id FROM Buildings");

            // 2. تحديث الشقق لترتبط بالدور الجديد بدلاً من الـ BuildingId القديم
            // ملاحظة: بما أننا قمنا بعمل Rename للعمود من BuildingId إلى FloorId، سنقوم بتحديث قيمته
            migrationBuilder.Sql("UPDATE Apartments SET FloorId = (SELECT TOP 1 Id FROM Floors WHERE Floors.BuildingId = Apartments.FloorId)");

            // 3. تحويل حالة الشقق القديمة إلى "متاح" (رقم 1) لأننا قمنا بعمل Rename لعمود FloorNumber إلى Status
            migrationBuilder.Sql("UPDATE Apartments SET Status = 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Floors_FloorId",
                table: "Apartments",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Floors_FloorId",
                table: "Apartments");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Apartments",
                newName: "FloorNumber");

            migrationBuilder.RenameColumn(
                name: "FloorId",
                table: "Apartments",
                newName: "BuildingId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_FloorId",
                table: "Apartments",
                newName: "IX_Apartments_BuildingId");

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Apartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Buildings_BuildingId",
                table: "Apartments",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
