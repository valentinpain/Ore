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
    /// The command that loads the list view
    /// </summary>
    public class LoadListViewCommand : ICommand
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
        /// Helps us to randomise properly the header color
        /// </summary>
        private static int memoryColorNumber = 0;
        public static int MemoryColorNumber
        {
            get { return memoryColorNumber = 0; }
            set { memoryColorNumber = value; }
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
        public LoadListViewCommand(ShellViewModel shellViewModel)
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
        /// Here, we are loading the list view
        /// </remarks>
        public void Execute(object parameter)
        {
            var values = (object[])parameter;

            // The randomiser attributes
            Random random = new Random();
            int randomColorNumber = 0;

            ShellView shellView = values[1] as ShellView;
            ShellViewModel.FocusedListId = int.Parse(values[2].ToString());

            // Instantiating the list displayed
            ShellViewModel.FocusedList = ShellModel.retrieveTaskListFromDatabase(ShellViewModel.FocusedListId);

            ShellViewModel.FocusedListName = values[0].ToString();

            // Color randomiser
            randomColorNumber = random.Next(1, 9);

            while (randomColorNumber == memoryColorNumber)
                randomColorNumber = random.Next(1, 6);

            switch (randomColorNumber)
            {
                case 1:
                    ShellViewModel.RandomColor = "#0097e6";
                    memoryColorNumber = 1;
                    break;
                case 2:
                    ShellViewModel.RandomColor = "#8c7ae6";
                    memoryColorNumber = 2;
                    break;
                case 3:
                    ShellViewModel.RandomColor = "#e1b12c";
                    memoryColorNumber = 3;
                    break;
                case 4:
                    ShellViewModel.RandomColor = "#44bd32";
                    memoryColorNumber = 4;
                    break;
                case 5:
                    ShellViewModel.RandomColor = "#c23616";
                    memoryColorNumber = 5;
                    break;
                case 6:
                    ShellViewModel.RandomColor = "#40739e";
                    memoryColorNumber = 6;
                    break;
                case 7:
                    ShellViewModel.RandomColor = "#e67e22";
                    memoryColorNumber = 7;
                    break;
                case 8:
                    ShellViewModel.RandomColor = "#FDA7DF";
                    memoryColorNumber = 8;
                    break;
            }

            // Creating a new list view
            ListView listView = new ListView();
            listView.Height = 520;
            listView.Width = 910;

            // Removing the previous focused list
            if (shellView.stackList.Children.Count > 0)
                shellView.stackList.Children.RemoveAt(0);

            // Adding the view to his belonging stack
            shellView.stackList.Children.Add(listView);

            // Then displaying it
            shellView.stackDay.Visibility = System.Windows.Visibility.Collapsed;
            shellView.stackList.Visibility = System.Windows.Visibility.Visible;
            shellView.MainGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion
    }
}
