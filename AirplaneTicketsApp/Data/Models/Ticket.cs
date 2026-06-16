using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirplaneTicketsApp.Data.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        [Required]
        [MaxLength(5)]
        public string SeatNumber { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string TicketClass { get; set; }

        [Required]
        [ForeignKey(nameof(Passenger))]
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        [Required]
        [ForeignKey(nameof(Flight))]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}

