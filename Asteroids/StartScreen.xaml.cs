﻿using System;
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
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : Page
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //This will navigate the the Gamescreen when the Start button is clicked
            this.NavigationService.Navigate(new Uri("GameScreen.xaml", UriKind.Relative));
            
        }

        private void Highscores_Click(object sender, RoutedEventArgs e)
        {
            //This will navigate the the HighScore screen when the HighScore button is clicked
            this.NavigationService.Navigate(new Uri("HighScoreScreen.xaml", UriKind.Relative));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //This will exit the game when the exit button is clicked
            System.Windows.Application.Current.Shutdown();
        }
    }
}
