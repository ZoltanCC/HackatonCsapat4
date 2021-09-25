using HackatonCsapat4.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackatonCsapat4
{
    /// <summary>
    /// Exoplanet visitor route planner that can give you the best possible routes for a given destination
    /// </summary>
    public class RoutePlanner
    {

        private List<Node> _nodes;
        private List<Node> _tryedRoutes;
        private List<RouteModel> _totalTryedRoutes;
        private IInputData _inputData;
        private int _fuel;
        private int _budget;
        private int _dest;
        private int _start;


        /// <summary>
        /// Creates an instance of RoutePlanner that requires an IInputData of the nodes
        /// </summary>
        /// <param name="inputData"></param>
        public RoutePlanner(IInputData inputData)
        {
            _inputData = inputData;
            LoadNodes();
        }

        public List<Node> GetNodes => _nodes;
        public IInputData GetInputData => _inputData;
        public int GetBudget => _budget;
        public int GetFuel => _fuel;
        public int GetDestination => _dest;
        public RouteModel MostVisitedEvents { get; private set; }
        public RouteModel FastestRoute { get; private set; }

        public void SetFuelAndBudget(int fuel, int budget)
        {
            _fuel = fuel;
            _budget = budget;
        }

        public void SetDestination(int start, int dest)
        {
            _start = start;
            _dest = dest;
        }

        private void LoadNodes()
        {
            _nodes = new List<Node>();

            for (int j = 0; j < GetInputData.NodeFee.GetLength(0); j++)
            {
                _nodes.Add(new Node() { NodeID = j, EntryFee = GetInputData.NodePrice[j], NodeNeighbours = new Dictionary<int, int>() });
                for (int k = 0; k < GetInputData.NodeFee[j].Length; k++)
                {
                    int possibleNeighbour = GetInputData.NodeFee[j][k];
                    if (possibleNeighbour != 0)
                    {
                        _nodes[j].NodeNeighbours.Add(k, possibleNeighbour);
                    }
                }
            }
        }

        public void Plan()
        {
            _tryedRoutes = new List<Node>() { GetNodes[0] };
            List<int> pathList = new List<int>() { 0 };
            bool[] isVisited = new bool[GetInputData.NodeLength];
            _totalTryedRoutes = new List<RouteModel>();
            PlanRoute(GetNodes[_start], GetNodes[_dest], isVisited, pathList, GetFuel, GetBudget, 0, 0);
            MostVisitedEvents = (from route in _totalTryedRoutes orderby route.NodesVisited.Count, route.MoneySpent ascending select route).Last();
            FastestRoute = (from route in _totalTryedRoutes orderby route.NodesVisited.Count ascending select route).First();
        }

        private void PlanRoute(Node csomopont, Node destination, bool[] isVisited, List<int> pathList, int totalFuel, int totalMoney, int consumedFuel, int spentMoney)
        {
            if (csomopont.NodeID == destination.NodeID)
            {
                _totalTryedRoutes.Add(new RouteModel() { NodesVisited = new List<int>(pathList), FuelConsumed = consumedFuel, MoneySpent = spentMoney });
                return;
            }
            isVisited[csomopont.NodeID] = true;
            int i = 0;
            foreach (KeyValuePair<int,int> neighbour in csomopont.NodeNeighbours)
            {
                if (!isVisited[neighbour.Key])
                {
                    pathList.Add(neighbour.Key);
                    int costToNeighbour = GetNodes[neighbour.Key].EntryFee;
                    int consumptionToNeghbour = neighbour.Value;
                    if(spentMoney + costToNeighbour < totalMoney && consumedFuel + consumptionToNeghbour < totalFuel)
                    {
                        PlanRoute(GetNodes[neighbour.Key], destination, isVisited, pathList, totalFuel, totalMoney, consumedFuel + consumptionToNeghbour, spentMoney + costToNeighbour);
                    }
                    pathList.Remove(neighbour.Key);
                    i++;
                }
            }
            isVisited[csomopont.NodeID] = false;
        }
    }
}
