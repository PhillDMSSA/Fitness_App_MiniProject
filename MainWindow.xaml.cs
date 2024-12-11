using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace Fitness_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, string> Users = new Dictionary<string, string>
        {
            {"admin", "admin123" }, //sign in credentials
            {"user1", "user123" }

        };
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) //allows the mouse to drap application w/ left button on mouse
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void ButtonMinimixe_Click(object sender, RoutedEventArgs e) //minimize window
        {
            WindowState = WindowState.Minimized;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e) //closes window
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

            if (Users.ContainsKey(username) && Users[username] == password)
            {
                if (username == "admin")
                {
                    MessageBox.Show("Welcome, Admin!");
                    //Open Admin Window
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                }
                else
                {
                    MessageBox.Show("Welcome, User!");
                    //Open User Window
                    UserWindow userWindow = new UserWindow();
                    userWindow.Show();
                }
                this.Hide(); //hides login window
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
    }
}