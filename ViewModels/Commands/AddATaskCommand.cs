using Ore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    /// <summary>
    /// The commands that add a task for the day and the connected user
    /// </summary>
    public class AddATaskCommand : ICommand
    {
        #region Properties

        /// <summary>
        /// The attribute that helps us to communicate with the behavior of the view <c>ShellView</c>
        /// </summary>
        private ShellViewModel shellViewModel = new ShellViewModel();
        public ShellViewModel ShellViewModel
        {
            get { return shellViewModel = new ShellViewModel(); }
            set { shellViewModel = value; }
        }

        /// <summary>
        /// The event of the <c>ICommand</c> interface which fires the command event
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialises our <c>ShellViewModel</c> attribute so we can communicate with him and the <c>ShellView</c>
        /// </summary>
        /// <param name="shellViewModel">The actual used view-model</param>
        public AddATaskCommand(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the command will be executed or not
        /// </summary>
        /// <param name="parameter">Data used by the command</param>
        /// <returns>true if this command can be executed, false otherwise</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// The actions needed to be executed when the command occurs
        /// </summary>
        /// <param name="parameter">The parameter sent by the view to help the command to process if needed</param>
        /// <remarks>
        /// Here, we are adding a task for the day and the connected user
        /// </remarks>
        public void Execute(object parameter)
        {
            // Updating the last id for a new task
            if (shellViewModel.TaskId == 0)
                shellViewModel.TaskId = ShellModel.lastRowTaskNumber() + 1;

            // Verifying if all the necessary fields are filled
            if (shellViewModel.TaskName != null && shellViewModel.TaskFinishDay != null && shellViewModel.TaskStartTime != null && shellViewModel.TaskFinishTime != null && shellViewModel.TaskFinishDay != null)
            {
                if (shellViewModel.TaskColor == null)
                    shellViewModel.TaskColor = "#FFFFFFFF";

                if (shellViewModel.TaskDescription == null)
                    shellViewModel.TaskDescription = "";

                string[] dayNameSplitted = ShellViewModel.ChosenDate.Split(' ');

                // Formatting the data
                string[] finishDateSplittedByDay = shellViewModel.TaskFinishDay.Split(' ');
                string[] finishDayFormatted = TaskViewModel.setDate(finishDateSplittedByDay[0]);

                shellViewModel.TaskFinishDay = finishDayFormatted[0];
                shellViewModel.TaskFinishMonth = finishDayFormatted[1];
                shellViewModel.TaskFinishYear = finishDayFormatted[2];

                shellViewModel.addTask();

                // Resetting all the fields
                shellViewModel.TaskName = shellViewModel.TaskDescription = shellViewModel.TaskFinishDay = shellViewModel.TaskStartTime = shellViewModel.TaskFinishTime = shellViewModel.TaskStartMonth = shellViewModel.TaskStartYear = null;
                shellViewModel.TaskColor = "#FFFFFFFF";

                // Adding the task in the database
                ShellModel.addTaskToDatabase(shellViewModel.Tasks[shellViewModel.Tasks.Count - 1]);

                shellViewModel.TaskId++;

                // Sorting the most urgent tasks to actualise the home view
                ShellViewModel.SortTasks(ShellModel.retrieveAllTasks(LoginViewModel.User.Id));

                shellViewModel.WrongInformations = false;
            }
            else
            {
                // Warning if the user did not provide all the informations for the required fields
                shellViewModel.TextInformations = "Certains champs obligatoires sont vides";
                shellViewModel.WrongInformations = true;
            }
        }

        #endregion
    }
}
