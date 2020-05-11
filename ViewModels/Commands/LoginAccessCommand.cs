using Ore.Models;
using Ore.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    /// <summary>
    /// The command that logs the user in the application
    /// </summary>
    public class LoginAccessCommand : ICommand
    {
        #region Properties

        /// <summary>
        /// The attribute that helps us to communicate with the behavior of the view <c>ShellView</c>
        /// </summary>
        private ShellView shellView = new ShellView();
        public ShellView ShellView
        {
            get { return shellView; }
            set { shellView = value; }
        }

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
        /// The event of the <c>ICommand</c> interface which fires the command event
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialises our <c>LoginViewModel</c> attribute so we can communicate with him and the <c>LoginView</c>
        /// </summary>
        /// <param name="shellViewModel">The actual used view-model</param>
        public LoginAccessCommand(LoginViewModel loginViewModel)
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
        /// Here, we are logging in the user
        /// </remarks>
        public void Execute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;

            if(passwordBox != null)
            {
                // Binding the password in the view to his property in the behavior code
                if (!passwordBox.Password.Equals(""))
                    LoginViewModel.User.Password = passwordBox.Password;
                else
                    LoginViewModel.User.Password = "";

                // Checking whether the user exists or not
                int userId = LoginModel.findUserId(LoginViewModel.User.Username, LoginViewModel.User.Password);

                if (userId > 0)
                {
                    // Setting the user informations in our code
                    LoginViewModel.User.Id = userId;
                    ShellViewModel.TaskIdUser = userId;

                    // Retrieving all the task that belongs to the connected user
                    ShellViewModel.SortTasks(ShellModel.retrieveAllTasks(LoginViewModel.User.Id));

                    // Retrieving all the lists that belongs to the connected user
                    ObservableCollection<ListViewModel> lists = ShellModel.retrieveAllLists(LoginViewModel.User.Id);

                    foreach (ListViewModel list in lists)
                        ShellViewModel.Lists.Add(list);

                    // Closing the login view
                    loginViewModel.CloseAction();

                    // Displaying the shell view
                    shellView.Show();
                }
                else
                    // Warning that the login informations are incorrects
                    loginViewModel.WrongInformations = "Visible";
            }
            
        }

        #endregion

    }
}
