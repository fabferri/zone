// Summary:
//   simulation of ball collision in WPF based on System.Windows.Threading.DispatcherTimer class
//
// Description:
//   One possibile approach in WPF to run animation of circles in canvas is to use System.Windows.Threading.DispatcherTimer class
//   DispatcherTimer is a timer that is integrated into the Dispatcher queue which is processed at a specified interval of time and at a specified priority.
//    
//   On Loaded event is added a RoutedEventHandler that set the DispatcherTimer.
//   
// Author: Fabrizio Ferri
// Date: 12/01/2012
// Reference:
//  -Ball to Ball Collision - Detection and Handling http://stackoverflow.com/questions/345838/ball-to-ball-collision-detection-and-handling
//  -Introduction - The World Of Bouncing Balls:     http://www.ntu.edu.sg/home/ehchua/programming/java/J8a_GameIntro-BouncingBalls.html
//  -How to: Render on a Per Frame Interval Using CompositionTarget http://msdn.microsoft.com/en-us/library/ms748838.aspx
//  -The Physics of an Elastic Collision (Part 2):   http://www.director-online.com/buildArticle.php?id=532
//  -Elastic collision:                              http://en.wikipedia.org/wiki/Elastic_collision
//  -Determine where two circles intersect in C#     http://blog.csharphelper.com/2010/03/29/determine-where-two-circles-intersect-in-c.aspx
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Controls.Primitives;


namespace BallsCollisionTimerBased
{
    public class Ball
    {
        public Ellipse ellip = new Ellipse();
        public double r; // ball radius
        public double m; // mass
        public Vector p; // position vector
        public Vector v; // speed vector
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int TotNumCircles = 50;  //50
        public static int tmpTotalNumCircles = TotNumCircles;

        public bool randomSize = false;       // true
        public bool flagStopContinue = true;

        public int minSpeed = 2;
        public int maxSpeed = 4;
        public int MaxRadiusBall = 16;
        public double density = 1.0;
        public static Border bord = new Border();
        public static Canvas canv = new Canvas();
        public static Ball[] balls = new Ball[TotNumCircles];

        public Random rnd1 = new Random(DateTime.Now.Millisecond - 999999);
        public Random rnd2 = new Random(DateTime.Now.Millisecond + 111111);

        // A DispatcherTimer object named timer is created.
        // The event handler timer1_Tick is added to the Tick event of dispatcherTimer.
        // The Interval is set to 5 millisecond using a TimeSpan object, and the timer is started.
        // a Dispatcher priority describes the priorities at which operations can be invoked:
        //    Send    : The enumeration value is 10. Operations are processed before other asynchronous operations. This is the highest priority. 
        //    Normal  : The enumeration value is 9. Operations are processed at normal priority. This is the typical application priority.
        //    DataBind: The enumeration value is 8. Operations are processed at the same priority as data binding.
        //    Render  : The enumeration value is 7. Operations processed at the same priority as rendering.
        //    Loaded  : The enumeration value is 6. Operations are processed when layout and render has finished but just before items at input priority are serviced. Specifically this is used when raising the Loaded event.
        //    ......
        public static DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer(System.Windows.Threading.DispatcherPriority.Send);

        public MainWindow()
        {
            // InitializeComponent() call LoadComponent() method. 
            // The LoadComponent() method extracts the BAML (the compiled XAML) from your assembly, parses the BAML, to build the user interface 
            InitializeComponent();

            this.Content = myGrid;

            // Create dynamically a cavas as child of grid
            myGrid.Children.Add(canv);

            // Create a Canvas with smaller size than parent grid
            canv.Width = myGrid.Width - 30;
            canv.Height = myGrid.Height - 70;
            canv.Margin = new Thickness(0, 0, 0, 0);
            canv.Background = new SolidColorBrush(Colors.White);
            canv.HorizontalAlignment = HorizontalAlignment.Center;
            canv.VerticalAlignment = VerticalAlignment.Bottom;
            canv.Children.Add(bord);

            bord.Width = (double)canv.Width; ;
            bord.Height = (double)canv.Height;
            bord.Margin = new Thickness { Left = 0, Top = 0, Right = 0, Bottom = 0 };
            bord.BorderThickness = new Thickness { Top = 2, Bottom = 2, Left = 2, Right = 2 };  // Turn on border
            bord.HorizontalAlignment = HorizontalAlignment.Center;                              // HorizontalAlignment.Left 
            bord.VerticalAlignment = VerticalAlignment.Bottom;                                  // VerticalAlignment.Top;
            bord.CornerRadius = new CornerRadius(5);
            bord.BorderBrush = Brushes.Blue;
            bord.Background = Brushes.White;
            bord.Padding = new Thickness(1);
            StopContinueButton.IsEnabled = false;
            this.SliderTotalBalls.Value = TotNumCircles;
            this.TextBoxTotNumBalls.Text = TotNumCircles.ToString();

            // Function to draw circles on the canvas. After execution of function all cicles have fixed position in the canvas, without animation.
            StartDraw();

            // The Loaded event says that the tree is not only built and initialized, but layout has run on it, data has been bound, it's connected to a rendering surface (window), 
            // and you're on the verge of being rendered.  When we reach that point, we canvas the tree by broadcasting the Loaded event, starting at the root. 
            // http://msdn.microsoft.com/en-us/library/ms748948.aspx#Window_Lifetime_Events
            this.Loaded += new System.Windows.RoutedEventHandler(Window1_Loaded);
        }


