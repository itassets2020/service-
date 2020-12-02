using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Service_Center
{
    class DBProcedures
    {
        private SqlCommand command = new SqlCommand("", Configuration_Class.connection);
        private SqlCommand command1 = new SqlCommand("", Configuration_Class.connection);
        private SqlCommand command2 = new SqlCommand("", Configuration_Class.connection);
        private SqlCommand command3 = new SqlCommand("", Configuration_Class.connection);
        private SqlCommand command4 = new SqlCommand("", Configuration_Class.connection);

        private void commandConfig(string config)
        {
            command.CommandType =
            System.Data.CommandType.StoredProcedure;
            command.CommandText = "[dbo].[" + config + "]";
            command.Parameters.Clear();
        }


        //Механизм авторизации в приложении
        public Int32 Authorization(string Login, string Password)
        {
           Int32 ID_record = 0;
            string FIO;
            string Dolgnost;
            command.CommandType = System.Data.CommandType.Text;

            command.CommandText = "select Access_of_Role from [dbo].[Role] " +
            "inner join [dbo].[Staff] on [dbo].[Role].[ID_Role] = [dbo].[Staff].[Role_ID] where LoginS = '" + Login + "' and PasswordS = '" + Password + "'";

            command2.CommandText = "SELECT [ID_Staff]  FROM [dbo].[Staff] where [LoginS] = '" + Login + "' and [PasswordS] = '" + Password + "'";

            command1.CommandText = "SELECT Surname_of_Staff + ' ' + Name_of_Staff + ' ' + Middle_Name_of_Staff from [dbo].[Staff] where LoginS = '" + Login + "' and PasswordS = '" + Password + "'";

            command3.CommandText = "SELECT [Name_of_Position] FROM [dbo].[Staff] inner join [dbo].[Position] on[dbo].[Staff].[Position_ID]  = [dbo].[Position].[ID_Position] where [LoginS] = '" + Login + "' and [PasswordS] = '" + Password + "'";
            DBConnection.Log = Login;
            DBConnection.Pass = Password;


            Configuration_Class.connection.Open();

            FIO = command1.ExecuteScalar().ToString();
            Dolgnost = command3.ExecuteScalar().ToString();

            DBConnection.Dolgnost = Dolgnost;
            DBConnection.FIO = FIO;
            ID_record = Convert.ToInt32(command.ExecuteScalar().ToString());
            DBConnection.IDuser = ID_record;
            Configuration_Class.connection.Close();

            return (ID_record);
        }



        //Процедура регестрации в приложении 
        public void Staff_Regestration(string Surname_of_Staff, string Name_of_Staff, string Middle_Name_of_Staff, string LoginS, string PasswordS)
        {
            commandConfig("Staff_Regestration");
            command.Parameters.AddWithValue("@Name_of_Staff", Name_of_Staff);
            command.Parameters.AddWithValue("@Surname_of_Staff", Surname_of_Staff);
            command.Parameters.AddWithValue("@Middle_Name_of_Staff", Middle_Name_of_Staff);
            command.Parameters.AddWithValue("@LoginS", LoginS);
            command.Parameters.AddWithValue("@PasswordS", PasswordS);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура добавления данных в таблицу "Клиенты"
        public void Client_Insert(string Name_of_Client, string Surname_of_Client, string Middle_Name_Client, string Number_Phone_of_Client, string Email_of_Client)
        {
            commandConfig("Client_Insert");
            command.Parameters.AddWithValue("@Name_of_Client", Name_of_Client);
            command.Parameters.AddWithValue("@Surname_of_Client", Surname_of_Client);
            command.Parameters.AddWithValue("@Middle_Name_Client", Middle_Name_Client);
            command.Parameters.AddWithValue("@Number_Phone_of_Client", Number_Phone_of_Client);
            command.Parameters.AddWithValue("@Email_of_Client", Email_of_Client);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура изменения данных в таблице "Клиенты"
        public void Client_Update(Int32 ID_Client, string Name_of_Client, string Surname_of_Client, string Middle_Name_Client, string Number_Phone_of_Client, string Email_of_Client)
        {
            commandConfig("Client_Update");
            command.Parameters.AddWithValue("@ID_Client", ID_Client);
            command.Parameters.AddWithValue("@Name_of_Client", Name_of_Client);
            command.Parameters.AddWithValue("@Surname_of_Client", Surname_of_Client);
            command.Parameters.AddWithValue("@Middle_Name_Client", Middle_Name_Client);
            command.Parameters.AddWithValue("@Number_Phone_of_Client", Number_Phone_of_Client);
            command.Parameters.AddWithValue("@Email_of_Client", Email_of_Client);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура удаления данных из таблицы "Клиенты"
        public void Client_Delete(Int32 ID_Client)
        {
            commandConfig("Client_Delete");
            command.Parameters.AddWithValue("@ID_Client", ID_Client);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура добавления данных в таблицу "Техника"
        public void Technic_Insert(string Name_of_Technic, string Model_of_Technic, string Info_of_Error, Int32 Client_ID)
        {
            commandConfig("Technic_Insert");
            command.Parameters.AddWithValue("@Name_of_Technic", Name_of_Technic);
            command.Parameters.AddWithValue("@Model_of_Technic", Model_of_Technic);
            command.Parameters.AddWithValue("@Info_of_Error", Info_of_Error);
            command.Parameters.AddWithValue("@Client_ID", Client_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура изменения данных в таблице "Техника"
        public void Technic_Update(Int32 ID_Technic, string Name_of_Technic, string Model_of_Technic, string Info_of_Error, Int32 Client_ID)
        {
            commandConfig("Technic_Update");
            command.Parameters.AddWithValue("@ID_Technic", ID_Technic);
            command.Parameters.AddWithValue("@Name_of_Technic", Name_of_Technic);
            command.Parameters.AddWithValue("@Model_of_Technic", Model_of_Technic);
            command.Parameters.AddWithValue("@Info_of_Error", Info_of_Error);
            command.Parameters.AddWithValue("@Client_ID", Client_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура удаления данных из таблицы "Техника"
        public void Technic_Delete(Int32 ID_Technic)
        {
            commandConfig("Technic_Delete");
            command.Parameters.AddWithValue("@ID_Technic", ID_Technic);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура добавления данных в таблицу "Данные товарного чека"
        public void Check_Insert(Int32 Number_of_Check, Int32 Client_ID, Int32 Staff_ID, Int32 Service_ID)
        {
            commandConfig("Check_Insert");
            command.Parameters.AddWithValue("@Number_of_Check", Number_of_Check);
            command.Parameters.AddWithValue("@Client_ID", Client_ID);
            command.Parameters.AddWithValue("@Staff_ID", Staff_ID);
            command.Parameters.AddWithValue("@Service_ID", Service_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура изменения данных в таблице "Данные товарного чека"
        public void Check_Update(Int32 ID_Check, Int32 Number_of_Check, Int32 Client_ID, Int32 Staff_ID, Int32 Service_ID)
        {
            commandConfig("Check_Update");
            command.Parameters.AddWithValue("@ID_Check", ID_Check);
            command.Parameters.AddWithValue("@Number_of_Check", Number_of_Check);
            command.Parameters.AddWithValue("@Client_ID", Client_ID);
            command.Parameters.AddWithValue("@Staff_ID", Staff_ID);
            command.Parameters.AddWithValue("@Service_ID", Service_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура удаления данных из таблицы "Данные товарного чека"
        public void Check_Delete(Int32 ID_Check)
        {
            commandConfig("Check_Delete");
            command.Parameters.AddWithValue("@ID_Check", ID_Check);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура добавления данных в таблицу "Поставщики"
        public void Supplier_Insert(string Name_of_Supplier, string Surname_of_Supplier, string Middle_Name_of_Supplier, string Sires_Document, string Number_Document, Int32 Supply_Contract_ID)
        {
            commandConfig("Supplier_Insert");
            command.Parameters.AddWithValue("@Name_of_Supplier", Name_of_Supplier);
            command.Parameters.AddWithValue("@Surname_of_Supplier", Surname_of_Supplier);
            command.Parameters.AddWithValue("@Middle_Name_of_Supplier", Middle_Name_of_Supplier);
            command.Parameters.AddWithValue("@Sires_Document", Sires_Document);
            command.Parameters.AddWithValue("@Number_Document", Number_Document);
            command.Parameters.AddWithValue("@Supply_Contract_ID", Supply_Contract_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура изменения данных в таблице "Поставщики"
        public void Supplier_Update(Int32 ID_Supplier, string Name_of_Supplier, string Surname_of_Supplier, string Middle_Name_of_Supplier, string Sires_Document, string Number_Document, Int32 Supply_Contract_ID)
        {
            commandConfig("Supplier_Update");
            command.Parameters.AddWithValue("@ID_Supplier", ID_Supplier);
            command.Parameters.AddWithValue("@Name_of_Supplier", Name_of_Supplier);
            command.Parameters.AddWithValue("@Surname_of_Supplier", Surname_of_Supplier);
            command.Parameters.AddWithValue("@Middle_Name_of_Supplier", Middle_Name_of_Supplier);
            command.Parameters.AddWithValue("@Sires_Document", Sires_Document);
            command.Parameters.AddWithValue("@Number_Document", Number_Document);
            command.Parameters.AddWithValue("@Supply_Contract_ID", Supply_Contract_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура удаления данных из таблицы "Поставщики"
        public void Supplier_Delete(Int32 ID_Supplier)
        {
            commandConfig("Supplier_Delete");
            command.Parameters.AddWithValue("@ID_Supplier", ID_Supplier);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура добавления данных в таблицу "Договоры поставки"
        public void Supply_Contract_Insert(string Name_of_Contract, decimal Price_of_Contract, string Name_of_Component, Int32 Technic_ID)
        {
            commandConfig("Supply_Contract_Insert");
            command.Parameters.AddWithValue("@Name_of_Contract", Name_of_Contract);
            command.Parameters.AddWithValue("@Price_of_Contract", Price_of_Contract);
            command.Parameters.AddWithValue("@Name_of_Component", Name_of_Component);
            command.Parameters.AddWithValue("@Technic_ID", Technic_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура изменения данных в таблице "Договоры поставки"
        public void Supply_Contract_Update(Int32 ID_Supply_Contract, string Name_of_Contract, decimal Price_of_Contract, string Name_of_Component, Int32 Technic_ID)
        {
            commandConfig("Supply_Contract_Update");
            command.Parameters.AddWithValue("@ID_Supply_Contract", ID_Supply_Contract);
            command.Parameters.AddWithValue("@Name_of_Contract", Name_of_Contract);
            command.Parameters.AddWithValue("@Price_of_Contract", Price_of_Contract);
            command.Parameters.AddWithValue("@Name_of_Component", Name_of_Component);
            command.Parameters.AddWithValue("@Technic_ID", Technic_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура удаления данных из таблицы "Договоры поставки"
        public void Supply_Contract_Delete(Int32 ID_Supply_Contract)
        {
            commandConfig("Supply_Contract_Delete");
            command.Parameters.AddWithValue("@ID_Supply_Contract", ID_Supply_Contract);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура добавления данных в таблицу "Услуги"
        public void Service_Insert(string Name_of_Service, decimal Price_of_Service, Int32 Technic_ID)
        {
            commandConfig("Service_Insert");
            command.Parameters.AddWithValue("@Name_of_Service", Name_of_Service);
            command.Parameters.AddWithValue("@Price_of_Service", Price_of_Service);
            command.Parameters.AddWithValue("@Technic_ID", Technic_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура изменения данных в таблице "Услуги"
        public void Service_Update(Int32 ID_Service, string Name_of_Service, decimal Price_of_Service, Int32 Technic_ID)
        {
            commandConfig("Service_Update");
            command.Parameters.AddWithValue("@ID_Service", ID_Service);
            command.Parameters.AddWithValue("@Name_of_Service", Name_of_Service);
            command.Parameters.AddWithValue("@Price_of_Service", Price_of_Service);
            command.Parameters.AddWithValue("@Technic_ID", Technic_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура удаления данных из таблицы "Услуги"
        public void Service_Delete(Int32 ID_Service)
        {
            commandConfig("Service_Delete");
            command.Parameters.AddWithValue("@ID_Service", ID_Service);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура добавления данных в таблицу "Сотрудники"
        public void Staff_Insert(string Surname_of_Staff, string Name_of_Staff, string Middle_Name_of_Staff, string Date_of_Birth_Staff, string Sires_Document_Staff,
            string Number_Document_Staff, string LoginS, string PasswordS, Int32 Position_ID, Int32 Role_ID)
        {
            commandConfig("Staff_Insert");
            command.Parameters.AddWithValue("@Surname_of_Staff", Surname_of_Staff);
            command.Parameters.AddWithValue("@Name_of_Staff", Name_of_Staff);
            command.Parameters.AddWithValue("@Middle_Name_of_Staff", Middle_Name_of_Staff);
            command.Parameters.AddWithValue("@Date_of_Birth_Staff", Date_of_Birth_Staff);
            command.Parameters.AddWithValue("@Sires_Document_Staff", Sires_Document_Staff);
            command.Parameters.AddWithValue("@Number_Document_Staff", Number_Document_Staff);
            command.Parameters.AddWithValue("@LoginS", LoginS);
            command.Parameters.AddWithValue("@PasswordS", PasswordS);
            command.Parameters.AddWithValue("@Position_ID", Position_ID);
            command.Parameters.AddWithValue("@Role_ID", Role_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура изменения данных в таблице "Сотрудники"
        public void Staff_Update(Int32 ID_Staff, string Surname_of_Staff, string Name_of_Staff, string Middle_Name_of_Staff, string Date_of_Birth_Staff, string Sires_Document_Staff,
            string Number_Document_Staff, string LoginS, string PasswordS, Int32 Position_ID, Int32 Role_ID)
        {
            commandConfig("Staff_Update");
            command.Parameters.AddWithValue("@ID_Staff", ID_Staff);
            command.Parameters.AddWithValue("@Surname_of_Staff", Surname_of_Staff);
            command.Parameters.AddWithValue("@Name_of_Staff", Name_of_Staff);
            command.Parameters.AddWithValue("@Middle_Name_of_Staff", Middle_Name_of_Staff);
            command.Parameters.AddWithValue("@Date_of_Birth_Staff", Date_of_Birth_Staff);
            command.Parameters.AddWithValue("@Sires_Document_Staff", Sires_Document_Staff);
            command.Parameters.AddWithValue("@Number_Document_Staff", Number_Document_Staff);
            command.Parameters.AddWithValue("@LoginS", LoginS);
            command.Parameters.AddWithValue("@PasswordS", PasswordS);
            command.Parameters.AddWithValue("@Position_ID", Position_ID);
            command.Parameters.AddWithValue("@Role_ID", Role_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура удаления данных из таблицы "Сотрудники"
        public void Staff_Delete(Int32 ID_Staff)
        {
            commandConfig("Staff_Delete");
            command.Parameters.AddWithValue("@ID_Staff", ID_Staff);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура добавления данных в таблицу "Должности"
        public void Position_Insert(string Name_of_Position, decimal Salaty_of_Position)
        {
            commandConfig("Position_Insert");
            command.Parameters.AddWithValue("@Name_of_Position", Name_of_Position);
            command.Parameters.AddWithValue("@Salaty_of_Position", Salaty_of_Position);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура изменения данных в таблице "Должности"
        public void Position_Update(Int32 ID_Position, string Name_of_Position, decimal Salaty_of_Position)
        {
            commandConfig("Position_Update");
            command.Parameters.AddWithValue("@ID_Position", ID_Position);
            command.Parameters.AddWithValue("@Name_of_Position", Name_of_Position);
            command.Parameters.AddWithValue("@Salaty_of_Position", Salaty_of_Position);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура удаления данных из таблицы "Должности"
        public void Position_Delete(Int32 ID_Position)
        {
            commandConfig("Position_Delete");
            command.Parameters.AddWithValue("@ID_Position", ID_Position);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура добавления данных в таблицу "Авторизация"
        public void Authorization_Insert(string Login1, string Password1, Int32 Role_ID)
        {
            commandConfig("Authorization_Insert");
            command.Parameters.AddWithValue("@Login1", Login1);
            command.Parameters.AddWithValue("@Password1", Password1);
            command.Parameters.AddWithValue("@Role_ID", Role_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура изменения данных в таблице "Авторизация"
        public void Authorization_Update(Int32 ID_Authorization, string Login1, string Password1, Int32 Role_ID)
        {
            commandConfig("Authorization_Update");
            command.Parameters.AddWithValue("@ID_Authorization", ID_Authorization);
            command.Parameters.AddWithValue("@Login1", Login1);
            command.Parameters.AddWithValue("@Password1", Password1);
            command.Parameters.AddWithValue("@Role_ID", Role_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура удаления данных из таблицы "Авторизация"
        public void Authorization_Delete(Int32 ID_Authorization)
        {
            commandConfig("Authorization_Delete");
            command.Parameters.AddWithValue("@ID_Authorization", ID_Authorization);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура добавления данных в таблицу "Роли"
        public void Role_Insert(string Name_of_Role, Int32 Access_of_Role)
        {
            commandConfig("Role_Insert");
            command.Parameters.AddWithValue("@Name_of_Role", Name_of_Role);
            command.Parameters.AddWithValue("@Access_of_Role", Access_of_Role);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура изменения данных в таблице "Роли"
        public void Role_Update(Int32 ID_Role, string Name_of_Role, Int32 Access_of_Role)
        {
            commandConfig("Role_Update");
            command.Parameters.AddWithValue("@ID_Role", ID_Role);
            command.Parameters.AddWithValue("@Name_of_Role", Name_of_Role);
            command.Parameters.AddWithValue("@Access_of_Role", Access_of_Role);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура удаления данных из таблицы "Роли"
        public void Role_Delete(Int32 ID_Role)
        {
            commandConfig("Role_Delete");
            command.Parameters.AddWithValue("@ID_Role", ID_Role);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура добавления данных в таблицу "Данные дополнительного запроса"
        public void Request_Insert(Int32 Supply_Contract_ID, Int32 Supplier_ID, Int32 Technic_ID)
        {
            commandConfig("Request_Insert");
            command.Parameters.AddWithValue("@Supply_Contract_ID", Supply_Contract_ID);
            command.Parameters.AddWithValue("@Supplier_ID", Supplier_ID);
            command.Parameters.AddWithValue("@Technic_ID", Technic_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура изменения данных в таблице "Данные дополнительного запроса"
        public void Request_Update(Int32 ID_Request, Int32 Supply_Contract_ID, Int32 Supplier_ID, Int32 Technic_ID)
        {
            commandConfig("Request_Update");
            command.Parameters.AddWithValue("@ID_Request", ID_Request);
            command.Parameters.AddWithValue("@Supply_Contract_ID", Supply_Contract_ID);
            command.Parameters.AddWithValue("@Supplier_ID", Supplier_ID);
            command.Parameters.AddWithValue("@Technic_ID", Technic_ID);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Процедура удаления данных из таблицы "Данные дополнительного запроса"
        public void Request_Delete(Int32 ID_Request)
        {
            commandConfig("Request_Delete");
            command.Parameters.AddWithValue("@ID_Request", ID_Request);
            Configuration_Class.connection.Open();
            command.ExecuteNonQuery();
            Configuration_Class.connection.Close();
        }

        //Механизм авторизации в приложении
        public Int32 Authorization2(string Login, string Password, string Position)
        {
            DBConnection connection = new DBConnection();
            Int32 ID_Staff = 0;
            string FIO;
            string Dolgnost;
            command.CommandType = System.Data.CommandType.Text;

            command1.CommandText = "SELECT Surname_of_Staff + ' ' + Name_of_Staff + ' ' + Middle_Name_of_Staff from [dbo].[Staff] where LoginS = '" + Login + "' and PasswordS = '" + Password + "'";

            command3.CommandText = "SELECT [Name_of_Position] FROM [dbo].[Staff] inner join [dbo].[Position] on[dbo].[Staff].[Position_ID]  = [dbo].[Position].[ID_Position] where [LoginS] = '" + Login + "' and [PasswordS] = '" + Password + "'";
            command2.CommandText = "SELECT [ID_Staff]  FROM [dbo].[Staff] where [LoginS] = '" + Login + "' and [PasswordS] = '" + Password + "'";
            Configuration_Class.connection.Open();

            FIO = command1.ExecuteScalar().ToString();
            Dolgnost = command3.ExecuteScalar().ToString(); ;
            ID_Staff = Convert.ToInt32(command2.ExecuteScalar().ToString());
            DBConnection.ID_Staff = ID_Staff;
            DBConnection.Dolgnost = Dolgnost;
            DBConnection.FIO = FIO;

            Configuration_Class.connection.Close();

            return (ID_Staff);
        }
    }
}
