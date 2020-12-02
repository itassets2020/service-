using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Clipboard = System.Windows.Forms.Clipboard;
using DataGrid = System.Windows.Controls.DataGrid;
using DataFormats = System.Windows.Forms.DataFormats;
using Xamarin.Forms;
using System.Diagnostics;
using System.Threading;

namespace Service_Center
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        public Client()
        {
            InitializeComponent();
        }
     
        private string QR = "";

        //Подключение класса "DBProcedures"
        DBProcedures procedures = new DBProcedures();

        //Real Time
        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
            dgFill(QR);
        }

        //Метод заполнения таблицы "Клиенты"
        private void dgFill(string qr)
        {

            Action action = () =>
            {
                DBConnection connection = new DBConnection();
                DBConnection.qrClient = qr;
                connection.ClientFill();
                connection.Dependency.OnChange += Dependency_OnChange;
                dgClient.ItemsSource = connection.dtClient.DefaultView;
                dgClient.Columns[0].Visibility = Visibility.Collapsed;
            };

            Dispatcher.Invoke(action);


        }

        //Событие при загрузки формы
        //с заполенением таблицы "Клиенты"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QR = DBConnection.qrClient;
            dgFill(QR);

            //Гость
            if (DBConnection.IDuser == 4)
            {
                bt_Insert_Client.IsEnabled = false;
                bt_Update_Client.IsEnabled = false;
                bt_Delete_Client.IsEnabled = false;
                QR = DBConnection.qrClient;
                dgFill(QR);
            }

            //Касир
            if (DBConnection.IDuser == 5)
            {
                bt_Insert_Client.IsEnabled = true;
                bt_Update_Client.IsEnabled = false;
                bt_Delete_Client.IsEnabled = false;
                QR = DBConnection.qrClient;
                dgFill(QR);
            }

            //Стажер
            if (DBConnection.IDuser == 2)
            {
                bt_Insert_Client.IsEnabled = true;
                bt_Update_Client.IsEnabled = false;
                bt_Delete_Client.IsEnabled = false;
                QR = DBConnection.qrClient;
                dgFill(QR);
            }

            //Администратор БД
            if (DBConnection.IDuser == 1)
            {
                bt_Insert_Client.IsEnabled = true;
                bt_Update_Client.IsEnabled = true;
                bt_Delete_Client.IsEnabled = true;
                QR = DBConnection.qrClient;
                dgFill(QR);
            }
        }

        //Добавление данных в таблицу "Клиенты"
        private void Bt_Insert_Client_Click(object sender, RoutedEventArgs e)
        {
        
            if (tb_Name_Client.Text.ToString() == "" | tb_Middle_Name_Client.Text.ToString() == "" | tb_Surname_of_Client.Text.ToString() == "" | tb_Number_Phone_of_Client.Text.ToString() == "" | tb_Email_of_Client.Text.ToString() == "")
            {
                System.Windows.MessageBox.Show("Поля не заполнены! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                procedures.Client_Insert(tb_Name_Client.Text.ToString(), tb_Surname_of_Client.Text.ToString(), tb_Middle_Name_Client.Text.ToString(), tb_Number_Phone_of_Client.Text.ToString(), tb_Email_of_Client.Text.ToString());
                dgFill(QR);
                Client ps2 = new Client();
                ps2.Show();
                Hide();
            }


        }

            //Перименовка названия столбцов
            private void DgClient_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
            {
                switch (e.Column.Header)
             {
                case ("Name_of_Client"):
                    e.Column.Header = "Имя клиента";
                    break;
                case ("Surname_of_Client"):
                    e.Column.Header = "Фамилия клиента";
                    break;
                case ("Middle_Name_Client"):
                    e.Column.Header = "Отчестсво клиента";
                    break;
                case ("Number_Phone_of_Client"):
                    e.Column.Header = "Номер телефона";
                    break;
                case ("Email_of_Client"):
                    e.Column.Header = "Email";
                    break;
              }
            }

        //Изменение данных в таблице "Клиенты"
        private void Bt_Update_Client_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_Client.Text.ToString() == "" | tb_Middle_Name_Client.Text.ToString() == "" | tb_Surname_of_Client.Text.ToString() == "" | tb_Number_Phone_of_Client.Text.ToString() == "" | tb_Email_of_Client.Text.ToString() == "")
            {
                System.Windows.MessageBox.Show("Поля не выбраны! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DataRowView ID = (DataRowView)dgClient.SelectedItems[0];
                procedures.Client_Update(Convert.ToInt32(ID["ID_Client"]), tb_Name_Client.Text.ToString(), tb_Surname_of_Client.Text.ToString(), tb_Middle_Name_Client.Text.ToString(), tb_Number_Phone_of_Client.Text.ToString(), tb_Email_of_Client.Text.ToString());

            }
        }

        //Удаление данных из таблицы "Клиенты"
        private void Bt_Delete_Client_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Name_Client.Text.ToString() == "" | tb_Middle_Name_Client.Text.ToString() == "" | tb_Surname_of_Client.Text.ToString() == "" | tb_Number_Phone_of_Client.Text.ToString() == "" | tb_Email_of_Client.Text.ToString() == "")
            {
                System.Windows.MessageBox.Show("Поля не выбраны! " +
                "\n Повторите попытку ввода!", "Сервис+",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DataRowView ID = (DataRowView)dgClient.SelectedItems[0];
                procedures.Client_Delete(Convert.ToInt32(ID["ID_Client"]));
                dgFill(QR);
            }
                
        }

        //Функция поиска
        private void Bt_Search_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView dataRow in (DataView)dgClient.ItemsSource)
            {
                if (dataRow.Row.ItemArray[1].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[2].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[3].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[4].ToString() == tb_Search.Text ||
                    dataRow.Row.ItemArray[5].ToString() == tb_Search.Text)
                {
                    dgClient.SelectedItem = dataRow;
                }
            }
        }

        //Возвращение в главное меню приложения
        private void Bt_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ps2 = new MainWindow();
            ps2.Show();
            Hide();
        }

        //Обновить страницу
        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            tb_Name_Client.Text = "";
            tb_Middle_Name_Client.Text = "";
            tb_Surname_of_Client.Text = "";
            tb_Number_Phone_of_Client.Text = "";
            tb_Email_of_Client.Text = "";
        }

        //Проверка поля
        private void tb_Surname_of_Client_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_Name_Client_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_Middle_Name_Client_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_Number_Phone_of_Client_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "01234567890".IndexOf(e.Text) < 0;
        }

        //Проверка поля
        private void tb_Email_of_Client_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@._1234567890".IndexOf(e.Text) < 0;
        }

        Export export = new Export();

        //Экспортв MS Word
        private void bt_Eport_Word_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dg = dgClient;
            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.Text);
            try
            {
                StreamWriter sw = new StreamWriter("export.doc");
                sw.WriteLine(result);
                sw.Close();
                Process.Start("export.doc");
            }
            catch (Exception ex) { }
        }




        //Экспортв MS Excel
        private void bt_Eport_Excel_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Office.Interop.Excel.Application app = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;
            Microsoft.Office.Interop.Excel.Worksheet ws = null;

            var process = Process.GetProcessesByName("EXCEL");

            Microsoft.Win32.SaveFileDialog openDlg = new Microsoft.Win32.SaveFileDialog();
            openDlg.FileName = DBConnection.Name_File;
            openDlg.Filter = "Excel (.xls)|*.xls |Excel (.xlsx)|*.xlsx |All files (*.*)|*.*";
            openDlg.FilterIndex = 2;
            openDlg.RestoreDirectory = true;
            string path = openDlg.FileName;

            if (openDlg.ShowDialog() == true)
            {
                
                app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;
                app.DisplayAlerts = false;
                wb = app.Workbooks.Add();
                ws = wb.ActiveSheet;
                dgClient.SelectAllCells();
                dgClient.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, dgClient);
                ws.Paste();
                ws.Range["A1", "E1"].Font.Bold = true;
                int number1 = ws.UsedRange.Rows.Count;
                Microsoft.Office.Interop.Excel.Range myRange = ws.Range["A1", "E" + number1];
                myRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                myRange.WrapText = false;
                ws.Columns.EntireColumn.AutoFit();
                //wb.SaveAs(path);
                
            }
        }
    }
}
