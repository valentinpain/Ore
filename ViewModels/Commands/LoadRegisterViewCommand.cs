using Ore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    /// <summary>
    /// The command that loads the register view
    /// </summary>
    public class LoadRegisterViewCommand : ICommand
    {
        #region Properties

        /// <summary>
        /// The attribute that helps us to communicate with the behavior of the view <c>LoginView</c>
        /// </summary>
        private LoginViewModel loginViewModel;
        public LoginViewModel LoginViewModel
        {
            get { return loginViewModel; }
            set { loginViewModel = value; }
        }

        /// <summary>
        /// The attribute that helps us to communicate with the behavior of the view <c>RegisterView</c>
        /// </summary>
        private RegisterView registerView = new RegisterView();
        public RegisterView RegisterView
        {
            get { return registerView; }
            set { registerView = value; }
        }

        /// <summary>
        /// The event of the <c>ICommand</c> interface which fires the command event
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialises our <c>LoginViewModel</c> attribute so we can communicate with him and the <c>LoginView</c>
        /// </summary>
        /// <param name="shellViewModel">The actual used view-model</param>
        public LoadRegisterViewCommand(LoginViewModel loginViewModel)
        {
            this.LoginViewModel = loginViewModel;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the command will be executed or not
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        /// <returns>true if this command can be executed, false otherwise</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// The actions needed to be executed when the command occurs
        /// </summary>
        /// <param name="parameter">The parameter sent by the view to help the command to process if needed</param>
        /// <remarks>
        /// Here, we are loading the register view
        /// </remarks>
        public void Execute(object parameter)
        {
            registerView.Show();
        }

        #endregion
    }
}
