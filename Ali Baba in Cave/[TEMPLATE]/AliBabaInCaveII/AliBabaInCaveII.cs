using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem
{
    public static class AliBabaInCaveII
    {
        public static int SolveValue(int camelsLoad, int itemsCount, int[] weights, int[] profits, int[] instances)
        {
            int[,] dp = new int[itemsCount + 1, camelsLoad + 1];

            for (int ind = 1; ind <= itemsCount; ind++)
            {
                int curr_weight = weights[ind];
                int curr_Prof = profits[ind];
                int curr_inst = instances[ind];

                n3mel_table_el_DP(dp, ind, camelsLoad, curr_weight, curr_Prof, curr_inst);
            }
            //FUNCTION #1 DONE 
            return dp[itemsCount, camelsLoad];
        }

        private static void n3mel_table_el_DP(int[,] dp, int itemIndex, int camelsLoad, int currentWeight, int currentProfit, int currentInstances)
        {
            foreach (int l in Enumerable.Range(1, camelsLoad))
            {
                dp[itemIndex, l] = dp[itemIndex - 1, l];

                foreach (int k in Enumerable.Range(1, Math.Min(currentInstances, l / currentWeight)))
                {
                    int kol_elWeight = k * currentWeight;
                    int kol_elprofit = k * currentProfit + dp[itemIndex - 1, l - kol_elWeight];

                    dp[itemIndex, l] = Math.Max(dp[itemIndex, l], kol_elprofit);
                }
            }
        }
        static int[,] instancesTaken;

        public static Tuple<int, int>[] ConstructSolution(int camelsLoad, int itemsCount, int[] weights, int[] profits, int[] instances)
        {
            int[,] dp = new int[itemsCount + 1, camelsLoad + 1];
            instancesTaken = new int[itemsCount + 1, camelsLoad + 1];

            n3mel_tabel_eldp2(dp, itemsCount, camelsLoad, weights, profits, instances);

            
            Tuple<int, int>[] selectedItems = ReconstructSolution(itemsCount, camelsLoad, weights, instancesTaken);


            //FUNCTION #2 DONE
            return selectedItems;
        }

        private static void n3mel_tabel_eldp2(int[,] dp, int itemsCount, int camelsLoad, int[] weights, int[] profits, int[] instances)
        {
            for (int ASHRAF = 1; ASHRAF <= itemsCount; ASHRAF++)
            {
                for (int AHMED = 1; AHMED <= camelsLoad; AHMED++)
                {
                    dp[ASHRAF, AHMED] = dp[ASHRAF - 1, AHMED];

                    for (int k = 1; k <= Math.Min(instances[ASHRAF], AHMED / weights[ASHRAF]); k++)
                    {
                        int kol_weight = k * weights[ASHRAF];
                        int el_maksab = k * profits[ASHRAF] + dp[ASHRAF - 1, AHMED - kol_weight];

                        if (el_maksab > dp[ASHRAF, AHMED])
                        {
                            dp[ASHRAF, AHMED] = el_maksab;
                            instancesTaken[ASHRAF, AHMED] = k;
                        }
                    }
                }
            }
        }

        private static Tuple<int, int>[] ReconstructSolution(int itemsCount, int camelsLoad, int[] weights, int[,] instancesTaken)
        {
            List<Tuple<int, int>> selectedItems = new List<Tuple<int, int>>();
            int el_baki = camelsLoad;

            for (int o = itemsCount; o > 0 && el_baki > 0; o--)
            {
                if (instancesTaken[o, el_baki] > 0)
                {
                    selectedItems.Add(new Tuple<int, int>(o, instancesTaken[o, el_baki]));

                    el_baki -= instancesTaken[o, el_baki] * weights[o];
                }
            }
            return selectedItems.ToArray();
        }


    }
    //ALGO ASSIGNMENT 4 #DONE 
    //_AhmedAshraf
}