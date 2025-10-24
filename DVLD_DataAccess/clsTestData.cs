using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestData
    {
        static public bool GetTestInfo(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Tests WHERE TestID = @TestID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                    Notes = reader["Notes"].ToString();
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
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
        static public int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [Tests]
           ([TestAppointmentID]
           ,[TestResult]
           ,[Notes]
           ,[CreatedByUserID])
     VALUES
           (@TestAppointmentID
           ,@TestResult
           ,@Notes
           ,@CreatedByUserID);
            UPDATE TestAppointments
            SET IsLocked = 1
WHERE TestAppointmentID = @TestAppointmentID;
";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);

            if(Notes.Trim() == "")
                        command.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                        command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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
                        TestID = InsertedID;
                    }
                }
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            {
                connection.Close();
            }
            return TestID;
        }

        static public bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [Tests]
   SET [TestAppointmentID] = @TestAppointmentID
      ,[TestResult] = @TestResult
      ,[Notes] = @Notes
      ,[CreatedByUserID] = @CreatedByUserID
 WHERE TestID = @TestID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);

            if (Notes.Trim() == "")
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@TestID", TestID);

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

        static public bool DeleteTest(int TestID)
        {
            int rowsAffected = 0;
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Tests WHERE TestID = @TestID";
            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("TestID", TestID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            { connection.Close(); }
            return (rowsAffected > 0);
        }

        static public DataTable GetAllTests()
        {
            DataTable dt = new DataTable();

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Tests";

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
            finally { connection.Close(); }
            return dt;
        }

        static public int GetPassedTests(int LocalDrivingLicenseID)
        {
            int PassedTests = 0;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT        COUNT(TestTypeID) As PassedTests
FROM            Tests INNER JOIN
                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
                         WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseID AND TestResult = 1";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseID", LocalDrivingLicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int passTest))
                {
                    PassedTests = passTest;
                }
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally { connection.Close(); }
            return PassedTests;
        }
    }
}
