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
    /// The command that deletes a task
    /// </summary>
    public class DeleteATaskCommand : ICommand
    {
        #region Properties

        /// <summary>
        /// The attribute that helps us to communicate with the behavior of the view <c>ShellView</c>
        /// </summary>
        private ShellViewModel shellViewModel = new ShellViewModel();
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
        public DeleteATaskCommand(ShellViewModel shellViewModel)
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
        /// Here, we are deletting a task
        /// </remarks>
        public void Execute(object parameter)
        {
            // Check if the task belongs to a day
            foreach (TaskViewModel task in ShellViewModel.Tasks)
            {
                if (int.Parse(task.Id.ToString()) == int.Parse(parameter.ToString()))
                {
                    ShellViewModel.Tasks.Remove(task);
                    ShellModel.deleteTask(task.Id);
                    ShellViewModel.SortTasks(ShellModel.retrieveAllTasks(LoginViewModel.User.Id));
                    break;
                }
            }

            // Check if the task belongs to a list
            foreach (TaskViewModel task in ShellViewModel.FocusedList)
            {
                if (int.Parse(task.Id.ToString()) == int.Parse(parameter.ToString()))
                {
                    ShellViewModel.FocusedList.Remove(task);
                    ShellModel.deleteTask(task.Id);
                    break;
                }
            }
        }

        #endregion

    }
}
