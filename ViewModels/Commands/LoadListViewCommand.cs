using Ore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ore.ViewModels.Commands
{
    public class LoadListViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private static int memoryColorNumber = 0;

        public static int MemoryColorNumber
        {
            get { return memoryColorNumber = 0; }
            set { memoryColorNumber = value; }
        }

        private ShellViewModel shellViewModel;

        public ShellViewModel ShellViewModel
        {
            get { return shellViewModel; }
            set { shellViewModel = value; }
        }

        public LoadListViewCommand(ShellViewModel shellViewModel)
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

            ShellView shellView = values[1] as ShellView;
            ListViewModel.FocusedName = values[0].ToString();

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

            ListView listView = new ListView();
            listView.Height = 520;
            listView.Width = 910;

            shellView.stackList.Children.Add(listView);

            shellView.stackDay.Visibility = System.Windows.Visibility.Collapsed;
            shellView.stackList.Visibility = System.Windows.Visibility.Visible;
            shellView.MainGrid.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
