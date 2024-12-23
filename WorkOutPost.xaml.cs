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
    
    public partial class WorkOutPost : Page
    {
        private const string WorkoutPath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\WorkOuts\WorkoutOfTheDay.txt";
        public WorkOutPost()
        {
            InitializeComponent();
            LoadWorkOuts();
        }

        private void LoadWorkOuts()
        {
            if(!File.Exists(WorkoutPath))
            {
                MessageBox.Show("No data file found. Please save some data first.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                var lines = File.ReadAllLines(WorkoutPath);

                var data = lines
                    .Where(lines => !string.IsNullOrWhiteSpace(lines)) //ignores empty lines
                    .Select(line =>
                    {
                        var fields = line.Split(',');
                        return new Workouts
                        {
                            Date = fields.ElementAtOrDefault(0),
                            MuscleGroup = fields.ElementAtOrDefault(1),
                            Workout = fields.ElementAtOrDefault(2),
                            Sets = fields.ElementAtOrDefault(3),
                            Reps = fields.ElementAtOrDefault(4),
                            Rest_Time = fields.ElementAtOrDefault(5),
                            Notes = fields.ElementAtOrDefault(6)
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
