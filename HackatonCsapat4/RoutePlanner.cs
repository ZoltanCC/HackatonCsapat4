using HackatonCsapat4.Model;
using System.Collections.Generic;
using System.Linq;

namespace HackatonCsapat4
{
    /// <summary>
    /// Exoplanet visitor route planner that can give you the best possible routes for a given destination
    /// </summary>
    public class RoutePlanner
    {

        #region Field

        private List<Node> _nodes;
        private List<Node> _tryedRoutes;
        private List<RouteModel> _totalTryedRoutes;
        private IInputData _inputData;
        private int _fuel;
        private int _budget;
        private int _dest;
        private int _start;

        #endregion


        #region Constructor

        /// <summary>
        /// Creates an instance of RoutePlanner that requires an IInputData of the nodes
        /// </summary>
        /// <param name="inputData"></param>
        public RoutePlanner(IInputData inputData)
        {
            _inputData = inputData;
            LoadNodes();
        }

        #endregion


        #region Properties

        public List<Node> GetNodes => _nodes;
        public IInputData GetInputData => _inputData;
        public int GetBudget => _budget;
        public int GetFuel => _fuel;
        public int GetDestination => _dest;
        /// <summary>
        /// Gets the most visited events calculated by RoutePlanner
        /// </summary>
        public RouteModel MostVisitedEvents { get; private set; }
        /// <summary>
        /// Gets the fastest route calculated by RoutePlanner
        /// </summary>
        public RouteModel FastestRoute { get; private set; }

        #endregion


        #region Methods

        /// <summary>
        /// Set the fuel level and budget
        /// </summary>
        /// <param name="fuel"></param>
        /// <param name="budget"></param>
        public void SetFuelAndBudget(int fuel, int budget)
        {
            _fuel = fuel;
            _budget = budget;
        }

        /// <summary>
        /// Set the start and destination Nodes
        /// </summary>
        /// <param name="start"></param>
        /// <param name="dest"></param>
        public void SetDestination(int start, int dest)
        {
            _start = start;
            _dest = dest;
        }

        /// <summary>
        /// Load and Initialize the RoutePlanner class that creates Nodes
        /// </summary>
        private void LoadNodes()
        {
            // Converting IInputData to List<Node>
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

        /// <summary>
        /// Plan a route between two Node's (configured in SetDestination)
        /// </summary>
        public void Plan()
        {
            // Preparing, declaring, implementing local variables for the recursive algorythm
            _tryedRoutes = new List<Node>() { GetNodes[0] };
            List<int> pathList = new List<int>() { 0 };
            bool[] isVisited = new bool[GetInputData.NodeLength];
            _totalTryedRoutes = new List<RouteModel>();
            // Here is the first call of the recursive algorythm
            PlanRoute(GetNodes[_start], GetNodes[_dest], isVisited, pathList, GetFuel, GetBudget, 0, 0);
            // LINQ query to select the most visited events
            MostVisitedEvents = (from route in _totalTryedRoutes orderby route.NodesVisited.Count, route.MoneySpent ascending select route).Last();
            // LINQ query to select the fastest route to the destination
            FastestRoute = (from route in _totalTryedRoutes orderby route.NodesVisited.Count ascending select route).First();
        }

        /// <summary>
        /// Recursive Algorythm to solve all the possible routes to given parameter
        /// </summary>
        /// <param name="csomopont"></param>
        /// <param name="destination"></param>
        /// <param name="isVisited"></param>
        /// <param name="pathList"></param>
        /// <param name="totalFuel"></param>
        /// <param name="totalMoney"></param>
        /// <param name="consumedFuel"></param>
        /// <param name="spentMoney"></param>
        private void PlanRoute(Node csomopont, Node destination, bool[] isVisited, List<int> pathList, int totalFuel, int totalMoney, int consumedFuel, int spentMoney)
        {
            // Checks if the current node is in the destination
            if (csomopont.NodeID == destination.NodeID)
            {
                // Adds RouteModel to the possible Routes
                _totalTryedRoutes.Add(new RouteModel() { NodesVisited = new List<int>(pathList), FuelConsumed = consumedFuel, MoneySpent = spentMoney });
                // Returns to make an end for a possible route
                return;
            }
            // Adds the Node to IsVisited bool array as true // Meaning that we dont need to check this node later for possible direction
            isVisited[csomopont.NodeID] = true;
            // foreach to call all the neighbours of the current node
            foreach (KeyValuePair<int,int> neighbour in csomopont.NodeNeighbours)
            {
                // If an element is visible
                if (!isVisited[neighbour.Key])
                {
                    // Add to the path list the neighbour
                    pathList.Add(neighbour.Key);
                    // Getting the cost to enter a Node [neighbour]
                    int costToNeighbour = GetNodes[neighbour.Key].EntryFee;
                    // Getting the consumption to the Node [neighbour]
                    int consumptionToNeghbour = neighbour.Value;
                    // Check if there is enough money and fuel to travel the
                    if(spentMoney + costToNeighbour < totalMoney && consumedFuel + consumptionToNeghbour < totalFuel)
                    {
                        // Recursive recall
                        PlanRoute(GetNodes[neighbour.Key], destination, isVisited, pathList, totalFuel, totalMoney, consumedFuel + consumptionToNeghbour, spentMoney + costToNeighbour);
                    }
                    // Remove neighbour from the pathList
                    pathList.Remove(neighbour.Key);
                }
            }
            // Sets the isVisited status of current Node to 'false'
            isVisited[csomopont.NodeID] = false;
        }

        #endregion

    }
}
