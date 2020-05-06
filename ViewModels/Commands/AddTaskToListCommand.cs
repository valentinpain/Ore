using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    public class AddTaskToListCommand : ICommand
    {
        private ShellViewModel shellViewModel;

        public ShellViewModel ShellViewModel
        {
            get { return shellViewModel; }
            set { shellViewModel = value; }
        }

        public event EventHandler CanExecuteChanged;

        public AddTaskToListCommand(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (shellViewModel.TaskId == 0)
            {
                //shellViewModel.TaskIdList = ShellModel.lastRowNumber() + 1;
            }

            if (shellViewModel.TaskName != null && shellViewModel.TaskFinishDay != null && shellViewModel.TaskStartTime != null && shellViewModel.TaskFinishTime != null && shellViewModel.TaskStartDay != null && shellViewModel.TaskFinishDay != null)
            {
                if (shellViewModel.TaskColor == null)
                    shellViewModel.TaskColor = "#FFFFFFFF";

                if (shellViewModel.TaskDescription == null)
                    shellViewModel.TaskDescription = "";

                string[] startDateSplittedByDay = shellViewModel.TaskStartDay.Split(' ');
                string[] startDayFormatted = TaskViewModel.setDate(startDateSplittedByDay[0]);

                shellViewModel.TaskStartDay = startDayFormatted[0];
                shellViewModel.TaskStartMonth = startDayFormatted[1];
                shellViewModel.TaskStartYear = startDayFormatted[2];

                string[] finishDateSplittedByDay = shellViewModel.TaskFinishDay.Split(' ');
                string[] finishDayFormatted = TaskViewModel.setDate(finishDateSplittedByDay[0]);

                shellViewModel.TaskFinishDay = finishDayFormatted[0];
                shellViewModel.TaskFinishMonth = finishDayFormatted[1];
                shellViewModel.TaskFinishYear = finishDayFormatted[2];

                foreach (ListViewModel list in ShellViewModel.Lists)
                {
                    ShellViewModel.FocusedList.Add(new TaskViewModel()
                    {
                        id = shellViewModel.TaskId,
                        name = shellViewModel.TaskName,
                        description = shellViewModel.TaskDescription,
                        color = shellViewModel.TaskColor,
                        startDay = shellViewModel.TaskStartDay,
                        finishDay = shellViewModel.TaskFinishDay,
                        startTime = TaskViewModel.formatTime(shellViewModel.TaskStartTime),
                        finishTime = TaskViewModel.formatTime(shellViewModel.TaskFinishTime),
                        startMonth = shellViewModel.TaskStartMonth,
                        finishMonth = shellViewModel.TaskFinishMonth,
                        startYear = shellViewModel.TaskStartYear,
                        finishYear = shellViewModel.TaskFinishYear,
                        isComplete = false,
                        useId = LoginViewModel.User.Id
                    });
                }

                shellViewModel.TaskName = shellViewModel.TaskDescription = shellViewModel.TaskFinishDay = shellViewModel.TaskStartTime = shellViewModel.TaskFinishTime = shellViewModel.TaskStartMonth = shellViewModel.TaskStartYear = null;
                shellViewModel.TaskColor = "#FFFFFFFF";

                //ShellModel.addTaskToDatabase(shellViewModel.Tasks[shellViewModel.Tasks.Count - 1]);
                shellViewModel.TaskId++;
                //ShellViewModel.SortTasks(ShellModel.retrieveAllTasks(LoginViewModel.User.Id));
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
