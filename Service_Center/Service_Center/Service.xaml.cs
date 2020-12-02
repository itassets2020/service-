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
    /// Логика взаимодействия для Service.xaml
    /// </summary>
    public partial class Service : Window
    {
        private string QR = "";

        //Подключение класса "DBProcedures"
        DBProcedures procedures = new DBProcedures();

        public Service()
        {
            InitializeComponent();
        }

        //Real Time
        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        //Метод заполнения таблицы "Услуги"
        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrService = qr;
                connection.ServiceFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgService.ItemsSource = connection.dtService.DefaultView;
                dgService.Columns[0].Visibility = Visibility.Collapsed;
            };

            Dispatcher.Invoke(action);
        }

        //Метод заполнения выподающего списка
        private void cbFill()
        {
            DBConnection connection = new DBConnection();
            connection.qrTechnic_for_cbFill_2();
            cb_Technic_Info.ItemsSource = connection.dt_Technic_for_cb_2.DefaultView;
            cb_Technic_Info.SelectedValuePath = "ID_Technic";
            cb_Technic_Info.DisplayMemberPath = "Technic_Info";
        }

        //Возвращение в главное меню
        private void Bt_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ps2 = new MainWindow();
            ps2.Show();
            Hide();
        }

        //Событие при загрузки формы
        //с заполенением таблицы "Услуги"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Гость
            if (DBConnection.IDuser == 4)
            {
                bt_Insert_Service.IsEnabled = false;
                bt_Update_Service.IsEnabled = false;
                bt_Delete_Service.IsEnabled = false;

                QR = DBConnection.qrService;
                dgFill(QR);
                cbFill();
            }

            //Администратор БД
            if (DBConnection.IDuser == 1)
            {
                bt_Insert_Service.IsEnabled = true;
                bt_Update_Service.IsEnabled = true;
                bt_Delete_Service.IsEnabled = true;

                QR = DBConnection.qrService;
                dgFill(QR);
                cbFill();
            }

            //Касир
            if (DBConnection.IDuser == 5)
            {
                bt_Insert_Service.IsEnabled = true;
                bt_Update_Service.IsEnabled = false;
                bt_Delete_Service.IsEnabled = false;

                QR = DBConnection.qrService;
                dgFill(QR);
                cbFill();

            }

            //Стажер
            if (DBConnection.IDuser == 2)
            {
                bt_Insert_Service.IsEnabled = true;
                bt_Update_Service.IsEnabled = false;
                bt_Delete_Service.IsEnabled = false;

                QR = DBConnection.qrService;
                dgFill(QR);
                cbFill();

            }


        }

        //Очистить все поля
        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Service ps2 = new Service();
            ps2.Show();
            Hide();
        }

        //Переименовка столбцов компонента DataGrid
        private void dgService_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Name_of_Service"):
                    e.Column.Header = "Название услуги";
                    break;
                case ("Price_of_Service"):
                    e.Column.Header = "Цена услуги";
                    break;
                case ("Name_of_Technic"):
                    e.Column.Header = "Название техники";
                    break;
                case ("Model_of_Technic"):
                    e.Column.Header = "Модель техники";
                    break;
            }
        }

        //Поиск данных
        private void bt_Search_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgService.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tb_Search.Text)
                {
                    dgService.SelectedItem = dataRow;
                }
            }
        }

        //Добавление данных
        private void bt_Insert_Service_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Service.Text.ToString() == "" | tb_Price_of_Service.Text.ToString() == "" | cb_Technic_Info.Text.ToString() == "")
            {
                MessageBox.Show("Присутсвуют пустые поля" +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                procedures.Service_Insert(tb_Name_of_Service.Text.ToString(), Convert.ToDecimal(tb_Price_of_Service.Text.ToString()), Convert.ToInt32(cb_Technic_Info.SelectedValue.ToString()));
                dgFill(QR);

                Service ps2 = new Service();
                ps2.Show();
                Hide();
            }
        }

        //Изменение данных
        private void bt_Update_Service_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Service.Text.ToString() == "" | tb_Price_of_Service.Text.ToString() == "" | cb_Technic_Info.Text.ToString() == "")
            {
                MessageBox.Show("Данные не выбраны!" +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DataRowView ID = (DataRowView)dgService.SelectedItems[0];
                procedures.Service_Update(Convert.ToInt32(ID["ID_Service"]), tb_Name_of_Service.Text.ToString(), Convert.ToDecimal(tb_Price_of_Service.Text.ToString()), Convert.ToInt32(cb_Technic_Info.SelectedValue.ToString()));
            }
        }

        //Удаление данных
        private void bt_Delete_Service_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Service.Text.ToString() == "" | tb_Price_of_Service.Text.ToString() == "")
            {
                MessageBox.Show("Данные не выбраны!" +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DataRowView ID = (DataRowView)dgService.SelectedItems[0];
                //procedures.Service_Delete(Convert.ToInt32(ID["ID_Service"]));
                dgFill(QR);
            }    
                
        }

        //Проверка поля
        private void tb_Name_of_Service_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_Price_of_Service_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }
    }
}
