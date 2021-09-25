namespace HackatonCsapat4.Model
{
    /// <summary>
    /// InputData for star system traveling
    /// </summary>
    public interface IInputData
    {
        /// <summary>
        /// Total number of Nodes in a Star Travel System
        /// </summary>
        int NodeLength { get; }
        /// <summary>
        /// Prices array for the stations on Exoplanets
        /// </summary>
        int[] NodePrice { get; }
        /// <summary>
        /// FuelPrices to reach a given Node [matrix]
        /// </summary>
        int[][] NodeFee { get; }
    }
}
