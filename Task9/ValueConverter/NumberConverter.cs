using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Task9.ValueConverter
{
    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
            {
                return string.Empty;
            }
            else
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string number = (string)value;
            string numbers = "0123456789";
            foreach (var digit in number)
            {
                if (!numbers.Contains(digit))
                {
                    return 0;
                }
            }
            if (!string.IsNullOrEmpty(number))
            {
                return Int64.Parse(number);
            }
            else
                return 0;
        }
    }
}
