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
    /// Logique d'interaction pour la vue <c>LoginView</c>
    /// </summary>
    public partial class LoginView : Window
    {
        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Initialises the view, the data context and the <c>CloseAction</c> feature
        /// </summary>
        public LoginView()
        {
            InitializeComponent();
            LoginViewModel loginViewModel = new LoginViewModel();
            this.DataContext = loginViewModel;
            if (loginViewModel.CloseAction == null)
                loginViewModel.CloseAction = new Action(this.Close);
        }

        #endregion
    }
}
