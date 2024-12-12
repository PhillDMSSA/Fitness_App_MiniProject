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
using System.IO;

namespace Fitness_App
{
    
    public partial class Profiles : Page
    {
        private List<Dictionary<string, object>> Profiles_Users;
        public Profiles(List<Dictionary<string, object>> profiles_Users)
        {
            InitializeComponent();

            // Ensure Profiles_Users is not null
            Profiles_Users = profiles_Users;
            // Bind profiles to the DataGrid
            UserProfiles.ItemsSource = ConvertProfilesToDisplayableList();
        }
       
        private List<object> ConvertProfilesToDisplayableList()
        {
            var displayList = new List<object>();

            // Convert dictionary data to a displayable object
            foreach (var user in Profiles_Users)
            {
                displayList.Add(new
                {
                    UserID = user["UserID"],
                    FirstName = user["FirstName"],
                    LastName = user["LastName"],
                    Age = user["Age"],
                    Gender = user["Gender"],
                    Weight = user["Weight"],
                    Goal = user["Goal"]
                });

            }
             return displayList;
        }
    }
}
