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
using System.Windows.Shapes;

namespace Service_Center
{
    /// <summary>
    /// Interaction logic for Personal_Area.xaml
    /// </summary>
    public partial class Personal_Area : Window
    {
        public Personal_Area()
        {
            InitializeComponent();
        }


        //Выход в главное меню
        private void bt_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ps2 = new MainWindow();
            ps2.Show();
            Hide();
        }

        //Событие при загрузки формы
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_FIO_2.Content = DBConnection.FIO;
            lbl_Position_2.Content = DBConnection.Dolgnost;
        }
    }
}
