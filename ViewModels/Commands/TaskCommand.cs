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

            if(shellViewModel.TaskName != null && shellViewModel.TaskFinishDay != null && shellViewModel.TaskStartTime != null && shellViewModel.TaskFinishTime != null && shellViewModel.TaskFinishDay != null)
            {
                if (shellViewModel.TaskColor == null)
                    shellViewModel.TaskColor = "#FFFFFFFF";

                if (shellViewModel.TaskDescription == null)
                    shellViewModel.TaskDescription = "";

                string[] dayNameSplitted = ShellViewModel.ChosenDate.Split(' ');

                string[] finishDateSplittedByDay = shellViewModel.TaskFinishDay.Split(' ');
                string[] finishDayFormatted = TaskViewModel.setDate(finishDateSplittedByDay[0]);

                shellViewModel.TaskFinishDay = finishDayFormatted[0];
                shellViewModel.TaskFinishMonth = finishDayFormatted[1];
                shellViewModel.TaskFinishYear = finishDayFormatted[2];

                shellViewModel.addTask();

                shellViewModel.TaskName = shellViewModel.TaskDescription = shellViewModel.TaskFinishDay = shellViewModel.TaskStartTime = shellViewModel.TaskFinishTime = shellViewModel.TaskStartMonth = shellViewModel.TaskStartYear = null;
                shellViewModel.TaskColor = "#FFFFFFFF";

                ShellModel.addTaskToDatabase(shellViewModel.Tasks[shellViewModel.Tasks.Count - 1]);
                shellViewModel.TaskId++;
                ShellViewModel.SortTasks(ShellModel.retrieveAllTasks(LoginViewModel.User.Id));
                shellViewModel.WrongInformations = false;
            }
            else
            {
                shellViewModel.TextInformations = "Certains champs obligatoires sont vides";
                shellViewModel.WrongInformations = true;
            }
        }
    }
}
