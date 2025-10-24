using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DVLD_Business;
using System.Net;
using Microsoft.Win32;

namespace WindowsFormsApp1
{
    public class clsGlobalUser
    {
        public static clsUser CurrentUser = clsUser.Find(19);

 

        //public static bool RememberLoginInfo(string username, string password)
        // {
        //     if(File.Exists("C:\\DVLD-Credentials\\LoginCredentials.txt"))
        //     {
        //         File.Delete("C:\\DVLD-Credentials\\LoginCredentials.txt");
        //     }

        //     string txtPath = "C:\\DVLD-Credentials";

        //     using (StreamWriter outputFile = new StreamWriter(Path.Combine(txtPath, "LoginCredentials.txt")))
        //     {
        //         outputFile.WriteLine(username + "#//#" + password);
        //     }

        //     return true;
        // }

        public static bool RememberLoginInfo(string username, string password)
        {
            string KeyPath = @"SOFTWARE\DVLD";
            try
            {
                using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(KeyPath))
                {
                    if (Key == null)
                    {
                        return false;
                    }
                    Key.SetValue("DVLD_UserName", username, RegistryValueKind.String);
                    Key.SetValue("DVLD_Password", password, RegistryValueKind.String);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //public static bool GetLoginCredentials(ref string UserName, ref string Password)
        //{
        //    if(!File.Exists("C:\\DVLD-Credentials\\LoginCredentials.txt"))
        //    {
        //        return false;
        //    }

        //    StreamReader sr = new StreamReader("C:\\DVLD-Credentials\\LoginCredentials.txt");

        //    string[] Info = sr.ReadLine().Split(new string[] {"#//#"}, StringSplitOptions.None);

        //    if(Info.Length > 0)
        //    {
        //       UserName = Info[0];
        //        Password = Info[1];
        //        sr.Close();
        //        return true;
        //    }

        //    sr.Close();
        //    return false;

        //}

        public static bool GetLoginCredentials(ref string UserName, ref string Password)
        {
            string KeyPath = @"SOFTWARE\DVLD";

            try
            {
                using (RegistryKey Key = Registry.CurrentUser.OpenSubKey(KeyPath))
                {
                    if (Key == null)
                    {
                        return false;
                    }

                    UserName = Key.GetValue("DVLD_UserName") as string;
                    Password = Key.GetValue("DVLD_Password") as string;
                }
            }
            catch (Exception)
            {

                return false ;
            }
            return true;
        }

    }
}
