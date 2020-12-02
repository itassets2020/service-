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
    /// Логика взаимодействия для Technic.xaml
    /// </summary>
    public partial class Technic : Window
    {
        private string QR = "";

        //Подключение класса "DBProcedures"
        DBProcedures procedures = new DBProcedures();

        public Technic()
        {
            InitializeComponent();
        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        //Метод заполнения таблицы "Техника"
        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrTechnic = qr;
                connection.TechnicFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgTechnic.ItemsSource = connection.dtTechnic.DefaultView;
                dgTechnic.Columns[0].Visibility = Visibility.Collapsed;
            };

            Dispatcher.Invoke(action);
        }

        //Событие при загрузки формы
        //с заполенением таблицы "Клиенты"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrTechnic;
            dgFill(QR);

            //Гость
            if (DBConnection.IDuser == 4)
            {
                bt_Insert_Technic.IsEnabled = false;
                bt_Update_Technic.IsEnabled = false;
                bt_Delete_Technic.IsEnabled = false;

                QR = DBConnection.qrTechnic;
                dgFill(QR);
                cbFill();
            }

            //Администратор БД
            if (DBConnection.IDuser == 1)
            {
                bt_Insert_Technic.IsEnabled = true;
                bt_Update_Technic.IsEnabled = true;
                bt_Delete_Technic.IsEnabled = true;

                QR = DBConnection.qrTechnic;
                dgFill(QR);
                cbFill();

            }

            //Касир
            if (DBConnection.IDuser == 5)
            {
                bt_Insert_Technic.IsEnabled = true;
                bt_Update_Technic.IsEnabled = false;
                bt_Delete_Technic.IsEnabled = false;

                QR = DBConnection.qrTechnic;
                dgFill(QR);
                cbFill();

            }

            //Стажер
            if (DBConnection.IDuser == 2)
            {
                bt_Insert_Technic.IsEnabled = true;
                bt_Update_Technic.IsEnabled = false;
                bt_Delete_Technic.IsEnabled = false;

                QR = DBConnection.qrTechnic;
                dgFill(QR);
                cbFill();

            }
        }

        //Метод заполнения выподающего списка
        private void cbFill()
        {
            DBConnection connection = new DBConnection();
            connection.qrClient_for_cbFill();
            cbClient.ItemsSource = connection.dtqrClient_for_cb.DefaultView;
            cbClient.SelectedValuePath = "ID_Client";
            cbClient.DisplayMemberPath = "Client_Info";
        }

        //Возвращение в главное меню
        private void Bt_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ps2 = new MainWindow();
            ps2.Show();
            Hide();
        }

        //Переименовка заголовков столбцов в компоненте DataGrid
        private void dgTechnic_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Name_of_Technic"):
                    e.Column.Header = "Название техники";
                    break;
                case ("Model_of_Technic"):
                    e.Column.Header = "Модель техники";
                    break;
                case ("Info_of_Error"):
                    e.Column.Header = "Данные неисправности";
                    break;
                case ("Name_of_Client"):
                    e.Column.Header = "Имя клиента";
                    break;
                case ("Surname_of_Client"):
                    e.Column.Header = "Фамилия клиента";
                    break;
                case ("Middle_Name_Client"):
                    e.Column.Header = "Отчестсво клиента";
                    break;
            }
        }

        //Поиск
        private void bt_Search_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgTechnic.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tb_Search.Text)
                {
                    dgTechnic.SelectedItem = dataRow;
                }
            }
        }

        //Добавление данных
        private void bt_Insert_Technic_Click(object sender, RoutedEventArgs e)
        {
            

            if (tb_Name_of_Technic.Text.ToString() == "" | tb_Model_of_Technic.Text.ToString() == "" | tb_Info_of_Error.Text.ToString() == "" | cbClient.Text.ToString() == "")
            {
                MessageBox.Show("Поля не заполнены! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                procedures.Technic_Insert(tb_Name_of_Technic.Text.ToString(), tb_Model_of_Technic.Text.ToString(), tb_Info_of_Error.Text.ToString(), Convert.ToInt32(cbClient.SelectedValue.ToString()));
                dgFill(QR);

                Technic ps2 = new Technic();
                ps2.Show();
                Hide();
            }

        }

        //Изменить
        private void bt_Update_Technic_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Technic.Text.ToString() == "" | tb_Model_of_Technic.Text.ToString() == "" | tb_Info_of_Error.Text.ToString() == "" | cbClient.Text.ToString() == "")
            {
                MessageBox.Show("Поля не выбраны!" +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DataRowView ID = (DataRowView)dgTechnic.SelectedItems[0];
                procedures.Technic_Update(Convert.ToInt32(ID["ID_Technic"]), tb_Name_of_Technic.Text.ToString(), tb_Model_of_Technic.Text.ToString(), tb_Info_of_Error.Text.ToString(), Convert.ToInt32(cbClient.SelectedValue.ToString()));

            }

        }

        //Удаление
        private void bt_Delete_Technic_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Technic.Text.ToString() == "" | tb_Model_of_Technic.Text.ToString() == "" | tb_Info_of_Error.Text.ToString() == "" | cbClient.Text.ToString() == "")
            {
                MessageBox.Show("Поля не выбраны!" +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DataRowView ID = (DataRowView)dgTechnic.SelectedItems[0];
                procedures.Technic_Delete(Convert.ToInt32(ID["ID_Technic"]));
                dgFill(QR);
            }    
                
        }

        //Сбросить
        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            tb_Name_of_Technic.Text = "";
            tb_Model_of_Technic.Text = "";
            tb_Info_of_Error.Text = "";
        }

        //Валидация данных
        private void tb_Name_of_Technic_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Валидация данных
        private void tb_Model_of_Technic_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ._1234567890".IndexOf(e.Text) < 0;
        }

        //Валидация данных
        private void tb_Info_of_Error_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }
    }
}

