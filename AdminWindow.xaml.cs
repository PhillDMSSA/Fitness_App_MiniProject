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
using System.IO;
using Fitness_App;

namespace Fitness_App
{
    
    public partial class AdminWindow : Window
    {
        private List<Dictionary<string, object>> Profiles_Users;

        public AdminWindow()
        {
            InitializeComponent();
            // Initialize the list with sample data
            Profiles_Users = new List<Dictionary<string, object>>();

            // Example of adding a profile
            Profiles_Users.Add(new Dictionary<string, object>
        {
            { "UserID", 1 },
            { "FirstName", "John" },
            { "LastName", "Doe" },
            { "Age", 30 },
            { "Gender", "Male" },
            { "Weight", 180 },
            { "Goal", "Lose weight" }
        });

        }
        private void Button_Data_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DataPage());

        }
        private void Button_Profiles_Click(object sender, RoutedEventArgs e)
        {
            // Pass the Profiles_Users list to the Profiles page
            MainFrame.Navigate(new Profiles(Profiles_Users));
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) //allows the mouse to drap application w/ left button on mouse
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void ButtonMinimixe_Click(object sender, RoutedEventArgs e) //minimize window
        {
            WindowState = WindowState.Minimized;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e) //closes window
        {
            MessageBox.Show("Error Occurred: You have been logged off!");
            Application.Current.Shutdown();
            
        }
        private void ButtonLogOff_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            MessageBox.Show("Logged off successful!");


            this.Close(); //closest current window
        }
    }
}
