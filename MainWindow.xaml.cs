using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Fitness_App
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> Users = new Dictionary<string, string>();
        private const string UserAndPassFilePath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\AddDataPage_Data\UserName_Passwords.txt";
        private const string ProfilesFilePath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\AddDataPage_Data\Profiles.txt"; // Corrected file path for profiles

        public MainWindow()
        {
            InitializeComponent();
            LoadUsers(); // Load users from the file when the app starts
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // Load users from the file
        private void LoadUsers()
        {
            if (File.Exists(UserAndPassFilePath))
            {
                var lines = File.ReadAllLines(UserAndPassFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        string username = parts[0];
                        string password = parts[1];
                        Users[username] = password;
                    }
                }
            }
            else
            {
                // If the file does not exist, create an empty one
                File.Create(UserAndPassFilePath).Close();
            }
        }

        // Save users to the file
        private void SaveUsers()
        {
            var lines = new List<string>();
            foreach (var user in Users)
            {
                lines.Add($"{user.Key},{user.Value}");
            }
            File.WriteAllLines(UserAndPassFilePath, lines);
        }

        private void ButtonMinimixe_Click(object sender, RoutedEventArgs e) // Minimize window
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e) // Close window
        {
            Application.Current.Shutdown();
        }

        private bool ValidateUserCredentials(string username, string password)
        {
            // Check if the file exists
            if (!File.Exists(UserAndPassFilePath))
            {
                MessageBox.Show("Credentials file not found.");
                return false;
            }

            // Read all lines from the file
            string[] lines = File.ReadAllLines(UserAndPassFilePath);

            // Iterate through each line to check for matching credentials
            foreach (var line in lines)
            {
                string[] credentials = line.Split('|');
                if (credentials.Length == 2)
                {
                    string storedUsername = credentials[0].Trim();
                    string storedPassword = credentials[1].Trim();

                   

                    // Check if the entered username and password match any stored pair
                    if (storedUsername.Equals(username.Trim(), StringComparison.OrdinalIgnoreCase) &&
                        storedPassword.Equals(password.Trim()))
                    {
                        return true;
                    }
                }
            }

            return false; // If no match is found
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = UsernameTextBox.Text;
                string password = PasswordTextBox.Text;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Please enter both username and password.");
                    return;
                }

                // Check if username and password match
                if (ValidateUserCredentials(username, password))
                {
                    // Check if it's the admin
                    if (username == "admin")
                    {
                        MessageBox.Show("Welcome, Admin!");
                        List<Dictionary<string, object>> profiles = LoadProfiles();  // Load profiles for admin
                        AdminWindow adminWindow = new AdminWindow(profiles);  // Pass profiles to constructor
                        adminWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Welcome, User!");
                        UserWindow userWindow = new UserWindow(); // Open User Window
                        userWindow.Show();
                    }
                    this.Hide(); // Hide the login window
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    UsernameTextBox.Clear();
                    PasswordTextBox.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        private void SignUpButton_Click(Object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            if (signUpWindow.ShowDialog() == true) // Wait for sign-up window to close
            {
                // Update the Users dictionary after a successful sign-up
                LoadUsers();
            }
        }

        // Load user profiles from the correct file
        private List<Dictionary<string, object>> LoadProfiles()
        {
            var profiles = new List<Dictionary<string, object>>();

            if (File.Exists(ProfilesFilePath))
            {
                var lines = File.ReadAllLines(ProfilesFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 7) // Assuming there are 7 fields per profile (UserID, FirstName, LastName, Age, Gender, Weight, Goal)
                    {
                        var profile = new Dictionary<string, object>
                        {
                            { "UserID", parts[0] },
                            { "FirstName", parts[1] },
                            { "LastName", parts[2] },
                            { "Age", int.Parse(parts[3]) },
                            { "Gender", parts[4] },
                            { "Weight", int.Parse(parts[5]) },
                            { "Goal", parts[6] }
                        };
                        profiles.Add(profile);
                    }
                }
            }

            return profiles;
        }
    }
}
