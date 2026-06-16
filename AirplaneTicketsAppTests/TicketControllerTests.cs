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
    public class TicketControllerTests
    {
        private AirplaneTicketsContext context;
        private TicketController controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AirplaneTicketsContext>()
                .UseInMemoryDatabase(databaseName:"AirplaneTicketsAppDb")
                .Options;

            context = new AirplaneTicketsContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            context.Tickets.Add(
            new Ticket
            {
                   TicketId = 1,
                   SeatNumber = "12A",
                   PurchaseDate = DateTime.Now,
                   TicketClass = "Economy",
                   PassengerId = 1,
                   FlightId = 1
            });

            context.SaveChanges();

            context.SaveChanges();

            controller = new TicketController(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public void GetAll_ReturnsTickets()
        {
            var result = controller.GetAll();

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetById_ValidId_ReturnsTicket()
        {
            var result = controller.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("12A", result.SeatNumber);
        }

        [Test]
        public void GetById_InvalidId_ReturnsNull()
        {
            var result = controller.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public void Create_AddsTicket()
        {
            var ticket = new Ticket
            {
                TicketId = 2,
                SeatNumber = "32A",
                PurchaseDate = DateTime.Now,
                TicketClass = "Bussiness class",
                PassengerId = 1,
                FlightId = 1
            };

            controller.Create(ticket);

            Assert.AreEqual(2, context.Tickets.Count());
        }

        [Test]
        public void Update_UpdatesTicket()
        {
            var ticket = new Ticket
            {
                TicketId = 1,
                SeatNumber = "33B",
                PurchaseDate = DateTime.Now,
                TicketClass = "Economy",
                PassengerId = 1,
                FlightId = 1
            };

            controller.Update(ticket);

            var result = context.Tickets.Find(1);

            Assert.AreEqual("33B", result.SeatNumber);
        }

        [Test]
        public void Delete_RemovesTicket()
        {
            controller.Delete(1);

            Assert.AreEqual(0, context.Airports.Count());
        }
    }
}
