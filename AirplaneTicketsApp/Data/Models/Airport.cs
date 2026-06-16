using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirplaneTicketsApp.Data.Models
{
    public class Airport
    {
        [Key]
        public int AirportId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        [MaxLength(5)]
        public string IATACode { get; set; }

        [Required]
        public int Capacity { get; set; }

        public List<Flight> DepartureFlights { get; set; }
            = new List<Flight>();

        public List<Flight> ArrivalFlights { get; set; }
            = new List<Flight>();
    }
}

