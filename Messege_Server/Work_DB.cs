using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using LiteDB;
using System.Data.SQLite;

namespace Messege_Server
{

    class Work_DB
    {
        String fileName = @"Server_Messeger.db";
        //private SQLiteConnection sql_con;
        //private SQLiteCommand sql_cmd;
        //private SQLiteDataAdapter DB;
        //private SQLiteDataReader DR;


        public string print_users()
        {
            string UserS = "";


            SQLiteConnection db = new SQLiteConnection();
            try
            {
                db.ConnectionString = "Data Source=\"" + fileName + "\"";
                db.Open();
                try
                {
                    SQLiteCommand cmdSelect = db.CreateCommand();
                    cmdSelect.CommandText = "SELECT * FROM Users;";

                    SQLiteDataReader reader = cmdSelect.ExecuteReader();
                    StringBuilder sb = new StringBuilder();
                    //for (int colCtr = 0; colCtr < reader.FieldCount; ++colCtr)
                    //{
                    //    // Add Seperator (If After First Column)
                    //    if (colCtr > 0) sb.Append("|");

                    //    // Add Column Name
                    //    sb.Append(reader.GetName(colCtr));
                    //}
                    while (reader.Read())
                    {
                        for (int colCtr = 0; colCtr < reader.FieldCount; ++colCtr)
                        {
                            // Add Seperator (If After First Column)
                            if (colCtr > 0) sb.Append("█");

                            // Add Column Text
                            sb.Append(reader.GetValue(colCtr).ToString());
                        }
                        sb.AppendLine();
                    }

                    UserS = sb.ToString();
                }
                catch (Exception e)
                {

                }

                db.Close();
            }
            finally
            {
                // delete(IDisposable )db;
            }
            return UserS;
        }
        private void SQL_command(string Qvery, string Table, bool Reset_AvtoIncr)
        {
            SQLiteConnection db = new SQLiteConnection();
            try
            {
                db.ConnectionString = "Data Source=\"" + fileName + "\"";
                db.Open();
                try
                {
                    SQLiteCommand cmdInsertValue = db.CreateCommand();
                    if (Reset_AvtoIncr)
                    {
                        cmdInsertValue.CommandText = "UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='"+ Table + "';";
                        cmdInsertValue.ExecuteNonQuery();
                    }

                    cmdInsertValue.CommandText = Qvery;
                    cmdInsertValue.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }
                db.Close();
            }
            finally
            {

            }
        }

        public void add_users(string login, string password, string name, string tel, int status)
        {
            string Qvery = "INSERT INTO Users (login, password,name , tel ,status) VALUES('" + login + "','" + password + "','" + name + "','" + tel + "', " + status + ");";
            SQL_command(Qvery, "Users", false);
        }
        public void del_users(int id)
        {
            string Qvery = "DELETE FROM Users WHERE id =" + id + "; ";
            SQL_command(Qvery, "Users", true);
            
        }
        private string Coma(int R)
        {
            if (R > 0)
            {
                return " , ";
            }else
            {
                return "";
            }

        }

        public void update_users(int id, string login, string password, string name, string tel, int status)
        {
            int rows = 0;
            string Qvery = "UPDATE Users SET ";
            if (login.Length > 0)
            {
                Qvery += " login = '"+login+"' ";
                rows++;
            }
            if (password.Length > 0)
            {
                Qvery += Coma(rows);
                Qvery += " password = '" + password + "' ";
                rows++;
            }
            if (name.Length > 0)
            {
                Qvery += Coma(rows);
                Qvery += " name = '" + name + "' ";
                rows++;
            }
            if (tel.Length > 0)
            {
                Qvery += Coma(rows);
                Qvery += " tel = '" + tel + "' ";
                rows++;
            }
            if (status >= 0)
            {
                Qvery += Coma(rows);
                Qvery += " status = " + status + " ";
                rows++;
            }

            Qvery += " WHERE id =" + id + "; ";
            if(rows > 0)
            {
                SQL_command(Qvery, "Users", true);
            }
            
            
        }


        public class Users
        {
            public int id { get; set; }
            public string login { get; set; }
            public string password { get; set; }
            public string name { get; set; }
            public string tel { get; set; }
            public int status { get; set; }

        }

        // Open database (or create if not exits)


    }
}
