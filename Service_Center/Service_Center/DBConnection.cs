using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Asn1.Nist;

namespace Service_Center
{

    class DBConnection
    {
        
        //Переменные для авторизации
        public static Int32 IDrecord, IDuser, ID_Staff, nomer;
        public static string FIO, Log, Pass, Dolgnost, FolderZ, Name_File, Name_Grid, NoName;
      
        //Прослушивание сервера
        public SqlDependency Dependency = new SqlDependency();

        public static Authentication qwe = new Authentication();

        public DataTable dtClient   = new DataTable("Client");
        public DataTable dtTechnic  = new DataTable("Technic");
        public DataTable dtSupply_Contract = new DataTable("Supply_Contract");
        public DataTable dtService  = new DataTable("Service"); 
        public DataTable dtPosition = new DataTable("Position");
        public DataTable dtSupplier = new DataTable("Supplier");
        public DataTable dtCheck    = new DataTable("Check");
        public DataTable dtStaff    = new DataTable("Staff");
        public DataTable dtRole     = new DataTable("Role");
        public DataTable dtAuthorization = new DataTable("Authorization");
        public DataTable dtRequest = new DataTable("Request");
        public DataTable dtAU1 = new DataTable("AU1");


        //Для ComboBox
        public DataTable dtqrClient_for_cb = new DataTable("qrClient_for_cb");
        public DataTable dt_Technic_for_cb = new DataTable("Technic_for_cb");
        public DataTable dt_Supply_Contract_for_cb = new DataTable("Supply_Contract_for_cb");
        public DataTable dt_Technic_for_cb_2 = new DataTable("Technic_for_cb_2");
        public DataTable dt_Supplier_for_cb = new DataTable("Supplier_for_cb");
        public DataTable dt_Role_for_cb = new DataTable("Role_for_cb");
        public DataTable dt_Role_for_cb2 = new DataTable("Role_for_cb2");
        public DataTable dt_Position_for_cb = new DataTable("Position_for_cb");
        public DataTable dt_Staff_for_cb = new DataTable("Staff_for_cb");
        public DataTable dt_Service_for_cb = new DataTable("Service_for_cb");

        //Выборка данных из SQL
        public static string

        //Для Combo_Box
        qrClient_for_cb = "SELECT [ID_Client], [Surname_of_Client] + ' ' + [Name_of_Client] + ' ' + [Middle_Name_Client] as Client_Info FROM [dbo].[Client]",
        qr_Supplier_for_cb = "SELECT [ID_Supplier], [Surname_of_Supplier] + ' ' + [Name_of_Supplier] + ' ' + [Middle_Name_of_Supplier] as Supplier_Info FROM [dbo].[Supplier]",
        qrRole_for_cb1 = "SELECT [ID_Role], [Access_of_Role] FROM [dbo].[Role]",
        qrRole_for_cb2 = "SELECT [ID_Role], [Name_of_Role] as Role_Info FROM [dbo].[Role]",
        qrTechnic_for_cb = "select [ID_Technic], [Name_of_Technic] + ' ' + [Model_of_Technic] as Technic_Info FROM [dbo].[Technic]",
        qrPosition_for_cb = "SELECT [ID_Position], [Name_of_Position] as Position_Info FROM [dbo].[Position]",
        qrStaff_for_cb = "SELECT [ID_Staff], [Surname_of_Staff] + ' ' + [Name_of_Staff] + ' ' + [Middle_Name_of_Staff] as Staff_Info FROM [dbo].[Staff]",
        qrService_for_cb = "SELECT [ID_Service], [Name_Of_Service] as Service_Info FROM [dbo].[Service]",

        //Таблицы без FK
        qrClient = "SELECT [ID_Client], [Surname_of_Client], [Name_of_Client], [Middle_Name_Client], [Number_Phone_of_Client], [Email_of_Client] FROM [dbo].[Client]",

        qrPosition = "SELECT [ID_Position], [Name_of_Position], [Salaty_of_Position] FROM [dbo].[Position]",
        qrRole = "SELECT [ID_Role], [Name_of_Role], [Access_of_Role] FROM [dbo].[Role]",
        qrAU1 = "SELECT [ID_AU], [Login_0], [Password_0], [Dostup] FROM [dbo].[AU1]",

