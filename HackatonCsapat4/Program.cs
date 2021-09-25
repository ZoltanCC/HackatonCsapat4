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
            routeCalculator.SetDestination(0, 31);
            routeCalculator.SetFuelAndBudget(100, 150);
            routeCalculator.Plan();
            Console.WriteLine(routeCalculator.FastestRoute);
            Console.WriteLine(routeCalculator.MostVisitedEvents);

            Console.ReadLine();
        }
    }
}
