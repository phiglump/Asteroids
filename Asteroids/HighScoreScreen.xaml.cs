using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
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
        public HighScoreScreen()
        {
            InitializeComponent();
            this.databaseRead();

        }

        public void databaseRead()
        {
            String line;
            StreamReader sr = new StreamReader("highscores.txt");
            line = sr.ReadLine();
            bool check = true;

            while(line != null)
            {
                if ( check == true)
                {
                    txtscores.AppendText(line);
                    txtscores.AppendText(Environment.NewLine);
                    line = sr.ReadLine();
                    check = false;
                }
                else
                {
                    txtinitials.AppendText(line);
                    txtinitials.AppendText(Environment.NewLine);
                    line = sr.ReadLine();
                    check = true;
                }
            }
            sr.Close();

            for(int i = 1; i <= 10; i++)
            {
                txtnumbers.AppendText(i.ToString());
                txtnumbers.AppendText(Environment.NewLine);
            }
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
