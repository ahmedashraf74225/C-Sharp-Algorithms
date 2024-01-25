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
    public static class MatrixMultiplication
    {
        public static int[,] NaiveMultiply(int[,] A, int[,] B, int n)
        {
            int[,] C = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }

            return C;
        }

        public static void SplitMatrix(int[,] M, int[,] M11, int[,] M12, int[,] M21, int[,] M22)
        {
            int n = M.GetLength(0);

            for (int i = 0; i < n / 2; i++)
            {
                for (int j = 0; j < n / 2; j++)
                {
                    M11[i, j] = M[i, j];
                    M12[i, j] = M[i, j + n / 2];
                    M21[i, j] = M[i + n / 2, j];
                    M22[i, j] = M[i + n / 2, j + n / 2];
                }
            }
        }




        private static int[,] AddMatrices(int[,] a, int[,] b, int N)
        {
            int n = a.GetLength(0);
            int[,] c = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }

            return c;
        }

        private static int[,] SubtractMatrices(int[,] a, int[,] b, int N)
        {
            int n = a.GetLength(0);
            int[,] c = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    c[i, j] = a[i, j] - b[i, j];
                }
            }

            return c;
        }

        public static int[,] CombineMatrix(int[,] M11, int[,] M12, int[,] M21, int[,] M22, int n)
        {
            int[,] M = new int[n, n];

            for (int i = 0; i < n / 2; i++)
            {
                for (int j = 0; j < n / 2; j++)
                {
                    M[i, j] = M11[i, j];
                    M[i, j + n / 2] = M12[i, j];
                    M[i + n / 2, j] = M21[i, j];
                    M[i + n / 2, j + n / 2] = M22[i, j];
                }
            }

            return M;
        }
        public static int[,] GetSubMatrix(int[,] matrix, int row, int col, int size)
        {
            int[,] result = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = matrix[row + i, col + j];
                }
            }
            return result;
        }

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 square matrices in an efficient way [Strassen's Method]
        /// </summary>
        /// <param name="M1">First square matrix</param>
        /// <param name="M2">Second square matrix</param>
        /// <param name="N">Dimension (power of 2)</param>
        /// <returns>Resulting square matrix</returns>
        static public int[,] MatrixMultiply(int[,] M1, int[,] M2, int N)
        {
            if (N <= 64)
            {
                // base case: multiply using the standard algorithm
                return NaiveMultiply(M1, M2, N);
            }

            int[,] A11 = GetSubMatrix(M1, 0, 0, N / 2);
            int[,] A12 = GetSubMatrix(M1, 0, N / 2, N / 2);
            int[,] A21 = GetSubMatrix(M1, N / 2, 0, N / 2);
            int[,] A22 = GetSubMatrix(M1, N / 2, N / 2, N / 2);

            int[,] B11 = GetSubMatrix(M2, 0, 0, N / 2);
            int[,] B12 = GetSubMatrix(M2, 0, N / 2, N / 2);
            int[,] B21 = GetSubMatrix(M2, N / 2, 0, N / 2);
            int[,] B22 = GetSubMatrix(M2, N / 2, N / 2, N / 2);

            int N2 = (N / 2);
            int[,] P1 = new int[N2, N2];
            int[,] P2 = new int[N2, N2];
            int[,] P3 = new int[N2, N2];
            int[,] P4 = new int[N2, N2];
            int[,] P5 = new int[N2, N2];
            int[,] P6 = new int[N2, N2];
            int[,] P7 = new int[N2, N2];

            Parallel.Invoke(
                   () => { P1 = MatrixMultiply(AddMatrices(A11, A22, N2), AddMatrices(B11, B22, N2), N2); },
                   () => { P2 = MatrixMultiply(AddMatrices(A21, A22, N2), B11, N2); },
                   () => { P3 = MatrixMultiply(A11, SubtractMatrices(B12, B22, N2), N2); },
                   () => { P4 = MatrixMultiply(A22, SubtractMatrices(B21, B11, N2), N2); },
                   () => { P5 = MatrixMultiply(AddMatrices(A11, A12, N2), B22, N2); },
                   () => { P6 = MatrixMultiply(SubtractMatrices(A21, A11, N2), AddMatrices(B11, B12, N2), N2); },
                   () => { P7 = MatrixMultiply(SubtractMatrices(A12, A22, N2), AddMatrices(B21, B22, N2), N2); }
               );


            // Combine
            int[,] C11 = AddMatrices(SubtractMatrices(AddMatrices(P1, P4, N / 2), P5, N / 2), P7, N / 2);
            int[,] C12 = AddMatrices(P3, P5, N / 2);
            int[,] C21 = AddMatrices(P2, P4, N / 2);
            int[,] C22 = AddMatrices(SubtractMatrices(AddMatrices(P1, P3, N / 2), P2, N / 2), P6, N / 2);

            // Combine the results into a single matrix
            int[,] C = CombineMatrix(C11, C12, C21, C22, N);
            return C;

        }


    }
}
