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
    /// Interaction logic for GameOverScreen.xaml
    /// </summary>
    public partial class GameOverScreen : Page
    {
        OleDbConnection cn;
        public GameOverScreen()
        {
            InitializeComponent();
            cn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = |DataDirectory|Highscores.accdb");
            txtboxInitials.MaxLength = 3;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
