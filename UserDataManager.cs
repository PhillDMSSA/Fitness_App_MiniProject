using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_App
{
    public static class UserDataManager
    {
        private static string UserDataDirectory = @"C:\Users\phillipdeleon\source\repos\Coding_Practice\Fitness_App\User_Data_Input\User_Data_Input.txt";

        static UserDataManager()
        {
            if (!Directory.Exists(UserDataDirectory))
            {
                Directory.CreateDirectory(UserDataDirectory);
            }     
        }
        

        //Get the file path for a user
        public static string GetUserFilePath(string username)
        {
            return Path.Combine(UserDataDirectory, $"{username}_data.txt");

        }

        //Save data to the users file
        public static void SaveData(string username, string data)
        {
            string filePath = GetUserFilePath(username);
            File.AppendAllLines(filePath, new[] { data });
        }

        //Load data from the users file
        public static string[] LoadData(string username)
        {
            string filePath = GetUserFilePath(username);
            if(File.Exists(filePath))
            {
                return File.ReadAllLines(filePath);
            }
            return new string[0]; //Return empty arry if not files exist
        }
    }
}
