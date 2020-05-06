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

        private void ButtonDay_Click(object sender, RoutedEventArgs e)
        {
            if (stackDay.Children.Count == 1)
                stackDay.Children.RemoveAt(0);

            string[] clickedButtonSplitted = sender.ToString().Split(' ');

            ShellViewModel.ChosenDate = clickedButtonSplitted[1] + " " + clickedButtonSplitted[2];

            DayView dayView = new DayView();
            dayView.Height = 520;
            dayView.Width = 910;

            stackDay.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Collapsed;

            stackDay.Children.Add(dayView);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;

            if (border.Background != Brushes.Orange)
                border.Background = Brushes.AliceBlue;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;

            if (border.Background != Brushes.Orange)
                border.Background = Brushes.White;
        }
    }
}
