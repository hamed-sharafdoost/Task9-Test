using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Task9.ValueConverter
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateTime)
            {
                DateTime date = (DateTime)value;
                PersianCalendar pc = new PersianCalendar();
                return string.Format("{0}/{1}/{2} {3}:{4}:{5}", pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date),
                                                pc.GetHour(date), pc.GetMinute(date), pc.GetSecond(date));
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
