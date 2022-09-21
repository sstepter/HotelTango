using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelTango.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int RoomTypeID { get; set; }
        [ForeignKey("RoomTypeID")]
        public virtual RoomType RoomType { get; set; }
    }
}
