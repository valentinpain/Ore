using Ore.Models;
using Ore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    public class LoginAccessCommand : ICommand
    {
        private LoginViewModel loginViewModel;

        public event EventHandler CanExecuteChanged;

        public LoginViewModel LoginViewModel
        {
            get { return loginViewModel; }
            set { loginViewModel = value; }
        }

        private ShellView shellView = new ShellView();

        public ShellView ShellView
        {
            get { return shellView; }
            set { shellView = value; }
        }

        public LoginAccessCommand(LoginViewModel loginViewModel)
        {
            this.LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;

            if(passwordBox != null)
            {
                if (!passwordBox.Password.Equals(""))
                    LoginViewModel.User.Password = passwordBox.Password;
                else
                    LoginViewModel.User.Password = "";

                int userId = LoginModel.findUserId(LoginViewModel.User.Username, LoginViewModel.User.Password);

                if (userId > 0)
                {
                    LoginViewModel.User.Id = userId;
                    ShellViewModel.TaskIdUser = userId;
                    loginViewModel.CloseAction();

                    shellView.Show();
                }
                else
                    loginViewModel.WrongInformations = "Visible";
            }
            
        }
    }
}
