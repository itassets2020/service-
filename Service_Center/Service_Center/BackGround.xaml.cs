using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Service_Center
{
    /// <summary>
    /// Interaction logic for BackGround.xaml
    /// </summary>
    public partial class BackGround : Window
    {

     
        
        public BackGround()
        {
            InitializeComponent();
        }


        private DispatcherTimer timer = null;
        private int x;

        private void timerStart()
        {
            timer = new DispatcherTimer();  // если надо, то в скобках указываем приоритет, например DispatcherPriority.Render
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {


            int buttonclick = 0;
            while (buttonclick < 5)
            {
                buttonclick++;
                x++;
                Thread.Sleep(800);
            }

           


        }

        //Событие при загрузке формы
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            

         
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timerTick(sender, e);

            if (x == 5)
            {
                Authentication ps2 = new Authentication();
                ps2.Show();
                Hide();
            }
        }
    }
}
