using System;
using System.Collections.Generic;
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
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Page
    {
        public GameScreen()
        {
            InitializeComponent();
        }
        double angle = 0;
        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            // This is the key down event linked to the canvas
            if (e.Key == Key.Down && Canvas.GetTop(rec1) + rec1.Height < 420)
            {
                // in this if statement we are checking if the down key is pressed
                // AND the rec1 objects top plus the height is less than 420 pixels
                Canvas.SetTop(rec1, Canvas.GetTop(rec1) + 10);
                // if these conditions match then we move the object down 10 pixels
            }
            else if (e.Key == Key.Up && Canvas.GetTop(rec1) > 0)
            {
                // in this if statement we are checking if they up key is pressed
                // and rec1s top is greater than 10 pixels
                Canvas.SetTop(rec1, Canvas.GetTop(rec1) - 10);
                // if these conditions are met then we move the object up 10 pixels
            }
            else if (e.Key == Key.Left && Canvas.GetLeft(rec1) > 0)
            {
                // in this if statement we are checking if they left key is pressed
                // and rec1s left is greater than 0 pixels
                Canvas.SetLeft(rec1, Canvas.GetLeft(rec1) - 10);
                // if these conditions are met then we move the object left 10 pixels
            }
            else if (e.Key == Key.Right && Canvas.GetLeft(rec1) + rec1.Width < 790)
            {
                // in this if statement we are checking if the right key is pressed
                // and rec1s right and rec1s width is less than 790 pixels
                Canvas.SetLeft(rec1, Canvas.GetLeft(rec1) + 10);
                // if these conditions are met then we move the object 10 pixels to the right
            }
            else if (e.Key == Key.A)
            {
                RotateTransform rotateTransform2 = new RotateTransform();
                rotateTransform2.CenterX = 25;
                rotateTransform2.CenterY = 25;
                angle = angle - 10;
                rotateTransform2.Angle = angle;
                rec1.RenderTransform = rotateTransform2;
            }
            else if (e.Key == Key.D)
            {
                RotateTransform rotateTransform1 = new RotateTransform();
                rotateTransform1.CenterX = 25;
                rotateTransform1.CenterY = 25;
                angle = angle + 10;
                rotateTransform1.Angle = angle;
                rec1.RenderTransform = rotateTransform1;
            }
        }
    }
}
