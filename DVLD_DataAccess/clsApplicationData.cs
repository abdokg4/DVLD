using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {
        static public bool GetApplicationInfoByID(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate,
            ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate,
            ref decimal PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
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

        static public int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
                                            byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees,
                                            int CreatedByUserID)
        {
            int ApplicationID = -1;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [Applications]
           ([ApplicantPersonID],[ApplicationDate],[ApplicationTypeID],[ApplicationStatus],[LastStatusDate],[PaidFees],[CreatedByUserID])
     VALUES
           (@ApplicantPersonID ,@ApplicationDate ,@ApplicationTypeID ,@ApplicationStatus ,@LastStatusDate ,@PaidFees ,@CreatedByUserID); 
            ";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
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
                        ApplicationID = InsertedID;
                    }
                }
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            {
                connection.Close();
            }
            return ApplicationID;
        }

        static public bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,int ApplicationTypeID,
                                           byte ApplicationStatus,DateTime LastStatusDate,decimal PaidFees,
                                           int CreatedByUserID)
        {
            int rowsAffected = 0;
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [Applications]
                   SET [ApplicantPersonID] = @ApplicantPersonID
                      ,[ApplicationDate] = @ApplicationDate
                      ,[ApplicationTypeID] = @ApplicationTypeID
                      ,[ApplicationStatus] = @ApplicationStatus
                      ,[LastStatusDate] = @LastStatusDate
                      ,[PaidFees] = @PaidFees
                      ,[CreatedByUserID] = @CreatedByUserID
                 WHERE ApplicationID = @ApplicationID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        static public bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";

            SQLiteCommand command = new SQLiteCommand(query,connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        static public bool IsApplicationExist(int ApplicationID)
        {
            bool isFound  = false;
            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 From Applications WHERE ApplicationID = @ApplicationID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

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

        static public int ApplicationExistWithSameClassID(int ApplicantPersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ApplicationID = -1;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT R1.ApplicationID FROM (SELECT        Applications.ApplicationID, LocalDrivingLicenseApplications.LicenseClassID, 
                         Applications.ApplicantPersonID, Applications.ApplicationStatus, Applications.ApplicationTypeID
FROM            Applications INNER JOIN
                         LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID) R1 WHERE R1.ApplicantPersonID = @ApplicantPersonID
						 AND LicenseClassID = @LicenseClassID AND ApplicationStatus = 1 And ApplicationTypeID = @ApplicationTypeID";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object obj = command.ExecuteScalar();

                if(obj != null && int.TryParse(obj.ToString(), out int AppID))
                {
                    ApplicationID = AppID;
                }
            }
            catch (Exception ex){clsLogger.Log(ex);}
            finally
            {
                connection.Close();
            }
            return ApplicationID;
        }

        static public bool SwitchApplicationStatus(int ApplicationID, byte ApplicationStatus)
        {
            int rowsAffected = 0;

            SQLiteConnection connection = new SQLiteConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE Applications SET ApplicationStatus = @ApplicationStatus, LastStatusDate = @LastStatusDate WHERE ApplicationID = @ApplicationID";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

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
