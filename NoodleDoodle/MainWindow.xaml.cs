using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace NoodleDoodle
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        
        List<int> number = new List<int>();
        List<int> status = new List<int>();
        int gotovka = 0;

        private void DrTxt(Image a,int num,Color myColor)
        {

            BitmapSource bitmapSource = a.Source as BitmapImage; 
    var visual = new DrawingVisual();
    using (DrawingContext drawingContext = visual.RenderOpen())
    {
        drawingContext.DrawImage(bitmapSource, new Rect(0, 0, 340, 270));
       

        SolidColorBrush brush = new SolidColorBrush( myColor );
        drawingContext.DrawText(
            new FormattedText("№ ", CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                new Typeface("Panton Bold"), 36, brush), new Point(10, 67));
        drawingContext.DrawText(
            new FormattedText(num.ToString(), CultureInfo.InvariantCulture, FlowDirection.LeftToRight,
                new Typeface("Panton Bold"), 54, brush), new Point(55, 50));
    }
    var image = new DrawingImage(visual.Drawing);
    a.Source = image;
        }


           private void dispatcherTimer_Tick(object sender, EventArgs e)
    {
    number.Clear();
    status.Clear();
    Grid G24 = new Grid();
    gotovka = 0;
    int t = 0;
    Color myColor = new Color();
    Image[] Im15 = new Image[15];
    Image[] Im24 = new Image[24];
    G15.Children.CopyTo(Im15, 0);
            

               for (int i = 0; i < 15; i++)
    {
        Im15[i].Visibility = Visibility.Hidden;
    }


    string cs = System.IO.File.ReadAllText(@"config.cfg");
               //"server=localhost;userid=root;
           // password=12211221;database=noodle";

    MySqlDataReader read = null;
    MySqlConnection connection = null;

    try
    {
        connection = new MySqlConnection(cs);
        connection.Open();
        string stm = @"SELECT Number, Status From InformBar";

        MySqlCommand cmd = new MySqlCommand(stm, connection);
        read = cmd.ExecuteReader();

        while (read.Read())
        {
            number.Add(read.GetInt32(0));
            status.Add(read.GetInt32(1));
            if ((read.GetInt32(1) == 0) || (read.GetInt32(1) == 1)) gotovka++;
        }


        
        if (gotovka <= 15)
        {
            this.Content = G15;
            for (int i = 0; i < number.Count(); i++)
            {
                if ((status[i] == 0) || (status[i] == 1))
                {
                    if (status[i] == 0)
                    {
                        Im15[t].Source = new BitmapImage(new Uri(@"1.png", UriKind.Relative)); Im15[t].Visibility = Visibility.Visible; 
                        
                        myColor.R = 124;
                        myColor.G = 14;
                        myColor.B = 103;
                        myColor.A = 255;
                    }

                    else if (status[i] == 1) 
                    { 
                        Im15[t].Source = new BitmapImage(new Uri(@"2.png", UriKind.Relative)); Im15[t].Visibility = Visibility.Visible;
                        
                        myColor.R = 253;
                        myColor.G = 198;
                        myColor.B = 62;
                        myColor.A = 255;
                    }



                    DrTxt(Im15[t],number[i],myColor);
                    t++;
                }
            }
        }



        if (gotovka > 15)
        {
           
            G24.Width = G15.Width;
            G24.Height = G15.Height;
            G24.HorizontalAlignment = G15.HorizontalAlignment;
            G24.VerticalAlignment = G15.VerticalAlignment;
            G24.Margin=G15.Margin;
            

            ColumnDefinition gridCol1 = new ColumnDefinition();
            ColumnDefinition gridCol2 = new ColumnDefinition();
            ColumnDefinition gridCol3 = new ColumnDefinition();
            ColumnDefinition gridCol4 = new ColumnDefinition();
            ColumnDefinition gridCol5 = new ColumnDefinition();
            ColumnDefinition gridCol6 = new ColumnDefinition();

            G24.ColumnDefinitions.Add(gridCol1);
            G24.ColumnDefinitions.Add(gridCol2);
            G24.ColumnDefinitions.Add(gridCol3);
            G24.ColumnDefinitions.Add(gridCol4);
            G24.ColumnDefinitions.Add(gridCol5);
            G24.ColumnDefinitions.Add(gridCol6);

           
            RowDefinition gridRow1 = new RowDefinition();
            RowDefinition gridRow2 = new RowDefinition();
            RowDefinition gridRow3 = new RowDefinition();
            RowDefinition gridRow4 = new RowDefinition();
           
        
            G24.RowDefinitions.Add(gridRow1);
            G24.RowDefinitions.Add(gridRow2);
            G24.RowDefinitions.Add(gridRow3);
            G24.RowDefinitions.Add(gridRow4);


            for (int i = 0; i < 4; i++)
            {int l=0;
                for (int j = 0; j < 6; j++)
                {
                    Im24[l] = new Image();
                    Grid.SetRow(Im24[l], i);
                    Grid.SetColumn(Im24[l], j);
                    G24.Children.Add(Im24[l]);
                    Im24[l].Margin = Im15[0].Margin;
                    Im24[l].Stretch = Stretch.Uniform;
                    Im24[l].Width = Im15[0].Width;
                    Im24[l].Height = Im15[0].Height;
                    l++;
                }
            }

            G24.Children.CopyTo(Im24, 0);

            for (int i = 0; i < number.Count(); i++)
            {
                if ((status[i] == 0) || (status[i] == 1))
                {
                    if (status[i] == 0)
                    {
                        Im24[t].Source = new BitmapImage(new Uri(@"1.png", UriKind.Relative));// Im24[t].Visibility = Visibility.Visible;

                        myColor.R = 124;
                        myColor.G = 14;
                        myColor.B = 103;
                        myColor.A = 255;
                    }

                    else if (status[i] == 1)
                    {
                        Im24[t].Source = new BitmapImage(new Uri(@"2.png", UriKind.Relative));// Im24[t].Visibility = Visibility.Visible;

                        myColor.R = 253;
                        myColor.G = 198;
                        myColor.B = 62;
                        myColor.A = 255;
                    }



                    DrTxt(Im24[t], number[i], myColor);
                    t++;
                }


            }


            this.Content = G24;

        }


    }

    catch (MySqlException ex)
    {
        MessageBox.Show("Error: {0}", ex.ToString());
    }

    finally
    {
        if (read != null)
        {
            read.Close();
        }

        if (connection != null)
        {
            connection.Close();
        }


    }
       }

           private void Window_Loaded_1(object sender, RoutedEventArgs e)
           {
               DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
               dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
               dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
               dispatcherTimer.Start();
           }

           private void Window_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
           {
               this.WindowState=WindowState.Minimized;
           }
        



        }
    }

