using HackatonCsapat4.Model;
using System;
using System.Collections.Generic;
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
        private IInputData _inputData;
        /// <summary>
        /// Creates an instance of RoutePlanner that requires an IInputData of the nodes
        /// </summary>
        /// <param name="inputData"></param>
        public RoutePlanner(IInputData inputData)
        {
            _inputData = inputData;
        }

        public void Plan()
        {
            
        }
    }
}
