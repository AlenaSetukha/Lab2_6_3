using System;
using System.ComponentModel;
using ClassLibrary1;

namespace WpfApp1
{
    public class ViewData
    {
        public SplinesData s_data { get; set; }
        public ChartData char_data { get; set; }
        public bool Input_Error { get; set; }//индикатор ошибки ввода


        public ViewData()
        {
            Input_Error = false;
            MeasuredData md_def = new(10, -2.0, 15.0, SPf.linear, 25, 2, 6, 9);//значение по умолчанию
            s_data = new(md_def);
            s_data.md.MeasureData_fill();
            char_data = new(s_data.md.nodes_non_uniform);
        }

        // Event handling
        private void InputChanged(object sender, PropertyChangedEventArgs e)
        {
            Input_Error = false;
        }
    }
}
