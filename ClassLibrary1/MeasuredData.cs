using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ClassLibrary1
{
    public class MeasuredData: IDataErrorInfo
    {
        //две сетки, проверка на ошибку ввода
        public int n { get; set; }
        public double a { get; set; }
        public double b { get; set; }
        public SPf f { get; set; }
        public int n_uniform { get; set; }
        public double x1 { get; set; }
        public double x2 { get; set; }
        public double x3 { get; set; }

        public double[] nodes_non_uniform { get; set; }
        public double[] res_f { get; set; }
        public ObservableCollection<string> col { get; set; }


        public MeasuredData(int nn, double start, double end, SPf fin, int nn_uniform,
                double x1_in, double x2_in, double x3_in)
        {
            n = nn;
            a = start;
            b = end;
            f = fin;
            n_uniform = nn_uniform;
            x1 = x1_in;
            x2 = x2_in;
            x3 = x3_in;
            col = new();
        }

        public void MeasureData_fill()
        {
            nodes_non_uniform = new double[n];
            Random rnd = new Random();
            nodes_non_uniform[0] = a;
            nodes_non_uniform[n - 1] = b;
            for (int i = 1; i < n - 1; i++)
            {
                nodes_non_uniform[i] = a + rnd.NextDouble() * (b - a);
            }
            Array.Sort(nodes_non_uniform);

            res_f = new double[n];
            if (f == SPf.linear)
            {
                for (int i = 0; i < n; i++)
                {
                    res_f[i] = 2 * nodes_non_uniform[i] + 1.0;//2x + 1
                }
            }
            if (f == SPf.cubic)
            {
                for (int i = 0; i < n; i++)
                {
                    res_f[i] = 3 * nodes_non_uniform[i] * nodes_non_uniform[i] 
                            * nodes_non_uniform[i] + 2.0;//3x^3 + 2
                }
            }

            if (f == SPf.rand)
            {
                for (int i = 0; i < n; i++)
                {
                    res_f[i] = 5.0 * rnd.NextDouble();//rand
                }
            }

            col.Clear();
            for (int i = 0; i < n; i++)
            {
                col.Add($"Nod: {nodes_non_uniform[i]:0.00000}  F(nod): {res_f[i]:0.00000}\n");
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string field_name]
        {
            get
            {
                string error = null;

                switch (field_name)
                {
                    case "n_uniform":
                        if (n_uniform <= 2)
                        {
                            error = "Число узлов равномерной сетки меньше 3";
                        }
                        break;
                    case "n":
                        if (n < 3)
                        {
                            error = "Число узлов неравномерной сетки меньше 3";
                        }
                        break;
                    case "a":
                        if ((a > x1) || (x1 >= x2) || (x2 >= x3) || (x3 > b))
                        {
                            error = "Нарушение a <= x1 < x2 < x3 <= b";
                        }
                        break;
                    default:
                        break;
                }
                return error;
            }
        }
    }
}