        private void CleanBalls()
        {
            List<UIElement> itemstoremove = new List<UIElement>();
            foreach (UIElement ui in canv.Children)
            {
                // remove all elements in canvas that are ellipse type
                if (ui is Ellipse)
                {
                    itemstoremove.Add(ui);
                }
            }
            foreach (UIElement ui in itemstoremove)
            {
                canv.Children.Remove(ui);
            }
            this.UpdateLayout();
        }

        /// <summary>
        /// This routine draws all balls, using System.Windows.Shapes.Ellipse shapes
        /// Fill color associated every ellipse is defined by module operation
        /// 
        /// </summary>
        public void StartDraw()
        {
            double R;                         // R: radius to position the balls in circles from center of canvas control
            double R0 = 80d;                  // R0: initial value of radius to position circles from center of canvas to the edges
            double theta = 0;                 // theta: angle to distribute the balls in canvas, avoding overlap
            double theta_old = -1 * Math.PI;  // initial value of angle of radius R
            double m = 0d;

            R = R0;                         

            // Create a vector to identify the center of canvas control
            Vector startVector = new Vector(canv.Width / 2, canv.Height / 2);

            for (int i = 0; i < TotNumCircles; i++) balls[i] = new Ball();

            for (int k = 0; k < TotNumCircles; k++)
            {
                if (randomSize == true)
                {
                    balls[k].r = Convert.ToDouble(rnd2.Next(10, MaxRadiusBall));  
                }
                else
                {
                    balls[k].r = Convert.ToDouble(10);
                }
                // mass= densisty* volume
                // in case of a sphere the volume is proportional to radius^3
                balls[k].m = density * Convert.ToDouble(balls[k].r * balls[k].r * balls[k].r / 1000);
                balls[k].ellip.Width = 2 * balls[k].r;                // set size of ellipse to shape as circle
                balls[k].ellip.Height = 2 * balls[k].r;               // set size of ellipse to shape as circle
                balls[k].ellip.Uid = "ellipse" + Convert.ToString(k); // set a specific Id for every ellipse (do not use in this program)

                switch (k % 7)
                {
                    case 0:
                        balls[k].ellip.Fill = Brushes.Red;
                        break;
                    case 1:
                        balls[k].ellip.Fill = Brushes.Yellow;
                        break;
                    case 2:
                        balls[k].ellip.Fill = Brushes.Blue;
                        break;
                    case 3:
                        balls[k].ellip.Fill = Brushes.Blue;
                        break;
                    case 4:
                        balls[k].ellip.Fill = Brushes.Aquamarine;
                        break;
                    case 5:
                        balls[k].ellip.Fill = Brushes.GreenYellow;
                        break;
                    default:
                        balls[k].ellip.Fill = Brushes.GreenYellow;
                        break;
                }


                theta = (k == 0) ? Math.Atan2(3 * balls[k].r, R) : Math.Atan2(2 * balls[k].r + 2 * balls[k - 1].r, R);
                theta = theta + theta_old;
                m = Convert.ToInt32(theta / (2 * Math.PI));
                R = R0 + (R0 / 2) * m;

                theta_old = theta;
                balls[k].v = rnd1.Next(minSpeed, maxSpeed) * new Vector(Math.Cos(theta), Math.Sin(theta));
                balls[k].p = startVector + new Vector(R * Math.Cos(theta), R * Math.Sin(theta));

                balls[k].ellip.SetValue(Canvas.LeftProperty, balls[k].p.X - balls[k].r);
                balls[k].ellip.SetValue(Canvas.TopProperty, balls[k].p.Y - balls[k].r);
                canv.Children.Add(balls[k].ellip);
            }
        }
        /// <summary>
        /// This will check for collisions between every ball but skip redundant checks:
        ///    -if you have to check if ball 1 collides with ball 2 then you don't need to check if ball 2 collides with ball 1. 
        ///    -it skips checking for collisions with itself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RenderFrame(object sender, System.EventArgs e)
        {
            for (int j = 0; j < TotNumCircles; j++)
            {
                //  balls[j].p.X = balls[j].p.X + balls[j].v.X;
                //  balls[j].p.Y = balls[j].p.Y + +balls[j].v.Y;
                balls[j].p = balls[j].p + balls[j].v;

                if ((balls[j].p.X + balls[j].r) >= canv.Width)
                {
                    balls[j].v.X = -1 * balls[j].v.X;
                    balls[j].p.X = canv.Width - balls[j].r;
                }
                if ((balls[j].p.X - balls[j].r) <= 0)
                {
                    balls[j].v.X = -1 * balls[j].v.X;
                    balls[j].p.X = balls[j].r;
                }

                if ((balls[j].p.Y + balls[j].r) >= canv.Height)
                {
                    balls[j].v.Y = -1 * balls[j].v.Y;
                    balls[j].p.Y = canv.Height - balls[j].r;
                }
                if ((balls[j].p.Y - balls[j].r) <= 0)
                {
                    balls[j].v.Y = -1 * balls[j].v.Y;
                    balls[j].p.Y = balls[j].r;
                }

                Canvas.SetLeft(balls[j].ellip, balls[j].p.X - balls[j].r);
                Canvas.SetTop(balls[j].ellip, balls[j].p.Y - balls[j].r);

                for (int k = j + 1; k < TotNumCircles; k++)
                {
                    if (colliding(balls[j], balls[k]))
                    {
                        TextBoxCollision.Text = "Collision pair: " + j.ToString() + "-" + k.ToString();
                        resolveCollision(balls[k], balls[j]);
                    }
                }
            }
        }

