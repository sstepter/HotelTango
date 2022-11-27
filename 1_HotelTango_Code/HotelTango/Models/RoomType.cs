using System.ComponentModel.DataAnnotations;

namespace HotelTango.Models
{
    public class RoomType
    {
        [Key]
        public int Id { get; set; }
        public string RoomTypeName { get; set; }
        public string BedType { get; set; }
        public int NumberOfBeds { get; set; }
        public int RoomRate { get; set; }
    }
}
