using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fitness_App
{
    public partial class AdminMessagePage : Page
    {
        private string adminID = "Coach"; // Admin's ID

        public AdminMessagePage()
        {
            InitializeComponent();
            LoadMessages();
        }

        private void LoadMessages()
        {
            // Get all messages sent to or from the user
            var messages = MessageManager.GetMessagesForUser(adminID);
            MessageListBox.ItemsSource = messages.Select(m => $"{m.Timestamp:G} - {m.Sender}: {m.Content}");
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            
                var content = NewMessageTextBox.Text;
                if (string.IsNullOrWhiteSpace(content)) return;

                var message = new Message
                {
                    Sender = adminID,
                    Recipient = "User",
                    Content = content,
                    Timestamp = DateTime.Now,
                    UserName = adminID // Example: This should be dynamically assigned based on the user you're sending to
                };

                MessageManager.AddMessage(message);
                NewMessageTextBox.Clear();
                LoadMessages();
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