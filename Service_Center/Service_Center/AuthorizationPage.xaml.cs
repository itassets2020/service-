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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Window
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        //Вход
        private void Bt_Login_Click(object sender, RoutedEventArgs e)
        {
            DBProcedures procedures = new DBProcedures();


            if (tbEnterLogin.Password == "" | tbEnterPassword.Password == "")
            {
                MessageBox.Show("Поля не заполнены! " +
                    "\n Повторите попытку ввода!", "Сервис+",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Int32 ID_Record = procedures.Authorization(tbEnterLogin.Password.ToString(), tbEnterPassword.Password.ToString());
                if (ID_Record == 0)
                {
                    tbEnterLogin.Clear();
                    tbEnterPassword.Clear();
                    MessageBox.Show("Не верно введён логин или пароль! " +
                        "\n Повторите попытку ввода!", "Сервис+",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    DBConnection.IDuser = ID_Record;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Visibility = Visibility.Collapsed;
                }
            }
        }

        //Вернуться к аутентификации
        private void bt_Back_Click(object sender, RoutedEventArgs e)
        {
            Authentication mainWindow = new Authentication();
            mainWindow.Show();
            Visibility = Visibility.Collapsed;
        }

        //Вход как гость
        private void bt_Guest_Click(object sender, RoutedEventArgs e)
        {
            DBConnection.IDuser = 4;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Visibility = Visibility.Collapsed;
        }

        //Выход из программы
        private void bt_LExit_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
