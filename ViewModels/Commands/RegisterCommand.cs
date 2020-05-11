using Ore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    /// <summary>
    /// The command that registers a new user
    /// </summary>
    public class RegisterCommand : ICommand
    {
        #region Properties 

        /// <summary>
        /// The attribute that helps us to communicate with the behavior of the view <c>RegisterView</c>
        /// </summary>
        private RegisterViewModel registerViewModel;
        public RegisterViewModel RegisterViewModel
        {
            get { return registerViewModel; }
            set { registerViewModel = value; }
        }

        /// <summary>
        /// The event of the <c>ICommand</c> interface which fires the command event
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialises our <c>RegisterViewModel</c> attribute so we can communicate with him and the <c>RegisterView</c>
        /// </summary>
        /// <param name="shellViewModel">The actual used view-model</param>
        public RegisterCommand(RegisterViewModel registerViewModel)
        {
            this.registerViewModel = registerViewModel;
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
        /// Here, we are registering a new user
        /// </remarks>
        public void Execute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;

            // Binding the password in the view to the behavior code
            if (!passwordBox.Password.Equals(""))
                RegisterViewModel.User.Password = passwordBox.Password;
            else
                RegisterViewModel.User.Password = "";

            // Check whether the user has already been created or not
            if (!RegisterModel.isUserAlreadyCreated(registerViewModel.User.Username, RegisterViewModel.User.Password))
            {
                if (!RegisterViewModel.User.Username.Equals("") && !RegisterViewModel.User.Password.Equals(""))
                {
                    registerViewModel.TextInformations = "Création du compte réussie !";
                    registerViewModel.WrongInformations = "Visible";

                    // Adding the new account in the database
                    RegisterModel.InsertNewAccountInDatabase(registerViewModel.LastUserId, RegisterViewModel.User.Username, RegisterViewModel.User.Password);
                    registerViewModel.LastUserId++;
                }
                else
                {
                    // Warning that the informations filled are incorrects
                    registerViewModel.TextInformations = "*** Informations incorrectes ***";
                    registerViewModel.WrongInformations = "Visible";
                }
            }
            else 
            {
                // Warning that there is already an account with those same informations
                registerViewModel.TextInformations = "Ce compte existe déjà";
                registerViewModel.WrongInformations = "Visible";
            }
        }

        #endregion
    }
}
