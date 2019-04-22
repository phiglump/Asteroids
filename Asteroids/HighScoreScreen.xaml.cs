using System;
using System.Collections.Generic;
using System.Data.OleDb;
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

namespace Asteroids
{
    /// <summary>
    /// Interaction logic for HighScoreScreen.xaml
    /// </summary>
    public partial class HighScoreScreen : Page
    {
        OleDbConnection cn;
        public HighScoreScreen()
        {
            InitializeComponent();

        }
        //this creates a button that when clicked exits the program.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        
        //this creates a button that when clicked navigates the user to the menu screen.
        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StartScreen.xaml", UriKind.Relative));
        }
    }
}
