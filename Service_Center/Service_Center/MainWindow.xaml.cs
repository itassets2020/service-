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

namespace Service_Center
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

        //Переход в таблицу "Клиенты"
        private void Bt_Client_Open_Click(object sender, RoutedEventArgs e)
        {
            //DBConnection.Name_File = "Клиенты.doc";
            //DBConnection.Name_Grid = "dgClient";
            Client ps2 = new Client();
            ps2.Show();
            Hide();
        }

        //Переход в таблицу "Техника"
        private void Bt_Technic_Open_Click(object sender, RoutedEventArgs e)
        {
            Technic ps2 = new Technic();
            ps2.Show();
            Hide();
        }

        //Переход в таблицу "Договоры поставки"
        private void Bt_Supply_Contract_Open_Click(object sender, RoutedEventArgs e)
        {
            Supply_Contract ps2 = new Supply_Contract();
            ps2.Show();
            Hide();
        }

        //Переход в таблицу "Улсуги"
        private void Bt_Service_Open_Click(object sender, RoutedEventArgs e)
        {
            Service ps2 = new Service();
            ps2.Show();
            Hide();
        }

        //Переход в таблицу "Должности"
        private void Bt_Position_Open_Click(object sender, RoutedEventArgs e)
        {
            Position ps2 = new Position();
            ps2.Show();
            Hide();
        }

        //Переход в таблицу "Поставщики"
        private void Bt_Supplier_Open_Click(object sender, RoutedEventArgs e)
        {
            Supplier ps2 = new Supplier();
            ps2.Show();
            Hide();
        }

        //Переход в таблицу "Товарные чеки"
        private void Bt_Check_Open_Click(object sender, RoutedEventArgs e)
        {
            Check ps2 = new Check();
            ps2.Show();
            Hide();
        }

        //Переход в таблицу "Сотрудники"
        private void Bt_Staff_Open_Click(object sender, RoutedEventArgs e)
        {

            Staff ps2 = new Staff();
            ps2.Show();
            Hide();
        }

        //Переход в таблицу "Роли"
        private void Bt_Role_Open_Click(object sender, RoutedEventArgs e)
        {
            Role ps2 = new Role();
            ps2.Show();
            Hide();
        }

        //Переход в таблицу "Авторизация"
        private void Bt_Authorization_Open_Click(object sender, RoutedEventArgs e)
        {
            
        }

        //Переход в таблицу "Данные дополнительного запроса"
        private void Bt_Request_Open_Click(object sender, RoutedEventArgs e)
        {
            Request ps2 = new Request();
            ps2.Show();
            Hide();
        }

        //Блокировка программы
        private void Bt_Block_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationPage ps2 = new AuthorizationPage();
            ps2.Show();
            Hide();
        }

        //Привесвтие оператора
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DBConnection.IDuser == 3)
            {
                bt_Personal_Area.IsEnabled = false;
            }

            //Администратор БД
            if (DBConnection.IDuser == 1)
            {
                bt_Personal_Area.IsEnabled = true;
            }

            //Касир
            if (DBConnection.IDuser == 5)
            {
                bt_Supply_Contract_Open.IsEnabled = false;
                bt_Supplier_Open.IsEnabled = false;
                bt_Position_Open.IsEnabled = false;
                bt_Request_Open.IsEnabled = false;
                bt_Role_Open.IsEnabled = false;
                bt_Staff_Open.IsEnabled = false;
            }

            //Стажер
            if (DBConnection.IDuser == 2)
            {
                bt_Supply_Contract_Open.IsEnabled = false;
                bt_Supplier_Open.IsEnabled = false;
                bt_Position_Open.IsEnabled = false;
                bt_Request_Open.IsEnabled = false;
                bt_Role_Open.IsEnabled = false;
                bt_Staff_Open.IsEnabled = false;
            }

            //Гость
            if (DBConnection.IDuser == 4)
            {
                bt_Personal_Area.IsEnabled = false;
            }


        }

        //Переход в личный кабинет
        private void bt_Personal_Area_Click(object sender, RoutedEventArgs e)
        {
            DBProcedures procedures = new DBProcedures();
            procedures.Authorization2(DBConnection.Log, DBConnection.Pass, DBConnection.Dolgnost);


            Personal_Area ps2 = new Personal_Area();
            ps2.Show();
            Hide();
        }

        //Переход в настройки
        private void bt_Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings ps2 = new Settings();
            ps2.Show();
            Hide();
        }
    }
}
