using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            /* TextBlock nouvelleTextBlock = new TextBlock();
            nouvelleTextBlock.Height = 20;
            nouvelleTextBlock.Width = 875;
            nouvelleTextBlock.Text = "Faire les courses";
            nouvelleTextBlock.FontFamily = new FontFamily("Global Sans Serif");
            nouvelleTextBlock.VerticalAlignment = VerticalAlignment.Center;
            nouvelleTextBlock.FontWeight = FontWeights.Bold;
            nouvelleTextBlock.Margin = new Thickness(20, 0, 0, 0);
            nouvelleTextBlock.Padding = new Thickness(5, 0, 0, 0);
            nouvelleTextBlock.Background = new LinearGradientBrush(Color.FromRgb(185, 162, 232), Color.FromRgb(250, 162, 232), 0);
            nouvelleTextBlock.FontSize = 16;
            nouvelleTextBlock.TextDecorations = new TextDecorationCollection();

            listBox1.Items.Add(nouvelleTextBlock); */

            var taskWindow = new TaskWindow();
            taskWindow.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.Items.IndexOf(listBox1.SelectedItem));
        }
    }
}
