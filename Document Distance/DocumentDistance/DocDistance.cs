using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDistance
{
    class DocDistance
    {
        // *****************************************
        // DON'T CHANGE CLASS OR FUNCTION NAME
        // YOU CAN ADD FUNCTIONS IF YOU NEED TO
        // *****************************************
        /// <summary>
        /// Write an efficient algorithm to calculate the distance between two documents
        /// </summary>
        /// <param name="doc1FilePath">File path of 1st document</param>
        /// <param name="doc2FilePath">File path of 2nd document</param>
        /// <returns>The angle (in degree) between the 2 documents</returns>
        /// 


      
        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {
            string str1 = File.ReadAllText(doc1FilePath);
            str1 = str1.ToLower();
            string str2 = File.ReadAllText(doc2FilePath);
             str2=str2.ToLower();
           
            Dictionary<string,long> d1 = new Dictionary<string, long>();
            Dictionary<string, long> d2 = new Dictionary<string, long>();

            string s1 ="", s2="";

            foreach (char j in str2)
            {
                if (!Char.IsLetterOrDigit(j) && s2.Length!=0)
                {
                   
                        if (!d2.ContainsKey(s2)) {
                            d2.Add(s2, 0);
                            d2[s2]++;
                        }
                        else
                        {
                            d2[s2]++;
                        }
                    s2 = "";
                }
                else if (Char.IsLetterOrDigit(j)  ) {
                    
                    s2 += j;

                }
            }
            if (s2.Length!=0)
            {
                if (!d2.ContainsKey(s2))
                {
                    d2.Add(s2, 0);
                    d2[s2]++;
                }
                else
                {
                    d2[s2]++;
                }

            }
			


			foreach (char j in str1)
            {
                if (!Char.IsLetterOrDigit(j) && s1.Length!=0)
                {

                    if (!d1.ContainsKey(s1))
                    {
                        d1.Add(s1, 0);
                        d1[s1]++;
                    }
                    else
                    {
                        d1[s1]++;
                    }
                    s1 = "";
                }
                else if (Char.IsLetterOrDigit(j))
                {
                    s1 += j;

                }
            
        }
			if (s1.Length!=0)
			{
				if (!d1.ContainsKey(s1))
				{
					d1.Add(s1, 0);
					d1[s1]++;
				}
				else
				{
					d1[s1]++;
				}

			}

			double Numerator = 0.0;
            foreach (string word in d1.Keys)
            {
                if (d2.ContainsKey(word))
                {
                    Numerator += d1[word] * d2[word];
                }
            }

            double mag1 = 0.0,mag2=0.0;
            foreach (var item in d1)
            {
                mag1 += (item.Value * item.Value);
            }

            foreach (var item in d2)
            {
                mag2 += (item.Value * item.Value);
            }

            double res = Math.Acos(Math.Round(Numerator / Math.Sqrt(mag1 * mag2), 5));
            return res*180/Math.PI;
        }

    }
}
