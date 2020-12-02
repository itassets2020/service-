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
using System.Security.Cryptography.X509Certificates;

namespace Service_Center
{
    /// <summary>
    /// Логика взаимодействия для Check.xaml
    /// </summary>
    public partial class Check : Window
    {
        private string QR = "";
        private SqlCommand command = new SqlCommand("", Configuration_Class.connection);

        //Подключение класса "DBProcedures"
        DBProcedures procedures = new DBProcedures();

        public Check()
        {
            InitializeComponent();
        }

        //Real time
        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        //Метод заполнения таблицы "Товарные чеки"
        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrCheck = qr;
                connection.CheckFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgCheck.ItemsSource = connection.dtCheck.DefaultView;
                dgCheck.Columns[0].Visibility = Visibility.Collapsed;
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
        //с заполенением таблицы "Товарные чеки"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            QR = DBConnection.qrCheck;
            dgFill(QR);
            cbFill();
            cbFill2();
            cbFill3();

            //Гость
            if (DBConnection.IDuser == 4)
            {
                bt_Insert_Check.IsEnabled = false;
                bt_Update_Check.IsEnabled = false;
                bt_Delete_Check.IsEnabled = false;
            }

            //Администратор БД
            if (DBConnection.IDuser == 1)
            {
                bt_Insert_Check.IsEnabled = true;
                bt_Update_Check.IsEnabled = true;
                bt_Delete_Check.IsEnabled = true;
            }

            //Касир
            if (DBConnection.IDuser == 5)
            {
                bt_Insert_Check.IsEnabled = true;
                bt_Update_Check.IsEnabled = false;
                bt_Delete_Check.IsEnabled = false;
            }

            //Стажер
            if (DBConnection.IDuser == 2)
            {
                bt_Insert_Check.IsEnabled = true;
                bt_Update_Check.IsEnabled = false;
                bt_Delete_Check.IsEnabled = false;
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

        //Метод заполнения выподающего списка
        private void cbFill2()
        {
            DBConnection connection = new DBConnection();
            connection.qrStaff_for_cbFill();
            cbStaff.ItemsSource = connection.dt_Staff_for_cb.DefaultView;
            cbStaff.SelectedValuePath = "ID_Staff";
            cbStaff.DisplayMemberPath = "Staff_Info";
        }

        //Метод заполнения выподающего списка
        private void cbFill3()
        {
            DBConnection connection = new DBConnection();
            connection.qrService_for_cbFill();
            cbService.ItemsSource = connection.dt_Service_for_cb.DefaultView;
            cbService.SelectedValuePath = "ID_Service";
            cbService.DisplayMemberPath = "Service_Info";
        }

        //Переименовка стобцов компонента DataGrid
        private void dgCheck_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Number_of_Check"):
                    e.Column.Header = "Номер чека";
                    break;
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
                case ("Surname_of_Staff"):
                    e.Column.Header = "Фамилия сотрудника";
                    break;
                case ("Name_of_Service"):
                    e.Column.Header = "Название услуги";
                    break;
            }
        }

        //Добавление данных
        private void bt_Insert_Check_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Number_of_Check.Text.ToString() == "" | cbStaff.Text.ToString() == "" | cbService.Text.ToString() == "" | cbClient.Text.ToString() == "")
            {
                MessageBox.Show("Поля не заполнены! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {

                procedures.Check_Insert(Convert.ToInt32(tb_Number_of_Check.Text.ToString()), Convert.ToInt32(cbClient.SelectedValue.ToString()), Convert.ToInt32(cbService.SelectedValue.ToString()),
            Convert.ToInt32(cbStaff.SelectedValue.ToString()));
                dgFill(QR);

                Check ps2 = new Check();
                ps2.Show();
                Hide();
            }


        }

        //Удаление данных
        private void bt_Delete_Check_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dgCheck.SelectedItems[0];
            procedures.Check_Delete(Convert.ToInt32(ID["ID_Check"]));
            dgFill(QR);
        }

        //Изменить данные
        private void bt_Update_Check_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Number_of_Check.Text.ToString() == "" | cbStaff.Text.ToString() == "" | cbService.Text.ToString() == "" | cbClient.Text.ToString() == "")
            {
                MessageBox.Show("Поля не выбраны! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DataRowView ID = (DataRowView)dgCheck.SelectedItems[0];
                procedures.Check_Update(Convert.ToInt32(ID["ID_Check"]), Convert.ToInt32(tb_Number_of_Check.Text.ToString()), Convert.ToInt32(cbClient.SelectedValue.ToString()), Convert.ToInt32(cbService.SelectedValue.ToString()), Convert.ToInt32(cbStaff.SelectedValue.ToString()));


            }


        }

        //Сбросить
        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Check ps2 = new Check();
            ps2.Show();
            Hide();
        }

        //Поиск данных
        private void bt_Search_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgCheck.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tb_Search.Text)
                {
                    dgCheck.SelectedItem = dataRow;
                }
            }
        }
    }
}
