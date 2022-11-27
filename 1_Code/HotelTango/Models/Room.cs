using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelTango.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        [ForeignKey("RoomTypeID")]
        public virtual int RoomTypeID { get; set; }
        public virtual RoomType RoomType { get; set; }
    }
}
