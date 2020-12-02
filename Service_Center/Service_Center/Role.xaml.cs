using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Логика взаимодействия для Role.xaml
    /// </summary>
    public partial class Role : Window
    {
        private string QR = "";

        //Подключение класса "DBProcedures"
        DBProcedures procedures = new DBProcedures();

        public Role()
        {
            InitializeComponent();
        }

        //Real Time
        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                dgFill(QR);
        }

        //Метод заполнения таблицы "Роли"
        private void dgFill(string qr)
        {
            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrRole = qr;
                connection.RoleFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgRole.ItemsSource = connection.dtRole.DefaultView;
                dgRole.Columns[0].Visibility = Visibility.Collapsed;
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
        //с заполенением таблицы "Роли"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Гость
            if (DBConnection.IDuser == 4)
            {
                bt_Insert_Role.IsEnabled = false;
                bt_Update_Role.IsEnabled = false;
                bt_Delete_Role.IsEnabled = false;

                QR = DBConnection.qrRole;
                dgFill(QR);
                cbFill();
                
            }

            //Администратор БД
            if (DBConnection.IDuser == 1)
            {
                bt_Insert_Role.IsEnabled = true;
                bt_Update_Role.IsEnabled = true;
                bt_Delete_Role.IsEnabled = true;

                QR = DBConnection.qrRole;
                dgFill(QR);
                cbFill();
            }
        }

        //Переименовка столбцов в компоненте Data Grid
        private void dgRole_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header)
            {
                case ("Name_of_Role"):
                    e.Column.Header = "Название роли";
                    break;
                case ("Access_of_Role"):
                    e.Column.Header = "Доступ роли";
                    break;
            }
        }

        //Заполнение компонента Combo_Box
        private void cbFill()
        {
            DBConnection connection = new DBConnection();
            connection.qrRole_for_cbFill();
            cb_Role.ItemsSource = connection.dt_Role_for_cb.DefaultView;
            cb_Role.SelectedValuePath = "ID_Role";
            cb_Role.DisplayMemberPath = "Access_of_Role";
        }

       
        //Добавление данных
        private void bt_Insert_Role_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Role.Text.ToString() == "" | cb_Role.Text == "")
            {
                MessageBox.Show("Поля не заполнены! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {

                procedures.Role_Insert(tb_Name_of_Role.Text.ToString(), Convert.ToInt32(cb_Role.SelectedValue.ToString()));
                dgFill(QR);

                Role ps2 = new Role();
                ps2.Show();
                Hide();
            }


            
        }

        //Сбросить
        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Role ps2 = new Role();
            ps2.Show();
            Hide();
        }

        //Поиск данных
        private void bt_Search_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgRole.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tb_Search.Text)
                {
                    dgRole.SelectedItem = dataRow;
                }
            }
        }

        //Изменить данные
        private void bt_Update_Role_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Role.Text.ToString() == "" | cb_Role.Text == "")
            {
                MessageBox.Show("Поля не выбраны! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //DataRowView ID = (DataRowView)dgRole.SelectedItems[0];
                //procedures.Role_Update(Convert.ToInt32(ID["ID_Role"]), tb_Name_of_Role.Text.ToString(), Convert.ToInt32(cb_Role.SelectedValue.ToString()));

            }
        }

        //Удаление данных
        private void bt_Delete_Role_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_of_Role.Text.ToString() == "" | cb_Role.Text == "")
            {
                MessageBox.Show("Поля не выбраны! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DataRowView ID = (DataRowView)dgRole.SelectedItems[0];
                procedures.Role_Delete(Convert.ToInt32(ID["ID_Role"]));
                dgFill(QR);
            }
                
        }

        //Валидцаия данных
        private void tb_Name_of_Role_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }
    }
}
