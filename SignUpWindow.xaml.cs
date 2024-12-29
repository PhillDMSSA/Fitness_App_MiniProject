using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Fitness_App
{
    public partial class SignUpWindow : Window
    {
        
        private List<UserProfiles> Profiles { get; set; } // Profile list created from the properties in UserProfiles window
        private Dictionary<string, string> Users { get; set; } // List for users and their passwords
        private int UserIdCounter;

        private const string ProfilesFilePath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\AddDataPage_Data\Profiles.txt";
        private const string UsersFilePath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\AddDataPage_Data\UserName_Passwords.txt";

        public SignUpWindow()
        {
            InitializeComponent();

            // Initialize or load data
            Profiles = LoadProfilesFromFile() ?? new List<UserProfiles>(); //??(Null coalescing operator): fallback value when left-hand side it null. (If LoadProfilesFromFile() = null; new List<UserProfiles> is created. 
            Users = LoadUsersFromFile() ?? new Dictionary<string, string>();

            // Set UserIdCounter to the next available ID
            UserIdCounter = Profiles.Count > 0 ? Profiles.Count + 1 : 1; //? (Temaray Operator) = shorthand for if-else statement. If a profile is not created count will start at :1 or Id + 1
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
               
                // Generate unique username and password
                string userID = "UID" + UserIdCounter++;
                string username = $"{FirstNameTextBox.Text.ToLower()}.{LastNameTextBox.Text.ToLower()}";
                string password = GenerateRandomPassword();

                // Create and save profile
                var profile = new UserProfiles
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    Age = AgeTextBox.Text,
                    Gender = GenderTextBox.Text,
                    Weight = WeightTextBox.Text,
                    Goal = GoalTextBox.Text,
                    UserID = userID
                };

                Profiles.Add(profile);
                Users[username] = password;

                // Save data to plain text files
                SaveProfilesToFile();
                SaveUsersToFile();

                MessageBox.Show($"Profile created successfully!\nYour User ID is {userID}\nUsername: {username}\nPassword: {password}\n\nPlease be sure to write down username and password!!");

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

        // Load profiles from a plain text file
        private List<UserProfiles> LoadProfilesFromFile()
        {
            if (!File.Exists(ProfilesFilePath)) 
                return null;
            try
            {
                var profiles = new List<UserProfiles>();
                var lines = File.ReadAllLines(ProfilesFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|'); // Assuming data is stored like: FirstName|LastName|Age|Gender|Weight|Goal|UserID
                    if (parts.Length == 7)
                    {
                        var profile = new UserProfiles
                        {
                            FirstName = parts[0],
                            LastName = parts[1],
                            Age = parts[2],
                            Gender = parts[3],
                            Weight = parts[4],
                            Goal = parts[5],
                            UserID = parts[6]
                        };
                        profiles.Add(profile);
                    }
                }
                return profiles;
            }
            catch
            {
                return null;
            }
        }

        // Load users and passwords from a plain text file
        private Dictionary<string, string> LoadUsersFromFile()
        {
            if (!File.Exists(UsersFilePath)) return null;
            try
            {
                var users = new Dictionary<string, string>();
                var lines = File.ReadAllLines(UsersFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|'); // Assuming data is stored like: username|password
                    if (parts.Length == 2)
                    {
                        users[parts[0]] = parts[1];
                    }
                }
                return users;
            }
            catch
            {
                return null;
            }
        }

        // Save profiles to a plain text file
        private void SaveProfilesToFile()
        {
            try
            {
                var lines = new List<string>();
                foreach (var profile in Profiles)
                {
                    // Save data as a pipe-separated string: FirstName|LastName|Age|Gender|Weight|Goal|UserID
                    var line = $"{profile.FirstName}|{profile.LastName}|{profile.Age}|{profile.Gender}|{profile.Weight}|{profile.Goal}|{profile.UserID}";
                    lines.Add(line);
                }
                File.WriteAllLines(ProfilesFilePath, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving profiles: {ex.Message}");
            }
        }

        // Save users to a plain text file
        private void SaveUsersToFile()
        {
            try
            {
                var lines = new List<string>();
                foreach (var user in Users)
                {
                    // Save data as a pipe-separated string: username|password
                    var line = $"{user.Key}|{user.Value}";
                    lines.Add(line);
                }
                File.WriteAllLines(UsersFilePath, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving users: {ex.Message}");
            }
        }

        private static string GenerateRandomPassword(int length = 8) //sets default value to 8 length password
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


}