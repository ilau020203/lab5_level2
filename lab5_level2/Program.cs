using System;

namespace lab5_level2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----TASK 1----- ");
            int n = 3;
            ToeplitzMatrix toeplitz = new ToeplitzMatrix(n);
            double[] b = new double[n];
            for (int i = 0; i < n; i++)
            {
                b[i] = (n * 17 / (2.5 * i + 1) + 19 / (2 * n - 1)) % 250;
            }
            double[] x = new double[n];
            Console.WriteLine("Equation:");
            MatrixMethods.PrintEquation(n, toeplitz, b);
            x = MatrixMethods.SolveToeplitz(toeplitz, b);
            Console.WriteLine("Solution:");
            MatrixMethods.PrintVector(x);
            Console.WriteLine("Check:");
            double[][] matrix = MatrixMethods.FormMatrix(n, n, toeplitz);
            double[] b_tested = MatrixMethods.Dot(matrix, x);
            MatrixMethods.PrintVector(b_tested, "b");
            Console.WriteLine();

            Console.WriteLine(" -----TASK 2----- ");
            Solver.CPPSolve(n, 1, toeplitz, b, x);
            MatrixMethods.PrintVector(x);
            Console.WriteLine();

            Console.WriteLine(" -----TASK 3----- ");
            TimesList times = new TimesList();
            string filename = "";
            Console.Write("Enter filename:\n>> ");
            filename = Console.ReadLine();
            times.Load(filename);
            Console.WriteLine();

            Console.WriteLine("-----TASK 4-----");
            while (true)
            {
                int order;
                Console.Write("Close app? ([y|Y|yes]/everything else)\n>> ");
                string status = Console.ReadLine();
                if (status == "yes" || status == "y" || status == "Y")
                {
                    Console.WriteLine(times);
                    times.Save(filename);
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                Console.Write("Enter matrix order (an integer greater than 1)\n>> ");
                while (!Int32.TryParse(Console.ReadLine(), out order) || order < 1)
                    Console.Write("Enter matrix order (an integer greater than 1)\n>> ");
                int repeat;
                Console.Write("Enter calcualtions repeat number (an integer greater than 1)\n>> ");
                while (!Int32.TryParse(Console.ReadLine(), out repeat) || repeat < 1)
                    Console.Write("Enter calculations repeat number (an integer greater than 1)\n>> ");
                double cstime, cpptime;
                cstime = (double)Solver.CSSolve(order, repeat);
                cpptime = Solver.CPPSolve(order, repeat);
                times.Add(new TimeItem(order, repeat, (int)cstime, (int)cpptime));

            }
        }

    }
}
