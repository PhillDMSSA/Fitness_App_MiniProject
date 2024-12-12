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
        private const string UserAndPassFilePath = @"Z:\Documents\MSSA\Projects\Calorie Burner Calculator\TXT\UserName_Passwords.txt";
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
                    var parts = line.Split(';');
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
            // Add hardcoded admin and user if they don't already exist
            if (!Users.ContainsKey("admin"))
                Users.Add("admin", "admin123");
            if (!Users.ContainsKey("user1"))
                Users.Add("user1", "user123");
        }

        // Save users to the file
        private void SaveUsers()
        {
            var lines = new List<string>();
            foreach (var user in Users)
            {
                lines.Add($"{user.Key};{user.Value}");
            }
            File.WriteAllLines("UserAndPassFilePath", lines);
        }

        private void ButtonMinimixe_Click(object sender, RoutedEventArgs e) // Minimize window
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e) // Close window
        {
            Application.Current.Shutdown();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


                    // Check if username and password match
            if (Users.ContainsKey(username) && Users[username] == password)
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


        private void SignUpButton_Click(Object sender, RoutedEventArgs e)
        {
            // Open a sign-up window to add new users
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.ShowDialog();
        }
        private List<Dictionary<string, object>> LoadProfiles()
        {
            return new List<Dictionary<string, object>>()
            {
                new Dictionary<string, object>
                {
                    { "UserID", "UID1" },
                    { "FirstName", "John" },
                    { "LastName", "Doe" },
                    { "Age", 30 },
                    { "Gender", "Male" },
                    { "Weight", 180 },
                    { "Goal", "Lose weight" }
                },
                // Add more profiles here as necessary
            };
        }

    }
}
