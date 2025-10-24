using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsUserData
    {
        public static bool GetUserInfoByID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    UserName = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetUserInfoByPersonID(ref int UserID, int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE PersonID = @PersonID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    UserID = Convert.ToInt32(reader["UserID"]);
                    UserName = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetUserInfoByUserNameAndPassword(ref int UserID, ref int PersonID, string UserName, string Password, ref bool IsActive)
        {
            bool isFound = false;
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password;";

            SQLiteCommand command = new SQLiteCommand(query,connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;


                    UserID = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            { connection.Close(); }

            return isFound;
        }

        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [Users] ([PersonID],[UserName],[Password],[IsActive])
     VALUES
           (@PersonID ,@UserName ,@Password ,@IsActive);
            ";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            

            try
            {
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    SQLiteCommand cmdGetID = new SQLiteCommand("SELECT last_insert_rowid()", connection);
                    object result = cmdGetID.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        UserID = InsertedID;
                    }
                }
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            {
                connection.Close();
            }

            return UserID;
        }

        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            int rowsAffected = 0;
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [Users]
                       SET [PersonID] = @PersonID
                          ,[UserName] = @UserName
                          ,[Password] = @Password
                          ,[IsActive] = @IsActive
                          
                     WHERE UserID = @UserID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);

           
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT
                     Users.UserID,
                    Users.PersonID,
                    People.FirstName || ' ' || People.SecondName || ' ' || COALESCE(People.ThirdName, '') || ' ' || People.LastName AS FullName,
                    Users.UserName,
                    Users.IsActive
                        FROM
                        Users
                        INNER JOIN
                    People ON Users.PersonID = People.PersonID;";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            try
            {
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex){clsLogger.Log(ex);
                throw;
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Users WHERE UserID = @UserID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex){clsLogger.Log(ex);
                throw;            
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool IsUserExist(int UserID)
        {
            bool isFound = false;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found = 1 FROM Users WHERE UserID = @UserID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            bool isFound = false;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found = 1 FROM Users WHERE PersonID = @PersonID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool IsUserExistByUserName(string UserName)
        {
            bool isFound = false;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found = 1 FROM Users WHERE UserName = @UserName";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            {
                connection.Close();
            }
            return isFound;
        }
    }
}
