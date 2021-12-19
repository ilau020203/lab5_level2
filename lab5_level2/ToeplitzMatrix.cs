using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace lab5_level2
{
    public class ToeplitzMatrix : IGetElement<double>
    {
        public List<double> cols { get; set; }
        public List<double> rows { get; set; }
        public double[] T { get; set; }
        public int N { get; set; }

        private void CheckData()
        {
            if (cols.Count != rows.Count)
                throw new ArgumentException("Given arrays are of different size");
            if (cols.Count == 0)
                throw new ArgumentException("Arrays size must be greather than 0.");
            if (cols[0] != rows[0])
                throw new ArgumentException("First elements must be equal");
            if (cols[0] == 0)
                throw new ArgumentException("Main diagonal element cannot be 0!");
        }
        private void GenerateData(int n)
        {
            N = n;
            cols = new List<double>(n) { };
            rows = new List<double>(n) { };
            cols.Add(150 + 2 * n);
            rows.Add(cols[0]);
            for (int i = 1; i < n; i++)
            {
                cols.Add(3 * n * i * 17 % 100 - 50);
                rows.Add(4 * n * i * 21 % 100 - 50);
            }
        }
        private void FillData()
        {
            N = cols.Count;
            int cnt = 0;
            T = new double[2 * N - 1];
            for (int i = N - 1; i >= 0; i--)
            {
                T[cnt] = cols[i];
                cnt++;
            }
            for (int i = 1; i < N; i++)
            {
                T[cnt] = rows[i];
                cnt++;
            }
        }
        public ToeplitzMatrix(List<double> cols_, List<double> rows_)
        {
            cols = cols_;
            rows = rows_;
            CheckData();
            FillData();
        }
        public ToeplitzMatrix(double[] cols_, double[] rows_)
        {
            cols = cols_.ToList();
            rows = rows_.ToList();
            CheckData();
            FillData();
        }
        public ToeplitzMatrix(int n)
        {
            if (n < 1)
                throw new ArgumentException("Matrix order should be at least 1.");
            GenerateData(n);
            CheckData();
            FillData();
        }
        public ToeplitzMatrix()
        {
            GenerateData(3);
            CheckData();
            FillData();
        }
        public double t(int i)
        {
            return T[i + N - 1];
        }
        public double GetElement(int i, int j)
        {
            return t(i - j);
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    result += String.Format("{0,4}", T[j - i + N - 1]);
                }
                result += "\n";
            }
            return result;
        }
    }
}
