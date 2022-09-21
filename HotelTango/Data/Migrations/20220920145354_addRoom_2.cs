using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelTango.Data.Migrations
{
    public partial class addRoom_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomType_RoomTypeID",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "RoomTypeID",
                table: "Room",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomType_RoomTypeID",
                table: "Room",
                column: "RoomTypeID",
                principalTable: "RoomType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomType_RoomTypeID",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "RoomTypeID",
                table: "Room",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomType_RoomTypeID",
                table: "Room",
                column: "RoomTypeID",
                principalTable: "RoomType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
