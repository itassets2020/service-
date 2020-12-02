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
    /// Логика взаимодействия для Position.xaml
    /// </summary>
    public partial class Position : Window
    {
        private string QR = "";

        //Подключение класса "DBProcedures"
        DBProcedures procedures = new DBProcedures();

        public Position()
        {
            InitializeComponent();
        }

        //Real Time
        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        //Метод заполнения таблицы "Должности"
        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrPosition = qr;
                connection.PositionFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgPosition.ItemsSource = connection.dtPosition.DefaultView;
                dgPosition.Columns[0].Visibility = Visibility.Collapsed;
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
        //с заполенением таблицы "Должности"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Гость
            if (DBConnection.IDuser == 4)
            {
                bt_Insert_Position.IsEnabled = false;
                bt_Update_Position.IsEnabled = false;
                bt_Delete_Position.IsEnabled = false;

                QR = DBConnection.qrPosition;
                dgFill(QR);
            }

            //Администратор БД
            if (DBConnection.IDuser == 1)
            {
                bt_Insert_Position.IsEnabled = true;
                bt_Update_Position.IsEnabled = true;
                bt_Delete_Position.IsEnabled = true;

                QR = DBConnection.qrPosition;
                dgFill(QR);
            }

        }

        //Переименовка столбцов у компонента Data Grid
        private void dgPosition_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Name_of_Position"):
                    e.Column.Header = "Название должности";
                    break;
                case ("Salaty_of_Position"):
                    e.Column.Header = "Оклад";
                    break;
            }
        }

        //Перезагрузить страницу
        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Position ps2 = new Position();
            ps2.Show();
            Hide();
        }

        //Поиск
        private void bt_Search_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgPosition.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tb_Search.Text)
                {
                    dgPosition.SelectedItem = dataRow;
                }
            }
        }

        //Добавление данных
        private void bt_Insert_Service_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Position.Text.ToString() == "" | tb_Salaty_of_Position.Text.ToString() == "")
            {
                MessageBox.Show("Данные не указаны!" +
               "\n Повторите попытку!", "Сервис+",
               MessageBoxButton.OK, MessageBoxImage.Warning);
             
            }
            else
            {
                procedures.Position_Insert(tb_Name_of_Position.Text.ToString(), Convert.ToDecimal(tb_Salaty_of_Position.Text.ToString()));
                dgFill(QR);

                Position ps2 = new Position();
                ps2.Show();
                Hide();
            }
            

        }

        //Изменить данные
        private void bt_Update_Position_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Position.Text.ToString() == "" | tb_Salaty_of_Position.Text.ToString() == "")
            {
                MessageBox.Show("Данные не выбраны!" +
               "\n Повторите попытку!", "Сервис+",
               MessageBoxButton.OK, MessageBoxImage.Warning);
                
            }
            else
            {
                DataRowView ID = (DataRowView)dgPosition.SelectedItems[0];
                procedures.Position_Update(Convert.ToInt32(ID["ID_Position"]), tb_Name_of_Position.Text.ToString(), Convert.ToDecimal(tb_Salaty_of_Position.Text.ToString()));

            }

        }

        //Удалить данные
        private void bt_Delete_Position_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Position.Text.ToString() == "" | tb_Salaty_of_Position.Text.ToString() == "")
            {
                MessageBox.Show("Данные не выбраны!" +
               "\n Повторите попытку!", "Сервис+",
               MessageBoxButton.OK, MessageBoxImage.Warning);
               
            }
            else
            {
                DataRowView ID = (DataRowView)dgPosition.SelectedItems[0];
                procedures.Position_Delete(Convert.ToInt32(ID["ID_Position"]));
                dgFill(QR);
            }
            
        }

        //Проверка поля
        private void tb_Name_of_Position_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;

        }

        //Валидация данных
        private void tb_Salaty_of_Position_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;

        }
    }
}
