using Ore.Models;
using Ore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ore.Views
{
    /// <summary>
    /// Logique d'interaction pour ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
           var shellView = new ShellViewModel();
           this.DataContext = shellView;
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /* private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;

            ButtonDefaultView.Foreground = Brushes.LightGray;
            ButtonDailyView.Foreground = Brushes.LightGray;
            ButtonMonthlyView.Foreground = Brushes.LightGray;

            GridMain.Opacity = 20;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;

            ButtonDefaultView.Foreground = Brushes.White;
            ButtonDailyView.Foreground = Brushes.White;
            ButtonMonthlyView.Foreground = Brushes.White;
        } */

        public void Windowloading(object sender, RoutedEventArgs e)
        {
           //ShellViewModel.ToDoNowTasks = ShellViewModel.SortTasks();
        }

        private void ButtonDay_Click(object sender, RoutedEventArgs e)
        {
            if (stackName.Children.Count == 1)
                stackName.Children.RemoveAt(0);

            string[] clickedButtonSplitted = sender.ToString().Split(' ');

            ShellViewModel.ChosenDate = clickedButtonSplitted[1] + " " + clickedButtonSplitted[2];

            DayView dayView = new DayView();
            dayView.Height = 520;
            dayView.Width = 910;

            stackName.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Collapsed;

            stackName.Children.Add(dayView);
        }
    }
}
