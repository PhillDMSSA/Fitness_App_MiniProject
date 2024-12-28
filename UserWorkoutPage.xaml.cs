using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Fitness_App
{
    
    public partial class UserWorkoutPage : Page
    {
        public ObservableCollection<Workouts> Workouts { get; set; }
        public UserWorkoutPage()
        {
            InitializeComponent();
            Workouts = new ObservableCollection<Workouts>();
            WorkoutGrid.ItemsSource = Workouts;
        }

        private void BackButtonCal_Click(object sender, RoutedEventArgs e)
        {
            // Close the current page
            UserWindow userWindow = new UserWindow();
            userWindow.Show();

            // Close the current window
            Window currentWindow = Window.GetWindow(this);
            currentWindow?.Close();
        }

        private void GenerateWorkout_Click(object sender, RoutedEventArgs e)
        {
            bool addMoreWorkouts;

            do
            {
                //Prompt the user to select a muscle group and difficulty
                string selectedMuscleGroup = PromptForMuscleGroup();
                string selectedDifficulty = PromptForDifficulty();

                if (string.IsNullOrEmpty(selectedMuscleGroup) || string.IsNullOrEmpty(selectedDifficulty))
                {
                    MessageBox.Show("Please enter a valid muscle group and workout difficulty");
                    return;
                }

                string[] workoutDetails = GenerateWorkout(selectedMuscleGroup, selectedDifficulty);
                Workouts.Add(new Workouts
                {
                    MuscleGroup = selectedMuscleGroup,
                    Workout = workoutDetails[0],
                    SetsReps = workoutDetails[1]
                });

                //Ask user to add another workout
                MessageBoxResult result = MessageBox.Show("Would you like to add another workout?", "Add More", MessageBoxButton.YesNo);
                addMoreWorkouts = result == MessageBoxResult.Yes;
            }
            while (addMoreWorkouts);
            
        }

        private string PromptForMuscleGroup()
        {
            string[] muscleGroups = { "Abs", "Back", "Biceps", "Chest","Cardio","Glutes","Hamstrings","Quads","Legs", "Shoulder",};
            return PromptForSelection("Select Muscle Group", muscleGroups);
        }
        private string PromptForDifficulty()
        {
            string[] difficulties = { "Easy", "Moderate", "Extensive" };
            return PromptForSelection("Select Difficulty Level", difficulties);
        }

        private string PromptForSelection(string title, string[] options)
        {
            foreach (string option in options)
            {
                MessageBoxResult result = MessageBox.Show($"Do you want to select {option}?", title, MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    return option;
                }
            }
            return null;
        }

        private string[] GenerateWorkout(string muscleGroup, string difficulty)
        {
            Random random = new Random();
            string[] workouts;

            switch (muscleGroup)
            {
                case "Chest":
                    workouts = new[] { "Bench Press", "Incline Dumbbell Press", "Push-ups", "Cable Crossovers" };
                    break;
                case "Back":
                    workouts = new[] { "Pull-ups", "Deadlifts", "Barbell Rows", "Lat Pulldowns" };
                    break;
                case "Legs":
                    workouts = new[] { "Squats", "Lunges", "Leg Press", "Hamstring Curls" };
                    break;
                case "Shoulders":
                    workouts = new[] { "Overhead Press", "Lateral Raises", "Front Raises", "Arnold Press" };
                    break;
                case "Arms":
                    workouts = new[] { "Bicep Curls", "Tricep Dips", "Hammer Curls", "Tricep Pushdowns" };
                    break;
                default:
                    return new[] { "Unknown workout", "Unknown sets and reps" };
            }

            // Generate at least two workouts
            int firstIndex = random.Next(workouts.Length);
            int secondIndex;
            do
            {
                secondIndex = random.Next(workouts.Length);
            } while (secondIndex == firstIndex);

            string firstWorkout = workouts[firstIndex];
            string secondWorkout = workouts[secondIndex];

            // Determine sets and reps based on difficulty
            string setsReps;
            switch (difficulty)
            {
                case "Easy":
                    setsReps = "3 sets of 10 reps";
                    break;
                case "Moderate":
                    setsReps = "4 sets of 12 reps";
                    break;
                case "Extensive":
                    setsReps = "5 sets of 15 reps";
                    break;
                default:
                    setsReps = "Unknown difficulty level.";
                    break;
            }

            return new[] { $"{firstWorkout} and {secondWorkout}", setsReps };
        }
    }
}

    


    

