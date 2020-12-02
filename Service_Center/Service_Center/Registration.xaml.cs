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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        //Подключение класса "DBProcedures"
        DBProcedures procedures = new DBProcedures();

        public Registration()
        {
            InitializeComponent();
        }

        //Зарегестрировать пользователя
        public void bt_Regestration_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Surname_of_Staff.Text.ToString() == "" | tb_Name_of_Staff.Text.ToString() == "" | tb_Middle_Name_of_Staff.Text.ToString() == "" | tb_LoginS.Text.ToString() == "" | tb_PasswordS.Text.ToString() == "")
            {
                MessageBox.Show("Поля не заполнены! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
                Registration ps2 = new Registration();
                ps2.Show();
                Hide();

            }
            else
            {
                procedures.Staff_Regestration(tb_Surname_of_Staff.Text.ToString(), tb_Name_of_Staff.Text.ToString(), tb_Middle_Name_of_Staff.Text.ToString(), tb_LoginS.Text.ToString(), tb_PasswordS.Text.ToString());
                AuthorizationPage ps2 = new AuthorizationPage();
                ps2.Show();
                Hide();
            }

        }

        //Назад
        private void bt_Back_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationPage ps2 = new AuthorizationPage();
            ps2.Show();
            Hide();
        }

        //Проверка поля
        private void tb_Name_of_Staff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_Surname_of_Staff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_Middle_Name_of_Staff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_LoginS_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_PasswordS_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".IndexOf(e.Text) < 0;
        }
    }
}
