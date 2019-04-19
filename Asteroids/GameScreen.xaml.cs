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
        
        public GameScreen()
        {
            InitializeComponent();
            startTimers();
        }
        #region Timers
        public void startTimers()
        {
            DispatcherTimer timerShip = new DispatcherTimer();
            timerShip.Tick += new EventHandler(MovePlayer);
            timerShip.Start();

            DispatcherTimer timerLaser = new DispatcherTimer();
            timerLaser.Tick += new EventHandler(FireLaser);
            timerLaser.Interval = TimeSpan.FromMilliseconds(10);
            timerLaser.Start();

            DispatcherTimer timerAsteroids = new DispatcherTimer();
            timerAsteroids.Tick += MoveAsteroid1;
            timerAsteroids.Tick += MoveAsteroid2;
            timerAsteroids.Tick += MoveAsteroid3;
            timerAsteroids.Tick += MoveAsteroid4;
            timerAsteroids.Tick += MoveAsteroid5;
            timerAsteroids.Interval = TimeSpan.FromMilliseconds(12);
            timerAsteroids.Start();

            DispatcherTimer timerXYGen = new DispatcherTimer();
            timerXYGen.Tick += RandomXYGen;
            timerXYGen.Interval = TimeSpan.FromMilliseconds(1);
            timerXYGen.Start();

            DispatcherTimer timerHitDetection = new DispatcherTimer();
            timerHitDetection.Tick += HitDetection;
            timerHitDetection.Interval = TimeSpan.FromMilliseconds(100);
            timerHitDetection.Start();
            
        }
        #endregion

        #region Variables
        public TimeSpan Interval { get; set; }
        double a = 400;
        double b = 451;
        double c = 421;
        double angle;
        Random rand = new Random();
        int spaceShipLives = 4;
        int tempX = 0;
        int tempY = 0;
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
        int counter = 0;
        int counter2 = 0;
        int counter3 = 0;
        int counter4 = 0;
        int counter5 = 0;
        Stopwatch watch = new Stopwatch();
        bool isHit = false;
        #endregion

        #region Hit Detection
        public void HitDetection(object sender, EventArgs e)
        {
            scoredisplayed.Text = spaceShipLives.ToString();
            Rect spaceShip = new Rect(Canvas.GetLeft(SpaceShip), Canvas.GetTop(SpaceShip), SpaceShip.Width , SpaceShip.Height);
            Rect ast1 = new Rect(Canvas.GetLeft(asteroid1), Canvas.GetTop(asteroid1), asteroid1.Width, asteroid1.Height);
            Rect ast2 = new Rect(Canvas.GetLeft(asteroid2), Canvas.GetTop(asteroid2), asteroid2.Width, asteroid2.Height);
            Rect ast3 = new Rect(Canvas.GetLeft(asteroid3), Canvas.GetTop(asteroid3), asteroid3.Width, asteroid3.Height);
            Rect ast4 = new Rect(Canvas.GetLeft(asteroid4), Canvas.GetTop(asteroid4), asteroid4.Width, asteroid4.Height);
            Rect ast5 = new Rect(Canvas.GetLeft(asteroid5), Canvas.GetTop(asteroid5), asteroid5.Width, asteroid5.Height);
            if (isHit == false)
            {
                if (spaceShip.IntersectsWith(ast1))
                {
                    spaceShipLives -= 1;
                    isHit = true;
                }
                if (spaceShip.IntersectsWith(ast2))
                {
                    spaceShipLives -= 1;
                    isHit = true;
                }
                if (spaceShip.IntersectsWith(ast3))
                {
                    spaceShipLives -= 1;
                    isHit = true;
                }
                if (spaceShip.IntersectsWith(ast4))
                {
                    spaceShipLives -= 1;
                    isHit = true;
                }
                if (spaceShip.IntersectsWith(ast5))
                {
                    spaceShipLives -= 1;
                    isHit = true;
                }
            }
            if (isHit == true)
            {
                watch.Start();
                if(watch.ElapsedMilliseconds > 3000)
                {
                    watch.Stop();
                    watch.Reset();
                    isHit = false;
                }
            }
        }
        #endregion

        #region XYGenerator
        private void RandomXYGen(object sender, EventArgs e)
        {
            int[] intXValues = { -5, -4, -3, -2, -1, 96, 192, 288, 384, 479, 576, 672, 768, 864, 959, 961, 962, 963, 964 };
            int[] intYValues = { 50, 100, 150, 200, 249, 300, 350, 400, 450, 499 };
            int[] intYValuesTB = { -5, -4, -3, -2, -1, 501, 502, 503, 504, 505 };
            int temp1;
            int temp2;
            temp1 = rand.Next(intXValues.Length);
            if (temp1 < 0)
            {
                temp2 = rand.Next(intYValues.Length);
                tempX = intXValues[temp1];
                tempY = intYValues[temp2];
            }
            else if (0 < temp1 && temp1 < 960)
            {
                temp2 = rand.Next(intYValuesTB.Length);
                tempX = intXValues[temp1];
                tempY = intYValuesTB[temp2];
            }
            else if (temp1 > 960)
            {
                temp2 = rand.Next(intYValues.Length);
                tempX = intXValues[temp1];
                tempY = intYValues[temp2];
            }
        }
        #endregion

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
            if (counter < 1)
            {
                if (Ast1X > 963)
                {
                    Ast1X = -3;
                    counter++;
                }
                if (Ast1X < -3)
                {
                    Ast1X = 963;
                    counter++;
                }
                if (Ast1Y > 503)
                {
                    Ast1Y = -3;
                    counter++;
                }
                if (Ast1Y < -3)
                {
                    Ast1Y = 503;
                    counter++;
                }
            }
            else if (Ast1X > 965 || Ast1X < -5 || Ast1Y > 505 || Ast1Y < -5)
            {
                Ast1X = tempX;
                Ast1Y = tempY;
                //Quadrant 1
                if (Ast1X >= 480 && Ast1Y < 250)
                {
                    fDirection1 = rand.Next(200, 250);
                    counter = 0;
                }
                //Quadrant 2
                else if (Ast1X < 480 && Ast1Y < 250)
                {
                    fDirection1 = rand.Next(110, 160);
                    counter = 0;
                }
                //Quadrant 3
                else if (Ast1X < 480 && Ast1Y >= 250)
                {
                    fDirection1 = rand.Next(20, 70);
                    counter = 0;
                }
                //Quadrant 4
                else if (Ast1X >= 480 && Ast1Y >= 250)
                {
                    fDirection1 = rand.Next(290, 340);
                    counter = 0;
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
            if (counter2 < 1)
            {
                if (Ast2X > 963)
                {
                    Ast2X = -3;
                    counter2++;
                }
                if (Ast2X < -3)
                {
                    Ast2X = 963;
                    counter2++;
                }
                if (Ast2Y > 503)
                {
                    Ast2Y = -3;
                    counter2++;
                }
                if (Ast2Y < -3)
                {
                    Ast2Y = 503;
                    counter2++;
                }
            }
            else if (Ast2X > 965 || Ast2X < -5 || Ast2Y > 505 || Ast2Y < -5)
            {
                Ast2X = tempX;
                Ast2Y = tempY;
                //Quadrant 1
                if (Ast2X >= 480 && Ast2Y < 250)
                {
                    fDirection2 = rand.Next(200, 250);
                    counter2 = 0;
                }
                //Quadrant 2
                else if (Ast2X < 480 && Ast2Y < 250)
                {
                    fDirection2 = rand.Next(110, 160);
                    counter2 = 0;
                }
                //Quadrant 3
                else if (Ast2X < 480 && Ast2Y >= 250)
                {
                    fDirection2 = rand.Next(20, 70);
                    counter2 = 0;
                }
                //Quadrant 4
                else if (Ast2X >= 480 && Ast2Y >= 250)
                {
                    fDirection2 = rand.Next(290, 340);
                    counter2 = 0;
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
            if (counter3 < 1)
            {
                if (Ast3X > 963)
                {
                    Ast3X = -3;
                    counter3++;
                }
                if (Ast3X < -3)
                {
                    Ast3X = 963;
                    counter3++;
                }
                if (Ast3Y > 503)
                {
                    Ast3Y = -3;
                    counter3++;
                }
                if (Ast3Y < -3)
                {
                    Ast3Y = 503;
                    counter3++;
                }
            }
            else if (Ast3X > 965 || Ast3X < -5 || Ast3Y > 505 || Ast3Y < -5)
            {
                Ast3X = tempX;
                Ast3Y = tempY;
                //Quadrant 1
                if (Ast3X >= 480 && Ast3Y < 250)
                {
                    fDirection3 = rand.Next(200, 250);
                    counter3 = 0;
                }
                //Quadrant 2
                else if (Ast3X < 480 && Ast3Y < 250)
                {
                    fDirection3 = rand.Next(110, 160);
                    counter3 = 0;
                }
                //Quadrant 3
                else if (Ast3X < 480 && Ast3Y >= 250)
                {
                    fDirection3 = rand.Next(20, 70);
                    counter3 = 0;
                }
                //Quadrant 4
                else if (Ast3X >= 480 && Ast3Y >= 250)
                {
                    fDirection3 = rand.Next(290, 340);
                    counter3 = 0;
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
            if (counter4 < 1)
            {
                if (Ast4X > 963)
                {
                    Ast4X = -3;
                    counter4++;
                }
                if (Ast4X < -3)
                {
                    Ast4X = 963;
                    counter4++;
                }
                if (Ast4Y > 503)
                {
                    Ast4Y = -3;
                    counter4++;
                }
                if (Ast4Y < -3)
                {
                    Ast4Y = 503;
                    counter4++;
                }
            }
            else if (Ast4X > 965 || Ast4X < -5 || Ast4Y > 505 || Ast4Y < -5)
            {
                Ast4X = tempX;
                Ast4Y = tempY;
                //Quadrant 1
                if (Ast4X >= 480 && Ast4Y < 250)
                {
                    fDirection4 = rand.Next(200, 250);
                    counter4 = 0;
                }
                //Quadrant 2
                else if (Ast4X < 480 && Ast4Y < 250)
                {
                    fDirection4 = rand.Next(110, 160);
                    counter4 = 0;
                }
                //Quadrant 3
                else if (Ast4X < 480 && Ast4Y >= 250)
                {
                    fDirection4 = rand.Next(20, 70);
                    counter4 = 0;
                }
                //Quadrant 4
                else if (Ast4X >= 480 && Ast4Y >= 250)
                {
                    fDirection4 = rand.Next(290, 340);
                    counter4 = 0;
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
            if (counter5 < 1)
            {
                if (Ast5X > 963)
                {
                    Ast5X = -3;
                    counter5++;
                }
                if (Ast5X < -3)
                {
                    Ast5X = 963;
                    counter5++;
                }
                if (Ast5Y > 503)
                {
                    Ast5Y = -3;
                    counter5++;
                }
                if (Ast5Y < -3)
                {
                    Ast5Y = 503;
                    counter5++;
                }
            }
            else if (Ast5X > 965 || Ast5X < -5 || Ast5Y > 505 || Ast5Y < -5)
            {
                Ast5X = tempX;
                Ast5Y = tempY;
                //Quadrant 1
                if (Ast5X >= 480 && Ast5Y < 250)
                {
                    fDirection5 = rand.Next(200, 250);
                    counter5 = 0;
                }
                //Quadrant 2
                else if (Ast5X < 480 && Ast5Y < 250)
                {
                    fDirection5 = rand.Next(110, 160);
                    counter5 = 0;
                }
                //Quadrant 3
                else if (Ast5X < 480 && Ast5Y >= 250)
                {
                    fDirection5 = rand.Next(20, 70);
                    counter5 = 0;
                }
                //Quadrant 4
                else if (Ast5X >= 480 && Ast5Y >= 250)
                {
                    fDirection5 = rand.Next(290, 340);
                    counter5 = 0;
                }
            }

        }

        #endregion

        #region Laser
        private void FireLaser(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyToggled(Key.Space))
            {
                a -= 3.5;
                Canvas.SetTop(laser, a);
            }

        }
        #endregion

        #region Player Movement
        private void MovePlayer(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.A))
            {
                b -= .05;
                Canvas.SetLeft(SpaceShip, b);
                if (b < -10)
                {
                    b = 960;
                }
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                c -= .05;
                Canvas.SetTop(SpaceShip, c);
                if (c < -10)
                {
                    c = 500;
                }

            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                c += .05;
                Canvas.SetTop(SpaceShip, c);
                if (c > 505)
                {
                    c = 0;
                }
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                b += .05;
                Canvas.SetLeft(SpaceShip, b);
                if (b > 965)
                {
                    b = 0;
                }
            }
            if (Keyboard.IsKeyDown(Key.NumPad4))
            {
                RotateTransform rotateTransform2 = new RotateTransform();
                rotateTransform2.CenterX = 1;
                rotateTransform2.CenterY = 1;
                angle = angle - 0.125;
                rotateTransform2.Angle = angle;
                SpaceShip.RenderTransform = rotateTransform2;
            }
            if (Keyboard.IsKeyDown(Key.NumPad6))
            {
                RotateTransform rotateTransform1 = new RotateTransform();
                rotateTransform1.CenterX = 1;
                rotateTransform1.CenterY = 1;
                angle = angle + 0.125;
                rotateTransform1.Angle = angle;
                SpaceShip.RenderTransform = rotateTransform1;
            }
        }
        #endregion
    }
}