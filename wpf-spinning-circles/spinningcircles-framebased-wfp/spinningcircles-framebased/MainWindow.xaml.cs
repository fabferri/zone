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

namespace spinningcircles_framebased
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
            // This event handler method gets called once per frame; 
            // the event handler method is also called if changes to the visual tree force updates to the composition scene graph.
            CompositionTarget.Rendering += new EventHandler(RenderFrame);
        }
        /// <summary>
        /// This routine draws all System.Windows.Shapes.Ellipse shapes
        /// Fill color associated every ellipse is defined by module operation
        /// To draw circle Width and Height are equal value.
        /// </summary>
        public void StartDraw()
        {
            canv.Background = new SolidColorBrush(Colors.White);
            X0 = (Windows1.canv.Width / 2);
            Y0 = (Windows1.canv.Height / 2);

            for (int k = 0; k < TotNumCircles; k++)
            {
                ellip[k] = new Ellipse();
                ellip[k].Width = circleSize;
                ellip[k].Height = circleSize;
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
                x[k] = X0 + circleSize / 2 + (R * Math.Cos(theta[k])) + (r * Math.Cos(alfa[k]));
                y[k] = Y0 + circleSize / 2 + (R * Math.Sin(theta[k])) + (r * Math.Sin(alfa[k]));

                ellip[k].SetValue(Canvas.LeftProperty, x[k]);
                ellip[k].SetValue(Canvas.TopProperty, y[k]);
                canv.Children.Add(ellip[k]);
            }
        }


        /// <summary>
        /// function invoked to every frame. 
        ///  angolar speed of main Radius R 
        /// </summary>
        public void RenderFrame(object sender, System.EventArgs e)
        {
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
        /// GUI callbacks: these are called on various sliders  are clicked on.
        /// </summary>
        #region GUI callbacks


        /// Change the main radiuns R, when slider value is change.
        /// As specified in .xaml file the slider value is between [min value=50, max value= 200]
        private void sliderMainRadius_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            R = Convert.ToInt32(e.NewValue);
        }

        /// Change the secondary radiuns r, when slider value is change.
        /// As specified in .xaml file the slider value is between [min value=0, max value= 100]
        private void sliderSecondaryRadius_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            r = Convert.ToInt32(e.NewValue);
        }
        /// Change the ellipse size, when slider value is change.
        /// As specified in .xaml file the slider value is between [min value=20, max value= 80]
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
        /// As specified in .xaml file the slider value is between [min value=1, max value= 50]
        private void sliderAngularVelocity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speedCircles = Convert.ToInt32(e.NewValue);
            for (int j = 0; j < TotNumCircles; j++)
            {
                v[j] = (Math.PI / 180 * speedCircles / 10);  //0.1 gradi
            }
        }

        /// Change the total number of circles in canvas, when slider value is change.
        /// As specified in .xaml file the slider value is between [min value=10, max value= 300]
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
                    v[k] = (Math.PI / 180 * 0.8);       //0.1 gradi
                    v2[k] = (Math.PI / 180 * 0.3 * k);  //0.1 gradi
                    x[k] = X0 + circleSize / 2 + (R * Math.Cos(theta[k])) + (r * Math.Cos(alfa[k]));
                    y[k] = Y0 + circleSize / 2 + (R * Math.Sin(theta[k])) + (r * Math.Sin(alfa[k]));
                }
            }
        }
        #endregion
    }
}

