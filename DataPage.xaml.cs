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


namespace Fitness_App
{
   
    public partial class DataPage : Page
    {
        private const string filePath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\AddDataPage_Data\Data.txt";
        public DataPage()
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
                        return new DataEntry
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
        {
            // Navigate to AdminWindow if there's no back page
            AdminWindow adminWindow = new AdminWindow(new List<Dictionary<string, object>>()); // Pass an empty list for now);
            adminWindow.Show();

            // Close the parent window of this Page
            Window parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }

        
    }
    
}
