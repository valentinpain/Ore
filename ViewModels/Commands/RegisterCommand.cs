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
    public class RegisterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private RegisterViewModel registerViewModel;

        public RegisterViewModel RegisterViewModel
        {
            get { return registerViewModel; }
            set { registerViewModel = value; }
        }


        public RegisterCommand(RegisterViewModel registerViewModel)
        {
            this.registerViewModel = registerViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;

            if (!passwordBox.Password.Equals(""))
                RegisterViewModel.User.Password = passwordBox.Password;
            else
                RegisterViewModel.User.Password = "";

            if(!RegisterViewModel.User.Username.Equals("") && !RegisterViewModel.User.Password.Equals(""))
            {
                registerViewModel.TextInformations = "Création du compte réussie !";
                registerViewModel.WrongInformations = "Visible";
                RegisterModel.InsertNewAccountInDatabase(registerViewModel.LastUserId, RegisterViewModel.User.Username, RegisterViewModel.User.Password);
                registerViewModel.LastUserId++;
            }
            else
                registerViewModel.WrongInformations = "Visible";
        }
    }
}
