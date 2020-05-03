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

        public ShellViewModel ShellViewModel
        {
            get { return shellViewModel = new ShellViewModel(); }
            set { shellViewModel = value; }
        }

        public TaskCommand(ShellViewModel shellViewModel)    
        {
            this.shellViewModel = shellViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(shellViewModel.TaskId == 0)
            {
                shellViewModel.TaskId = ShellModel.lastRowNumber() + 1;
            }

            if(!shellViewModel.TaskName.Equals(""))
            {
                if(shellViewModel.TaskColor == null)
                    shellViewModel.TaskColor = "#FFFFFFFF";

                string[] dayNameSplitted = ShellViewModel.ChosenDate.Split(' ');

                shellViewModel.Tasks.Add(new TaskViewModel() { id = shellViewModel.TaskId,
                                                               name = shellViewModel.TaskName,
                                                               description = shellViewModel.TaskDescription,
                                                               color = shellViewModel.TaskColor,
                                                               startDay = ShellViewModel.ChosenDate,
                                                               finishDay = TaskViewModel.formatDay(shellViewModel.TaskFinishDay),
                                                               startTime = TaskViewModel.formatTime(shellViewModel.TaskStartTime),
                                                               finishTime = TaskViewModel.formatTime(shellViewModel.TaskFinishTime),
                                                               month = shellViewModel.TaskStartMonth,
                                                               year = shellViewModel.TaskYear,
                                                               isComplete = false,
                                                               useId = shellViewModel.TaskIdUser
                });

                shellViewModel.TaskName = "";
                shellViewModel.TaskDescription = "";
                shellViewModel.TaskColor = "#FFFFFFFF";
                shellViewModel.TaskFinishDay = "";
                shellViewModel.TaskStartTime = "";
                shellViewModel.TaskFinishTime = "";
                shellViewModel.TaskStartMonth = "";
                shellViewModel.TaskYear = "";

                ShellModel.addTaskToDatabase(shellViewModel.Tasks[shellViewModel.Tasks.Count - 1]);
                shellViewModel.TaskId++;
                shellViewModel.ToDoNowTasks = ShellViewModel.SortTasks(ShellModel.retrieveAllTasks(1));
            }
        }
    }
}
