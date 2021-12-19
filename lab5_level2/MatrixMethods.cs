using System;
using System.Collections.Generic;
using System.Text;

namespace lab5_level2
{
    public static class MatrixMethods
    {
        public static void PrintVector(double[] vec, string name = "x")
        {
            int n = vec.Length;
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(String.Format("{0}{1} = {2,4}", name, i, Math.Round(vec[i], 3)));
            }
        }
        public static void PrintVector<T>(List<T> vec, string name = "x")
        {
            int n = vec.Count;
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(String.Format("{0}{1} = {2,4}", name, i, vec[i]));
            }
        }
        public static void Print(int n, int m, double[][] matrix)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(String.Format("{0,4}", matrix[i][j]));
                }
                Console.WriteLine();
            }
        }
        public static void PrintEquation(int N, IGetElement<double> getElement, double[] B, string varname = "x")
        {
            if (N != B.Length)
            {
                throw new ArgumentException("B length should be N!");
            }
            for (int i = 0; i < N; i++)
            {
                string sep = "      "; 
                string subsep = sep.Substring(0, (int)sep.Length / 3);
                string tmp = "";
                for (int j = 0; j < N; j++)
                {
                    tmp += String.Format("{0,4}", Math.Round(getElement.GetElement(i, j), 2));
                }
                if (i == (int)N / 2)
                {
                    tmp += subsep + "x " + subsep;
                }
                else
                {
                    tmp += sep;
                }
                tmp += String.Format("{0,3}", String.Format("{0}{1}", varname, i));
                if (i == (int)N / 2)
                {
                    tmp += subsep + "==" + subsep;
                }
                else
                {
                    tmp += sep;
                }
                tmp += String.Format("{0,-4}", Math.Round(B[i], 2));
                Console.WriteLine(tmp);
            }
        }
        public static double[] SolveToeplitz(ToeplitzMatrix toeplitz, double[] B)
        {
            int m = toeplitz.N;
            if (m != B.Length)
                throw new ArgumentException("B length should be equal to matrix order!");
            double[] f = new double[m];
            double[] b = new double[m];
            double[] x = new double[m];

            int i, j, n;
            double rk, sk, tk, Gk, Fk, r__;
            f[0] = 1 / toeplitz.rows[0];
            b[0] = f[0];
            x[0] = f[0] * B[0];
            for (n = 1; n < m; ++n)
            {
                Fk = 0;
                Gk = 0;
                for (j = 0; j < n; ++j)
                {
                    Fk += (double)toeplitz.rows[n - j] * f[j];
                    Gk += (double)toeplitz.cols[j + 1] * b[j];
                }
                rk = (double)1 / (1 - Gk * Fk);
                sk = (double)(-Fk * rk);
                tk = (double)(-Gk * rk);
                Fk = f[0];
                Gk = b[0];
                f[0] = Fk * rk;
                b[0] = Fk * tk;
                for (j = 1; j <= n - 1; ++j)
                {
                    Fk = f[j];
                    f[j] = (double)(Fk * rk + Gk * sk);
                    r__ = b[j];
                    b[j] = (double)(Fk * tk + Gk * rk);
                    Gk = r__;
                }
                f[n] = (double)Gk * sk;
                b[n] = (double)Gk * rk;
                r__ = B[n];
                for (i = 0; i <= n - 1; i++)
                {
                    r__ -= (double)toeplitz.rows[n - i] * x[i];
                }
                for (i = 0; i <= n - 1; i++)
                {
                    x[i] += (double)b[i] * r__;
                }
                x[n] = b[n] * r__;
            }
            return x;
        }
        public static double[][] FormMatrix(int n, int m, IGetElement<double> getElement)
        {
            double[][] matrix = new double[n][];
            for (int i = 0; i < m; i++)
            {
                matrix[i] = new double[m];
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i][j] = getElement.GetElement(i, j);
                }
            }
            return matrix;
        }
        public static double[][] Dot(double[][] m1, double[][] m2)
        {
            if (m1.GetLength(0) != m2.GetLength(1))
                throw new ArgumentException("Matrix size are not consistent.");
            int r = m1.GetLength(0);
            int c = m2.GetLength(1);
            double[][] matrix = new double[r][];
            for (int i = 0; i < r; i++)
                matrix[i] = new double[c];
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    for (int k = 0; k < m1.GetLength(1); k++)
                        matrix[i][j] += m1[i][k] * m2[k][j];
                }
            }
            return matrix;
        }
        public static double[] Dot(double[][] m1, double[] m2)
        {
            if (m1.GetLength(0) != m2.Length)
                throw new ArgumentException("Matrix size are not consistent.");
            int r = m1.GetLength(0);
            double[] matrix = new double[r];
            for (int i = 0; i < r; i++)
            {
                for (int k = 0; k < m2.Length; k++)
                    matrix[i] += m1[i][k] * m2[k];
            }
            return matrix;
        }
    }
}
