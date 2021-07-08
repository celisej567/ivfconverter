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

using System.IO;
using System.Diagnostics;

namespace mp4_or_avi_to_ivf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string Location = new FileInfo("mp4 or avi to ivf.exe").DirectoryName;

        public void test(object sender, RoutedEventArgs e)
        {
            this.listbox.Items.Clear();
            string[] mp4 = Directory.GetFiles(Location, "*.mp4");
            string[] avi = Directory.GetFiles(Location, "*.avi");

            bool? checkmp4 = this.mp4box.IsChecked;
            bool? checkavi = this.avibox.IsChecked;



            if (checkmp4 == true)
            {
                foreach (string files in mp4)
                {
                    string filesclean = System.IO.Path.GetFileName(files);


                    this.listbox.Items.Add(filesclean);
                }
            }if(checkavi == true)
            {
                foreach(string files in avi)
                {
                    string filesclean = System.IO.Path.GetFileName(files);

                    this.listbox.Items.Add(filesclean);
                }
            }

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process startconvert = new Process();
        
            Console.WriteLine("INITIALISING ffmpeg.exe");

            startconvert.StartInfo.FileName = "ivfconvert.cmd";
            

            

            bool? checkmp4 = this.mp4box.IsChecked;
            bool? checkavi = this.avibox.IsChecked;


            if (checkmp4 == true)
            {
                Console.WriteLine("Selected File is mp4. \nStarting mp4 code.");
                startconvert.StartInfo.Arguments = "-convertanotherfile mp4";
                startconvert.Start();
                 
            }if (checkavi == true)
            {
                Console.WriteLine("Selected File is mp4. \nStarting mp4 code.");
                startconvert.StartInfo.Arguments = "-convertanotherfile avi";
                startconvert.Start();
            }
            else
            {

            }
        }

        private void BiggerHide_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Hide_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Hide.Visibility = Visibility.Hidden;
            this.BiggerHide.Visibility = Visibility.Visible;
        }

        private void BiggerHide_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Hide.Visibility = Visibility.Visible;
            this.BiggerHide.Visibility = Visibility.Hidden;
        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Exit.Visibility = Visibility.Hidden;
            this.BiggerExit.Visibility = Visibility.Visible;
        }

        private void BiggerExit_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Exit.Visibility = Visibility.Visible;
            this.BiggerExit.Visibility = Visibility.Hidden;
        }

        private void BiggerExit_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
