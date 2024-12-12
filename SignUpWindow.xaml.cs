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
using System.Windows.Shapes;

namespace Fitness_App
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private List<Dictionary<string,object>> Profiles {  get; set; } //user profiles 
        private Dictionary<string, Dictionary<string, object>> UserDictionary { get; set; } 

        private int UserIdCounter = 1; //userID, will generate a userID upon signup

        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstName = FirstNameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                int age;
                if (!int.TryParse(AgeTextBox.Text, out age))
                {
                    MessageBox.Show("Please enter a valid age");
                    return;
                }
                string gender = GenderTextBox.Text;
                int weight;
                if (!int.TryParse(WeightTextBox.Text, out weight))
                {
                    MessageBox.Show("Please enter a valid weight");
                    return;
                }
                string goal = GoalTextBox.Text;
                string userID = "UID" + UserIdCounter++;

                var profile = new Dictionary<string, object>
            {
                { "FirstName", firstName},
                { "LastName", lastName},
                { "Age", age},
                { "Gender", gender},
                { "Weight", weight},
                { "Goal", goal},
                { "UserID", userID},

            };
                Profiles.Add(profile);
                UserDictionary[userID] = profile;

                MessageBox.Show($"Profile created successfully! Your User ID is {userID}");

                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }
        
        private void ClearInputs()
        {
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            AgeTextBox.Text = "";
            GenderTextBox.Text = "";
            WeightTextBox.Text = "";
            GoalTextBox.Text = "";
        }



    }
}
