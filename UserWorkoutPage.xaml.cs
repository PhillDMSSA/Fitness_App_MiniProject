using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Fitness_App
{
    public partial class UserWorkoutPage : Page
    {
        public ObservableCollection<Workouts> Workouts { get; set; }
        private readonly string[] muscleGroups = { "Abs", "Back", "Biceps", "Chest", "Cardio", "Glutes", "Hamstrings", "Quads", "Legs", "Shoulder" };
        private readonly string[] difficulties = { "Easy", "Moderate", "Extensive" };

        public UserWorkoutPage()
        {
            InitializeComponent();
            Workouts = new ObservableCollection<Workouts>(); 
            WorkoutGrid.ItemsSource = Workouts; //assigned to datagrid

            // Initialize ComboBoxes
            MuscleGroupComboBox.ItemsSource = muscleGroups;
            DifficultyComboBox.ItemsSource = difficulties;
        }

        private void BackButtonCal_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow();
            userWindow.Show();
            Window.GetWindow(this)?.Close();
        }

       
           private void AddWorkout_Click(object sender, RoutedEventArgs e)
        {
            string selectedMuscleGroup = MuscleGroupComboBox.SelectedItem?.ToString();
            string selectedDifficulty = DifficultyComboBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedMuscleGroup) || string.IsNullOrEmpty(selectedDifficulty))
            {
                MessageBox.Show("Please select both a muscle group and difficulty level");
                return;
            }

            Workouts.Add(new Workouts
            {
                MuscleGroup = selectedMuscleGroup,
                Difficulty = selectedDifficulty,
                Workout = "Pending",
                Sets = "Pending",
                Reps = "Pending",
                Rest_Time = "Pending"
            });

            MuscleGroupComboBox.SelectedIndex = -1; //clears box for another workout input
            DifficultyComboBox.SelectedIndex = -1; //clears box for another workout input
        }

        private void GenerateWorkout_Click(object sender, RoutedEventArgs e)
        {
            if (Workouts.Count == 0)
            {
                MessageBox.Show("Please add at least one workout before generating");
                return;
            }

            // Create a snapshot of the Workouts collection to avoid modifying it during iteration
            var workoutsList = Workouts.ToList();

            foreach (var workout in workoutsList)
            {
                string[] workoutDetails = GenerateWorkout(workout.MuscleGroup, workout.Difficulty);


                // Debugging: Log the workout index to confirm it's valid
                int index = Workouts.IndexOf(workout);
                if (index >= 0)  // Ensure the index is valid
                {
                    // Replace the current workout with the generated details
                    Workouts[index] = new Workouts
                    {
                        MuscleGroup = workout.MuscleGroup,
                        Difficulty = workout.Difficulty,
                        Workout = workoutDetails[0],  // First workout
                        Sets = workoutDetails[1],     // Sets
                        Reps = workoutDetails[2],     // Reps
                        Rest_Time = workoutDetails[3],  // Rest time
                        Notes = workoutDetails[4]  // Optional: Add notes if needed
                    };
                }
                else
                {
                    MessageBox.Show("Error: Workout not found in the collection.");
                    return;
                }
            }

            MessageBox.Show("Workout generation complete!");
        }

        private string[] GenerateWorkout(string muscleGroup, string difficulty)
        {
            Random random = new Random();
            string[] workouts;

            // Select workouts based on muscle group
            switch (muscleGroup)
            {
                case "Abs":
                    workouts = new[] { "Crunches", "Planks", "Russian Twists", "Leg Raises" };
                    break;
                case "Back":
                    workouts = new[] { "Pull-ups", "Deadlifts", "Barbell Rows", "Lat Pulldowns" };
                    break;
                case "Biceps":
                    workouts = new[] { "Barbell Curls", "Hammer Curls", "Preacher Curls", "Concentration Curls" };
                    break;
                case "Chest":
                    workouts = new[] { "Bench Press", "Incline Dumbbell Press", "Push-ups", "Cable Crossovers" };
                    break;
                case "Cardio":
                    workouts = new[] { "Running", "Jump Rope", "Burpees", "Mountain Climbers" };
                    break;
                case "Glutes":
                    workouts = new[] { "Hip Thrusts", "Glute Bridges", "Bulgarian Split Squats", "Step-ups" };
                    break;
                case "Hamstrings":
                    workouts = new[] { "Romanian Deadlifts", "Leg Curls", "Good Mornings", "Nordic Curls" };
                    break;
                case "Quads":
                    workouts = new[] { "Front Squats", "Leg Extensions", "Walking Lunges", "Box Jumps" };
                    break;
                case "Legs":
                    workouts = new[] { "Squats", "Lunges", "Leg Press", "Hamstring Curls" };
                    break;
                case "Shoulder":
                    workouts = new[] { "Overhead Press", "Lateral Raises", "Front Raises", "Arnold Press" };
                    break;
                default:
                    return new[] { "Unknown workout", "Unknown sets and reps", "Unknown sets", "Unknown rest time" };
            }

            int firstIndex = random.Next(workouts.Length);
            int secondIndex;
            do
            {
                secondIndex = random.Next(workouts.Length);
            } while (secondIndex == firstIndex);

            string firstWorkout = workouts[firstIndex];
            string secondWorkout = workouts[secondIndex];

            string sets = difficulty switch
            {
                "Easy" => "3",
                "Moderate" => "5",
                "Extensive" => "7",
                _ => "0"
            };

            string reps = difficulty switch
            {
                "Easy" => "5-7",
                "Moderate" => "8-11",
                "Extensive" => "12-15",
                _ => "0"
            };

            string restTime = difficulty switch
            {
                "Easy" => "1 min",
                "Moderate" => "45 secs",
                "Extensive" => "30 secs",
                _ => "Unknown rest time"
            };

            string notes = "Weight light enough to complete all sets/reps!!";

            // Return workout details, including sets, reps, and rest time
            string[] workoutDetails = { $"{firstWorkout} and {secondWorkout}", sets, reps, restTime, notes};

            // Debugging log
            Console.WriteLine($"Generated workout details: {string.Join(", ", workoutDetails)}");

            return workoutDetails;
        }

    }
}

