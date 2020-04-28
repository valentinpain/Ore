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

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
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
        }

        public void Windowloading(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            int index = (date.ToString()).IndexOf('/');
            string month = date.ToString().Substring(index + 1, 2);

            string actualMonth;

            switch (month)
            {
                case "01":
                    actualMonth = "JANVIER";
                    break;
                case "02":
                    actualMonth = "FÉVRIER";
                    break;
                case "03":
                    actualMonth = "MARS";
                    break;
                case "04":
                    actualMonth = "AVRIL";
                    break;
                case "05":
                    actualMonth = "MAI";
                    break;
                case "06":
                    actualMonth = "JUIN";
                    break;
                case "07":
                    actualMonth = "JUILLET";
                    break;
                case "08":
                    actualMonth = "AOÛT";
                    break;
                case "09":
                    actualMonth = "SEPTEMBRE";
                    break;
                case "10":
                    actualMonth = "OCTOBRE";
                    break;
                case "11":
                    actualMonth = "NOVEMBRE";
                    break;
                case "12":
                    actualMonth = "DECEMBRE";
                    break;
                default:
                    actualMonth = "error";
                    break;
            }

            ShellViewModel.retrieveDataFromModel(actualMonth);
        }
    }
}
