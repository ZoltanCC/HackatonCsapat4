using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackatonCsapat4.Model
{
    public class Node
    {
        /// <summary>
        /// How much it cost to enter the Node
        /// </summary>
        public int EntryFee { get; set; }
        /// <summary>
        /// NodeID
        /// </summary>
        public int NodeID { get; set; }
        /// <summary>
        /// ID of nodes that you can travel from this node
        /// </summary>
        public Dictionary<int, int> NodeNeighbours { get; set; }
    }
}
