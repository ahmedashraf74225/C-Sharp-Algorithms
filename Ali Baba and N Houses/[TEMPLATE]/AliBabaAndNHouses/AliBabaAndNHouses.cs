using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class AliBabaAndNHouses
    {
        #region YOUR CODE IS HERE

        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// find the maximum amount of money that Ali baba can get, given the number of houses (N) and a list of the net gained value for each consecutive house (V)
        /// </summary>
        /// <param name="values">Array of the values of each given house (ordered by their consecutive placement in the city)</param>
        /// <param name="N">The number of the houses</param>
        /// <returns>the maximum amount of money the Ali Baba can get </returns>
        static public int SolveValue(int[] values, int N)
        {
            if (N == 0)
                return 0;

            if (N == 1)
                return values[0];

            int[] dp = new int[N];
            dp[0] = values[0];
            dp[1] = GetMaxValue(values[0], values[1]);

            FillDpArray(dp, values, N);

            return GetLastHouseMaxValue(dp, N);
        }

        static private int GetMaxValue(int value1, int value2)
        {
            // Return the maximum value between two values
            return Math.Max(value1, value2);
        }



        static private int GetLastHouseMaxValue(int[] dp, int N)
        {
            // Return the maximum value for the last house
            return dp[N - 1];
        }


        static public void FillDpArray(int[] dp, int[] values, int N)
        {
            // Iterate through the houses starting from the third one
            for (int i = 2; i < N; i++)
            {
                // Calculate the maximum value for the current house by either robbing it and adding its value to the maximum value of the previous house that was not robbed,
                // or skipping it and taking the maximum value of the previous house that was robbed
                dp[i] = Math.Max(dp[i - 1], dp[i - 2] + values[i]);
            }
        }

        static public int[] ConstructSolution(int[] values, int N)
        {
            int[] dp = new int[N]; // Create an array to store the maximum values for each house
            dp[0] = values[0]; // The maximum value for the first house is its own value
            dp[1] = Math.Max(values[0], values[1]); // The maximum value for the second house is the maximum of the first two values

            FillDpArray(dp, values, N); // Fill the dynamic programming array with maximum values

            List<int> robbedHouses = GetRobbedHouses(dp, N); // Get the sequence of robbed houses

            return robbedHouses.ToArray(); // Return the sequence of robbed houses as an array
        }

        static private List<int> GetRobbedHouses(int[] dp, int N)
        {
            List<int> robbedHouses = new List<int>();
            int legh = N - 1; // Start from the last house

            while (legh >= 0)
            {
                if (legh == 0)
                {
                    robbedHouses.Add(1); // Add the index of the first house (1) to the list
                    break; // Exit the loop
                }
                else if (legh == 1)
                {
                    robbedHouses.Add(GetSecondHouseIndex(dp[0], dp[1])); // Add the index of the second house (1 or 2) based on the values
                    break; // Exit the loop
                }
                else if (dp[legh] == dp[legh - 1])
                {
                    legh = MoveToPreviousHouse(legh); // Move to the previous house if the maximum value for the current house is equal to the maximum value of the previous house
                }
                else
                {
                    legh = MoveToTwoHousesBack(dp, robbedHouses, legh); // Add the index of the current house and move two houses back
                }
            }

            robbedHouses.Reverse(); // Reverse the list to restore the left-to-right order

            return robbedHouses; // Return the list of robbed houses
        }

        static private int GetSecondHouseIndex(int value1, int value2)
        {
            return value2 > value1 ? 2 : 1; // Return the index of the second house (1 or 2) based on the values
        }

        static private int MoveToPreviousHouse(int legh)
        {
            return legh - 1; // Move to the previous house by decrementing the index
        }

        static private int MoveToTwoHousesBack(int[] dp, List<int> robbedHouses, int legh)
        {
            robbedHouses.Add(legh + 1); // Add the index of the current house to the list
            return legh - 2; // Move two houses back by subtracting 2 from the index
        }



        #endregion

        #endregion
    }

}