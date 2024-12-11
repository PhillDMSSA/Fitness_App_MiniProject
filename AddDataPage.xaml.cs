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
using System.IO;

namespace Fitness_App
{
    /// <summary>
    /// Interaction logic for AddDataPage.xaml
    /// </summary>
    public partial class AddDataPage : Page
    {
        private const string FilePath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\AddDataPage_Data\Data.txt";

        public AddDataPage()
        {
            InitializeComponent();

            ClearFormFields(); //clears fields upon startup
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
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
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

            //FirstNameTextBox.Text = string.Empty;
            // LastNameTextBox.Text = string.Empty;
            //UserInputTextBox.Text = string.Empty;

            // Reset the ComboBox
            // WorkoutComboBox.SelectedIndex = -1; // Deselect any selected item
            ClearFormFields();

        }

        private void ClearFormFields() //clears fields
        {
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            UserInputTextBox.Clear();
            WorkoutComboBox.SelectedIndex = -1; // Deselect any selected item
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Get the workout type as a string (the content of the selected ComboBoxItem)
                string workoutType = (WorkoutComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                string dataToSave = $"{FirstNameTextBox.Text},{LastNameTextBox.Text},{workoutType},{UserInputTextBox.Text}";

                // Save the content of the TextBox to the file
                File.AppendAllText(FilePath, dataToSave + Environment.NewLine); //isstead of writing over data its creates a new line 

                ClearFormFields(); //clears fields after data is saved

                MessageBox.Show("Data saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
