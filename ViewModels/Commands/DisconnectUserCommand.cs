using Ore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    public class DisconnectUserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ShellViewModel shellViewModel;
        public ShellViewModel ShellViewModel
        {
            get { return shellViewModel; }
            set { shellViewModel = value; }
        }

        public DisconnectUserCommand(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ShellView shellView = parameter as ShellView;
            shellView.Visibility = System.Windows.Visibility.Hidden;

            LoginView loginView = new LoginView();
            loginView.Show();
        }
    }
}
