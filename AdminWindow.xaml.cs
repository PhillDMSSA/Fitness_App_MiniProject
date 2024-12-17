using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Fitness_App
{
    public partial class AdminWindow : Window
    {
        private List<Dictionary<string, object>> Profiles;

        public AdminWindow(List<Dictionary<string, object>> profiles)
        {
            InitializeComponent();
            Profiles = profiles ?? new List<Dictionary<string, object>>();

        }

        private void Button_Data_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to DataPage
            MainFrame.Navigate(new DataPage());
        }

        private void Button_Profiles_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Profiles Page
            MainFrame.Navigate(new Profiles());
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonMinimixe_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonLogOff_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ButtonMessages_Click(object sender, RoutedEventArgs e)
        {
            AdminMessagePage adminMessagesPage = new AdminMessagePage();
            MainFrame.Navigate(adminMessagesPage);
        }




    }
}