using Microsoft.Win32;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public string A;

        public Settings()
        {
            InitializeComponent();
        }

        //Возвращение в главное меню
        private void bt_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ps2 = new MainWindow();
            ps2.Show();
            Hide();
        }

        //Событие при загрузке формы
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tb_Put.Text = "C:";
        }

        //Сохранение пути по умолчанию
        private void bt_OK_Click(object sender, RoutedEventArgs e)
        {
            A = tb_Put.Text.ToString();
            RegistryKey registry = Registry.CurrentUser;
            RegistryKey key = registry.CreateSubKey("Put_Folder");
            key.SetValue("Put", A);//Запись значения в переменную реестра
        }
    }
}
