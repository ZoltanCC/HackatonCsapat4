using HackatonCsapat4.Service;
using System;

namespace HackatonCsapat4
{
    class Program
    {
        /// <summary>
        /// Entry point of the application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Creates an instance of ReadSystem that processes a text file for data construction
            ReadSystem readSystem = new ReadSystem(args[0]);
            // Creates an instance of RoutePlanner\
            RoutePlanner routeCalculator = new RoutePlanner(readSystem.GetInputData); // Giving [ctor] parameter from readSystem processed data
            
                // Test case #1

            // Set destination / fuel and budget
            routeCalculator.SetDestination(0, 31);
            routeCalculator.SetFuelAndBudget(100, 150);
            // Initiates routeCalculator recursive algorythm to plan the route
            routeCalculator.Plan();
            // Prints to the console the got Route
            Console.WriteLine("\n" + routeCalculator.MostVisitedEvents);

            // Test case #2

            routeCalculator.SetFuelAndBudget(100, 250);
            routeCalculator.Plan();
            Console.WriteLine("\n" + routeCalculator.MostVisitedEvents);

                // Test case #3

            routeCalculator.SetFuelAndBudget(150, 250);
            routeCalculator.Plan();
            Console.WriteLine("\n" + routeCalculator.MostVisitedEvents);

                // Test case #4

            routeCalculator.SetFuelAndBudget(150, 300);
            routeCalculator.Plan();
            Console.WriteLine("\n" + routeCalculator.MostVisitedEvents);

            // (stops the console)
            Console.ReadLine();
        }
    }
}
