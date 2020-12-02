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
    /// Логика взаимодействия для Supplier.xaml
    /// </summary>
    public partial class Supplier : Window
    {
        private string QR = "";

        //Подключение класса "DBProcedures"
        DBProcedures procedures = new DBProcedures();

        public Supplier()
        {
            InitializeComponent();
        }

        //Real Time
        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        //Метод заполнения таблицы "Поставщики"
        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrSupplier = qr;
                connection.SupplierFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgSupplier.ItemsSource = connection.dtSupplier.DefaultView;
                dgSupplier.Columns[0].Visibility = Visibility.Collapsed;
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

        //Метод заполнения выподающего списка
        private void cbFill()
        {
            DBConnection connection = new DBConnection();
            connection.qrSupply_Contract_for_cbFill();
            cb_Supply_Contract_Info.ItemsSource = connection.dt_Supply_Contract_for_cb.DefaultView;
            cb_Supply_Contract_Info.SelectedValuePath = "ID_Supply_Contract";
            cb_Supply_Contract_Info.DisplayMemberPath = "Name_of_Contract";
        }

        //Событие при загрузки формы
        //с заполенением таблицы "Поставщики"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Гость
            if (DBConnection.IDuser == 4)
            {
                bt_Insert_Supplier.IsEnabled = false;
                bt_Update_Supplier.IsEnabled = false;
                bt_Delete_Supplier.IsEnabled = false;

                QR = DBConnection.qrSupplier;
                dgFill(QR);
                cbFill();
            }

            //Администратор БД
            if (DBConnection.IDuser == 1)
            {
                bt_Insert_Supplier.IsEnabled = true;
                bt_Update_Supplier.IsEnabled = true;
                bt_Delete_Supplier.IsEnabled = true;

                QR = DBConnection.qrSupplier;
                dgFill(QR);
                cbFill();
            }
        }
        
        //Переименовка столбцов в компоненте DataGrid
        private void dgSupplier_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Surname_of_Supplier"):
                    e.Column.Header = "Фамилия поставщика";
                    break;
                case ("Name_of_Supplier"):
                    e.Column.Header = "Имя поставщика";
                    break;
                case ("Middle_Name_of_Supplier"):
                    e.Column.Header = "Отчестсво поставщика";
                    break;
                case ("Sires_Document"):
                    e.Column.Header = "Серия паспорта";
                    break;
                case ("Number_Document"):
                    e.Column.Header = "Номер паспорта";
                    break;
                case ("Name_of_Contract"):
                    e.Column.Header = "Название контракта";
                    break;
                case ("Price_of_Contract"):
                    e.Column.Header = "Цена контракта";
                    break;
                case ("Name_of_Component"):
                    e.Column.Header = "Название компонента";
                    break;
            }
        }

        //Кнопка "Сбросить"
        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Supplier ps2 = new Supplier();
            ps2.Show();
            Hide();
        }

        //Поиск данных 
        private void bt_Search_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgSupplier.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[5].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[6].ToString() == tb_Search.Text)
                {
                    dgSupplier.SelectedItem = dataRow;
                }
            }
        }

        //Добавление поставщика
        private void bt_Insert_Supplier_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Surname_of_Supplier.Text.ToString() == "" | tb_Name_of_Supplier.Text.ToString() == "" | tb_Middle_Name_of_Supplier.Text.ToString() == "" | 
                tb_Sires_Document.Text.ToString() == "" | tb_Number_Document.Text.ToString() == "" | cb_Supply_Contract_Info.Text.ToString() == "")
            {
                MessageBox.Show("Поля не заполнены!" +
                "\n Повторите попытку!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                procedures.Supplier_Insert(tb_Surname_of_Supplier.Text.ToString(), tb_Name_of_Supplier.Text.ToString(), tb_Middle_Name_of_Supplier.Text.ToString(),
                tb_Sires_Document.Text.ToString(), tb_Number_Document.Text.ToString(), Convert.ToInt32(cb_Supply_Contract_Info.SelectedValue.ToString()));
                dgFill(QR);

                Supplier ps2 = new Supplier();
                ps2.Show();
                Hide();
            }
            

        }

        //Изменение поставщика
        private void bt_Update_Supplier_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Surname_of_Supplier.Text.ToString() == "" | tb_Name_of_Supplier.Text.ToString() == "" | tb_Middle_Name_of_Supplier.Text.ToString() == "" |
                tb_Sires_Document.Text.ToString() == "" | tb_Number_Document.Text.ToString() == "" | cb_Supply_Contract_Info.Text.ToString() == "")
            {
                MessageBox.Show("Данные не выбраны!" +
                "\n Повторите попытку!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DataRowView ID = (DataRowView)dgSupplier.SelectedItems[0];
                procedures.Supplier_Update(Convert.ToInt32(ID["ID_Supplier"]), tb_Surname_of_Supplier.Text.ToString(), tb_Name_of_Supplier.Text.ToString(), tb_Middle_Name_of_Supplier.Text.ToString(), tb_Sires_Document.Text.ToString(), tb_Number_Document.Text.ToString(), Convert.ToInt32(cb_Supply_Contract_Info.SelectedValue.ToString()));
            }

        }

        //Удалить поставщика
        private void bt_Delete_Supplier_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Surname_of_Supplier.Text.ToString() == "" | tb_Name_of_Supplier.Text.ToString() == "" | tb_Middle_Name_of_Supplier.Text.ToString() == "" |
                tb_Sires_Document.Text.ToString() == "" | tb_Number_Document.Text.ToString() == "")
            {
                MessageBox.Show("Данные не выбраны!" +
                "\n Повторите попытку!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DataRowView ID = (DataRowView)dgSupplier.SelectedItems[0];
                procedures.Supplier_Delete(Convert.ToInt32(ID["ID_Supplier"]));
                dgFill(QR);
            }
            
        }

        //Валидация данных
        private void tb_Surname_of_Supplier_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Валидация данных
        private void tb_Name_of_Supplier_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Валидация данных
        private void tb_Middle_Name_of_Supplier_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Валидация данных
        private void tb_Sires_Document_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }

        //Валидация данных
        private void tb_Number_Document_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }
    }
}
