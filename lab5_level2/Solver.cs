using System.Diagnostics;
using System;
using System.Runtime.InteropServices;
namespace lab5_level2
{
    public static class Solver
    {

        public static long CSSolve(int order, int repeat, ToeplitzMatrix toeplitz, double[] b, double[] x)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < repeat; i++)
            {
                x = MatrixMethods.SolveToeplitz(toeplitz, b);
            }
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        public static long CSSolve(int order, int repeat)
        {
            ToeplitzMatrix toeplitz = new ToeplitzMatrix(order);
            double[] b = new double[order];
            for (int i = 0; i < order; i++)
            {
                b[i] = (order * 17 / (2.5 * i + 1) + 19 / (2 * order - 1)) % 250;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < repeat; i++)
            {
                MatrixMethods.SolveToeplitz(toeplitz, b);
            }
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        public const string dllpath = @".\dll\DllForC_sharp.dll";
        [DllImport(dllpath, CallingConvention = CallingConvention.StdCall)]
        public static extern void solutionAuto(int n, int repeat, double[] B, double[] x, double[] duration);
        [DllImport(dllpath, CallingConvention = CallingConvention.StdCall)]
        public static extern void solution(int n, int repeat, double[] B, double[] x, double[] duration, double[] cols, double[] rows);
        public static double CPPSolve(int order, int repeat, ToeplitzMatrix toeplitz, double[] b, double[] x)
        {
            double[] duration = new double[1];
            double[] cols = new double[order];
            double[] rows = new double[order];
            for (int i = 0; i < order; i++)
            {
                cols[i] = toeplitz.cols[i];
                rows[i] = toeplitz.rows[i];
            }
            try
            {
                solution(order, repeat, b, x, duration, cols, rows);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return duration[0];
        }
        public static double CPPSolve(int order, int repeat)
        {
            ToeplitzMatrix toeplitz = new ToeplitzMatrix(order);
            double[] b = new double[order];
            double[] x = new double[order];
            double[] duration = new double[1];
            for (int i = 0; i < order; i++)
            {
                b[i] = (order * 17 / (2.5 * i + 1) + 19 / (2 * order - 1)) % 250;
            }
            try
            {
                solutionAuto(order, repeat, b, x, duration);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return duration[0];
        }

    }
}
