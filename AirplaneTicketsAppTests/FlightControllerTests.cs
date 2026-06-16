using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirplaneTicketsApp.Controllers;
using AirplaneTicketsApp.Data;
using AirplaneTicketsApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AirplaneTicketsAppTests
{
    public class FlightControllerTests
    {
        private AirplaneTicketsContext context;
        private FlightController controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AirplaneTicketsContext>()
                .UseInMemoryDatabase(databaseName: "AirplaneTicketsAppDb")
                .Options;

            context = new AirplaneTicketsContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Airports.AddRange(
            new Airport
            {
                  AirportId = 1,
                  Name = "Sofia Airport",
                  City = "Sofia",
                  Country = "Bulgaria",
                  IATACode = "SOF",
                  Capacity = 7000
            },
            new Airport
            {
                  AirportId = 2,
                  Name = "Heathrow",
                  City = "London",
                  Country = "UK",
                  IATACode = "LHR",
                  Capacity = 10000
            });

            context.SaveChanges();

            context.Flights.Add(
                new Flight
                {
                    FlightId = 1,
                    FlightNumber = "FB101",
                    DepartureTime = DateTime.Now,
                    ArrivalTime = DateTime.Now.AddHours(2),
                    Price = 150,
                    DepartureAirportId = 1,
                    ArrivalAirportId = 2
                });

            context.SaveChanges();

            controller = new FlightController(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public void GetAll_ReturnsFlights()
        {
            var result = controller.GetAll();

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetById_ValidId_ReturnsFlight()
        {
            var result = controller.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("FB101", result.FlightNumber);
        }

        [Test]
        public void GetById_InvalidId_ReturnsNull()
        {
            var result = controller.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public void Create_AddsFlight()
        {
            var flight = new Flight
            {
                FlightId = 2,
                FlightNumber = "FB320",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(4),
                Price = 390,
                DepartureAirportId = 2,
                ArrivalAirportId = 1
            };

            controller.Create(flight);

            Assert.AreEqual(2, context.Flights.Count());
        }

        [Test]
        public void Update_UpdatesFlight()
        {
            var flight = new Flight
            {
                FlightId = 1,
                FlightNumber = "FB101",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(2),
                Price = 250,
                DepartureAirportId = 1,
                ArrivalAirportId = 2
            };

            controller.Update(flight);

            var result = context.Flights.Find(1);

            Assert.AreEqual(250, result.Price);
        }

        [Test]
        public void Delete_RemovesFlight()
        {
            controller.Delete(1);

            Assert.AreEqual(0, context.Flights.Count());
        }
    }
}
