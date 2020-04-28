using Ore.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Ore.ViewModels.Converter
{
    public class ActualDayColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string test1 = (ShellViewModel.DateTimeToString(DateTime.Now)).ToString();
            string test2 = value.ToString().Substring(4, 2);

            if(test1 == test2)
            {
                return "#FF673AB7";
            }

            return "Black";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
