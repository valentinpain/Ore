using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Ore.ViewModels.Converters
{
    /// <summary>
    /// The converter that returns an array of values for the list view
    /// </summary>
    public class LoadListViewConverter : IMultiValueConverter
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
        /// Here, we return an array of cloned values
        /// </remarks>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        /// <summary>
        /// The method used to convert the value back to normal
        /// </summary>
        /// <param name="value">The value converted</param>
        /// <param name="targetType">The type of the binding target property</param>
        /// <param name="parameter">The converter parameter to use</param>
        /// <param name="culture">The culture to use in the converter</param>
        /// <returns>A converted back value. If the method returns null, the valid null value is used</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
