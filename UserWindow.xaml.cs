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
using System.Net.WebSockets;

namespace Fitness_App
{

    public partial class UserWindow : Window
    {
        private AddDataPage addDataPage; // Declare the cached AddDataPage instance
        private CalculatorPage calculatorPage; // Example: Another cached page

        private string userID;

        public UserWindow()
        {
            InitializeComponent();
            addDataPage = new AddDataPage(); // Instantiate the pages
            calculatorPage = new CalculatorPage();
            this.userID = userID; // Initialize the userID

        }
        private void ButtonLogOff_Click(object sender, RoutedEventArgs e)
        {
            LoggedInUser.Username = null; // Clear the logged-in user session

            MainWindow main = new MainWindow();
            main.Show();
            MessageBox.Show("Logged off successful!");


            this.Close(); //closest current window
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (addDataPage != null && addDataPage.UserInputTextBox != null && !string.IsNullOrWhiteSpace(addDataPage.UserInputTextBox.Text))
            {
                try
                {
                    string filePath = "UserData.txt";
                    File.WriteAllText(filePath, addDataPage.UserInputTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving data on close: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Calculator_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content != calculatorPage) // Navigate only if it's not already displayed
            {
                MainFrame.Navigate(calculatorPage);
            }
        }
        private void Button_AddData_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content != addDataPage)
            {
                MainFrame.Navigate(addDataPage);
            }
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
            Application.Current.Shutdown();
            MessageBox.Show("Error Occurred: You have been logged off!");
        }
        private void ButtonMessages_Click(object sender, RoutedEventArgs e)
        {
            var messagePage = new UserMessagePage();
            this.Content = messagePage; // Assuming you're using a Frame to navigate between pages

        }
        private void Button_Data_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to DataPage
            MainFrame.Navigate(new UserDataPage());
        }


    }
}