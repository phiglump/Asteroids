using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public TimeSpan Interval { get; set; }
        double b = 480;
        double c = 366;
        double angle;
        public GameScreen()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(MovePlayer);
            timer.Start();
            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Tick += MoveAsteroid;
            timer2.Interval = TimeSpan.FromMilliseconds(12);
            timer2.Start();
            DispatcherTimer timer3 = new DispatcherTimer();
            timer3.Tick += MoveAsteroid1;
            timer3.Interval = TimeSpan.FromMilliseconds(12);
            timer3.Start();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
        }
        Stopwatch stopwatch1 = Stopwatch.StartNew();
        Stopwatch stopwatch2 = Stopwatch.StartNew();
        Random rand = new Random();
        int iSpeed = 5;
        int fDirection1 = 135;
        int fDirection2 = 135;
        double x = 0;
        double y = 0;
        double a = 100;
        double d = 100;
        private void MoveAsteroid(object sender, EventArgs e)
        {
            // use trig to work out new value for X & Y based on Speed and direction vector
            int iNewX = (int)(iSpeed * System.Math.Sin((System.Math.PI * fDirection1) / 180));  // uses radians
            int iNewY = -(int)(iSpeed * System.Math.Cos((System.Math.PI * fDirection1) / 180));
            x += iNewX;
            y += iNewY;
            Canvas.SetLeft(asteroid, x);
            Canvas.SetTop(asteroid, y);
            if(stopwatch1.Elapsed >= TimeSpan.FromSeconds(8))
            {
                x = rand.Next(0, 960);
                y = rand.Next(0, 500);
                stopwatch1.Restart();
                //Quadrant 1
                if (x >= 480 && y < 250)
                {
                    fDirection1 = rand.Next(180, 260);
                }
                //Quadrant 2
                else if (x < 480 && y < 250)
                {
                    fDirection1 = rand.Next(90, 180);
                }
                //Quadrant 3
                else if (x < 480 && y >= 250)
                {
                    fDirection1 = rand.Next(0, 90);
                }
                //Quadrant 4
                else if (x >= 480 && y >= 250)
                {
                    fDirection1 = rand.Next(260, 360);
                }
            }
        }
        private void MoveAsteroid1(object sender, EventArgs e)
        {
            // use trig to work out new value for X & Y based on Speed and direction vector
            int iNewX = (int)(iSpeed * System.Math.Sin((System.Math.PI * fDirection2) / 180));  // uses radians
            int iNewY = -(int)(iSpeed * System.Math.Cos((System.Math.PI * fDirection2) / 180));
            a += iNewX;
            d += iNewY;
            Canvas.SetLeft(asteroid1, a);
            Canvas.SetTop(asteroid1, d);
            if (stopwatch2.Elapsed >= TimeSpan.FromSeconds(8))
            {
                a = rand.Next(0, 960);
                d = rand.Next(0, 500);
                stopwatch2.Restart();
                //Quadrant 1
                if (a >= 480 && d < 250)
                {
                    fDirection2 = rand.Next(180, 260);
                }
                //Quadrant 2
                else if (a < 480 && d < 250)
                {
                    fDirection2 = rand.Next(90, 180);
                }
                //Quadrant 3
                else if (a < 480 && d >= 250)
                {
                    fDirection2 = rand.Next(0, 90);
                }
                //Quadrant 4
                else if (a >= 480 && d >= 250)
                {
                    fDirection2 = rand.Next(260, 360);
                }
            }

        }
        private void MovePlayer(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.A))
            {
                b -= .05;
                Canvas.SetLeft(rec1, b);
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                c -= .05;
                Canvas.SetTop(rec1, c);
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                c += .05;
                Canvas.SetTop(rec1, c);
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                b += .05;
                Canvas.SetLeft(rec1, b);
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
