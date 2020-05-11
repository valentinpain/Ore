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
    /// The command that adds a task to a list the user created
    /// </summary>
    public class AddTaskToListCommand : ICommand
    {
        #region Properties

        /// <summary>
        /// The attribute that helps us to communicate with the behavior of the view <c>ShellView</c>
        /// </summary>
        private ShellViewModel shellViewModel;
        public ShellViewModel ShellViewModel
        {
            get { return shellViewModel; }
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
        public AddTaskToListCommand(ShellViewModel shellViewModel)
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
        /// Here, we are adding a task for the focused list the user created
        /// </remarks>
        public void Execute(object parameter)
        {
            // Updating the last id to not overriding the previous ones already in the database
            if (shellViewModel.TaskId == 0)
                shellViewModel.TaskId = ShellModel.lastRowTaskNumber() + 1;

            // Checking if the fields are filled
            if (shellViewModel.TaskName != null && shellViewModel.TaskFinishDay != null && shellViewModel.TaskStartTime != null && shellViewModel.TaskFinishTime != null && shellViewModel.TaskStartDay != null && shellViewModel.TaskFinishDay != null)
            {
                if (shellViewModel.TaskColor == null)
                    shellViewModel.TaskColor = "#FFFFFFFF";

                if (shellViewModel.TaskDescription == null)
                    shellViewModel.TaskDescription = "";

                // Formattig all the data provided
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

                // Adding a task to the list only in the code
                shellViewModel.addTaskToList();

                // Resetting all the fields of the form
                shellViewModel.TaskName = shellViewModel.TaskDescription = shellViewModel.TaskFinishDay = shellViewModel.TaskStartTime = shellViewModel.TaskFinishTime = shellViewModel.TaskStartMonth = shellViewModel.TaskStartYear = null;
                shellViewModel.TaskColor = "#FFFFFFFF";

                // Addind the task in the database
                ShellModel.addTaskToDatabase(ShellViewModel.FocusedList[ShellViewModel.FocusedList.Count - 1]);

                shellViewModel.TaskId++;

                shellViewModel.WrongInformations = false;
            }
            else
            {
                // Warning that all the fields are not all filled
                shellViewModel.TextInformations = "Certains champs obligatoires sont vides";
                shellViewModel.WrongInformations = true;
            }
        }

        #endregion

    }
}
