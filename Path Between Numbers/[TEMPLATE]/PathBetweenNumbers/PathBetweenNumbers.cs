using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

namespace Problem
{

    public static class PathBetweenNumbers
    {
        public static int Find(int X, int Y)
        {

            int[] vis = new int[Y * 100];

            Queue<(int, int)> q = new Queue<(int, int)>();
            vis[X] = 1;
            q.Enqueue((X, 0));
            if (X == Y) return 0;
            while (q.Count > 0)
            {
                int cur = q.Peek().Item1;
                int cur_dist = q.Peek().Item2;
                q.Dequeue();
                if (cur == Y)
                {
                    return cur_dist;
                }
                int first = cur * 2;
                int second = cur - 1;
                int third = cur * 10 + 1;
                if (second > 0 && vis[second] == 0)
                {
                    q.Enqueue((second, cur_dist + 1));
                    vis[second] = cur_dist + 1;
                }
               

                if (first <= Y + (2* Y) && vis[first] ==0)
                {
                    q.Enqueue((first, cur_dist + 1));
                    vis[first] = cur_dist + 1;
                }

                if (third <= Y * 4 && vis[third] == 0)
                {
                    q.Enqueue((third, cur_dist + 1));
                    vis[third] = cur_dist + 1;
                }
            }
            return -1;
        }
    }

}

