using System;
using System.IO;
using System.Diagnostics;
using HackatonCsapat4.Model;

namespace HackatonCsapat4.Service
{
    /// <summary>
    /// This class reads the required information for data construction
    /// </summary>
    public class ReadSystem : IInputData
    {

        #region Field

        private string _path;
        private int _nodeLength;
        private int[] _nodePrice;
        private int[][] _nodeConnections;

        #endregion


        #region Constructor

        /// <summary>
        /// Creates an instance of ReadSystem that requires a path to process InputData
        /// </summary>
        /// <param name="path"></param>
        public ReadSystem(string path)
        {
            _path = path;
            ReadInput();
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the processed input data
        /// </summary>
        public IInputData GetInputData => this;

        public int NodeLength { get => _nodeLength; }

        public int[] NodePrice { get => _nodePrice; }

        public int[][] NodeFee { get => _nodeConnections; }

        #endregion


        #region Methods

        /// <summary>
        /// Reads and processes the required information from the given path
        /// </summary>
        private void ReadInput()
        {
            // try-catch statement if there is an error occurrs it catches
            try
            {
                // FileStream instance to the given path and the mode is to Open
                FileStream fs = new FileStream(_path, FileMode.Open);
                // Read opetation index pointer
                int readIndex = 0;
                // Opening a StreamReader for the FileStream we created
                using (StreamReader sr = new StreamReader(fs))
                {
                    // While loop breaks when the reading input reaches it's end
                    while (sr.Peek() != -1)
                    {
                        // Reading a line from the stream
                        string line = sr.ReadLine();
                        // If the line starts with a '#' character this will not be processed the program takes it as a comment line
                        if (line[0] == '#') continue;
                        // Switch statement for the operation index pointer
                        switch(readIndex)
                        {
                            case 0:
                                int.TryParse(line, out _nodeLength);
                                break;
                            case 1:
                                _nodePrice = ConvertInputToArray(line);
                                break;
                            case 2:
                                int[] inputArr = ConvertInputToArray(line);
                                int toEnd = sr.Peek();
                                _nodeConnections = new int[toEnd][];
                                for (int i = 0; i < toEnd; i++)
                                {
                                    if (i != 0) inputArr = ConvertInputToArray(sr.ReadLine());
                                    _nodeConnections[i] = new int[inputArr.Length];
                                    for (int j = 0; j < inputArr.Length; j++)
                                    {
                                        _nodeConnections[i][j] = inputArr[j];
                                    }
                                }
                                break;

                        }
                        // incrementing the readIndex whenewer
                        readIndex++;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Converts a StreamReader red line and gives back an integer array
        /// </summary>
        /// <param name="read"></param>
        /// <returns></returns>
        private int[] ConvertInputToArray(string read)
        {
            string[] arr = read.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            int[] check = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                check[i] = int.Parse(arr[i]);
            }
            return check;
        }

        #endregion

    }
}
