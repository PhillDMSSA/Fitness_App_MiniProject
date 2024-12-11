using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.IO;

namespace Fitness_App
{
   
    public partial class UserWindow : Window
    {
        private AddDataPage addDataPage; // Declare the cached AddDataPage instance
        private CalculatorPage calculatorPage; // Example: Another cached page

        public UserWindow()
        {
            InitializeComponent();
            addDataPage = new AddDataPage(); // Instantiate the pages
            calculatorPage = new CalculatorPage();
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
        public void Button_AddData_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content != addDataPage)
            {
                MainFrame.Navigate(addDataPage);
            }
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            // Start fade -in animation on the WelcomeText
            if (FindResource("FadeInAnimation") is Storyboard fadeIn)
            {
                fadeIn.Begin(WelcomeText);
            }
            else
            {
                MessageBox.Show("FadeInAnimation resource not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
