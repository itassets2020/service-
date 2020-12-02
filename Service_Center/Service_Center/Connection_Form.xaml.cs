using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Service_Center
{
    /// <summary>
    /// Interaction logic for Connection_Form.xaml
    /// </summary>
    public partial class Connection_Form : Window
    {
        public Connection_Form()
        {
            InitializeComponent();
        }

        //Событие при загрузки формы
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Configuration_Class configuration_Class = new Configuration_Class();
            configuration_Class.SQL_Server_Configuration_get();

            if (configuration_Class.DS == "")
            {
                //вызов класса конфигурации
                Configuration_Class configuration = new Configuration_Class();
                //присвоение event action событий
                configuration.server_Collection += Configuration_server_Collection;
                //
                Thread threadservers = new Thread(configuration.SQL_Server_Enumurator);
                //запуск потока
                threadservers.Start();
            }
            else
            {
                Authentication ps2 = new Authentication();
                ps2.Show();
                Hide();
            }
        }

        //Отображение серверов
        private void Configuration_server_Collection(DataTable obj)
        {
            //
            //Вызов делегата для присвоения в него фрагмента кода через лямбда выражение => в делегат присваивается код
            Action action = () =>
            {
                //для каждого строки таблицы в выпадающий список
                foreach (DataRow r in obj.Rows)
                {
                    cb_Servers.Items.Add(string.Format(@"{0}\{1}", r[0], r[1]));
                }
                //присвоение фонового потока в основной

            };
            Dispatcher.Invoke(action);
        }

        //Проверка
        private void test_Click(object sender, RoutedEventArgs e)
        {
            Configuration_Class configuration = new Configuration_Class();
            configuration.ds = cb_Servers.SelectedItem.ToString();
            configuration.connection_checked += Configuration_connection_checked;
            Thread thread = new Thread(configuration.SQL_Data_Base_Checking);
            thread.Start();
        }

        //Проверка
        private void Configuration_connection_checked(bool obj)
        {
            switch (obj)
            {
                //Если подключение выполнено верно то появляется сообщение
                case true:
                    System.Windows.MessageBox.Show("Проверка выполнена!");
                    Action action = () =>
                    {
                        //Повторение метода выбора
                        Configuration_Class configuration_coll
                        = new Configuration_Class();
                        configuration_coll.Data_Base_Collection
                        += Configuration_Data_Base_Collection;
                        Thread threadBases
                        = new Thread(configuration_coll.SQL_Data_Base_Collection);
                        threadBases.Start();

                    };
                    Dispatcher.Invoke(action);
                    break;
                case false:
                    //Вслучае если нет подключения повторяем сбор данных
                    //о сервере
                    Configuration_Class configuration
                    = new Configuration_Class();
                    configuration.server_Collection
                    += Configuration_server_Collection;
                    Thread threadServers
                    = new Thread(configuration.SQL_Server_Enumurator);
                    threadServers.Start();
                    break;
            }
        }

        //Список БД
        private void Configuration_Data_Base_Collection(DataTable obj)
        {
            Action action = () =>
            {
                foreach (DataRow r in obj.Rows)
                {
                    cb_bd.Items.Add(r[0]);
                }

            };
            Dispatcher.Invoke(action);
        }

        public string hhh;
        
        //Подключение к источнику данных
        private void test1_Click(object sender, RoutedEventArgs e)
        {
            Configuration_Class ttt = new Configuration_Class();
            switch (cb_bd.Text == "")
            {
                case true:
                    MessageBox.Show("Не выбрана нужная база данных!", "Bekary", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    cb_bd.Focus();
                    break;
                case false:
                    Configuration_Class configuration = new Configuration_Class();
                    configuration.SQL_Server_Configuration_Set(cb_Servers.Text, cb_bd.Text);
                    Visibility = Visibility.Hidden;
                    Authentication authentication = new Authentication();
                    authentication.Show();
                    break;
                    
            }
        }
    }
}
