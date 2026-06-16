using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirplaneTicketsApp.Data.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [Required]
        [MaxLength(10)]
        public string FlightNumber { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey(nameof(DepartureAirport))]
        public int DepartureAirportId { get; set; }
        public Airport DepartureAirport { get; set; }

        [Required]
        [ForeignKey(nameof(ArrivalAirport))]
        public int ArrivalAirportId { get; set; }
        public Airport ArrivalAirport { get; set; }

        public List<Ticket> Tickets { get; set; }
            = new List<Ticket>();
    }
}

