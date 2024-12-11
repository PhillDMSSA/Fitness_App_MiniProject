using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fitness_App
{
   
    public partial class CalculatorPage : Page
    {
        private CaloriesBurned caloriesBurned = new CaloriesBurned();
        public CalculatorPage()
        {
            InitializeComponent();
        }
        private void CalculateButton_Click(object sender, RoutedEventArgs e) //event handler
        {
            try
            {
                // Get user input from the textboxes
                double weight = double.Parse(WeightTextBox.Text);
                double duration = double.Parse(DurationTextBox.Text);
                int intensity = int.Parse(IntensityTextBox.Text);

                // Calculate calories burned using the CaloriesBurned class
                double calories = caloriesBurned.BurnedCalories(duration, weight, intensity);

                // Display the result
                ResultTextBlock.Text = $"You burned about {calories} calories!";
            }
            catch (Exception ex)
            {
                // Handle invalid input gracefully
                MessageBox.Show("Invalid input. Please enter valid numbers for weight, duration, and intensity.");
            }

        }
        private void ClearButtonCal_Click(object sender, RoutedEventArgs e)
        {

            WeightTextBox.Text = string.Empty;
            DurationTextBox.Text = string.Empty;
            IntensityTextBox.Text = string.Empty;

            // Reset the ComboBox
            //WorkoutComboBox.SelectedIndex = -1; // Deselect any selected item

        }

        private void BackButtonCal_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
                // Goes back to the previous page in the navigation stack
            }
            else
            {
                if (!(Application.Current.MainWindow is MainWindow mainWindow))
                {
                    // Navigate to UserWindow
                    UserWindow userWindow = new UserWindow();
                    userWindow.Show();
                }
                // Close the current page
                this.NavigationService.Content = null;  // Optionally clear the current content if necessary
            }
        }
    }
}
