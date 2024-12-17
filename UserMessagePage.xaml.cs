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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fitness_App
{
    public partial class UserMessagePage : Page
    {
        private string userID = "User";


        public UserMessagePage()
        {
            InitializeComponent();

            LoadMessages();
        }
        private void LoadMessages()
        {
            // Get all messages sent to or from the user
            var messages = MessageManager.GetMessagesForUser(userID);
            MessageListBox.ItemsSource = messages.Select(m => $"{m.Timestamp:G} - {m.Sender}: {m.Content}");
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            var content = NewMessageTextBox.Text;
            if (string.IsNullOrWhiteSpace(content)) return;

            var message = new Message
            {
                Sender = userID,
                Recipient = "Coach", // Send to the admin
                Content = content,
                Timestamp = DateTime.Now,
                UserName = userID
            };

            MessageManager.AddMessage(message);
            NewMessageTextBox.Clear();
            LoadMessages(); // Refresh the message list
        }
        private void BackButtonDisplayPage_Click(object sender, RoutedEventArgs e)
        {
            // Close the current page
            UserWindow userWindow = new UserWindow();
            userWindow.Show();

            // Close the current window
            Window currentWindow = Window.GetWindow(this);
            currentWindow?.Close();
        }


    }
}