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
    /// Interaction logic for DataPage.xaml
    /// </summary>
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
                            FirstName = fields.ElementAtOrDefault(0),
                            LastName = fields.ElementAtOrDefault(1),
                            WorkoutType = fields.ElementAtOrDefault(2),
                            Notes = fields.ElementAtOrDefault(3)
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
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();  // Goes back to the previous page in the navigation stack
            }
            else
            {
                // Navigate to MainWindow or another starting page if there's no back page
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();  // Show MainWindow
                
            }
        }
    }
    public class DataEntry
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkoutType { get; set; }
        public string Notes { get; set; }
    }
}