        /// <summary>
        /// function called every tick time. 
        /// time1_Tick runs the circles animation. 
        /// </summary>
        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += new EventHandler(RenderFrame);
            timer.Start();
        }

        /// <summary>
        /// function to determine if two ellipses are intercepting
        /// </summary>
        public bool colliding(Ball _ball1, Ball _ball2)
        {
            // Two circles intersect if, and only if, the distance between their centers is between the sum and the difference of their radii. 
            // Given two circles (x0,y0,R0) and (x1,y1,R1), the formula is as follows:
            // ABS(R0-R1) <= SQRT((x0-x1)^2+(y0-y1)^2) <= (R0+R1)
            // squaring both sides lets you avoid the slow SQRT, and stay with ints if your inputs are integer:
            // (R0-R1)^2 <= (x0-x1)^2+(y0-y1)^2 <= (R0+R1)^2
            double r1 = _ball1.r;
            double r2 = _ball2.r;

            double dist = (_ball1.p - _ball2.p).Length;
            if (dist > (r1 + r2))
            {
                // No solutions, the circles are too far apart.  
                return false;
            }
            else if (dist <= r1 + r2)
            {
                // One circle contains the other.
                return true;
            }
            else if ((dist == 0) && (r1 == r2))
            {
                // the circles coincide.
                return true;
            }
            else return true;
        }

