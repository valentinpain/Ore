using Ore.Models;
using Ore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    public class LoadDayViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ShellViewModel shellViewModel;

        private static int memoryColorNumber = 0;

        public static int MemoryColorNumber
        {
            get { return memoryColorNumber = 0; }
            set { memoryColorNumber = value; }
        }


        public ShellViewModel MyPrShellViewModeloperty
        {
            get { return shellViewModel; }
            set { shellViewModel = value; }
        }

        public LoadDayViewCommand(ShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var values = (object[])parameter;
            Random random = new Random();
            int randomColorNumber = 0;

            ShellView shellview = values[1] as ShellView;

            if (shellview.stackName.Children.Count == 1)
                shellview.stackName.Children.RemoveAt(0);

            ShellViewModel.ChosenDate = values[0].ToString();

            shellViewModel.Tasks = ShellModel.retrieveDataFromDatabase(ShellViewModel.ChosenDate, shellViewModel.TaskStartMonth, shellViewModel.TaskYear);

            randomColorNumber = random.Next(1, 9);

            while (randomColorNumber == memoryColorNumber)
            {
                randomColorNumber = random.Next(1, 6);
            }

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

            DayView dayView = new DayView();
            dayView.Height = 520;
            dayView.Width = 910;

            shellview.stackName.Visibility = Visibility.Visible;
            shellview.MainGrid.Visibility = Visibility.Collapsed;

            shellview.stackName.Children.Add(dayView);
        }
    }
}
