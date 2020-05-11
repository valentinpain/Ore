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
    /// The command that set the task to done
    /// </summary>
    public class TaskDoneCommand : ICommand
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
        public TaskDoneCommand(ShellViewModel shellViewModel)
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
        /// Here, we are setting a task to done
        /// </remarks>
        public void Execute(object parameter)
        {
            // Checking whether the task belongs to a day
            foreach (TaskViewModel task in ShellViewModel.Tasks)
            {
                if (int.Parse(task.Id.ToString()) == int.Parse(parameter.ToString()) && !ShellModel.isChecked(int.Parse(parameter.ToString())))
                {
                    // If the task is unchecked then check it
                    ShellModel.checkTask(task.Id);
                    ShellViewModel.SortTasks(ShellModel.retrieveAllTasks(LoginViewModel.User.Id));
                    break;
                }
                else if(int.Parse(task.Id.ToString()) == int.Parse(parameter.ToString()) && ShellModel.isChecked(int.Parse(parameter.ToString())))
                {
                    // If the task is checked then uncheck it
                    ShellModel.unCheckTask(task.Id);
                    ShellViewModel.SortTasks(ShellModel.retrieveAllTasks(LoginViewModel.User.Id));
                    break;
                }
            }

            // Checking whether the task belongs to a list
            foreach (TaskViewModel task in ShellViewModel.FocusedList)
            {
                if (int.Parse(task.Id.ToString()) == int.Parse(parameter.ToString()) && !ShellModel.isChecked(int.Parse(parameter.ToString())))
                {
                    // If the task is unchecked then check it
                    ShellModel.checkTask(task.Id);
                    break;
                }
                else
                {
                    // If the task is checked then uncheck it
                    ShellModel.unCheckTask(task.Id);
                    break;
                }
            }
        }

        #endregion
    }
}
