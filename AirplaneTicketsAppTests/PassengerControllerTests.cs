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
    public class PassengerControllerTests
    {
        private AirplaneTicketsContext context;
        private PassengerController controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AirplaneTicketsContext>()
                .UseInMemoryDatabase(databaseName:"AirplaneTicketsAppDb")
                .Options;

            context = new AirplaneTicketsContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Passengers.Add(
                new Passenger
                {
                    PassengerId = 1,
                    FirstName = "Sofia",
                    LastName = "Ivanova",
                    PassportNumber = "AB32416",
                    Email = "snm@abv.bg",
                    Nationality = "Bulgarian"
                });

            context.SaveChanges();

            controller = new PassengerController(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public void GetAll_ReturnsPassengers()
        {
            var result = controller.GetAll();

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetById_ValidId_ReturnsPassenger()
        {
            var result = controller.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("AB32416", result.PassportNumber);
        }

        [Test]
        public void GetById_InvalidId_ReturnsNull()
        {
            var result = controller.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public void Create_AddsPassenger()
        {
            var passenger = new Passenger
            {
                PassengerId = 2,
                FirstName = "Adam",
                LastName = "Reed",
                PassportNumber = "CB32416",
                Email = "arbl@abv.bg",
                Nationality = "Englishman"
            };

            controller.Create(passenger);

            Assert.AreEqual(2, context.Passengers.Count());
        }

        [Test]
        public void Update_UpdatesPassenger()
        {
            var passenger = new Passenger
            {
                PassengerId = 1,
                FirstName = "Sofia",
                LastName = "Ivanova-Vasileva",
                PassportNumber = "AB32416",
                Email = "sof@abv.bg",
                Nationality = "Bulgarian"
            };

            controller.Update(passenger);

            var result = context.Passengers.Find(1);

            Assert.AreEqual("sof@abv.bg", result.Email);
        }

        [Test]
        public void Delete_RemovesPassenger()
        {
            controller.Delete(1);

            Assert.AreEqual(0, context.Passengers.Count());
        }
    }
}
