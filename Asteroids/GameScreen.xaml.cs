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
            timerLaser.Interval = TimeSpan.FromMilliseconds(70);
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
            timerMisc.Tick += HitDetectionLaser;
            timerMisc.Interval = TimeSpan.FromMilliseconds(1);
            timerMisc.Start();
            
        }
        #endregion

        #region Variables
        public TimeSpan Interval { get; set; }
        Random rand = new Random();
        Stopwatch watch = new Stopwatch();
        Stopwatch newWatch1 = new Stopwatch();
        Stopwatch newWatch2 = new Stopwatch();
        Stopwatch newWatch3 = new Stopwatch();
        Stopwatch newWatch4 = new Stopwatch();
        Stopwatch newWatch5 = new Stopwatch();
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

        #region Hit Detection for the Lasers and Score Counter
        // sets the score equal to 0
        int score = 0;
        // set the score ro have an increment of 20
        const int score_increment = 20;
        public void GameScore()
        {
            if (true == int.TryParse(scoredisplayed.Text, out score))
            {
                // sets the new score to the score + the score increment which is 20. this will add 20 points to the score
                score += score_increment;
                //this will display the score
                scoredisplayed.Text = (score).ToString();

            }
        }
        public void HitDetectionLaser(object sender, EventArgs e)
        {
            
            Rect ast1 = new Rect(Canvas.GetLeft(asteroid1), Canvas.GetTop(asteroid1), asteroid1.Width, asteroid1.Height);
            Rect ast2 = new Rect(Canvas.GetLeft(asteroid2), Canvas.GetTop(asteroid2), asteroid2.Width, asteroid2.Height);
            Rect ast3 = new Rect(Canvas.GetLeft(asteroid3), Canvas.GetTop(asteroid3), asteroid3.Width, asteroid3.Height);
            Rect ast4 = new Rect(Canvas.GetLeft(asteroid4), Canvas.GetTop(asteroid4), asteroid4.Width, asteroid4.Height);
            Rect ast5 = new Rect(Canvas.GetLeft(asteroid5), Canvas.GetTop(asteroid5), asteroid5.Width, asteroid5.Height);
            Rect bul1 = new Rect(Canvas.GetLeft(bullets[0]), Canvas.GetTop(bullets[0]), bullets[0].Width, bullets[0].Height);
            Rect bul2 = new Rect(Canvas.GetLeft(bullets[1]), Canvas.GetTop(bullets[1]), bullets[1].Width, bullets[1].Height);
            Rect bul3 = new Rect(Canvas.GetLeft(bullets[2]), Canvas.GetTop(bullets[2]), bullets[2].Width, bullets[2].Height);
            Rect bul4 = new Rect(Canvas.GetLeft(bullets[3]), Canvas.GetTop(bullets[3]), bullets[3].Width, bullets[3].Height);
            Rect bul5 = new Rect(Canvas.GetLeft(bullets[4]), Canvas.GetTop(bullets[4]), bullets[4].Width, bullets[4].Height);
            if (Bullet1 == false)
            {
                //if(Canvas.GetLeft(bullets[0]) > 960 || Canvas.GetLeft(bullets[0]) < 0 || Canvas.GetTop(bullets[0]) > 500 || Canvas.GetTop(bullets[0]) < 0)
                //{
                //    bulletTimer1.Stop();
                //    bullets[0].Visibility = Visibility.Hidden;
                //    Bullet1 = true;
                //    getAngle0 = false;
                //}
                newWatch1.Start();
                if (bul1.IntersectsWith(ast1) || bul1.IntersectsWith(ast2) || bul1.IntersectsWith(ast3) || bul1.IntersectsWith(ast4) || bul1.IntersectsWith(ast5) || newWatch1.ElapsedMilliseconds > 2000)
                {
                    newWatch1.Stop();
                    newWatch1.Reset();
                    bulletTimer1.Stop();
                    bullets[0].Visibility = Visibility.Hidden;
                    Bullet1 = true;
                    getAngle0 = false;
                    if (bul1.IntersectsWith(ast1))
                    {
                        Ast1X = 1000;
                        Ast1Y = 600;
                        counter = 1;
                        GameScore();
                    }
                    if (bul1.IntersectsWith(ast2))
                    {
                        Ast2X = 1000;
                        Ast2Y = 600;
                        counter2 = 1;
                        GameScore();
                    }
                    if (bul1.IntersectsWith(ast3))
                    {
                        Ast3X = 1000;
                        Ast3Y = 600;
                        counter3 = 1;
                        GameScore();
                    }
                    if (bul1.IntersectsWith(ast4))
                    {
                        Ast4X = 1000;
                        Ast4Y = 600;
                        counter4 = 1;
                        GameScore();
                    }
                    if (bul1.IntersectsWith(ast5))
                    {
                        Ast5X = 1000;
                        Ast5Y = 600;
                        counter5 = 1;
                        GameScore();
                    }
                }
            }
            if (Bullet2 == false)
            {
                //if (Canvas.GetLeft(bullets[1]) > 960 || Canvas.GetLeft(bullets[1]) < 0 || Canvas.GetTop(bullets[1]) > 500 || Canvas.GetTop(bullets[1]) < 0)
                //{
                //    bulletTimer2.Stop();
                //    bullets[1].Visibility = Visibility.Hidden;
                //    Bullet2 = true;
                //    getAngle1 = false;
                //}
                newWatch2.Start();
                if (bul2.IntersectsWith(ast1) || bul2.IntersectsWith(ast2) || bul2.IntersectsWith(ast3) || bul2.IntersectsWith(ast4) || bul2.IntersectsWith(ast5) || newWatch2.ElapsedMilliseconds > 2000)
                {
                    newWatch2.Stop();
                    newWatch2.Reset();
                    bulletTimer2.Stop();
                    bullets[1].Visibility = Visibility.Hidden;
                    Bullet2 = true;
                    getAngle1 = false;
                    if (bul2.IntersectsWith(ast1))
                    {
                        Ast1X = 1000;
                        Ast1Y = 600;
                        counter = 1;
                        GameScore();
                    }
                    if (bul2.IntersectsWith(ast2))
                    {
                        Ast2X = 1000;
                        Ast2Y = 600;
                        counter2 = 1;
                        GameScore();
                    }
                    if (bul2.IntersectsWith(ast3))
                    {
                        Ast3X = 1000;
                        Ast3Y = 600;
                        counter3 = 1;
                        GameScore();
                    }
                    if (bul2.IntersectsWith(ast4))
                    {
                        Ast4X = 1000;
                        Ast4Y = 600;
                        counter4 = 1;
                        GameScore();
                    }
                    if (bul2.IntersectsWith(ast5))
                    {
                        Ast5X = 1000;
                        Ast5Y = 600;
                        counter5 = 1;
                        GameScore();
                    }
                }
            }
            if (Bullet3 == false)
            {
                //if (Canvas.GetLeft(bullets[2]) > 960 || Canvas.GetLeft(bullets[2]) < 0 || Canvas.GetTop(bullets[2]) > 500 || Canvas.GetTop(bullets[2]) < 0)
                //{
                //    bulletTimer3.Stop();
                //    bullets[2].Visibility = Visibility.Hidden;
                //    Bullet3 = true;
                //    getAngle2 = false;
                //}
                newWatch3.Start();
                if (bul3.IntersectsWith(ast1) || bul3.IntersectsWith(ast2) || bul3.IntersectsWith(ast3) || bul3.IntersectsWith(ast4) || bul3.IntersectsWith(ast5) || newWatch3.ElapsedMilliseconds > 2000)
                {
                    newWatch3.Stop();
                    newWatch3.Reset();
                    bulletTimer3.Stop();
                    bullets[2].Visibility = Visibility.Hidden;
                    Bullet3 = true;
                    getAngle2 = false;
                    if (bul3.IntersectsWith(ast1))
                    {
                        Ast1X = 1000;
                        Ast1Y = 600;
                        counter = 1;
                        GameScore();
                    }
                    if (bul3.IntersectsWith(ast2))
                    {
                        Ast2X = 1000;
                        Ast2Y = 600;
                        counter2 = 1;
                        GameScore();
                    }
                    if (bul3.IntersectsWith(ast3))
                    {
                        Ast3X = 1000;
                        Ast3Y = 600;
                        counter3 = 1;
                        GameScore();
                    }
                    if (bul3.IntersectsWith(ast4))
                    {
                        Ast4X = 1000;
                        Ast4Y = 600;
                        counter4 = 1;
                        GameScore();
                    }
                    if (bul3.IntersectsWith(ast5))
                    {
                        Ast5X = 1000;
                        Ast5Y = 600;
                        counter5 = 1;
                        GameScore();
                    }
                }
            }
            if (Bullet4 == false)
            {
                //if (Canvas.GetLeft(bullets[3]) > 960 || Canvas.GetLeft(bullets[3]) < 0 || Canvas.GetTop(bullets[3]) > 500 || Canvas.GetTop(bullets[3]) < 0)
                //{
                //    bulletTimer4.Stop();
                //    bullets[3].Visibility = Visibility.Hidden;
                //    Bullet4 = true;
                //    getAngle3 = false;
                //}
                newWatch4.Start();
                if (bul4.IntersectsWith(ast1) || bul4.IntersectsWith(ast2) || bul4.IntersectsWith(ast3) || bul4.IntersectsWith(ast4) || bul4.IntersectsWith(ast5) || newWatch4.ElapsedMilliseconds > 2000)
                {
                    newWatch4.Stop();
                    newWatch4.Reset();
                    bulletTimer4.Stop();
                    bullets[3].Visibility = Visibility.Hidden;
                    Bullet4 = true;
                    getAngle3 = false;
                    if (bul4.IntersectsWith(ast1))
                    {
                        Ast1X = 1000;
                        Ast1Y = 600;
                        counter = 1;
                        GameScore();
                    }
                    if (bul4.IntersectsWith(ast2))
                    {
                        Ast2X = 1000;
                        Ast2Y = 600;
                        counter2 = 1;
                        GameScore();
                    }
                    if (bul4.IntersectsWith(ast3))
                    {
                        Ast3X = 1000;
                        Ast3Y = 600;
                        counter3 = 1;
                        GameScore();
                    }
                    if (bul4.IntersectsWith(ast4))
                    {
                        Ast4X = 1000;
                        Ast4Y = 600;
                        counter4 = 1;
                        GameScore();
                    }
                    if (bul4.IntersectsWith(ast5))
                    {
                        Ast5X = 1000;
                        Ast5Y = 600;
                        counter5 = 1;
                        GameScore();
                    }
                }
            }
            if (Bullet5 == false)
            {
                //if (Canvas.GetLeft(bullets[4]) > 960 || Canvas.GetLeft(bullets[4]) < 0 || Canvas.GetTop(bullets[4]) > 500 || Canvas.GetTop(bullets[4]) < 0)
                //{
                //    bulletTimer5.Stop();
                //    bullets[4].Visibility = Visibility.Hidden;
                //    Bullet5 = true;
                //    getAngle4 = false;
                //}
                newWatch5.Start();
                if (bul5.IntersectsWith(ast1) || bul5.IntersectsWith(ast2) || bul5.IntersectsWith(ast3) || bul5.IntersectsWith(ast4) || bul5.IntersectsWith(ast5) || newWatch5.ElapsedMilliseconds > 2000)
                {
                    newWatch5.Stop();
                    newWatch5.Reset();
                    bulletTimer5.Stop();
                    bullets[4].Visibility = Visibility.Hidden;
                    Bullet5 = true;
                    getAngle4 = false;
                    if (bul5.IntersectsWith(ast1))
                    {
                        Ast1X = 1000;
                        Ast1Y = 600;
                        counter = 1;
                        GameScore();
                    }
                    if (bul5.IntersectsWith(ast2))
                    {
                        Ast2X = 1000;
                        Ast2Y = 600;
                        counter2 = 1;
                        GameScore();
                    }
                    if (bul5.IntersectsWith(ast3))
                    {
                        Ast3X = 1000;
                        Ast3Y = 600;
                        counter3 = 1;
                        GameScore();
                    }
                    if (bul5.IntersectsWith(ast4))
                    {
                        Ast4X = 1000;
                        Ast4Y = 600;
                        counter4 = 1;
                        GameScore();
                    }
                    if (bul5.IntersectsWith(ast5))
                    {
                        Ast5X = 1000;
                        Ast5Y = 600;
                        counter5 = 1;
                        GameScore();
                    }
                }
            }
        }
        #endregion

        #region Hit Detection
        public void HitDetection(object sender, EventArgs e)
        {
            //The following creates rectangles that represent the position of the asteroids and the Spacship on the canvas
            //Allows the ability to fine tune the size of the hit box
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
        #region LaserVariables/Timers
        List<Rectangle> bullets = new List<Rectangle>(5);
        bool hasRun = false;
        double[] top = new double[5];
        double[] left = new double[5];
        double bulletAngle0 = 0;
        double bulletAngle1 = 0;
        double bulletAngle2 = 0;
        double bulletAngle3 = 0;
        double bulletAngle4 = 0;
        double bulletSpeed = 3;
        bool Bullet1 = true;
        bool Bullet2 = true;
        bool Bullet3 = true;
        bool Bullet4 = true;
        bool Bullet5 = true;
        bool getAngle0 = false;
        bool getAngle1 = false;
        bool getAngle2 = false;
        bool getAngle3 = false;
        bool getAngle4 = false;
        DispatcherTimer bulletTimer1 = new DispatcherTimer();
        DispatcherTimer bulletTimer2 = new DispatcherTimer();
        DispatcherTimer bulletTimer3 = new DispatcherTimer();
        DispatcherTimer bulletTimer4 = new DispatcherTimer();
        DispatcherTimer bulletTimer5 = new DispatcherTimer();
        #endregion
        private void FireLaser(object sender, EventArgs e)
        {
            //The if statement below will determine if the bullets have been initialy created if not then this will do so
            if(hasRun == false)
            {
                int number = 5;
                int width = 5;
                int height = 5;
                //this for loop will generate 5 rectangles designed as bullets
                //then will each of the bullets to the canvas based on the current position of the spaceship
                //it will also set the visibility of each of the bullets to null so that the player can see them on screen yet
                //also adds each of the bullets to a list of bullets for reference later
                for (int i = 0; i < number; i++)
                {
                    // Create the rectangle
                    Rectangle rec = new Rectangle()
                    {
                        Width = width,
                        Height = height,
                        Fill = Brushes.White,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1,
                    };
                    bullets.Add(rec);
                    // Add to a canvas for example
                    MyCanvas.Children.Add(rec);
                    rec.Visibility = Visibility.Hidden;
                    double tempTop = Canvas.GetTop(SpaceShip);
                    double tempLeft = Canvas.GetLeft(SpaceShip);
                    top[i] = tempTop - 2;
                    left[i] = tempLeft + 9;
                    Canvas.SetTop(rec, tempTop);
                    Canvas.SetLeft(rec, tempLeft);
                }
                hasRun = true;
            }
            // the following if statement will run when the space bar has been hit
            if (Keyboard.IsKeyDown(Key.Space))
            {
                //the following if statements will determine if the bullet is available to shoot and if so will then run the timer on the corresponding function
                //and then will set bullet[i] to false essentially saying that the bullet is no longer avaiable right now
                if(Bullet1 == true)
                {
                    newWatch1.Start();
                    bulletTimer1.Tick += moveBullet1;
                    bulletTimer1.Interval = TimeSpan.FromMilliseconds(10);
                    bulletTimer1.Start();
                    Bullet1 = false;
                }
                else if(Bullet2 == true)
                {
                    newWatch2.Start();
                    bulletTimer2.Tick += moveBullet2;
                    bulletTimer2.Interval = TimeSpan.FromMilliseconds(10);
                    bulletTimer2.Start();
                    Bullet2 = false;
                }
                else if (Bullet3 == true)
                {
                    newWatch3.Start();
                    bulletTimer3.Tick += moveBullet3;
                    bulletTimer3.Interval = TimeSpan.FromMilliseconds(10);
                    bulletTimer3.Start();
                    Bullet3 = false;
                }
                else if (Bullet4 == true)
                {
                    newWatch4.Start();
                    bulletTimer4.Tick += moveBullet4;
                    bulletTimer4.Interval = TimeSpan.FromMilliseconds(10);
                    bulletTimer4.Start();
                    Bullet4 = false;
                }
                else if (Bullet5 == true)
                {
                    newWatch5.Start();
                    bulletTimer5.Tick += moveBullet5;
                    bulletTimer5.Interval = TimeSpan.FromMilliseconds(10);
                    bulletTimer5.Start();
                    Bullet5 = false;
                }
            }

        }
        //each of the moveBullet[i] below is what causes the bullet to move
        //it begins by getting the current angle of the space ship or in other words the direction of the ship so that it shots in the right direction
        //its ran off an if statement that only runs once
        //the function sets the visibility to true and then uses the movement algorithm used with the asteroids to move the bullet.
        public void moveBullet1(object sender, EventArgs e)
        {
            if (getAngle0 == false)
            {
                bulletAngle0 = angle;
                double tempTop = Canvas.GetTop(SpaceShip);
                double tempLeft = Canvas.GetLeft(SpaceShip);
                top[0] = tempTop - 2;
                left[0] = tempLeft + 9;
                Canvas.SetTop(bullets[0], tempTop);
                Canvas.SetLeft(bullets[0], tempLeft);
                getAngle0 = true;
            }
            bullets[0].Visibility = Visibility.Visible;
            int iNewX0 = (int)(bulletSpeed * System.Math.Sin((System.Math.PI * (bulletAngle0)) / 180));
            int iNewY0 = -(int)(bulletSpeed * System.Math.Cos((System.Math.PI * (bulletAngle0)) / 180));
            top[0] += iNewY0;
            left[0] += iNewX0;
            Canvas.SetTop(bullets[0], top[0]);
            Canvas.SetLeft(bullets[0], left[0]);
        }
        public void moveBullet2(object sender, EventArgs e)
        {
            if (getAngle1 == false)
            {
                bulletAngle1 = angle;
                double tempTop = Canvas.GetTop(SpaceShip);
                double tempLeft = Canvas.GetLeft(SpaceShip);
                top[1] = tempTop - 2;
                left[1] = tempLeft + 9;
                Canvas.SetTop(bullets[1], tempTop);
                Canvas.SetLeft(bullets[1], tempLeft);
                getAngle1 = true;
            }
            bullets[1].Visibility = Visibility.Visible;
            int iNewX1 = (int)(bulletSpeed * System.Math.Sin((System.Math.PI * (bulletAngle1)) / 180));
            int iNewY1 = -(int)(bulletSpeed * System.Math.Cos((System.Math.PI * (bulletAngle1)) / 180));
            top[1] += iNewY1;
            left[1] += iNewX1;
            Canvas.SetTop(bullets[1], top[1]);
            Canvas.SetLeft(bullets[1], left[1]);
        }
        public void moveBullet3(object sender, EventArgs e)
        {
            if (getAngle2 == false)
            {
                bulletAngle2 = angle;
                double tempTop = Canvas.GetTop(SpaceShip);
                double tempLeft = Canvas.GetLeft(SpaceShip);
                top[2] = tempTop - 2;
                left[2] = tempLeft + 9;
                Canvas.SetTop(bullets[2], tempTop);
                Canvas.SetLeft(bullets[2], tempLeft);
                getAngle2 = true;
            }
            bullets[2].Visibility = Visibility.Visible;
            int iNewX2 = (int)(bulletSpeed * System.Math.Sin((System.Math.PI * (bulletAngle2)) / 180));
            int iNewY2 = -(int)(bulletSpeed * System.Math.Cos((System.Math.PI * (bulletAngle2)) / 180));
            top[2] += iNewY2;
            left[2] += iNewX2;
            Canvas.SetTop(bullets[2], top[2]);
            Canvas.SetLeft(bullets[2], left[2]);
        }
        public void moveBullet4(object sender, EventArgs e)
        {
            if (getAngle3 == false)
            {
                bulletAngle3 = angle;
                double tempTop = Canvas.GetTop(SpaceShip);
                double tempLeft = Canvas.GetLeft(SpaceShip);
                top[3] = tempTop - 2;
                left[3] = tempLeft + 9;
                Canvas.SetTop(bullets[3], tempTop);
                Canvas.SetLeft(bullets[3], tempLeft);
                getAngle3 = true;
            }
            bullets[3].Visibility = Visibility.Visible;
            int iNewX3 = (int)(bulletSpeed * System.Math.Sin((System.Math.PI * (bulletAngle3)) / 180));
            int iNewY3 = -(int)(bulletSpeed * System.Math.Cos((System.Math.PI * (bulletAngle3)) / 180));
            top[3] += iNewY3;
            left[3] += iNewX3;
            Canvas.SetTop(bullets[3], top[3]);
            Canvas.SetLeft(bullets[3], left[3]);
        }
        public void moveBullet5(object sender, EventArgs e)
        {
            if (getAngle4 == false)
            {
                bulletAngle4 = angle;
                double tempTop = Canvas.GetTop(SpaceShip);
                double tempLeft = Canvas.GetLeft(SpaceShip);
                top[4] = tempTop - 2;
                left[4] = tempLeft + 9;
                Canvas.SetTop(bullets[4], tempTop);
                Canvas.SetLeft(bullets[4], tempLeft);
                getAngle4 = true;
            }
            bullets[4].Visibility = Visibility.Visible;
            int iNewX4 = (int)(bulletSpeed * System.Math.Sin((System.Math.PI * (bulletAngle4)) / 180));
            int iNewY4 = -(int)(bulletSpeed * System.Math.Cos((System.Math.PI * (bulletAngle4)) / 180));
            top[4] += iNewY4;
            left[4] += iNewX4;
            Canvas.SetTop(bullets[4], top[4]);
            Canvas.SetLeft(bullets[4], left[4]);
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