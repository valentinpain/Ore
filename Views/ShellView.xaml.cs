using Ore.Models;
using Ore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ore.Views
{
    /// <summary>
    /// Logique d'interaction de la vue <c>ShellView</c>
    /// </summary>
    public partial class ShellView : Window
    {
        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Initialises the view and his data context
        /// </summary>
        public ShellView()
        {
            InitializeComponent();
            var shellView = new ShellViewModel();
            this.DataContext = shellView;
        }

        /// <summary>
        /// The enter hover effect on day buttons
        /// </summary>
        /// <param name="sender">The enter-hovered button</param>
        /// <param name="e">The event object</param>
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;

            if (border.Background != Brushes.Orange)
                border.Background = Brushes.AliceBlue;
        }

        /// <summary>
        /// The exit hover effect on day buttons
        /// </summary>
        /// <param name="sender">The exit-hovered button</param>
        /// <param name="e">The event object</param>
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;

            if (border.Background != Brushes.Orange)
                border.Background = Brushes.White;
        }

        #endregion
    }
}
