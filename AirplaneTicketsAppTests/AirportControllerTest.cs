using AirplaneTicketsApp.Controllers;
using AirplaneTicketsApp.Data;
using AirplaneTicketsApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AirplaneTicketsAppTests
{
    public class AirportControllerTest
    {
        private AirplaneTicketsContext context;
        private AirportController controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AirplaneTicketsContext>()
                .UseInMemoryDatabase(databaseName:"AirplaneTicketsAppDb")
                .Options;

            context = new AirplaneTicketsContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Airports.Add(
                new Airport
                {
                    AirportId = 1,
                    Name = "Sofia Airport",
                    City = "Sofia",
                    Country = "Bulgaria",
                    IATACode = "SOF",
                    Capacity = 7000
                });

            context.SaveChanges();

            controller = new AirportController(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public void GetAll_ReturnsAirports()
        {
            var result = controller.GetAll();

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetById_ValidId_ReturnsAirport()
        {
            var result = controller.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("SOF", result.IATACode);
        }

        [Test]
        public void GetById_InvalidId_ReturnsNull()
        {
            var result = controller.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public void Create_AddsAirport()
        {
            var airport = new Airport
            {
                AirportId = 2,
                Name = "Heathrow",
                City = "London",
                Country = "UK",
                IATACode = "LHR",
                Capacity = 10000
            };

            controller.Create(airport);

            Assert.AreEqual(2, context.Airports.Count());
        }

        [Test]
        public void Update_UpdatesAirport()
        {
            var airport = new Airport
            {
                AirportId = 1,
                Name = "Updated Airport",
                City = "Sofia",
                Country = "Bulgaria",
                IATACode = "SOF",
                Capacity = 9000
            };

            controller.Update(airport);

            var result = context.Airports.Find(1);

            Assert.AreEqual("Updated Airport", result.Name);
        }

        [Test]
        public void Delete_RemovesAirport()
        {
            controller.Delete(1);

            Assert.AreEqual(0, context.Airports.Count());
        }
    }
}