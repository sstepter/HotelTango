using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HotelTango.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CustomerID")]
        public virtual int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [ForeignKey("RoomID")]
        public virtual int RoomID { get; set; }
        public virtual Room Room { get; set; }
        public string WIFI_Passcode {get; private set; }
        public DateTime StartDate  { get; set; }
        public DateTime EndDate { get; set; }

    }        
}
