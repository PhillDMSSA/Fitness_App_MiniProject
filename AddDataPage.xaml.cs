﻿using System;
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

           
            ClearFormFields();

        }

        private void ClearFormFields() //clears fields
        {
            FullNameTextBox.Clear();
            BurnedCaloriesTextBox.Clear();
            UserInputTextBox.Clear();
            DateTextBox.Clear();
            WorkoutComboBox.SelectedIndex = -1; // Deselect any selected item
            WorkoutsComboBox.SelectedIndex = -1;
            SetTextBox.Clear();
            RepTextBox.Clear();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Get the workout type as a string (the content of the selected ComboBoxItem)
                string workoutType = (WorkoutComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                string workout = WorkoutsComboBox.SelectedItem?.ToString() ?? string.Empty;
                


                string dataToSave = $"{FullNameTextBox.Text},{DateTextBox.Text},{workoutType},{workout},{SetTextBox.Text},{RepTextBox.Text},{BurnedCaloriesTextBox.Text},{UserInputTextBox.Text}";

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

        private void WorkoutComboBox_Workout(object sender, SelectionChangedEventArgs e)
        {
            if(WorkoutComboBox.SelectedItem is ComboBoxItem selectedItem )
            {
                string muscleGroup = selectedItem.Content.ToString();

                //clears previous item
                WorkoutsComboBox.Items.Clear();

                switch(muscleGroup)
                {
                    case "Abs":
                        WorkoutsComboBox.Items.Add("Crunches");
                        WorkoutsComboBox.Items.Add("Plank");
                        WorkoutsComboBox.Items.Add("Leg Raises");
                        break;
                    case "Back":
                        WorkoutsComboBox.Items.Add("Pull-ups");
                        WorkoutsComboBox.Items.Add("Deadlifts");
                        WorkoutsComboBox.Items.Add("Barbell Rows");
                        break;
                    case "Biceps":
                        WorkoutsComboBox.Items.Add("Bicep Curls");
                        WorkoutsComboBox.Items.Add("Hammer Curls");
                        WorkoutsComboBox.Items.Add("Concentration Curls");
                        break;
                    case "Cardio":
                        WorkoutsComboBox.Items.Add("Running");
                        WorkoutsComboBox.Items.Add("Cycling");
                        WorkoutsComboBox.Items.Add("Jump Rope");
                        break;
                    case "Chest":
                        WorkoutsComboBox.Items.Add("Bench Press");
                        WorkoutsComboBox.Items.Add("Incline Dumbbell Press");
                        WorkoutsComboBox.Items.Add("Chest Flys");
                        break;
                    case "Legs":
                        WorkoutsComboBox.Items.Add("Squats");
                        WorkoutsComboBox.Items.Add("Lunges");
                        WorkoutsComboBox.Items.Add("Leg Press");
                        break;
                    case "Shoulders":
                        WorkoutsComboBox.Items.Add("Overhead Press");
                        WorkoutsComboBox.Items.Add("Lateral Raises");
                        WorkoutsComboBox.Items.Add("Front Raises");
                        break;
                    case "Triceps":
                        WorkoutsComboBox.Items.Add("Tricep Dips");
                        WorkoutsComboBox.Items.Add("Skull Crushers");
                        WorkoutsComboBox.Items.Add("Overhead Tricep Extensions");
                        break;

                }
            }
        }
    }
}