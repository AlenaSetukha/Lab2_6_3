using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace WpfApp1
{
    class FullConverter: IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)//из MeasureData в строку
        {
            try
            {
                string res = value[0].ToString() + " " +
                    value[1].ToString() + " " + value[2].ToString() + " " +
                    value[3].ToString() + " " + value[4].ToString();
                return res;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка конвертации FullConverter.Convert");
                object res = "0 " + "0 " + "0" + "0" + "0";
                return res;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)//из строки в Meausure
        {
            try
            {
                string s = value as string;
                string[] words = s.Split(' ');
                object[] res = new object[5];
                res[0] = double.Parse(words[0]);//a
                res[1] = double.Parse(words[1]);//b
                res[2] = double.Parse(words[2]);//x1
                res[3] = double.Parse(words[3]);//x2
                res[4] = double.Parse(words[4]);//x3
                return res;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка FullConverter.Convertback");
                object[] res = new object[5];
                res[0] = 0.0;
                res[1] = 10.0;
                res[2] = 2.0;
                res[3] = 4.0;
                res[4] = 6.0;
                return res;
            }
        }
    }
}
