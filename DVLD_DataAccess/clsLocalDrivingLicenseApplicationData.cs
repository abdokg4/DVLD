using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLocalDrivingLicenseApplicationData
    {
        static public bool GetLocalDrivingLicenseApplicationByID(int LdlID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LdlID);

            try
            {
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
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

        static public bool GetLocalDrivingLicenseApplicationByApplicationID(ref int LdlID, int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    LdlID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationsID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        static public int AddNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int lblID = -1;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO LocalDrivingLicenseApplications VALUES (@ApplicationID, @LicenseClassID);
                            ";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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
                        lblID = InsertedID;
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return lblID;
        }

        static public bool UpdateLocalDrivingLicenseApplication(int LblID, int ApplicationID, int LicenseClassID)
        {
            int rowsAffected = 0;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE LocalDrivingLicenseApplications SET ApplicationID = @ApplicationID,
                             LicenseClassID = @LicenseClassID WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LblID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        static public bool DeleteLocalDrivingLicenseApplication(int LblID)
        {
            int rowsAffected = 0;
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LblID);

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

        static public bool DidPassTestType(int LblID, int TestTypeID)
        {
            bool isPass = false;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT top 1 TestResult FROM Tests  inner join TestAppointments ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestAppointments.TestTypeID = @TestTypeID ORDER BY TestAppointments.TestAppointmentID desc;";

        SQLiteCommand command = new SQLiteCommand(query,connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LblID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

               if(result != null && bool.TryParse(result.ToString(), out bool Pass))
                {
                    isPass = Pass;
                }
                
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally

            { connection.Close(); }
            return isPass;

            
        }

        static public bool DoesHaveActiveTestWithTestType(int LblID, int TestTypeID)
        {
            bool isFound = false;
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT FOUND = 1 FROM TestAppointments inner join LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
WHERE (TestAppointments.LocalDrivingLicenseApplicationID = @LblID AND TestAppointments.TestTypeID = @TestTypeID) AND TestAppointments.IsLocked = 0 ORDER BY TestAppointments.TestAppointmentID desc;";

            SQLiteCommand command = new SQLiteCommand(query,connection);

            command.Parameters.AddWithValue("@LblID", LblID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally { connection.Close(); } 
            return isFound;
        }

        static public int TotalTrialsAmountPerTest(int LblID, int TestTypeID)
        {
            int Trials = 0;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT COUNT(TestID) FROM TestAppointments INNER JOIN Tests ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
							WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestTypeID = @TestTypeID";

            SQLiteCommand command = new SQLiteCommand(query,connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LblID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int Num))
                {
                    Trials = Num;
                }
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally { connection.Close(); }
            return Trials;
        }

        

        static public DataTable GetAllLdlApplications()
        {
            DataTable dt = new DataTable();

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications_View Order By ApplicationDate desc";

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
            { connection.Close(); }
            return dt;
        }
    }
}
