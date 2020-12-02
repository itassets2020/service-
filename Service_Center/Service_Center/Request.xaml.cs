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
    /// Логика взаимодействия для Request.xaml
    /// </summary>
    public partial class Request : Window
    {
        private string QR = "";

        //Подключение класса "DBProcedures"
        DBProcedures procedures = new DBProcedures();

        public Request()
        {
            InitializeComponent();
        }

        //Real Time
        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        //Метод заполнения таблицы "Данные дополнительного запроса"
        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrRequest = qr;
                connection.RequestFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgRequest.ItemsSource = connection.dtRequest.DefaultView;
                dgRequest.Columns[0].Visibility = Visibility.Collapsed;
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
        //с заполенением таблицы "Данные дополнительного запроса"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Гость
            if (DBConnection.IDuser == 4)
            {
                bt_Insert_Requestt.IsEnabled = false;
                bt_Update_Request.IsEnabled = false;
                bt_Delete_Request.IsEnabled = false;

                QR = DBConnection.qrRequest;
                dgFill(QR);
                cbFill();
                cbFill1();
                cbFill2();
            }

            //Администратор БД
            if (DBConnection.IDuser == 1)
            {
                bt_Insert_Requestt.IsEnabled = true;
                bt_Update_Request.IsEnabled = true;
                bt_Delete_Request.IsEnabled = true;

                QR = DBConnection.qrRequest;
                dgFill(QR);
                cbFill();
                cbFill1();
                cbFill2();
            }
        }

        //Метод заполнения выподающего списка
        private void cbFill()
        {
            DBConnection connection = new DBConnection();
            connection.qrSupplier_for_cbFill();
            cb_Supplier_Info.ItemsSource = connection.dt_Supplier_for_cb.DefaultView;
            cb_Supplier_Info.SelectedValuePath = "ID_Supplier";
            cb_Supplier_Info.DisplayMemberPath = "Supplier_Info";
        }

        //Метод заполнения выподающего списка
        private void cbFill1()
        {
            DBConnection connection = new DBConnection();
            connection.qrTechnic_for_cbFill();
            cb_Name_of_Technic.ItemsSource = connection.dt_Technic_for_cb.DefaultView;
            cb_Name_of_Technic.SelectedValuePath = "ID_Technic";
            cb_Name_of_Technic.DisplayMemberPath = "Name_of_Technic";
        }

        //Метод заполнения выподающего списка
        private void cbFill2()
        {
            DBConnection connection = new DBConnection();
            connection.qrSupply_Contract_for_cbFill();
            tb_Name_of_Contract.ItemsSource = connection.dt_Supply_Contract_for_cb.DefaultView;
            tb_Name_of_Contract.SelectedValuePath = "ID_Supply_Contract";
            tb_Name_of_Contract.DisplayMemberPath = "Name_of_Contract";
        }

        //Переименовка столбцов в компоненте Data Grid
        private void dgRequest_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Name_of_Contract"):
                    e.Column.Header = "Название контракта";
                    break;
                case ("Surname_of_Supplier"):
                    e.Column.Header = "Фамилия поставщика";
                    break;
                case ("Name_of_Supplier"):
                    e.Column.Header = "Имя поставщика";
                    break;
                case ("Middle_Name_of_Supplier"):
                    e.Column.Header = "Отчестсво поставщика";
                    break;
                case ("Name_of_Technic"):
                    e.Column.Header = "Название техники";
                    break;
            }
        }

        //Обновить страницу
        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Request ps2 = new Request();
            ps2.Show();
            Hide();
        }

        //Поиск
        private void bt_Search_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgRequest.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[5].ToString() == tb_Search.Text)
                {
                    dgRequest.SelectedItem = dataRow;
                }
            }
        }

        //Добавить данные
        private void bt_Insert_Requestt_Click(object sender, RoutedEventArgs e)
        {
            procedures.Request_Insert(Convert.ToInt32(tb_Name_of_Contract.SelectedValue.ToString()), Convert.ToInt32(cb_Supplier_Info.SelectedValue.ToString()), Convert.ToInt32(cb_Name_of_Technic.SelectedValue.ToString()));
            dgFill(QR);

            Request ps2 = new Request();
            ps2.Show();
            Hide();
        }

        //Изменить данные
        private void bt_Update_Request_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgRequest.SelectedItems[0];
            procedures.Request_Update(Convert.ToInt32(ID["ID_Request"]), Convert.ToInt32(tb_Name_of_Contract.SelectedValue.ToString()), Convert.ToInt32(cb_Supplier_Info.SelectedValue.ToString()), Convert.ToInt32(cb_Name_of_Technic.SelectedValue.ToString()));

        }

        //Удалить данные
        private void bt_Delete_Request_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgRequest.SelectedItems[0];
            procedures.Request_Delete(Convert.ToInt32(ID["ID_Request"]));
            dgFill(QR);
        }
    }
}
