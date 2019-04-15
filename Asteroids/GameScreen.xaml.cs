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
            #region Timers
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(MovePlayer);
            timer.Start();
            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Tick += MoveAsteroid1;
            timer2.Interval = TimeSpan.FromMilliseconds(12);
            timer2.Start();
            DispatcherTimer timer3 = new DispatcherTimer();
            timer3.Tick += MoveAsteroid2;
            timer3.Interval = TimeSpan.FromMilliseconds(12);
            timer3.Start();
            DispatcherTimer timer4 = new DispatcherTimer();
            timer4.Tick += MoveAsteroid3;
            timer4.Interval = TimeSpan.FromMilliseconds(12);
            timer4.Start();
            DispatcherTimer timer5 = new DispatcherTimer();
            timer5.Tick += MoveAsteroid4;
            timer5.Interval = TimeSpan.FromMilliseconds(12);
            timer5.Start();
            DispatcherTimer timer6 = new DispatcherTimer();
            timer6.Tick += MoveAsteroid5;
            timer6.Interval = TimeSpan.FromMilliseconds(12);
            timer6.Start();
            #endregion
        }
        Random rand = new Random();
        int iSpeed = 3;
        int fDirection1 = 135;
        int fDirection2 = 200;
        int fDirection3 = 200;
        int fDirection4 = 300;
        int fDirection5 = 300;
        double Ast1X = 0;
        double Ast1Y = 0;
        double Ast2X = 960;
        double Ast2Y = 100;
        double Ast3X = 800;
        double Ast3Y = 100;
        double Ast4X = 600;
        double Ast4Y = 400;
        double Ast5X = 500;
        double Ast5Y = 400;
        #region Asteriod1
        private void MoveAsteroid1(object sender, EventArgs e)
        {
            // use trig to work out new value for X & Y based on Speed and direction vector
            int iNewX = (int)(iSpeed * System.Math.Sin((System.Math.PI * fDirection1) / 180));  // uses radians
            int iNewY = -(int)(iSpeed * System.Math.Cos((System.Math.PI * fDirection1) / 180));
            Ast1X += iNewX;
            Ast1Y += iNewY;
            Canvas.SetLeft(asteroid1, Ast1X);
            Canvas.SetTop(asteroid1, Ast1Y);
            if(Ast1X > 1010 || Ast1X <-50 || Ast1Y > 550 || Ast1Y < -50)
            {
                Ast1X = rand.Next(0, 960);
                Ast1Y = rand.Next(0, 500);
                //Quadrant 1
                if (Ast1X >= 480 && Ast1Y < 250)
                {
                    fDirection1 = rand.Next(190, 260);
                }
                //Quadrant 2
                else if (Ast1X < 480 && Ast1Y < 250)
                {
                    fDirection1 = rand.Next(100, 170);
                }
                //Quadrant 3
                else if (Ast1X < 480 && Ast1Y >= 250)
                {
                    fDirection1 = rand.Next(10, 80);
                }
                //Quadrant 4
                else if (Ast1X >= 480 && Ast1Y >= 250)
                {
                    fDirection1 = rand.Next(270, 350);
                }
            }
        }
        #endregion 
        #region Asteroid2
        private void MoveAsteroid2(object sender, EventArgs e)
        {
            // use trig to work out new value for X & Y based on Speed and direction vector
            int iNewX = (int)(iSpeed * System.Math.Sin((System.Math.PI * fDirection2) / 180));  // uses radians
            int iNewY = -(int)(iSpeed * System.Math.Cos((System.Math.PI * fDirection2) / 180));
            Ast2X += iNewX;
            Ast2Y += iNewY;
            Canvas.SetLeft(asteroid2, Ast2X);
            Canvas.SetTop(asteroid2, Ast2Y);
            if (Ast2X > 1010 || Ast2X < -50 || Ast2Y > 550 || Ast2Y < -50)
            {
                Ast2X = rand.Next(0, 960);
                Ast2Y = rand.Next(0, 500);
                //Quadrant 1
                if (Ast2X >= 480 && Ast2Y < 250)
                {
                    fDirection2 = rand.Next(190, 260);
                }
                //Quadrant 2
                else if (Ast2X < 480 && Ast2Y < 250)
                {
                    fDirection2 = rand.Next(100, 170);
                }
                //Quadrant 3
                else if (Ast2X < 480 && Ast2Y >= 250)
                {
                    fDirection2 = rand.Next(10, 80);
                }
                //Quadrant 4
                else if (Ast2X >= 480 && Ast2Y >= 250)
                {
                    fDirection2 = rand.Next(280, 350);
                }
            }

        }
        #endregion
        #region Asteroid3
        private void MoveAsteroid3(object sender, EventArgs e)
        {
            // use trig to work out new value for X & Y based on Speed and direction vector
            int iNewX = (int)(iSpeed * System.Math.Sin((System.Math.PI * fDirection3) / 180));  // uses radians
            int iNewY = -(int)(iSpeed * System.Math.Cos((System.Math.PI * fDirection3) / 180));
            Ast3X += iNewX;
            Ast3Y += iNewY;
            Canvas.SetLeft(asteroid3, Ast3X);
            Canvas.SetTop(asteroid3, Ast3Y);
            if (Ast3X > 1010 || Ast3X < -50 || Ast3Y > 550 || Ast3Y < -50)
            {
                Ast3X = rand.Next(0, 960);
                Ast3Y = rand.Next(0, 500);
                //Quadrant 1
                if (Ast3X >= 480 && Ast3Y < 250)
                {
                    fDirection3 = rand.Next(190, 260);
                }
                //Quadrant 2
                else if (Ast3X < 480 && Ast3Y < 250)
                {
                    fDirection3 = rand.Next(100, 170);
                }
                //Quadrant 3
                else if (Ast3X < 480 && Ast3Y >= 250)
                {
                    fDirection3 = rand.Next(10, 80);
                }
                //Quadrant 4
                else if (Ast3X >= 480 && Ast3Y >= 250)
                {
                    fDirection3 = rand.Next(280, 350);
                }
            }

        }
        #endregion
        #region Asteroid4
        private void MoveAsteroid4(object sender, EventArgs e)
        {
            // use trig to work out new value for X & Y based on Speed and direction vector
            int iNewX = (int)(iSpeed * System.Math.Sin((System.Math.PI * fDirection4) / 180));  // uses radians
            int iNewY = -(int)(iSpeed * System.Math.Cos((System.Math.PI * fDirection4) / 180));
            Ast4X += iNewX;
            Ast4Y += iNewY;
            Canvas.SetLeft(asteroid4, Ast4X);
            Canvas.SetTop(asteroid4, Ast4Y);
            if (Ast4X > 1010 || Ast4X < -50 || Ast4Y > 550 || Ast4Y < -50)
            {
                Ast4X = rand.Next(0, 960);
                Ast4Y = rand.Next(0, 500);
                //Quadrant 1
                if (Ast4X >= 480 && Ast4Y < 250)
                {
                    fDirection4 = rand.Next(190, 260);
                }
                //Quadrant 2
                else if (Ast4X < 480 && Ast4Y < 250)
                {
                    fDirection4 = rand.Next(100, 170);
                }
                //Quadrant 3
                else if (Ast4X < 480 && Ast4Y >= 250)
                {
                    fDirection4 = rand.Next(10, 80);
                }
                //Quadrant 4
                else if (Ast4X >= 480 && Ast4Y >= 250)
                {
                    fDirection4 = rand.Next(280, 350);
                }
            }

        }
        #endregion
        #region Asteroid5
        private void MoveAsteroid5(object sender, EventArgs e)
        {
            // use trig to work out new value for X & Y based on Speed and direction vector
            int iNewX = (int)(iSpeed * System.Math.Sin((System.Math.PI * fDirection5) / 180));  // uses radians
            int iNewY = -(int)(iSpeed * System.Math.Cos((System.Math.PI * fDirection5) / 180));
            Ast5X += iNewX;
            Ast5Y += iNewY;
            Canvas.SetLeft(asteroid5, Ast5X);
            Canvas.SetTop(asteroid5, Ast5Y);
            if (Ast5X > 1010 || Ast5X < -50 || Ast5Y > 550 || Ast5Y < -50)
            {
                Ast5X = rand.Next(0, 960);
                Ast5Y = rand.Next(0, 500);
                //Quadrant 1
                if (Ast5X >= 480 && Ast5Y < 250)
                {
                    fDirection5 = rand.Next(190, 260);
                }
                //Quadrant 2
                else if (Ast5X < 480 && Ast5Y < 250)
                {
                    fDirection5 = rand.Next(100, 170);
                }
                //Quadrant 3
                else if (Ast5X < 480 && Ast5Y >= 250)
                {
                    fDirection5 = rand.Next(10, 80);
                }
                //Quadrant 4
                else if (Ast5X >= 480 && Ast5Y >= 250)
                {
                    fDirection5 = rand.Next(280, 350);
                }
            }

        }
        #endregion
        #region Player Movement
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
                rotateTransform2.CenterX = 1;
                rotateTransform2.CenterY = 1;
                angle = angle - 0.125;
                rotateTransform2.Angle = angle;
                rec1.RenderTransform = rotateTransform2;
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                RotateTransform rotateTransform1 = new RotateTransform();
                rotateTransform1.CenterX = 1;
                rotateTransform1.CenterY = 1;
                angle = angle + 0.125;
                rotateTransform1.Angle = angle;
                rec1.RenderTransform = rotateTransform1;
            }
        }
        #endregion
    }
}
