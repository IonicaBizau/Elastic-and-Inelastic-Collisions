using System;
using System.Collections.Generic;
//using System.Linq;
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
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Threading;
using WpfApplication1;
using System.Net;
using System.IO;

namespace Fizica_ciocniri
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double x1, x2, v1, v2, m1, m2;
        int ok = 1, ok1 = 1, dp_t = 1, ciocnite = 0;
        
        int oke = 1, ok1e = 1, dp_te = 1, ciocnitee = 0;
        double x1e, x2e;
        int id_cioc_elastic = 0;
        public MainWindow()
        {
            InitializeComponent();
            c1.Width = c1.Height = c2.Width = c2.Height = 50;
            this.Closed += MainWindow_Closed;
            x1 = 20;
            x2 = 150;
            Canvas.SetLeft(c1, x1);
            Canvas.SetLeft(c2, x2);
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                AddIPInUsersList();
            }
            catch { }
        }

        void AddIPInUsersList()
        {
            string information = "IP: " + GetPublicIP() + " | TIME: " + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + "-" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + " | OS: " + Environment.OSVersion.VersionString + " | Username: " + Environment.UserName.ToString() + " | Machine Name: " + Environment.MachineName;
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://ionicabizau.altervista.org/simulareaCiocnirilor/ip.php?ip=" + information);
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            myHttpWebResponse.Close();
        }

        public string GetPublicIP()
        {
            String direction = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                direction = stream.ReadToEnd();
            }

            //Search for the ip in the html
            int first = direction.IndexOf("Address: ") + 9;
            int last = direction.LastIndexOf("</body>");
            direction = direction.Substring(first, last - first);

            return direction;
        }

        /*-------------CIOCNIRI PLASTICE-------------*/
        private void start_click(object sender, RoutedEventArgs e)
        {
            if (ciocniri_plastice.Visibility == Visibility.Visible)
            {
                try
                {
                    v1 = Convert.ToDouble(textBox1.Text);
                    v2 = Convert.ToDouble(textBox2.Text);
                    m1 = Convert.ToDouble(textBox3.Text);
                    m2 = Convert.ToDouble(textBox4.Text);

                    if ((m1 <= 0) || (m2 <= 0) || (v1 > 50) || (v1 < -50) || (v2 > 50) || (v2 < -50) || (m1 > 100) || (m2 > 100))
                    {
                        MessageBox.Show("Datele introduse sunt eronate.");
                    }
                    else
                    {
                        textBlock5.Text = "Viteza celor doua corpuri este (m/s): ";
                        start.IsEnabled = false;
                        stop.IsEnabled = true;

                        c_elastice.IsEnabled = false;
                        textBox1.IsEnabled = false;
                        textBox2.IsEnabled = false;
                        textBox3.IsEnabled = false;
                        textBox4.IsEnabled = false;

                        ok = 1;
                        ok1 = 1;

                        c1.Width = c1.Height = c2.Width = c2.Height = 50;
                        x1 = 20;
                        x2 = 150;
                        Canvas.SetLeft(c1, x1);
                        Canvas.SetLeft(c2, x2);
                        Canvas.SetTop(c1, 438);
                        Canvas.SetTop(c2, 438);
                        ciocnite = 0;
                        if (can_plastice.Visibility == Visibility.Visible)
                        {
                            if ((textBox1.Text != "") && (textBox1.Text != "") && (textBox1.Text != "") && (textBox1.Text != ""))
                            {
                                if (dp_t == 1)
                                {
                                    DispatcherTimer viteza = new DispatcherTimer();
                                    viteza.Tick += new EventHandler(cioc_plastice);
                                    viteza.Interval = new TimeSpan(0, 0, 0, 0, 50);
                                    viteza.Start();
                                    dp_t = 0;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Datele introduse sunt eronate.");
                }
            }
            /*-----------CIOCNIRI ELASTICE----------*/
            if (ciocniri_elastice.Visibility == Visibility.Visible)
            {
                try
                {
                    id_cioc_elastic = 0;
                    ciocnitee = 0;
                    v1 = Convert.ToDouble(textBox5.Text);
                    v2 = Convert.ToDouble(textBox6.Text);
                    m1 = Convert.ToDouble(textBox7.Text);
                    m2 = Convert.ToDouble(textBox8.Text);

                    if ((m1 <= 0) || (m2 <= 0) || (v1 > 100) || (v1 < -100) || (v2 > 1000) || (v2 < -100) || (m1 > 100) || (m2 > 100))
                    {
                        MessageBox.Show("Datele introduse sunt eronate.");
                    }
                    else
                    {
                        start.IsEnabled = false;
                        stop.IsEnabled = true;
                        c_plastice.IsEnabled = false;
                        textBox5.IsEnabled = false;
                        textBox6.IsEnabled = false;
                        textBox7.IsEnabled = false;
                        textBox8.IsEnabled = false;

                        oke = 1;
                        ok1e = 1;

                        c3.Width = c3.Height = c4.Width = c4.Height = 30;
                        x1e = 300;
                        x2e = 400;

                        Canvas.SetLeft(c3, x1e);
                        Canvas.SetLeft(c4, x2e);

                        Canvas.SetTop(c3, 458);
                        Canvas.SetTop(c4, 458);
                        ciocnite = 0;
                        if (ciocniri_elastice.Visibility == Visibility.Visible)
                        {
                            if ((textBox5.Text != "") && (textBox6.Text != "") && (textBox7.Text != "") && (textBox8.Text != ""))
                            {
                                if (dp_te == 1)
                                {
                                    DispatcherTimer viteza = new DispatcherTimer();
                                    viteza.Tick += new EventHandler(cioc_elastice);
                                    viteza.Interval = new TimeSpan(0, 0, 0, 0, 50);
                                    viteza.Start();
                                    dp_te = 0;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Datele introduse sunt eronate.");
                }
            }

        }


        /*--------------TIMER CIOCNIRI PLASTICE------------------*/
        private void cioc_plastice(object sender, EventArgs e)
        {

            if (ok1 == 1)
            {
                if ((x1 >= x2 - 50) && (ok == 1))
                {
                    c1.Width = c2.Width = 48;
                    c1.Height = c2.Height  = 52;
                    Canvas.SetLeft(c1, x1);
                    Canvas.SetLeft(c2, x2 - 80);
                    Canvas.SetTop(c1, 436);
                    Canvas.SetTop(c2, 436);
                    textBlock5.Text = "Viteza celor doua corpuri este (m/s): " + Math.Round(( (m1 * v1 + m2 * v2) / (m1 + m2) ), 2).ToString();
                    ok = 0;
                    ciocnite = 1;
                }

                if (ciocnite == 0)
                {
                    try
                    {
                        Canvas.SetLeft(c1, x1);
                        Canvas.SetLeft(c2, x2);
                        x1 = x1 + v1 / 3;
                        x2 = x2 + v2 / 3;

                    }
                    catch (Exception x)
                    {
                        MessageBox.Show("Datele introduse sunt eronate.\n" + x);
                        textBox1.Text = "0";
                        textBox2.Text = "0";
                    }
                }
                else
                {
                    Canvas.SetLeft(c1, x1);
                    Canvas.SetLeft(c2, x2);
                    x1 = x1 + ((m1*v1 + m2 * v2)/(m1 + m2))/3;
                    x2 = x1 + 48;
                }
            }
        }
    
        /*-----------CIOCNIRI ELASTICE--------*/
        private void cioc_elastice(object sender, EventArgs e)
        {
            if (id_cioc_elastic == 0)
            {
                if (ok1e == 1)
                {
                    if (ciocnitee == 0)
                    {
                        Canvas.SetLeft(c3, x1e);
                        Canvas.SetLeft(c4, x2e);

                        x1e = x1e + v1 / 3;
                        x2e = x2e + v2 / 3;
                    }
                    else
                    {
                        c3.Width = c4.Width = 30;
                        c3.Height = c4.Height = 30;

                        Canvas.SetLeft(c3, x1e);
                        Canvas.SetLeft(c4, x2e);
                        Canvas.SetTop(c3, 458);
                        Canvas.SetTop(c4, 458);

                        x1e = x1e + (((2 * (m1 * v1 + m2 * v2)) / (m1 + m2)) - v1) / 3;
                        x2e = x2e + (((2 * (m2 * v2 + m1 * v1)) / (m1 + m2)) - v2) / 3;
                    }
                    if ((x1e >= x2e - 30) && (oke == 1))
                    {
                        c3.Width = c4.Width = 27;
                        c3.Height = c4.Height = 33;

                        Canvas.SetLeft(c3, x1e);
                        Canvas.SetLeft(c4, x2e - 3);
                        Canvas.SetTop(c3, 455);
                        Canvas.SetTop(c4, 455);
                        textBlock10.Text = "Viteza de dupa ciocnire a primului corp este (m/s): " + Math.Round((((2 * (m1 * v1 + m2 * v2)) / (m1 + m2)) - v1), 2).ToString();
                        textBlock11.Text = "Viteza de dupa ciocnire a corpului al doilea (m/s): " + Math.Round((((2 * (m1 * v1 + m2 * v2)) / (m1 + m2)) - v2), 2).ToString();
                        oke = 0;
                        ciocnitee = 1;
                        id_cioc_elastic = 1;
                        delay(100);
                    }
                }
            }
            else
            {
                id_cioc_elastic++;
                if (id_cioc_elastic == 3)
                    id_cioc_elastic = 0;
            }
        }
        private void stop_click(object sender, RoutedEventArgs e)
        {
            if (ciocniri_plastice.Visibility == Visibility.Visible)
            {
                ok1 = 0;

                textBox1.IsEnabled = true;
                textBox2.IsEnabled = true;
                textBox3.IsEnabled = true;
                textBox4.IsEnabled = true;


                textBox5.IsEnabled = true;
                textBox6.IsEnabled = true;
                textBox7.IsEnabled = true;
                textBox8.IsEnabled = true;

                c_elastice.IsEnabled = true;
                
                start.IsEnabled = true; //buton - start
                stop.IsEnabled = false; //buton - stop
            }
            else
            {
                ok1e = 0;

                textBox5.IsEnabled = true;
                textBox6.IsEnabled = true;
                textBox7.IsEnabled = true;
                textBox8.IsEnabled = true;
                
                c_plastice.IsEnabled = true;
                
                start.IsEnabled = true; //buton - start
                stop.IsEnabled = false; //buton - stop
            }
        }

        private void c_plastice_Click(object sender, RoutedEventArgs e)
        {
            ciocniri_plastice.Visibility = Visibility.Visible;
            ciocniri_elastice.Visibility = Visibility.Hidden;

            start.IsEnabled = true;
            c_plastice.IsEnabled = false;
            c_elastice.IsEnabled = true;
        }

        private void c_elastice_Click(object sender, RoutedEventArgs e)
        {
            ciocniri_elastice.Visibility = Visibility.Visible;
            ciocniri_plastice.Visibility = Visibility.Hidden;
            
            c_elastice.IsEnabled = false;
            c_plastice.IsEnabled = true;
            start.IsEnabled = true;
            stop.IsEnabled = false;
        }

        private void iesire_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ajutor_click(object sender, RoutedEventArgs e)
        {
            Window1 aj = new Window1();
            aj.Show();           
        }

        int a;
        
        void delay(int milliseconds)
        {
            Dispatcher.Invoke(new Action(() => { a = 0; }), DispatcherPriority.ContextIdle);
            Thread.Sleep(milliseconds);
        }

        private void despre_click(object sender, RoutedEventArgs e)
        {
            Window2 despre = new Window2();
            despre.Show();
        }

        private void textBox5_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double z1 = Convert.ToDouble(textBox5.Text);
                if((z1 < -100) || (z1 > 100))
                    textBox5.Background = Brushes.Red;
                else
                    textBox5.Background = Brushes.White;               
            }
            catch
            {
                textBox5.Background = Brushes.Red;
            }
        }

        private void textBox6_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double z2 = Convert.ToDouble(textBox6.Text);
                if ((z2 < -100) || (z2 > 100))
                    textBox6.Background = Brushes.Red;
                else
                    textBox6.Background = Brushes.White;
            }
            catch
            {
                textBox6.Background = Brushes.Red;
            }
        }

        private void textBox7_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double z3 = Convert.ToDouble(textBox7.Text);
                if ((z3 < -100) || (z3 > 100) || (z3 <= 0))
                    textBox7.Background = Brushes.Red;
                else
                    textBox7.Background = Brushes.White;
            }
            catch
            {
                textBox7.Background = Brushes.Red;
            }
        }

        private void textBox8_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double z4 = Convert.ToDouble(textBox8.Text);
                if ((z4 < -100) || (z4 > 100) || (z4 <= 0))
                    textBox8.Background = Brushes.Red;
                else
                    textBox8.Background = Brushes.White;
            }
            catch
            {
                textBox8.Background = Brushes.Red;
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double z5 = Convert.ToDouble(textBox1.Text);
                if ((z5 < -50) || (z5 > 50))
                    textBox1.Background = Brushes.Red;
                else
                    textBox1.Background = Brushes.White;
            }
            catch
            {
                textBox1.Background = Brushes.Red;
            }
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double z6 = Convert.ToDouble(textBox2.Text);
                if ((z6 < -50) || (z6 > 50))
                    textBox2.Background = Brushes.Red;
                else
                    textBox2.Background = Brushes.White;
            }
            catch
            {
                textBox2.Background = Brushes.Red;
            }
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double z7 = Convert.ToDouble(textBox3.Text);
                if ((z7 < -100) || (z7 > 100) || (z7 <= 0))
                    textBox3.Background = Brushes.Red;
                else
                    textBox3.Background = Brushes.White;
            }
            catch
            {
                textBox3.Background = Brushes.Red;
            }
        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double z8 = Convert.ToDouble(textBox4.Text);
                if ((z8 < -100) || (z8 > 100) || (z8 <= 0))
                    textBox4.Background = Brushes.Red;
                else
                    textBox4.Background = Brushes.White;
            }
            catch
            {
                textBox4.Background = Brushes.Red;
            }
        }
    }
}
