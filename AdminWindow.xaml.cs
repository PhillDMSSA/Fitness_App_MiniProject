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
    
    public partial class AdminWindow : Window
    {
       
        public AdminWindow()
        {
            InitializeComponent();
            
        }
        private void Button_ViewChart_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DataPage());

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) //allows the mouse to drap application w/ left button on mouse
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void ButtonMinimixe_Click(object sender, RoutedEventArgs e) //minimize window
        {
            WindowState = WindowState.Minimized;
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e) //closes window
        {
            MessageBox.Show("Error Occurred: You have been logged off!");
            Application.Current.Shutdown();
            
        }
        private void ButtonLogOff_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            MessageBox.Show("Logged off successful!");


            this.Close(); //closest current window
        }
    }
}
