using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    public static class SumOfMin
    {
        private static List<int>[] adjacencyList = new List<int>[8001];
        private static int[] visited = new int[8001];
        private static int minimumValue = int.MaxValue;

        private static void DepthFirstSearch(int vertex, ref int[] vertexValues)
        {
            visited[vertex] = 1;
            minimumValue = Math.Min(minimumValue, vertexValues[vertex]);
            foreach (int adjacentVertex in adjacencyList[vertex])
            {
                if (visited[adjacentVertex] == 0)
                    DepthFirstSearch(adjacentVertex, ref vertexValues);
            }
        }
        public static int CalcSumOfMinInComps(int[] valuesOfVertices, KeyValuePair<int, int>[] edges)
        {


            int numVertices = valuesOfVertices.Length;

            for (int i = 0; i <= numVertices; i++)
            {
                adjacencyList[i] = new List<int>();
                visited[i] = 0;
            }

            foreach (KeyValuePair<int, int> edge in edges)
            {
                int vertex1 = edge.Key;
                int vertex2 = edge.Value;
                adjacencyList[vertex1].Add(vertex2);
                adjacencyList[vertex2].Add(vertex1);
            }

            int minimumSum = 0;
            for (int i = 1; i < numVertices; i++)
            {
                minimumValue = int.MaxValue;
                if (visited[i] == 0)
                {
                    DepthFirstSearch(i, ref valuesOfVertices);
                    minimumSum += minimumValue;
                }
            }

            return minimumSum;
        }
    }
 }
    

