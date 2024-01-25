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
    public static class AliBabaAssembleTeam
    {
        //Your Code is Here:
        //==================
        /// <summary>
        /// Find the minimum cost for any team that can be assembled 
        /// </summary>
        /// <param name="N">size of the array</param>
        /// <param name="array">contains the cost of each theif (+ve, -ve or 0) </param>
        /// <returns>min total cost of a team</returns>
        static public long AssembleTeam(int N, short[] array)
        { 
            long cur_sum = 0;
            long mn_sum = int.MaxValue;

            // [ -6   1    6   -4    -3  ]  -> -7
            // [ 1   -6    3   -4     1  ]  -> -7
            for (long i = 0; i < N; i++)
            {
                cur_sum += array[i];

                if (array[i] < cur_sum)
                {
                    mn_sum = array[i];
                    cur_sum = array[i];
                }
                else if (cur_sum < mn_sum)
                {
                    mn_sum = cur_sum;
                }
            }
               return mn_sum;
           
        }
    }
}
