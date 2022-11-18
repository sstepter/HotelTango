using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelTango.Data.Migrations
{
    public partial class addReservation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomTypeID",
                table: "Reservation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_RoomTypeID",
                table: "Reservation",
                column: "RoomTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_RoomType_RoomTypeID",
                table: "Reservation",
                column: "RoomTypeID",
                principalTable: "RoomType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_RoomType_RoomTypeID",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_RoomTypeID",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "RoomTypeID",
                table: "Reservation");
        }
    }
}
