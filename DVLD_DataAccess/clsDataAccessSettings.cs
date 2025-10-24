using System;
using System.Data.SQLite; // Make sure this is at the top
using System.Windows.Forms;  // Make sure this is at the top

public static class clsDataAccessSettings
{
    // Your existing connection string
    public static string ConnectionString = @"Data Source=|DataDirectory|\DVLD.db;Version=3;";

    // --- ADD THIS NEW FUNCTION ---
    public static bool TestConnection()
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                return true; // Connection successful!
            }
        }
        catch (Exception ex)
        {
            // If it fails, show a popup box with the *real* error!
            MessageBox.Show("Database Connection Error:\n\n" + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }
}