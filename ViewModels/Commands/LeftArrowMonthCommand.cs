using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    public class LeftArrowMonthCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ShellViewModel shellViewModel;

        public ShellViewModel ShellViewModel
        {
            get { return shellViewModel; }
            set { shellViewModel = value; }
        }


        public LeftArrowMonthCommand(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }

        public LeftArrowMonthCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            shellViewModel.changeMonthDown();
            shellViewModel.Tasks.Clear();
            ShellViewModel.DaysInMonth = shellViewModel.ArrayOfDaysInMonth(shellViewModel.convertMonthInt(ShellViewModel.TaskMonth));

            string[] monthYearSplitted = shellViewModel.ActualMonthYear.Split(' ');
            ShellViewModel.retrieveDataFromModel(monthYearSplitted[0]);
        }
    }
}
