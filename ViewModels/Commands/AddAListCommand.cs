using Ore.Models;
using Ore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    /// <summary>
    /// The command that adds a list for the connected user
    /// </summary>
    public class AddAListCommand : ICommand
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
        /// The id of the last list created
        /// </summary>
        /// <remarks>
        /// Helps us to not overriding already used ids
        /// </remarks>
        private static int lastListId = ShellModel.lastListRowNumber();
        public static int LastListId
        {
            get { return lastListId; }
            set { lastListId = value; }
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
        public AddAListCommand(ShellViewModel shellViewModel)
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
        /// Here, we are adding list for the connected user
        /// </remarks>
        public void Execute(object parameter)
        {
            ShellView shellView = parameter as ShellView;

            shellView.addAListPanel.Visibility = System.Windows.Visibility.Collapsed;

            lastListId++;

            ShellViewModel.Lists.Add(new ListViewModel() { IdList = lastListId, TaskList = new System.Collections.ObjectModel.ObservableCollection<TaskViewModel>(), Name = ShellViewModel.FocusedListName, UserId = ShellViewModel.TaskIdUser });
            ShellModel.addListToDatabase(ShellViewModel.Lists[ShellViewModel.Lists.Count - 1]);
        }

        #endregion
    }
}
