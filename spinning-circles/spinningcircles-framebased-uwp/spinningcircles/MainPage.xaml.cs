using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace spinningcircles
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static int TotNumCircles = 200;

        public static Canvas canv = new Canvas();
        public static Ellipse[] ellip = new Ellipse[TotNumCircles];
        public Int32[] x = new Int32[TotNumCircles];
        public Int32[] y = new Int32[TotNumCircles];
        public float[] theta = new float[TotNumCircles]; // angle of main radius
        public float[] alfa = new float[TotNumCircles];  // angle of minor radius 

        public float[] v = new float[TotNumCircles];
        public float[] v2 = new float[TotNumCircles];

        public int X0;                 // x-coordinate of main radius center
        public int Y0;                 // y-coordinate of main radius center
        public int R = 100;            // main radius
        public int r = 10;             // minor radius r=10, 20,50, 70,100
        public Int32 circleSize = 30;  // circle size
        public Int32 speedCircles = 30;

        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// change value of main radius R.
        /// </summary>
        private void slider1_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            R = Convert.ToInt32(e.NewValue);
        }

        /// <summary>
        /// change value of secondary radius r.
        /// </summary>
        /// 
        private void slider2_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            r = Convert.ToInt32(e.NewValue);
        }

        /// <summary>
        /// change value size of circle.
        /// </summary>
        private void slider3_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            circleSize = Convert.ToInt32(e.NewValue);

            foreach (UIElement child in canv.Children)
            {
                if (child is Ellipse)
                {
                    Ellipse ell = child as Ellipse;
                    ell.Width = circleSize;
                    ell.Height = circleSize;
                }
            }
        }

        /// <summary>
        /// change speed of circles
        /// </summary>
        private void slider4_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            speedCircles = Convert.ToInt32(e.NewValue);
            for (int j = 0; j < TotNumCircles; j++)
            {
                v[j] = (float)(Math.PI / 180 * speedCircles / 10);  //0.1 gradi
            }
        }

        /// <summary>
        /// change number of total ellipse.
        /// </summary>
        private void slider5_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
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
                    theta[k] = (float)(Math.PI / 180 * 360 * k / TotNumCircles);
                    alfa[k] = (float)(Math.PI / 180 * 360 * k / TotNumCircles);
                    v[k] = (float)(Math.PI / 180 * 0.8);

                    v2[k] = (float)(Math.PI / 180 * 0.3 * k);
                    x[k] = X0 + (int)(R * Math.Cos(theta[k])) + (int)(r * Math.Cos(alfa[k]));
                    y[k] = Y0 + (int)(R * Math.Sin(theta[k])) + (int)(r * Math.Sin(alfa[k]));

                    //  Canvas.SetLeft(ellip[k], x[k]);
                    //  Canvas.SetTop(ellip[k], y[k]);
                }
            }
            // Increasing total number of circles
            else
            {
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
                    ellip[k].Width = circleSize;
                    ellip[k].Height = circleSize;
                    switch (k % 7)
                    {
                        case 0:
                            ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                            break;
                        case 1:
                            ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
                            break;
                        case 2:
                            ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
                            break;
                        case 3:
                            ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.DarkViolet);
                            break;
                        case 4:
                            ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.Aquamarine);
                            break;
                        case 5:
                            ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
                            break;
                        default:
                            ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
                            break;
                    }

                    ellip[k].SetValue(Canvas.LeftProperty, (double)x[k]);
                    ellip[k].SetValue(Canvas.TopProperty, (double)y[k]);
                    canv.Children.Add(ellip[k]);
                }
                for (int k = 0; k < TotNumCircles; k++)
                {
                    theta[k] = (float)(Math.PI / 180 * 360 * k / TotNumCircles);
                    alfa[k] = (float)(Math.PI / 180 * 360 * k / TotNumCircles);
                    v[k] = (float)(Math.PI / 180 * 0.8);  //0.1 gradi
                    v2[k] = (float)(Math.PI / 180 * 0.3 * k);  //0.1 gradi
                    x[k] = X0 + (int)(R * Math.Cos(theta[k])) + (int)(r * Math.Cos(alfa[k]));
                    y[k] = Y0 + (int)(R * Math.Sin(theta[k])) + (int)(r * Math.Sin(alfa[k]));
                }
            }
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            canv = canv1;
            // Canvas
            //    this.Content = canv;          
            //    canv.Margin = new Thickness(0, 50, 0, 0);
            canv.Background = new SolidColorBrush(Windows.UI.Colors.White);

            Y0 = (int)(Window.Current.Bounds.Height / 2);   // 350
            X0 = (int)(Window.Current.Bounds.Width / 2);  // 400


            for (int k = 0; k < TotNumCircles; k++)
            {
                ellip[k] = new Ellipse();
                ellip[k].Width = circleSize;
                ellip[k].Height = circleSize;
                switch (k % 7)
                {
                    case 0:
                        ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.Red);// Brushes.Red;
                        break;
                    case 1:
                        ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
                        break;
                    case 2:
                        ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
                        break;
                    case 3:
                        ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.DarkViolet);
                        break;
                    case 4:
                        ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.Aquamarine);
                        break;
                    case 5:
                        ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
                        break;
                    default:
                        ellip[k].Fill = new SolidColorBrush(Windows.UI.Colors.Orange);
                        break;
                }

                v[k] = (float)(Math.PI / 180 * speedCircles / 10);
                v2[k] = (float)(Math.PI / 180 * 0.3 * k);

                theta[k] = (float)(Math.PI / 180 * 360 * k / TotNumCircles);
                alfa[k] = (float)(Math.PI / 180 * 360 * k / TotNumCircles);
                x[k] = X0 + (int)(R * Math.Cos(theta[k])) + (int)(r * Math.Cos(alfa[k]));
                y[k] = Y0 + (int)(R * Math.Sin(theta[k])) + (int)(r * Math.Sin(alfa[k]));

                ellip[k].SetValue(Canvas.LeftProperty, (double)x[k]);
                ellip[k].SetValue(Canvas.TopProperty, (double)y[k]);
                canv.Children.Add(ellip[k]);
            }
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }
        /// <summary>
        /// increase positioning of every circle draw in the canvas
        /// </summary>
        public void CompositionTarget_Rendering(object sender, object e)
        {
            for (int j = 0; j < TotNumCircles; j++)
            {
                theta[j] = theta[j] + v[j];
                alfa[j] = alfa[j] + v2[j];
                x[j] = X0 + (int)(R * Math.Cos(theta[j])) + (int)(r * Math.Cos(alfa[j]));
                y[j] = Y0 + (int)(R * Math.Sin(theta[j])) + (int)(r * Math.Sin(alfa[j]));
                Canvas.SetLeft(ellip[j], x[j]);
                Canvas.SetTop(ellip[j], y[j]);
            }
        }
    }
}
