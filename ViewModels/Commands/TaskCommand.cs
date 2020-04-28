using Ore.Models;
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
        private ShellViewModel shellViewModel = new ShellViewModel();
        private static int taskNumber = 0;

        public static int TaskNumber
        {
            get { return taskNumber; }
            set { taskNumber = value; }
        }


        public ShellViewModel ShellViewModel
        {
            get { return shellViewModel = new ShellViewModel(); }
            set { shellViewModel = value; }
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(TaskNumber == 0)
            {
                TaskNumber = ShellModel.lastRowNumber() + 1;
            }

            if(parameter is ShellViewModel taskList && taskList.TaskName != null)
            {
                if(taskList.TaskColor == null)
                {
                    taskList.TaskColor = "#FFFFFFFF";
                }

                taskList.Tasks.Add(new TaskViewModel() { _name = taskList.TaskName, _complete = false, _day = taskList.TaskDay, _time = taskList.TaskTime, _color = taskList.TaskColor, _id = TaskNumber, _month = taskList.TaskMonth });
                ShellModel.addTaskToDatabase(taskList.Tasks[taskList.Tasks.Count - 1]);
                TaskNumber++;
            }
        }
    }
}
