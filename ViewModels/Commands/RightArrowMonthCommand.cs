using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    public class RightArrowMonthCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ShellViewModel shellViewModel;

        public ShellViewModel ShellViewModel
        {
            get { return shellViewModel; }
            set { shellViewModel = value; }
        }


        public RightArrowMonthCommand(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        } 

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            shellViewModel.changeMonthUp();
            shellViewModel.Tasks.Clear();
            ShellViewModel.BitOfDay = 0;
            ShellViewModel.DaysInMonth = shellViewModel.ArrayOfDaysInMonth(shellViewModel.convertMonthInt(ShellViewModel.TaskMonth));

            string[] monthYearSplitted = shellViewModel.ActualMonthYear.Split(' ');

            ShellViewModel.retrieveDataFromModel(monthYearSplitted[0]);
        }
    }
}
