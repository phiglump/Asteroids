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
using System.IO;

namespace Asteroids
{
    /// <summary>
    /// Interaction logic for GameOverScreen.xaml
    /// </summary>
    public partial class GameOverScreen : Page
    {
        TextWriter sw = new StreamWriter("highscores.txt", true);
        public GameOverScreen()
        {
            InitializeComponent();
            txtboxInitials.MaxLength = 3;
            txtboxInitials.CharacterCasing = CharacterCasing.Upper;
        }

        public void databaseWrite(int score)
        {
            int Score = score;
            sw.WriteLine(Score);
            sw.Close();
            finalscore.Text = Score.ToString();
            
        }

        public void getinitials()
        {
            String initials = txtboxInitials.Text;
            sw.WriteLine(initials);
            sw.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.getinitials();
            this.NavigationService.Navigate(new Uri("HighScoreScreen.xaml", UriKind.Relative));
        }
    }
}
