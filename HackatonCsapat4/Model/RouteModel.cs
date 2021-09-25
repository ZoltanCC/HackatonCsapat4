using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackatonCsapat4.Model
{
    public class RouteModel
    {
        public List<int> NodesVisited { get; set; }
        public int FuelConsumed { get; set; }
        public int MoneySpent { get; set; }
        public override string ToString()
        {
            string build = string.Format("Route - {0} Planet → {1} Planet\nFuel Consumed: {2}\nMoneySpent: {3}\n", NodesVisited.First(), NodesVisited.Last(), FuelConsumed, MoneySpent);
            build += string.Join(" → ", NodesVisited);
            return build;
        }
    }
}
