using System.Collections.Generic;
using System.Linq;

namespace HackatonCsapat4.Model
{
    /// <summary>
    /// Model of a space travel between Nodes
    /// </summary>
    public class RouteModel
    {
        /// <summary>
        /// Nodes that visited between start and destination
        /// </summary>
        public List<int> NodesVisited { get; set; }
        /// <summary>
        /// Total fuel consumed during the travel
        /// </summary>
        public int FuelConsumed { get; set; }
        /// <summary>
        /// Total money spent to travel
        /// </summary>
        public int MoneySpent { get; set; }
        /// <summary>
        /// @Override of ToString() for printing the Route
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string build = string.Format("Route - {0} Planet → {1} Planet\nFuel Consumed: {2}\nMoneySpent: {3}\nTotal visited Nodes: ", NodesVisited.First(), NodesVisited.Last(), FuelConsumed, MoneySpent);
            build += string.Join(" → ", NodesVisited);
            return build;
        }
    }
}
