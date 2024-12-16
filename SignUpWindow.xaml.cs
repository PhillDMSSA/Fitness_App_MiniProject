using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Fitness_App
{
    public partial class SignUpWindow : Window
    {
        private List<UserProfile> Profiles { get; set; } //Profile list created from the proporties in UserProfiles windwow
        private Dictionary<string, string> Users { get; set; } //List for users and their passowords 
        private int UserIdCounter;

        private const string ProfilesFilePath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\AddDataPage_Data\Profiles.txt";
        private const string UsersFilePath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\AddDataPage_Data\UserName_Passwords.txt";

        public SignUpWindow()
        {
            InitializeComponent();

            // Initialize or load data
            Profiles = LoadFromFile<List<UserProfile>>(ProfilesFilePath) ?? new List<UserProfile>();
            Users = LoadFromFile<Dictionary<string, string>>(UsersFilePath) ?? new Dictionary<string, string>();

            // Set UserIdCounter to the next available ID
            UserIdCounter = Profiles.Count > 0 ? Profiles.Count + 1 : 1;
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Input validation
                if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(GenderTextBox.Text) ||
                    string.IsNullOrWhiteSpace(GoalTextBox.Text))
                {
                    MessageBox.Show("All fields are required.");
                    return;
                }

                if (!int.TryParse(AgeTextBox.Text, out int age) || age <= 0)
                {
                    MessageBox.Show("Please enter a valid age.");
                    return;
                }

                if (!int.TryParse(WeightTextBox.Text, out int weight) || weight <= 0)
                {
                    MessageBox.Show("Please enter a valid weight.");
                    return;
                }

                // Generate unique username and password
                string userID = "UID" + UserIdCounter++;
                string username = $"{FirstNameTextBox.Text.ToLower()}.{LastNameTextBox.Text.ToLower()}";
                string password = GenerateRandomPassword();

                // Create and save profile
                var profile = new UserProfile
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    Age = age,
                    Gender = GenderTextBox.Text,
                    Weight = weight,
                    Goal = GoalTextBox.Text,
                    UserID = userID
                };

                Profiles.Add(profile);
                Users[username] = password;

                // Save data to files
                SaveToFile(ProfilesFilePath, Profiles);
                SaveToFile(UsersFilePath, Users);

                MessageBox.Show($"Profile created successfully!\nYour User ID is {userID}\nUsername: {username}\nPassword: {password}");

                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }

        private void ClearInputs()
        {
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            AgeTextBox.Clear();
            GenderTextBox.Clear();
            WeightTextBox.Clear();
            GoalTextBox.Clear();
        }

        private static T LoadFromFile<T>(string filePath) where T : class
        {
            if (!File.Exists(filePath)) return null;
            try
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                return null;
            }
        }

        private static void SaveToFile<T>(string filePath, T data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data to file: {ex.Message}");
            }
        }

        private static string GenerateRandomPassword(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var password = new char[length];
            for (int i = 0; i < length; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            return new string(password);
        }
    }

    public class UserProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int Weight { get; set; }
        public string Goal { get; set; }
        public string UserID { get; set; }
    }
}
