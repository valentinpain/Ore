using Ore.Models;
using Ore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    public class LoadHomeViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ShellViewModel shellViewModel;

        public ShellViewModel ShellViewModel
        {
            get { return shellViewModel; }
            set { shellViewModel = value; }
        }

        public LoadHomeViewCommand(ShellViewModel shellViewModel)
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

            shellView.stackName.Visibility = System.Windows.Visibility.Collapsed;
            shellView.MainGrid.Visibility = System.Windows.Visibility.Visible;

            shellViewModel.ToDoNowTasks = ShellViewModel.SortTasks(ShellModel.retrieveAllTasks(1));
        }
    }
}
