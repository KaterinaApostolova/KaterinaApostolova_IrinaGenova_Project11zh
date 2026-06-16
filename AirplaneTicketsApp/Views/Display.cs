using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirplaneTicketsApp.Controllers;
using AirplaneTicketsApp.Data;

namespace AirplaneTicketsApp.Views
{
        public class Display
        {
            private AirplaneTicketsContext _context;
            private AirportController _airportController;
            private FlightController _flightController;
            private PassengerController _passengerController;
            private TicketController _ticketController;

            private AirportsView _airportsView;
            private FlightsView _flightsView;
            private PassengersView _passengersView;
            private TicketsView _ticketsView;

            public Display()
            {
                _context = new AirplaneTicketsContext();
                _airportController = new AirportController(_context);
                _flightController = new FlightController(_context);
                _passengerController = new PassengerController(_context);
                _ticketController = new TicketController(_context);

                _airportsView = new AirportsView(_airportController);
                _flightsView = new FlightsView(_flightController, _airportController);
                _passengersView = new PassengersView(_passengerController);
                _ticketsView = new TicketsView(_ticketController, _flightController, _passengerController);

                MainMenu();
            }

            private void MainMenu()
            {
                while (true)
                {
                    Console.WriteLine("\n=============================");
                    Console.WriteLine("     AIRPLANE TICKETS APP    ");
                    Console.WriteLine("=============================");
                    Console.WriteLine("1. Airports");
                    Console.WriteLine("2. Flights");
                    Console.WriteLine("3. Passengers");
                    Console.WriteLine("4. Tickets");
                    Console.WriteLine("0. Exit");
                    Console.Write("Choice: ");

                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Invalid input.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1: _airportsView.Start(); break;
                        case 2: _flightsView.Start(); break;
                        case 3: _passengersView.Start(); break;
                        case 4: _ticketsView.Start(); break;
                        case 0: return;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                }
            }
        }
}

