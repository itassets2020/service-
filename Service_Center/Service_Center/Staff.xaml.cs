using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Staff.xaml
    /// </summary>
    public partial class Staff : Window
    {
        private string QR = "";

        //Подключение класса "DBProcedures"
        DBProcedures procedures = new DBProcedures();

        public Staff()
        {
            InitializeComponent();
        }

        //Real Time
        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        //Метод заполнения таблицы "Сотрудники"
        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrStaff = qr;
                connection.StaffFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgStaff.ItemsSource = connection.dtStaff.DefaultView;
                dgStaff.Columns[0].Visibility = Visibility.Collapsed;
            };

            Dispatcher.Invoke(action);
        }

        //Возвращение в главное меню
        private void Bt_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ps2 = new MainWindow();
            ps2.Show();
            Hide();
        }

        //Событие при загрузки формы
        //с заполенением таблицы "Сотрудники"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //Гость
            if (DBConnection.IDuser == 4)
            {
                bt_Insert_Staff.IsEnabled = false;
                bt_Update_Staff.IsEnabled = false;
                bt_Delete_Staff.IsEnabled = false;
                QR = DBConnection.qrStaff;
                //dgFill(QR);
                cbFill();
                cbFill2();

            }

            //Администратор БД
            if (DBConnection.IDuser == 1)
            {
                bt_Insert_Staff.IsEnabled = true;
                bt_Update_Staff.IsEnabled = true;
                bt_Delete_Staff.IsEnabled = true;
                QR = DBConnection.qrStaff;
                dgFill(QR);
                cbFill();
                cbFill2();
            }
        }


        //Перименовка столбцов компонента Data Grid
        private void dgStaff_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Surname_of_Staff"):
                    e.Column.Header = "Фамилия сотрудника";
                    break;
                case ("Name_of_Staff"):
                    e.Column.Header = "Имя сотрудника";
                    break;
                case ("Middle_Name_of_Staff"):
                    e.Column.Header = "Отчество сотрудника";
                    break;
                case ("Date_of_Birth_Staff"):
                    e.Column.Header = "Дата рождения";
                    break;
                case ("Sires_Document_Staff"):
                    e.Column.Header = "Серия паспорта";
                    break;
                case ("Number_Document_Staff"):
                    e.Column.Header = "Номер паспорта";
                    break;
                case ("LoginS"):
                    e.Column.Header = "Логин";
                    break;
                case ("PasswordS"):
                    e.Column.Header = "Пароль";
                    break;
                case ("Name_of_Position"):
                    e.Column.Header = "Название должности";
                    break;
                case ("Name_of_Role"):
                    e.Column.Header = "Название роли";
                    break;
                case ("Salaty_of_Position"):
                    e.Column.Header = "Оклад";
                    break;
            }
        }

        //Метод заполнения выподающего списка
        private void cbFill()
        {
            DBConnection connection = new DBConnection();
            connection.qrPosition_for_cbFill();
            cbName_of_Position.ItemsSource = connection.dt_Position_for_cb.DefaultView;
            cbName_of_Position.SelectedValuePath = "ID_Position";
            cbName_of_Position.DisplayMemberPath = "Position_Info";
        }

        //Метод заполнения выподающего списка
        private void cbFill2()
        {
            DBConnection connection = new DBConnection();
            connection.qrRole_for_cbFill2();
            cbName_of_Role.ItemsSource = connection.dt_Role_for_cb2.DefaultView;
            cbName_of_Role.SelectedValuePath = "ID_Role";
            cbName_of_Role.DisplayMemberPath = "Role_Info";
        }

        //Добавление данных
        private void bt_Insert_Client_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Surname_of_Staff.Text.ToString() == "" | tb_Name_of_Staff.Text.ToString() == "" | tb_Middle_Name_of_Staff.Text.ToString() == "" | dp_Date_of_Birth_Staff.Text.ToString() == "" | tb_Sires_Document_Staff.Text.ToString() == ""
                | tb_Number_Document_Staff.Text.ToString() == "" | tb_LoginS.Text.ToString() == "" | tb_PasswordS.Text.ToString() == "" | cbName_of_Position.Text.ToString() == "" | cbName_of_Role.Text.ToString() == "")
            {
                MessageBox.Show("Поля не заполнены! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                procedures.Staff_Insert(tb_Surname_of_Staff.Text.ToString(), tb_Name_of_Staff.Text.ToString(), tb_Middle_Name_of_Staff.Text.ToString(),
                dp_Date_of_Birth_Staff.Text.ToString(), tb_Sires_Document_Staff.Text.ToString(), tb_Number_Document_Staff.Text.ToString(), tb_LoginS.Text.ToString(),
                tb_PasswordS.Text.ToString(), Convert.ToInt32(cbName_of_Position.SelectedValue.ToString()), Convert.ToInt32(cbName_of_Role.SelectedValue.ToString()));
                dgFill(QR);

                Staff ps2 = new Staff();
                ps2.Show();
                Hide();
            }
                

        }

        //Изменение данных
        private void bt_Update_Client_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Surname_of_Staff.Text.ToString() == "" | tb_Name_of_Staff.Text.ToString() == "" | tb_Middle_Name_of_Staff.Text.ToString() == "" | dp_Date_of_Birth_Staff.Text.ToString() == "" | tb_Sires_Document_Staff.Text.ToString() == ""
               | tb_Number_Document_Staff.Text.ToString() == "" | tb_LoginS.Text.ToString() == "" | tb_PasswordS.Text.ToString() == "" | cbName_of_Position.Text.ToString() == "" | cbName_of_Role.Text.ToString() == "")
            {
                MessageBox.Show("Данные не выбраны! " +
                "\n Повторите попытку!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
                Staff ps2 = new Staff();
                ps2.Show();
                Hide();
            }
            else
            {
                DataRowView ID = (DataRowView)dgStaff.SelectedItems[0];
                procedures.Staff_Update(Convert.ToInt32(ID["ID_Staff"]), tb_Surname_of_Staff.Text.ToString(), tb_Name_of_Staff.Text.ToString(), tb_Middle_Name_of_Staff.Text.ToString(),
                dp_Date_of_Birth_Staff.Text.ToString(), tb_Sires_Document_Staff.Text.ToString(), tb_Number_Document_Staff.Text.ToString(), tb_LoginS.Text.ToString(),
                tb_PasswordS.Text.ToString(), Convert.ToInt32(cbName_of_Position.SelectedValue.ToString()), Convert.ToInt32(cbName_of_Role.SelectedValue.ToString()));

            }

        }

        //Удаление данных
        private void bt_Delete_Client_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Surname_of_Staff.Text.ToString() == "" | tb_Name_of_Staff.Text.ToString() == "" | tb_Middle_Name_of_Staff.Text.ToString() == "" | dp_Date_of_Birth_Staff.Text.ToString() == "" | tb_Sires_Document_Staff.Text.ToString() == ""
               | tb_Number_Document_Staff.Text.ToString() == "" | tb_LoginS.Text.ToString() == "" | tb_PasswordS.Text.ToString() == "")
            {
                MessageBox.Show("Данные не выбраны! " +
                "\n Повторите попытку!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
                Staff ps2 = new Staff();
                ps2.Show();
                Hide();
            }
            else
            {
                DataRowView ID = (DataRowView)dgStaff.SelectedItems[0];
                procedures.Staff_Delete(Convert.ToInt32(ID["ID_Staff"]));
                dgFill(QR);
            }

           
        }

        //Поиск данных
        private void bt_Search_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Search.Text.ToString() == "")
            {
                MessageBox.Show("Поиск пуст!" +
                "\n Повторите попытку!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                foreach (DataRowView dataRow in (DataView)dgStaff.ItemsSource)
                {
                    if (dataRow.Row.ItemArray[1].ToString() == tb_Search.Text ||
                        dataRow.Row.ItemArray[2].ToString() == tb_Search.Text ||
                        dataRow.Row.ItemArray[3].ToString() == tb_Search.Text ||
                        dataRow.Row.ItemArray[4].ToString() == tb_Search.Text ||
                        dataRow.Row.ItemArray[5].ToString() == tb_Search.Text ||
                        dataRow.Row.ItemArray[6].ToString() == tb_Search.Text ||
                        dataRow.Row.ItemArray[7].ToString() == tb_Search.Text ||
                        dataRow.Row.ItemArray[8].ToString() == tb_Search.Text ||
                        dataRow.Row.ItemArray[9].ToString() == tb_Search.Text ||
                        dataRow.Row.ItemArray[10].ToString() == tb_Search.Text)
                    {
                        dgStaff.SelectedItem = dataRow;
                    }
                }
            }
           
        }

        //Сбросить
        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            tb_Surname_of_Staff.Text = "";
            tb_Name_of_Staff.Text = "";
            tb_Middle_Name_of_Staff.Text = "";
            dp_Date_of_Birth_Staff.Text = "";
            tb_LoginS.Text = "";
            tb_PasswordS.Text = "";
            tb_Sires_Document_Staff.Text = "";
            tb_Number_Document_Staff.Text = "";
            
        }

        //Проверка поля
        private void tb_Surname_of_Staff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_Name_of_Staff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_Middle_Name_of_Staff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_Sires_Document_Staff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_Number_Document_Staff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
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

        //Проверка поля
        private void dp_Date_of_Birth_Staff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890.".IndexOf(e.Text) < 0;
        }

    }
}
