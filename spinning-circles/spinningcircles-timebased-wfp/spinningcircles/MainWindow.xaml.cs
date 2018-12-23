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
using System.Windows.Threading;

namespace spinningcircles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int TotNumCircles = 200;                      // maximum size of array of Ellipse objects
        public static Ellipse[] ellip = new Ellipse[TotNumCircles]; // array of "System.Windows.Shapes.Ellipse" objects

        public double[] x = new double[TotNumCircles];      // array of x-coordinate of ellipse centers
        public double[] y = new double[TotNumCircles];      // array of y-coordinate of ellipse centers
        public double[] theta = new double[TotNumCircles];  // angle of main radius
        public double[] alfa = new double[TotNumCircles];   // angle of minor radius 

        public double[] v = new double[TotNumCircles];      // angular velocity of main radius (R)
        public double[] v2 = new double[TotNumCircles];     // angular velocity of secondary radius (r)

        public double X0;                   // x-coordinate of origin of the main radius R
        public double Y0;                   // y-coordinate of origin of the main radius R
        public double R = 100;              // main radius R
        public double r = 10;               // secondary radius r
        public double circleSize = 30;      // diameter of circle
        public double speedCircles = 30;    // circle speed
        public MainWindow()
        {
            // InitializeComponent() call LoadComponent() method. 
            // The LoadComponent() method extracts the BAML (the compiled XAML) from your assembly, parses the BAML, to build the user interface 
            InitializeComponent();

            // Function to draw circles on the canvas. After execution of function all cicles have fixed position in the canvas, without animation.
            StartDraw();

            // The Loaded event says that the tree is not only built and initialized, but layout has run on it, data has been bound, it's connected to a rendering surface (window), 
            // and you're on the verge of being rendered.  When we reach that point, we canvas the tree by broadcasting the Loaded event, starting at the root. 
            // http://msdn.microsoft.com/en-us/library/ms748948.aspx#Window_Lifetime_Events
            Loaded += new System.Windows.RoutedEventHandler(Window1_Loaded);
        }
        /// <summary>
        /// This routine draws all System.Windows.Shapes.Ellipse shapes
        /// Fill color associated every ellipse is defined by module operation
        /// To draw circle Width and Height are equal value.
        /// </summary>
        public void StartDraw()
        {
            canv.Background = new SolidColorBrush(Colors.White);

            // SystemParameters.ScrollWidth gets the  scroll width of the nonclient area of a nonminimized window
            X0 = (Windows1.Width / 2) - SystemParameters.ScrollWidth;
            Y0 = (Windows1.Height / 2);

            for (int k = 0; k < TotNumCircles; k++)
            {
                ellip[k] = new Ellipse();
                ellip[k].Width = circleSize;    // set size of ellipse to shape as circle
                ellip[k].Height = circleSize;   // set size of ellipse to shape as circle
                // Change colors of circles.
                // we set in total 7 available Brushes color.
                switch (k % 7)
                {
                    case 0:
                        ellip[k].Fill = Brushes.Red;
                        break;
                    case 1:
                        ellip[k].Fill = Brushes.Yellow;
                        break;
                    case 2:
                        ellip[k].Fill = Brushes.Blue;
                        break;
                    case 3:
                        ellip[k].Fill = Brushes.DarkViolet;
                        break;
                    case 4:
                        ellip[k].Fill = Brushes.Aquamarine;
                        break;
                    case 5:
                        ellip[k].Fill = Brushes.GreenYellow;
                        break;
                    default:
                        ellip[k].Fill = Brushes.Orange;
                        break;
                }

                v[k] = (Math.PI / 180 * speedCircles / 10);
                v2[k] = (Math.PI / 180 * 0.3 * k);

                theta[k] = (Math.PI / 180 * 360 * k / TotNumCircles);
                alfa[k] = (Math.PI / 180 * 360 * k / TotNumCircles);
                x[k] = X0 + (R * Math.Cos(theta[k])) + (r * Math.Cos(alfa[k]));
                y[k] = Y0 + (R * Math.Sin(theta[k])) + (r * Math.Sin(alfa[k]));

                Canvas.SetTop(ellip[k], y[k] - circleSize / 2);
                Canvas.SetLeft(ellip[k], x[k] - circleSize / 2);
                canv.Children.Add(ellip[k]);
            }
        }
        // When you want to set a timer working with GUI, you always come across threading problem. 
        // In such scenario, .Net indeed makes programmers life easier. 
        // It only matters that you choose the right timer to use.
        // In Win Form, you need to use System.Windows.Forms.Timer.
        // In WPF, the one is System.Windows.Threading.DispatcherTimer.
        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
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
            DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer(System.Windows.Threading.DispatcherPriority.Send);
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();
        }
        /// <summary>
        /// function called every tick time. 
        /// time1_Tick runs the circles animation. 
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // if you want this event handler executed for just once:
            // DispatcherTimer timer = (DispatcherTimer)sender;
            // timer.Stop();
            // this.Close();
            for (int j = 0; j < TotNumCircles; j++)
            {
                theta[j] = theta[j] + v[j];
                alfa[j] = alfa[j] + v2[j];
                x[j] = X0 + circleSize / 2 + (R * Math.Cos(theta[j])) + (r * Math.Cos(alfa[j]));
                y[j] = Y0 + circleSize / 2 + (R * Math.Sin(theta[j])) + (r * Math.Sin(alfa[j]));
                Canvas.SetLeft(ellip[j], x[j]);
                Canvas.SetTop(ellip[j], y[j]);
            }
        }


        /// <summary>
        /// GUI callbacks: these are called on various sliders  every time the current magnitude of the range control is changed.
        /// </summary>
        #region GUI callbacks


        /// Change the main radiuns R, when slider value is change.
        /// As specified in .xaml file the slider value is in the range [min value=50, max value= 200]
        ///   <Slider Name="sliderMainRadius" .... Value="0" Minimum="50" Maximum="200" IsSnapToTickEnabled="True" TickFrequency="1" ValueChanged="sliderMainRadius_ValueChanged" />
        private void sliderMainRadius_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            R = Convert.ToInt32(e.NewValue);
        }

        /// Change the secondary radiuns r, when slider value is change.
        /// As specified in .xaml file the slider value is between [min value=0, max value= 100]
        /// <Slider Name="sliderSecondaryRadius" Value="0" Minimum="0" Maximum="100" IsSnapToTickEnabled="True" TickFrequency="1" ValueChanged="sliderSecondaryRadius_ValueChanged"  />
        private void sliderSecondaryRadius_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            r = Convert.ToInt32(e.NewValue);
        }
        /// Change the ellipse size, when slider value is change.
        /// As specified in .xaml file the slider value is in the range [min value=20, max value= 80]
        ///    <Slider Name="sliderCirclesSize" ... Value="0" Minimum="20" Maximum="80" IsSnapToTickEnabled="True" TickFrequency="1" ValueChanged="sliderCirclesSize_ValueChanged" />
        private void sliderCirclesSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            circleSize = Convert.ToInt32(e.NewValue);

            foreach (UIElement child in this.canv.Children)
            {
                if (child is Ellipse)
                {
                    Ellipse ell = child as Ellipse;
                    ell.Width = circleSize;
                    ell.Height = circleSize;
                }
            }
        }

        /// Change the angular velocity, when slider value is change.
        /// As specified in .xaml file the slider value is in the range [min value=1, max value= 50]
        ///   <Slider Name="sliderAngularVelocity" ... Value="0" Minimum="1" Maximum="50" IsSnapToTickEnabled="True" TickFrequency="1" ValueChanged="sliderAngularVelocity_ValueChanged"  /> 
        private void sliderAngularVelocity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speedCircles = Convert.ToInt32(e.NewValue);
            for (int j = 0; j < TotNumCircles; j++)
            {
                v[j] = (Math.PI / 180 * speedCircles / 10);  //0.1 gradi
            }
        }

        /// Change the total number of circles in canvas, when slider value is change.
        /// As specified in .xaml file the slider value is in the range [min value=10, max value= 300]
        ///   <Slider Name="sliderTotalCirclesNumber" ... Value="0" Minimum="10" Maximum="300" IsSnapToTickEnabled="True" TickFrequency="1" ValueChanged="sliderTotalCirclesNumber_ValueChanged"  />
        private void sliderTotalCirclesNumber_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int oldTotalNumCircles = TotNumCircles;
            TotNumCircles = Convert.ToInt32(e.NewValue);

            // Reduce total number of circles
            if (oldTotalNumCircles >= TotNumCircles)
            {
                for (int k = TotNumCircles; k < oldTotalNumCircles; k++)
                {
                    canv.Children.Remove(ellip[k]);
                }
                Array.Resize(ref ellip, TotNumCircles);

                for (int k = 0; k < TotNumCircles; k++)
                {
                    theta[k] = (Math.PI / 180 * 360 * k / TotNumCircles);
                    alfa[k] = (Math.PI / 180 * 360 * k / TotNumCircles);
                    v[k] = (Math.PI / 180 * 0.8);

                    v2[k] = (Math.PI / 180 * 0.3 * k);
                    x[k] = X0 + circleSize / 2 + (R * Math.Cos(theta[k])) + (r * Math.Cos(alfa[k]));
                    y[k] = Y0 + circleSize / 2 + (R * Math.Sin(theta[k])) + (r * Math.Sin(alfa[k]));
                }
            }
            // Increasing total number of circles
            else
            {
                // resizing of all arrays used to animate the ellipses
                Array.Resize(ref ellip, TotNumCircles);
                Array.Resize(ref x, TotNumCircles);
                Array.Resize(ref y, TotNumCircles);
                Array.Resize(ref v, TotNumCircles);
                Array.Resize(ref v2, TotNumCircles);
                Array.Resize(ref theta, TotNumCircles);
                Array.Resize(ref alfa, TotNumCircles);
                for (int k = oldTotalNumCircles; k < TotNumCircles; k++)
                {
                    ellip[k] = new Ellipse();
                    ellip[k].Width = circleSize;   // set size of ellipse to shape as circle
                    ellip[k].Height = circleSize;  // set size of ellipse to shape as circle
                    switch (k % 7)
                    {
                        case 0:
                            ellip[k].Fill = Brushes.Red;
                            break;
                        case 1:
                            ellip[k].Fill = Brushes.Yellow;
                            break;
                        case 2:
                            ellip[k].Fill = Brushes.Blue;
                            break;
                        case 3:
                            ellip[k].Fill = Brushes.DarkViolet;
                            break;
                        case 4:
                            ellip[k].Fill = Brushes.Aquamarine;
                            break;
                        case 5:
                            ellip[k].Fill = Brushes.GreenYellow;
                            break;
                        default:
                            ellip[k].Fill = Brushes.Orange;
                            break;
                    }

                    ellip[k].SetValue(Canvas.LeftProperty, x[k]);
                    ellip[k].SetValue(Canvas.TopProperty, y[k]);
                    canv.Children.Add(ellip[k]);
                }
                for (int k = 0; k < TotNumCircles; k++)
                {
                    theta[k] = (Math.PI / 180 * 360 * k / TotNumCircles);
                    alfa[k] = (Math.PI / 180 * 360 * k / TotNumCircles);
                    v[k] = (Math.PI / 180 * 0.8);  //0.1 gradi
                    v2[k] = (Math.PI / 180 * 0.3 * k);  //0.1 gradi
                    x[k] = X0 + circleSize / 2 + (R * Math.Cos(theta[k])) + (r * Math.Cos(alfa[k]));
                    y[k] = Y0 + circleSize / 2 + (R * Math.Sin(theta[k])) + (r * Math.Sin(alfa[k]));
                }
            }
        }
        #endregion
    }
}
