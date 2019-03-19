using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Asteroids
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Page
    {
        double x = 480;
        double y = 366;
        double angle;
        public GameScreen()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(MovePlayer);
            timer.Start();
        }
        private void MovePlayer(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.A))
            {
                x -= .05;
                Canvas.SetLeft(rec1, x);
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                y -= .05;
                Canvas.SetTop(rec1, y);
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                y += .05;
                Canvas.SetTop(rec1, y);
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                x += .05;
                Canvas.SetLeft(rec1, x);
            }
            if (Keyboard.IsKeyDown(Key.Left))
            {
                RotateTransform rotateTransform2 = new RotateTransform();
                rotateTransform2.CenterX = 25;
                rotateTransform2.CenterY = 25;
                angle = angle - 0.125;
                rotateTransform2.Angle = angle;
                rec1.RenderTransform = rotateTransform2;
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                RotateTransform rotateTransform1 = new RotateTransform();
                rotateTransform1.CenterX = 25;
                rotateTransform1.CenterY = 25;
                angle = angle + 0.125;
                rotateTransform1.Angle = angle;
                rec1.RenderTransform = rotateTransform1;
            }
        }
    }
}
