using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Ore.ViewModels.Converters
{
    /// <summary>
    /// The converter that colors the actual day
    /// </summary>
    public class ActualDayColorConverter : IValueConverter
    {
        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// The method used to convert the value
        /// </summary>
        /// <param name="value">The value converted</param>
        /// <param name="targetType">The type of the binding target property</param>
        /// <param name="parameter">The converter parameter to use</param>
        /// <param name="culture">The culture to use in the converter</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used</returns>
        /// <remarks>
        /// Here, we return a color
        /// </remarks>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (bool.Parse(value.ToString()))
                return Brushes.Orange;

            return "White";
        }

        /// <summary>
        /// The method used to convert the value back to normal
        /// </summary>
        /// <param name="value">The value converted</param>
        /// <param name="targetType">The type of the binding target property</param>
        /// <param name="parameter">The converter parameter to use</param>
        /// <param name="culture">The culture to use in the converter</param>
        /// <returns>A converted back value. If the method returns null, the valid null value is used</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
