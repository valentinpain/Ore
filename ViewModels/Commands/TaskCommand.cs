using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    public class TaskCommand : ICommand
    {
        public ShellViewModel ViewModel { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is ShellViewModel taskList)
            {
                taskList.Tasks.Add(new TaskViewModel() { _name = taskList.TaskName, _complete = false, _day = taskList.TaskDay, _time = taskList.TaskTime, _color = taskList.TaskColor });
            }
        }
    }
}
