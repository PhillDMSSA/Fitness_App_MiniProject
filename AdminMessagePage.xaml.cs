using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fitness_App
{
    public partial class AdminMessagePage : Page
    {
        private string adminID = "Admin"; // Admin's ID

        public AdminMessagePage()
        {
            InitializeComponent();
            LoadMessages();
        }

        private void LoadMessages()
        {
            try
            {
                var messages = MessageManager.GetMessagesForUser(adminID);
                MessageListBox.ItemsSource = messages.Select(m => $"{m.Timestamp:G} - {m.UserName}: {m.Content}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading messages: {ex.Message}");
            }
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var content = NewMessageTextBox.Text;
                if (string.IsNullOrWhiteSpace(content)) return;

                var message = new Message
                {
                    Sender = "Coach",
                    Recipient = "User",
                    Content = content,
                    Timestamp = DateTime.Now,
                    UserName = "User1" // Example: This should be dynamically assigned based on the user you're sending to
                };

                MessageManager.AddMessage(message);
                NewMessageTextBox.Clear();
                LoadMessages();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}");
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
}