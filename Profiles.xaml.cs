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
using System.Net.Cache;
using System.Transactions;
using Newtonsoft.Json;

namespace Fitness_App
{

    public partial class Profiles : Page
    {
        
         public Profiles()
        {
            InitializeComponent();
            LoadProfiles();


        }

        private void LoadProfiles()
        {
            string filePath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\AddDataPage_Data\Profiles.txt";

            // Ensure file exists
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Profiles file not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Read all profiles from .txt
                var profiles = ProfileLoader.LoadProfiles(filePath);

                // Bind profiles to the DataGrid
                ProfilesDataGrid.ItemsSource = profiles;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profiles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void BackButtonDisplayPage_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack == true)
            {
                NavigationService.GoBack();  // Goes back to the previous page in the navigation stack
            }
            else
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
    
    public static class ProfileLoader
    {
        // Load profiles from the plain text file
        public static List<Profile> LoadProfiles(string filePath)
        {
            var profiles = new List<Profile>();

            // Read the file line by line
            foreach (var line in File.ReadLines(filePath))
            {
                try
                {
                    // Split the line by the '|' delimiter
                    var fields = line.Split('|');
                    if (fields.Length == 7) // Ensure the correct number of fields
                    {
                        profiles.Add(new Profile
                        {
                            FirstName = fields[0],
                            LastName = fields[1],
                            Age = fields[2],
                            Gender = fields[3],
                            Weight = fields[4],
                            Goal = fields[5],
                            UserID = fields[6]
                        });
                    }
                    else
                    {
                        throw new FormatException("Invalid data format in profile file.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing line: {line}\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return profiles;
        }
    }
}
    public class Profile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Weight { get; set; }
        public string Goal { get; set; }
        public string UserID { get; set; }
    }



