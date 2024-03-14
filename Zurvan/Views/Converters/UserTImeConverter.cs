using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Zurvan.Core.TimeModels;

namespace Zurvan.ClientApp.Views.Converters
{
    public class UserTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string weekday = (string)parameter;

                List<DateTimeData> dateTime = (List<DateTimeData>)value;

                return dateTime.Single(x => x.WeekDay == weekday).TimeUsed;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}