        /// <summary>
        /// Method to resolve collision between two balls
        ///  
        /// </summary>
        /// <param name="_ball1">first instance of the Ball class </param>
        /// <param name="_ball2">second instance of the Ball class</param>
        /// <returns>void</returns>
        public void resolveCollision(Ball _ball1, Ball _ball2)
        {
            double collisionision_angle = Math.Atan2((_ball2.p.Y - _ball1.p.Y), (_ball2.p.X - _ball1.p.X));
            // double speed1 = Math.Sqrt(_ball1.v.X * _ball1.v.X + _ball1.v.Y * _ball1.v.Y);
            // double speed2 = Math.Sqrt(_ball2.v.X * _ball2.v.X + _ball2.v.Y * _ball2.v.Y);
            double speed1 = _ball1.v.Length;
            double speed2 = _ball2.v.Length;

            double direction_1 = Math.Atan2(_ball1.v.Y, _ball1.v.X);
            double direction_2 = Math.Atan2(_ball2.v.Y, _ball2.v.X);
            double new_xspeed_1 = speed1 * Math.Cos(direction_1 - collisionision_angle);
            double new_yspeed_1 = speed1 * Math.Sin(direction_1 - collisionision_angle);
            double new_xspeed_2 = speed2 * Math.Cos(direction_2 - collisionision_angle);
            double new_yspeed_2 = speed2 * Math.Sin(direction_2 - collisionision_angle);


            double final_xspeed_1 = ((_ball1.m - _ball2.m) * new_xspeed_1 + (_ball2.m + _ball2.m) * new_xspeed_2) / (_ball1.m + _ball2.m);
            double final_xspeed_2 = ((_ball1.m + _ball1.m) * new_xspeed_1 + (_ball2.m - _ball1.m) * new_xspeed_2) / (_ball1.m + _ball2.m);
            double final_yspeed_1 = new_yspeed_1;
            double final_yspeed_2 = new_yspeed_2;


            // balls[k].v.X = Math.Cos(collisionision_angle) * final_xspeed_1 + Math.Cos(collisionision_angle + Math.PI / 2) * final_yspeed_1;
            // balls[k].v.Y = Math.Sin(collisionision_angle) * final_xspeed_1 + Math.Sin(collisionision_angle + Math.PI / 2) * final_yspeed_1;
            // balls[j].v.X = Math.Cos(collisionision_angle) * final_xspeed_2 + Math.Cos(collisionision_angle + Math.PI / 2) * final_yspeed_2;
            // balls[j].v.Y = Math.Sin(collisionision_angle) * final_xspeed_2 + Math.Sin(collisionision_angle + Math.PI / 2) * final_yspeed_2;
            double cosAngle = Math.Cos(collisionision_angle);
            double sinAngle = Math.Sin(collisionision_angle);
            _ball1.v.X = cosAngle * final_xspeed_1 - sinAngle * final_yspeed_1;
            _ball1.v.Y = sinAngle * final_xspeed_1 + cosAngle * final_yspeed_1;
            _ball2.v.X = cosAngle * final_xspeed_2 - sinAngle * final_yspeed_2;
            _ball2.v.Y = sinAngle * final_xspeed_2 + cosAngle * final_yspeed_2;

            Vector pos1 = new Vector(_ball1.p.X, _ball1.p.Y);
            Vector pos2 = new Vector(_ball2.p.X, _ball2.p.Y);

            // get the mtd
            Vector posDiff = pos1 - pos2;
            double d = posDiff.Length;

            // minimum translation distance to push balls apart after intersecting
            Vector mtd = posDiff * (((_ball1.r + _ball2.r) - d) / d);

            // resolve intersection --
            // computing inverse mass quantities
            double im1 = 1 / _ball1.m;
            double im2 = 1 / _ball2.m;

            // push-pull them apart based off their mass
            pos1 = pos1 + mtd * (im1 / (im1 + im2));
            pos2 = pos2 - mtd * (im2 / (im1 + im2));
            _ball1.p = pos1;
            _ball2.p = pos2;

            //-------------------

            if (((_ball1.p.X + _ball1.r) >= canv.Width) | ((_ball1.p.X - _ball1.r) <= 0))
                _ball1.v.X = -1 * _ball1.v.X;

            if (((_ball1.p.Y + _ball1.r) >= canv.Height) | ((_ball1.p.Y - _ball1.r) <= 0))
                _ball1.v.Y = -1 * _ball1.v.Y;

            if (((_ball2.p.X + _ball2.r) >= canv.Width) | ((_ball2.p.X - _ball2.r) <= 0))
                _ball2.v.X = -1 * _ball2.v.X;

            if (((_ball2.p.Y + _ball2.r) >= canv.Height) | ((_ball2.p.Y - _ball2.r) <= 0))
                _ball2.v.Y = -1 * _ball2.v.Y;
        }

        /// <summary>
        /// GUI callbacks: these are called on various sliders  every time the current magnitude of the range control is changed.
        /// </summary>
        #region GUI callbacks

        private bool dragStarted = false;
        private void Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            tmpTotalNumCircles = Convert.ToInt32((((Slider)sender).Value));
            this.dragStarted = false;
        }
        private void Slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.dragStarted = true;
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!dragStarted)
                tmpTotalNumCircles = Convert.ToInt32(e.NewValue);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            StopContinueButton.Content = "Stop";
            CleanBalls();
            TotNumCircles = tmpTotalNumCircles;
            Array.Resize(ref balls, TotNumCircles);
            StartDraw();
            timer.Start();
            StopContinueButton.IsEnabled = true;
            flagStopContinue = true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (flagStopContinue == true)
            {
                timer.Stop();
                StopContinueButton.Content = "Continue";
                StopContinueButton.Background = Brushes.LightBlue;
                flagStopContinue = false;
            }
            else
            {
                timer.Start();
                StopContinueButton.Content = "Stop";
                StopContinueButton.Background = Brushes.LightBlue;
                flagStopContinue = true;
            }
        }

        private void Check_Size(object sender, RoutedEventArgs e)
        {
            if (RandomSize.IsChecked == true) randomSize = true;
            if (FixSize.IsChecked == true) randomSize = false;
        }
  
        #endregion 
    }
}