         //Таблицы с FK
         qrTechnic = "SELECT [ID_Technic], [Name_of_Technic], [Model_of_Technic], [Info_of_Error], [Surname_of_Client], [Name_of_Client], [Middle_Name_Client] FROM [dbo].[Technic]" +
         " inner join [dbo].[Client]  on [dbo].[Technic].[Client_ID] = [dbo].[Client].[ID_Client]",
         qrCheck = "SELECT [ID_Check], [Number_of_Check], [Surname_of_Client], [Name_of_Client], [Middle_Name_Client], [Surname_of_Staff], [Name_of_Service]  FROM [dbo].[Check] " +
         " inner join [dbo].[Client]  on [dbo].[Check].[Client_ID]  = [dbo].[Client].[ID_Client]" +
         " inner join [dbo].[Service] on [dbo].[Check].[Service_ID] = [dbo].[Service].[ID_Service] " +
         " inner join [dbo].[Staff]   on [dbo].[Check].[Staff_ID]   = [dbo].[Staff].[ID_Staff]",
         qrRequest = "SELECT [ID_Request], [Name_of_Contract], [Surname_of_Supplier], [Name_of_Supplier], [Middle_Name_of_Supplier], [Name_of_Technic]  FROM [dbo].[Request] " +
         " inner join [dbo].[Supply_Contract]  on [dbo].[Request].[Supply_Contract_ID]  = [dbo].[Supply_Contract].[ID_Supply_Contract]" +
         " inner join [dbo].[Supplier] on [dbo].[Request].[Supplier_ID] = [dbo].[Supplier].[ID_Supplier] " +
         " inner join [dbo].[Technic]   on [dbo].[Request].[Technic_ID]   = [dbo].[Technic].[ID_Technic]",
         qrSupplier = "SELECT [ID_Supplier], [Surname_of_Supplier], [Name_of_Supplier], [Middle_Name_of_Supplier], [Sires_Document], [Number_Document], [Name_of_Contract], [Price_of_Contract], [Name_of_Component] FROM [dbo].[Supplier] " +
         " inner join [dbo].[Supply_Contract] on [dbo].[Supplier].[Supply_Contract_ID] = [dbo].[Supply_Contract].[ID_Supply_Contract]",
         qrSupply_Contract = "SELECT [ID_Supply_Contract], [Name_of_Contract], [Price_of_Contract], [Name_of_Component], [Name_of_Technic] FROM [dbo].[Supply_Contract]" +
         " inner join [dbo].[Technic]  on [dbo].[Supply_Contract].[Technic_ID] = [dbo].[Technic].[ID_Technic]",
         qrService = "SELECT [ID_Service], [Name_of_Service], [Price_of_Service], [Name_of_Technic], [Model_of_Technic] FROM [dbo].[Service]" +
         " inner join [dbo].[Technic]  on [dbo].[Service].[Technic_ID] = [dbo].[Technic].[ID_Technic]",
         qrStaff = "SELECT [ID_Staff], [Surname_of_Staff], [Name_of_Staff], [Middle_Name_of_Staff], [Date_of_Birth_Staff], [Sires_Document_Staff], [Number_Document_Staff], [LoginS], [PasswordS], [Name_of_Position], [Salaty_of_Position] FROM [dbo].[Staff]" +
         " inner join [dbo].[Position] on [dbo].[Staff].[Position_ID] = [dbo].[Position].[ID_Position]",
         qrAuthorization = "SELECT [ID_Authorization], [Login1], [Password1], [Name_of_Role], [Access_of_Role] FROM [dbo].[Authorization]" +
         " inner join [dbo].[Role] on [dbo].[Authorization].[Role_ID] = [dbo].[Role].[ID_Role]";

        private SqlCommand command = new SqlCommand("", Configuration_Class.connection);
       
        private void dtFill(DataTable table, string query)
        {
            command.CommandText = query;

            //Технология "Real Time"
            command.Notification = null;
            Dependency.AddCommandDependency(command);
            SqlDependency.Start(Configuration_Class.connection.ConnectionString);

            Configuration_Class.connection.Open();
            table.Load(command.ExecuteReader());
            Configuration_Class.connection.Close();
        }

        //Таблица "Клиенты
        public void ClientFill()
        {
            dtFill(dtClient, qrClient);
        }

        //Для cb "Клиенты"
        public void qrClient_for_cbFill()
        {
            dtFill(dtqrClient_for_cb, qrClient_for_cb);
        }

        //Для cb "Техника"
        public void qrTechnic_for_cbFill_2()
        {
            dtFill(dt_Technic_for_cb_2, qrTechnic_for_cb);
        }

        //Для cb "Техника"
        public void qrTechnic_for_cbFill()
        {
            dtFill(dt_Technic_for_cb, qrTechnic);
        }

        //Для cb "Договоры поставки"
        public void qrSupply_Contract_for_cbFill()
        {
            dtFill(dt_Supply_Contract_for_cb, qrSupply_Contract);
        }

        //Для cb "Роли"
        public void qrRole_for_cbFill()
        {
            dtFill(dt_Role_for_cb, qrRole_for_cb1);
        }

        //Для cb "Роли2"
        public void qrRole_for_cbFill2()
        {
            dtFill(dt_Role_for_cb2, qrRole_for_cb2);
        }

        //Для cb "Должности"
        public void qrPosition_for_cbFill()
        {
            dtFill(dt_Position_for_cb, qrPosition_for_cb);
        }

        //Для cb "Поставщики"
        public void qrSupplier_for_cbFill()
        {
            dtFill(dt_Supplier_for_cb, qr_Supplier_for_cb);
        }

        //Для cb "Услуга"
        public void qrService_for_cbFill()
        {
            dtFill(dt_Service_for_cb, qrService_for_cb);
        }

        //Для cb "Сотрудники"
        public void qrStaff_for_cbFill()
        {
            dtFill(dt_Staff_for_cb, qrStaff_for_cb);
        }

        //Таблица "Данные техники"
        public void TechnicFill()
        {
            dtFill(dtTechnic, qrTechnic);
        }

        //Таблица "Договоры поставки"
        public void Supply_ContractFill()
        {
            dtFill(dtSupply_Contract, qrSupply_Contract);
        }

        //Таблица "Услуги
        public void ServiceFill()
        {
            dtFill(dtService, qrService);
        }

        //Таблица "Должности"
        public void PositionFill()
        {
            dtFill(dtPosition, qrPosition);
        }

        // Таблица "Поставщики"
        public void SupplierFill()
        {
            dtFill(dtSupplier, qrSupplier);
        }



        // Таблица "Товарный чек"
        public void CheckFill()
        {
            dtFill(dtCheck, qrCheck);
        }

        // Таблица "Сотрудники"
        public void StaffFill()
        {
            dtFill(dtStaff, qrStaff);
        }

        // Таблица "Роли"
        public void RoleFill()
        {
            dtFill(dtRole, qrRole);
        }

        // Таблица "Авторизация"
        public void AuthorizationFill()
        {
            dtFill(dtAuthorization, qrAuthorization);
        }

        // Таблица "Данные дополнительного запроса"
        public void RequestFill()
        {
            dtFill(dtRequest, qrRequest);
        }

    }
}
