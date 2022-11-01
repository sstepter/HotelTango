using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelTango.Models
{
    public class Room
    {
        [Key]
        public int RoomNumber { get; set; }
        public string RoomTypeName { get; set; }
        public string BedType { get; set; }
        public int NumberOfBeds { get; set; }
        public int RoomRate { get; set; }
    }
}
