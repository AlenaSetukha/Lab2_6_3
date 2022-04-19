using System.Runtime.InteropServices;
using System.ComponentModel;
using System;

namespace ClassLibrary1
{
    public class SplinesData
    {
        public MeasuredData md { get; set; }    //неравномерная сетка

        public double[] cubic_res { get; set; }//кубический сплайн на равномернеой сетке
        public double[] integral_res { get; set; } = new double[2];
        public double[] derivatives { get; set; } = new double[4];//значения производных на концах

        public SplinesData(MeasuredData md_in)
        {
            md = md_in;
        }

        public SplinesData(SplinesData obj)
        {
            md = obj.md;
        }

        [DllImport("..\\..\\..\\..\\x64\\Debug\\Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Global_Spline(double[] v, int n, double[] vals, int n_uniform,
                double[] ab_uniform, double[] res);
        [DllImport("..\\..\\..\\..\\x64\\Debug\\Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Global_Integral(double[] v, int n, double[] vals, int n_uniform,
                double[] segment, double[] res_integral);

        public int build_spline()
        {
            int ret;
            double[] spline_res = new double[3 * md.n_uniform];
            ret = Global_Spline(md.nodes_non_uniform, md.n, md.res_f, md.n_uniform,
                    new double[] { md.a, md.b }, spline_res);
            if (ret != 0)
            {
                return ret;
            }

            double[] resault = new double[md.n_uniform];
            for (int i = 0; i < md.n_uniform; i++)
            {
                resault[i] = spline_res[3 * i];//вернул тройками(f, f', f")
            }
            cubic_res = resault;


            derivatives[0] = spline_res[1];
            derivatives[1] = spline_res[(3 * md.n_uniform) - 2];
            derivatives[2] = spline_res[2];
            derivatives[3] = spline_res[(3 * md.n_uniform) - 1];



            ret = Global_Integral(md.nodes_non_uniform, md.n, md.res_f, md.n_uniform,
                    new double[] { md.x1, md.x2, md.x3 }, integral_res);

            if (ret != 0)
            {
                return ret;
            }

            return 0;
        }
    }
}
