using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Service_Center
{
    /// <summary>
    /// Логика взаимодействия для Authentication.xaml
    /// </summary>
    public partial class Authentication : Window
    {
        public Authentication()
        {

            splash.Show(false);
            splash.Close(TimeSpan.FromSeconds(3));
            Thread.Sleep(3000);
            InitializeComponent();
           
        }

        SplashScreen splash = new SplashScreen("Icons\\ico.png");

        //Переход к страницу авторизации
        private void Bt_Login_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationPage ps2 = new AuthorizationPage();
            ps2.Show();
            Hide();
        }

        //Переход к странице регестрации
        private void bt_R_Click(object sender, RoutedEventArgs e)
        {
            Registration ps2 = new Registration();
            ps2.Show();
            Hide();
        }

        public string abc, v, v1,z;

        private void Zanisi()
        {
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("Server_Configuration");

            RegistryKey a = registry.OpenSubKey("Server_Configuration");
            RegistryKey b = registry.OpenSubKey("Server_Configuration");

            string DS = a.GetValue("DS").ToString();
            string IC = b.GetValue("IC").ToString();
            v = DS;
            v1 = IC;
            abc = "Data Source = " + v + "; Initial Catalog = " + v1 + "; Persist Security Info = true; User ID = sa; Password = \"23082001\"";
            z = abc;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Zanisi();
        }
    }
}
