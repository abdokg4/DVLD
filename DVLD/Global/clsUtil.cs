using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class clsUtil
    {
        public static string GenerateGuid()
        {
            Guid guid = Guid.NewGuid();

            return guid.ToString();
        }
    public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {

            // Check if the folder exists
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }

            return true;

        }

        public static string ReplaceFileNameWithGuid(string SourceFile)
        {
            string fileName = SourceFile;
            FileInfo fi = new FileInfo(fileName);

            string ext = fi.Extension;

            return GenerateGuid() + ext;
        }

        public static bool CopyPicturesToProjectImagesFile(ref string sourceFile)
        {
            string DestinationFolder = @"C:\DVLD-People-Images\";

            if(!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            

            string destinationFile = DestinationFolder + ReplaceFileNameWithGuid(sourceFile);
            try
            {
                File.Copy(sourceFile, destinationFile, true);

            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            sourceFile = destinationFile;
            return true;
        }

        public static string GetHash(string Password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        }
    }
