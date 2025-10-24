using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestTypesData
    {
        static public bool GetTestTypeByID(int TestTypeID, ref string TestTypeName,ref string TestTypeDescription ,ref decimal TestFees)
        {
            bool isFound = false;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;


                    TestTypeName = reader["TestTypeTitle"].ToString();
                    TestTypeDescription = reader["TestTypeDescription"].ToString();
                    TestFees = Convert.ToDecimal(reader["TestTypeFees"]);
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
        static public DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes";

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
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            {
                connection.Close();
            }

            return dt;
        }

        static public bool EditTestType(int TestTypeID, string TestTypeName,string TestTypeDescription ,decimal TestTypeFees)
        {
            int rowsAffected = 0;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE TestTypes
                            SET TestTypeTitle = @TestTypeTitle,
                            TestTypeFees = @TestTypeFees,
                            TestTypeDescription = @TestTypeDescription WHERE TestTypeID = @TestTypeID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeName);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);

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
    }
}
