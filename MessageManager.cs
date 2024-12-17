using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Printing;

namespace Fitness_App
{
    public static class MessageManager
    {
        private static string filePath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\AddDataPage_Data\Messages.txt";
        private static string profileFilePath = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\AddDataPage_Data\Profiles.txt";

        // Load messages for a specific user
        public static List<Message> GetMessagesForUser(string userId)
        {
            var messages = new List<Message>();

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 5)
                    {
                        var message = new Message
                        {
                            Sender = parts[0],
                            Recipient = parts[1],
                            Content = parts[2],
                            Timestamp = DateTime.Parse(parts[3]),
                            UserName = parts[4]
                        };

                        // Only return messages for the given user (either Sender or Recipient)
                        if (message.Sender == userId || message.Recipient == userId)
                        {
                            messages.Add(message);
                        }
                    }
                }
            }

            return messages;
        }

        // Add a new message to the file
        public static void AddMessage(Message message)
        {
            var messageLine = $"{message.Sender}|{message.Recipient}|{message.Content}|{message.Timestamp}|{message.UserName}";
            File.AppendAllLines(filePath, new[] { messageLine });
        }
    }

    public class Message
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }
    }
}
