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
    
    public partial class UserDataPage : Page
    {
        private const string filePath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\AddDataPage_Data\Data.txt";

        public UserDataPage()
        {
            InitializeComponent();
            LoadDataIntoDataGrid();
        }
        private void LoadDataIntoDataGrid()
        {


            if (!File.Exists(filePath))
            {
                MessageBox.Show("No data file found. Please save some data first.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var lines = File.ReadAllLines(filePath);

                // Parse the file into a list of objects
                var data = lines
                     .Where(line => !string.IsNullOrWhiteSpace(line)) // Ignore empty lines
                    .Select(line =>
                    {
                        var fields = line.Split(',');
                        return new DataEntry1
                        {
                            FullName = fields.ElementAtOrDefault(0),
                            WorkoutDate = fields.ElementAtOrDefault(1),
                            WorkoutType = fields.ElementAtOrDefault(2),
                            Workout = fields.ElementAtOrDefault(3),
                            Sets = fields.ElementAtOrDefault(4),
                            Reps = fields.ElementAtOrDefault(5),
                            BurnedCalories = fields.ElementAtOrDefault(6),
                            Notes = fields.ElementAtOrDefault(7)
                           

                        };
                    })
                    .ToList();

                // Bind the data to the DataGrid
                DataGridDisplay.ItemsSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void BackButtonDisplayPage_Click(object sender, RoutedEventArgs e)
        { // Close the current page
            UserWindow userWindow = new UserWindow();
            userWindow.Show();

            // Close the current window
            Window currentWindow = Window.GetWindow(this);
            currentWindow?.Close();
        }


    }
    public class DataEntry1
    {
        public string FullName { get; set; }
        public string BurnedCalories { get; set; }
        public string WorkoutType { get; set; }
        public string Notes { get; set; }
        public string WorkoutDate { get; set; }
        public string Sets { get; set; }
        public string Reps { get; set; }
        public string Workout {  get; set; }
    }
}

