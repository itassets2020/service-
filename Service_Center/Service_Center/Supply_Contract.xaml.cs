using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Supply_Contract.xaml
    /// </summary>
    public partial class Supply_Contract : Window
    {
        private string QR = "";

        //Подключение класса "DBProcedures"
        DBProcedures procedures = new DBProcedures();

        public Supply_Contract()
        {
            InitializeComponent();
        }

        //Real Time
        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        //Метод заполнения таблицы "Договоры поставки"
        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrSupply_Contract = qr;
                connection.Supply_ContractFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dg_Supply_Contract.ItemsSource = connection.dtSupply_Contract.DefaultView;
                dg_Supply_Contract.Columns[0].Visibility = Visibility.Collapsed;
            };

            Dispatcher.Invoke(action);
        }

        //Вернуться в главное меню
        private void Bt_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ps2 = new MainWindow();
            ps2.Show();
            Hide();
        }

        //Событие при загрузки формы
        //с заполенением таблицы "Клиенты"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Гость
            if (DBConnection.IDuser == 4)
            {
                bt_Insert_Supply_Contract.IsEnabled = false;
                bt_Update_Supply_Contract.IsEnabled = false;
                bt_Delete_Supply_Contract.IsEnabled = false;

                QR = DBConnection.qrSupply_Contract;
                dgFill(QR);
                cbFill();
            }

            //Администратор БД
            if (DBConnection.IDuser == 1)
            {
                bt_Insert_Supply_Contract.IsEnabled = true;
                bt_Update_Supply_Contract.IsEnabled = true;
                bt_Delete_Supply_Contract.IsEnabled = true;

                QR = DBConnection.qrSupply_Contract;
                dgFill(QR);
                cbFill();

            }
        }

        //Метод заполнения выподающего списка
        private void cbFill()
        {
            DBConnection connection = new DBConnection();
            connection.qrTechnic_for_cbFill();
            cb_Name_of_Technic.ItemsSource = connection.dt_Technic_for_cb.DefaultView;
            cb_Name_of_Technic.SelectedValuePath = "ID_Technic";
            cb_Name_of_Technic.DisplayMemberPath = "Name_of_Technic";
        }

        //Переименовка столбцов в компоненте DataGrid
        private void dg_Supply_Contract_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Name_of_Contract"):
                    e.Column.Header = "Название контракта";
                    break;
                case ("Price_of_Contract"):
                    e.Column.Header = "Цена контракта";
                    break;
                case ("Name_of_Component"):
                    e.Column.Header = "Название компонента";
                    break;
                case ("Name_of_Technic"):
                    e.Column.Header = "Название техники";
                    break;
            }
        }

        //Добавление данных
        private void bt_Insert_Supply_Contract_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Contract.Text.ToString() == "" | tb_Name_of_Component.Text.ToString() == "" | tb_Price_of_Contract.Text.ToString() == "" | tb_Name_of_Component.Text.ToString() == "" | cb_Name_of_Technic.Text.ToString() == "")
            {
                MessageBox.Show("Поля не заполнены! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
                Supply_Contract ps2 = new Supply_Contract();
                ps2.Show();
                Hide();
            }
            else
            {
                procedures.Supply_Contract_Insert(tb_Name_of_Contract.Text.ToString(), Convert.ToDecimal(tb_Price_of_Contract.Text.ToString()), tb_Name_of_Contract.Text.ToString(), Convert.ToInt32(cb_Name_of_Technic.SelectedValue.ToString()));
                dgFill(QR);

                Supply_Contract ps2 = new Supply_Contract();
                ps2.Show();
                Hide();
            }

                
        }

        //Изменение данных
        private void bt_Update_Supply_Contract_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Contract.Text.ToString() == "" | tb_Name_of_Component.Text.ToString() == "" | tb_Price_of_Contract.Text.ToString() == "" | tb_Name_of_Component.Text.ToString() == "" | cb_Name_of_Technic.Text.ToString() == "")
            {
                MessageBox.Show("Данные не выбраны! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
                Supply_Contract ps2 = new Supply_Contract();
                ps2.Show();
                Hide();
            }
            else
            {
                DataRowView ID = (DataRowView)dg_Supply_Contract.SelectedItems[0];
                procedures.Supply_Contract_Update(Convert.ToInt32(ID["ID_Supply_Contract"]), tb_Name_of_Contract.Text.ToString(), Convert.ToDecimal(tb_Price_of_Contract.Text.ToString()), tb_Name_of_Contract.Text.ToString(), Convert.ToInt32(cb_Name_of_Technic.SelectedValue.ToString()));

            }

        }

        //Удаление данных
        private void bt_Delete_Supply_Contract_Click(object sender, RoutedEventArgs e)
        {
            DataRowView ID = (DataRowView)dg_Supply_Contract.SelectedItems[0];
            procedures.Supply_Contract_Delete(Convert.ToInt32(ID["ID_Supply_Contract"]));
            dgFill(QR);
        }

        //Поиск
        private void bt_Search_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dg_Supply_Contract.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tb_Search.Text)
                {
                    dg_Supply_Contract.SelectedItem = dataRow;
                }
            }
        }

        //Обновить страницу
        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            tb_Name_of_Contract.Text = "";
            tb_Name_of_Component.Text = "";
            tb_Price_of_Contract.Text = "";
            cb_Name_of_Technic.Text = "";
        }

        //Валидация данных
        private void tb_Name_of_Contract_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Валидация данных
        private void tb_Price_of_Contract_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890.".IndexOf(e.Text) < 0;
        }

    }
}
