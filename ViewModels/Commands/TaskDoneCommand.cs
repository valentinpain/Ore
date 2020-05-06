using Ore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    public class TaskDoneCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ShellViewModel shellViewModel;

        public ShellViewModel ShellViewModel
        {
            get { return shellViewModel; }
            set { shellViewModel = value; }
        }

        public TaskDoneCommand(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            foreach (TaskViewModel task in ShellViewModel.Tasks)
            {
                if (int.Parse(task.id.ToString()) == int.Parse(parameter.ToString()) && !ShellModel.isChecked(int.Parse(parameter.ToString())))
                {
                    ShellModel.checkTask(task.id);
                    ShellViewModel.SortTasks(ShellModel.retrieveAllTasks(LoginViewModel.User.Id));
                    break;
                }
                else
                {
                    ShellModel.unCheckTask(task.id);
                    ShellViewModel.SortTasks(ShellModel.retrieveAllTasks(LoginViewModel.User.Id));
                    break;
                }
            }
        }
    }
}
