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

            loginViewModel.User.Password = passwordBox.Password;
            loginViewModel.CloseAction();
            shellView.Show();
            
        }
    }
}
