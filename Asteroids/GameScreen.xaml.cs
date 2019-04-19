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
            //Runs the Initialize Component funciton and the start timers as well
            InitializeComponent();
            startTimers();
        }
        #region Timers
        public void startTimers()
        {
            //This timer runs the move play function
            //That function doesn't have to be on the timer in order to work
            //But  this allows the ship to move much smoother over the screen
            DispatcherTimer timerShip = new DispatcherTimer();
            timerShip.Tick += new EventHandler(MovePlayer);
            timerShip.Start();
            //Runs the laser for the space ship
            DispatcherTimer timerLaser = new DispatcherTimer();
            timerLaser.Tick += new EventHandler(FireLaser);
            timerLaser.Interval = TimeSpan.FromMilliseconds(10);
            timerLaser.Start();
            //Runs the Asteroids to keep the moving smoothly
            DispatcherTimer timerAsteroids = new DispatcherTimer();
            timerAsteroids.Tick += MoveAsteroid1;
            timerAsteroids.Tick += MoveAsteroid2;
            timerAsteroids.Tick += MoveAsteroid3;
            timerAsteroids.Tick += MoveAsteroid4;
            timerAsteroids.Tick += MoveAsteroid5;
            timerAsteroids.Interval = TimeSpan.FromMilliseconds(12);
            timerAsteroids.Start();
            //Misc timer for other functions includes the Random XY generation and the HitDetection
            DispatcherTimer timerMisc = new DispatcherTimer();
            timerMisc.Tick += RandomXYGen;
            timerMisc.Tick += HitDetection;
            timerMisc.Interval = TimeSpan.FromMilliseconds(1);
            timerMisc.Start();
            
        }
        #endregion

        #region Variables
        public TimeSpan Interval { get; set; }
        Random rand = new Random();
        Stopwatch watch = new Stopwatch();
        //Variables for the ship and its lasers
        double a = 400;
        double shipX = 451;
        double shipY = 421;
        double angle;
        int spaceShipLives = 4;
        //All of the following is for the asteroids
        //It contains variables that represent the Direction, Position, and Speed
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
        //Counters to count the number of times the asteroid has been reset on the screen
        int counter = 0;
        int counter2 = 0;
        int counter3 = 0;
        int counter4 = 0;
        int counter5 = 0;
        //bool for the hit detection function
        bool isHit = false;
        #endregion

        #region Hit Detection
        public void HitDetection(object sender, EventArgs e)
        {
            //The following creates rectangles that represent the position of the asteroids and the Spacship on the canvas
            //Allows the ability to fine tune the size of the hit box
            scoredisplayed.Text = spaceShipLives.ToString();
            Rect spaceShip = new Rect(Canvas.GetLeft(SpaceShip), Canvas.GetTop(SpaceShip), SpaceShip.Width , SpaceShip.Height);
            Rect ast1 = new Rect(Canvas.GetLeft(asteroid1), Canvas.GetTop(asteroid1), asteroid1.Width, asteroid1.Height);
            Rect ast2 = new Rect(Canvas.GetLeft(asteroid2), Canvas.GetTop(asteroid2), asteroid2.Width, asteroid2.Height);
            Rect ast3 = new Rect(Canvas.GetLeft(asteroid3), Canvas.GetTop(asteroid3), asteroid3.Width, asteroid3.Height);
            Rect ast4 = new Rect(Canvas.GetLeft(asteroid4), Canvas.GetTop(asteroid4), asteroid4.Width, asteroid4.Height);
            Rect ast5 = new Rect(Canvas.GetLeft(asteroid5), Canvas.GetTop(asteroid5), asteroid5.Width, asteroid5.Height);

            //So long as the isHit is false then the internal if statements can run
            //because this function is ran on a timer if this wasn't done then it would constantly remove a life every time the timer ticks
            if (isHit == false)
            {
                //if the hit box intersects then the spacShipLives is subtracted once and the isHit is set to true 
                //applies to all of the internal if statements below for each of the asteroids
                if (spaceShip.IntersectsWith(ast1))
                {
                    // reduces the score by 1 each time the asteroid interacts with the spaceship.
                    spaceShipLives -= 1;
                    isHit = true;
                    // checks to see if the spaceshiplives is equal to 3 and then sets the lives to 3
                    if (spaceShipLives == 3)
                    {
                        shiplives4.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 2 and then sets the lives to 2
                    if (spaceShipLives == 2)
                    {
                        shiplives3.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 1 and then sets the lives to 1
                    if (spaceShipLives == 1)
                    {
                        shiplives2.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 0 and then sets the lives to 0
                    // send the user to the GameOverScreen
                    if (spaceShipLives == 0)
                    {
                        shiplives1.Visibility = Visibility.Hidden;
                        this.NavigationService.Navigate(new Uri("GameOverScreen.xaml", UriKind.Relative));
                    }

                }
                if (spaceShip.IntersectsWith(ast2))
                {
                    // reduces the score by 1 each time the asteroid interacts with the spaceship.
                    spaceShipLives -= 1;
                    isHit = true;
                    // checks to see if the spaceshiplives is equal to 3 and then sets the lives to 3
                    if (spaceShipLives == 3)
                    {
                        shiplives4.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 2 and then sets the lives to 2
                    if (spaceShipLives == 2)
                    {
                        shiplives3.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 1 and then sets the lives to 1
                    if (spaceShipLives == 1)
                    {
                        shiplives2.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 0 and then sets the lives to 0
                    // send the user to the GameOverScreen
                    if (spaceShipLives == 0)
                    {
                        shiplives1.Visibility = Visibility.Hidden;
                        this.NavigationService.Navigate(new Uri("GameOverScreen.xaml", UriKind.Relative));
                    }
                }
                if (spaceShip.IntersectsWith(ast3))
                {
                    // reduces the score by 1 each time the asteroid interacts with the spaceship.
                    spaceShipLives -= 1;
                    isHit = true;
                    // checks to see if the spaceshiplives is equal to 3 and then sets the lives to 3
                    if (spaceShipLives == 3)
                    {
                        shiplives4.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 2 and then sets the lives to 2
                    if (spaceShipLives == 2)
                    {
                        shiplives3.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 1 and then sets the lives to 1
                    if (spaceShipLives == 1)
                    {
                        shiplives2.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 0 and then sets the lives to 0
                    // send the user to the GameOverScreen
                    if (spaceShipLives == 0)
                    {
                        shiplives1.Visibility = Visibility.Hidden;
                        this.NavigationService.Navigate(new Uri("GameOverScreen.xaml", UriKind.Relative));
                    }
                }
                if (spaceShip.IntersectsWith(ast4))
                {
                    // reduces the score by 1 each time the asteroid interacts with the spaceship.
                    spaceShipLives -= 1;
                    isHit = true;
                    // checks to see if the spaceshiplives is equal to 3 and then sets the lives to 3
                    if (spaceShipLives == 3)
                    {
                        shiplives4.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 2 and then sets the lives to 2
                    if (spaceShipLives == 2)
                    {
                        shiplives3.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 1 and then sets the lives to 1
                    if (spaceShipLives == 1)
                    {
                        shiplives2.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 0 and then sets the lives to 0
                    // send the user to the GameOverScreen
                    if (spaceShipLives == 0)
                    {
                        shiplives1.Visibility = Visibility.Hidden;
                        this.NavigationService.Navigate(new Uri("GameOverScreen.xaml", UriKind.Relative));
                    }
                }
                if (spaceShip.IntersectsWith(ast5))
                {
                    // reduces the score by 1 each time the asteroid interacts with the spaceship.
                    spaceShipLives -= 1;
                    isHit = true;
                    // checks to see if the spaceshiplives is equal to 3 and then sets the lives to 3
                    if (spaceShipLives == 3)
                    {
                        shiplives4.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 2 and then sets the lives to 2
                    if (spaceShipLives == 2)
                    {
                        shiplives3.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 1 and then sets the lives to 1
                    if (spaceShipLives == 1)
                    {
                        shiplives2.Visibility = Visibility.Hidden;
                    }
                    // checks to see if the spaceshiplives is equal to 0 and then sets the lives to 0
                    // send the user to the GameOverScreen
                    if (spaceShipLives == 0)
                    {
                        shiplives1.Visibility = Visibility.Hidden;
                        this.NavigationService.Navigate(new Uri("GameOverScreen.xaml", UriKind.Relative));
                    }
                }
            }
            //once the isHit is set to true then the next internal function is ran
            if (isHit == true)
            {
                //A stopwatch is started
                watch.Start();
                //once the timer has exceeded 3 seconds then the stopwatch will stop, reset and then the isHit is set to false
                //essentially this is giving the ship invincibility frames for 3 seconds so that the player has time to adjust
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
        //This generator creates random XY values for the asteroids to use
        //This was done to prevent the asteroids from spawning in on the screen
        //The rand will choose x values from the array listed below.
        //Then if statements will determine where the the x was positioned and based off of that will determine the y position
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
            int iNewX = (int)(iSpeed * System.Math.Sin((System.Math.PI * fDirection1) / 180)); 
            int iNewY = -(int)(iSpeed * System.Math.Cos((System.Math.PI * fDirection1) / 180));
            //sets the asteroids x and y to the new x and y created above
            Ast1X += iNewX;
            Ast1Y += iNewY;
            Canvas.SetLeft(asteroid1, Ast1X);
            Canvas.SetTop(asteroid1, Ast1Y);
            //The following if statements checks the counter, that relates to whether the asteroid has left the screen yet, 
            //and if it hasn't will, meaning it's less than 1, then once the asteroid has passed a border boundry it will
            //set the asteroid to the other side of the screen and then set the counter to 1 not allowing to be reset again
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
            //after the asteroid has been reset once it is to be respawned completely
            //once the asteroid has passed a border boundry again it will set the 
            //asteroid x and y to the x and y from the XY generator
            else if (Ast1X > 965 || Ast1X < -5 || Ast1Y > 505 || Ast1Y < -5)
            {
                Ast1X = tempX;
                Ast1Y = tempY;
                //The following if statements will determine what quadrant the asteroid has spawned in
                //and based off of that the asteroid will be set to a corresponding direction
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
            //sets the asteroids x and y to the new x and y created above
            Ast2X += iNewX;
            Ast2Y += iNewY;
            Canvas.SetLeft(asteroid2, Ast2X);
            Canvas.SetTop(asteroid2, Ast2Y);
            //The following if statements checks the counter, that relates to whether the asteroid has left the screen yet, 
            //and if it hasn't will, meaning it's less than 1, then once the asteroid has passed a border boundry it will
            //set the asteroid to the other side of the screen and then set the counter to 1 not allowing to be reset again
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
            //after the asteroid has been reset once it is to be respawned completely
            //once the asteroid has passed a border boundry again it will set the 
            //asteroid x and y to the x and y from the XY generator
            else if (Ast2X > 965 || Ast2X < -5 || Ast2Y > 505 || Ast2Y < -5)
            {
                Ast2X = tempX;
                Ast2Y = tempY;
                //The following if statements will determine what quadrant the asteroid has spawned in
                //and based off of that the asteroid will be set to a corresponding direction
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
            //sets the asteroids x and y to the new x and y created above
            Ast3X += iNewX;
            Ast3Y += iNewY;
            Canvas.SetLeft(asteroid3, Ast3X);
            Canvas.SetTop(asteroid3, Ast3Y);
            //The following if statements checks the counter, that relates to whether the asteroid has left the screen yet, 
            //and if it hasn't will, meaning it's less than 1, then once the asteroid has passed a border boundry it will
            //set the asteroid to the other side of the screen and then set the counter to 1 not allowing to be reset again
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
            //after the asteroid has been reset once it is to be respawned completely
            //once the asteroid has passed a border boundry again it will set the 
            //asteroid x and y to the x and y from the XY generator
            else if (Ast3X > 965 || Ast3X < -5 || Ast3Y > 505 || Ast3Y < -5)
            {
                Ast3X = tempX;
                Ast3Y = tempY;
                //The following if statements will determine what quadrant the asteroid has spawned in
                //and based off of that the asteroid will be set to a corresponding direction
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
            //sets the asteroids x and y to the new x and y created above
            Ast4X += iNewX;
            Ast4Y += iNewY;
            Canvas.SetLeft(asteroid4, Ast4X);
            Canvas.SetTop(asteroid4, Ast4Y);
            //The following if statements checks the counter, that relates to whether the asteroid has left the screen yet, 
            //and if it hasn't will, meaning it's less than 1, then once the asteroid has passed a border boundry it will
            //set the asteroid to the other side of the screen and then set the counter to 1 not allowing to be reset again
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
            //after the asteroid has been reset once it is to be respawned completely
            //once the asteroid has passed a border boundry again it will set the 
            //asteroid x and y to the x and y from the XY generator
            else if (Ast4X > 965 || Ast4X < -5 || Ast4Y > 505 || Ast4Y < -5)
            {
                Ast4X = tempX;
                Ast4Y = tempY;
                //The following if statements will determine what quadrant the asteroid has spawned in
                //and based off of that the asteroid will be set to a corresponding direction
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
            //sets the asteroids x and y to the new x and y created above
            Ast5X += iNewX;
            Ast5Y += iNewY;
            Canvas.SetLeft(asteroid5, Ast5X);
            Canvas.SetTop(asteroid5, Ast5Y);
            //The following if statements checks the counter, that relates to whether the asteroid has left the screen yet, 
            //and if it hasn't will, meaning it's less than 1, then once the asteroid has passed a border boundry it will
            //set the asteroid to the other side of the screen and then set the counter to 1 not allowing to be reset again
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
            //after the asteroid has been reset once it is to be respawned completely
            //once the asteroid has passed a border boundry again it will set the 
            //asteroid x and y to the x and y from the XY generator
            else if (Ast5X > 965 || Ast5X < -5 || Ast5Y > 505 || Ast5Y < -5)
            {
                Ast5X = tempX;
                Ast5Y = tempY;
                //The following if statements will determine what quadrant the asteroid has spawned in
                //and based off of that the asteroid will be set to a corresponding direction
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
            //This function uses if statement to determine what key is pressed
            //based off of the key a corresponding movement is applied
            if (Keyboard.IsKeyDown(Key.A))
            {
                //the ship moves by a factor of .05
                //if the ship has reached a boundry it will spawn on the opposite side
                shipX -= .05;
                Canvas.SetLeft(SpaceShip, shipX);
                if (shipX < -10)
                {
                    shipX = 960;
                }
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                //the ship moves by a factor of .05
                //if the ship has reached a boundry it will spawn on the opposite side
                shipY -= .05;
                Canvas.SetTop(SpaceShip, shipY);
                if (shipY < -10)
                {
                    shipY = 500;
                }

            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                //the ship moves by a factor of .05
                //if the ship has reached a boundry it will spawn on the opposite side
                shipY += .05;
                Canvas.SetTop(SpaceShip, shipY);
                if (shipY > 505)
                {
                    shipY = 0;
                }
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                //the ship moves by a factor of .05
                //if the ship has reached a boundry it will spawn on the opposite side
                shipX += .05;
                Canvas.SetLeft(SpaceShip, shipX);
                if (shipX > 965)
                {
                    shipX = 0;
                }
            }
            //The last two if statements here will rotate the ship 
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