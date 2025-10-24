using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsLicense
    {
        public enum enMode {AddNew = 1, Update = 2}
        enMode Mode = enMode.AddNew;
        public enum enIssueReasons { FirstTime = 1, Renew = 2, ReplaceLost = 3, ReplaceDamaged = 4 }
        
        public int LicenseID { get; set; }

        public int DriverID { get; set; }

        public clsDrivers DriverInfo { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReasons IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public clsDetainedLicenses DetainedInfo { get; set; }

        public string IssueReasonText
        {
            get
            {
                return _GetIssueReasonText();
            }
        }

        public bool IsDetained
        { get
            {
                return IsLicenseDetained();
            }
        }
        public clsLicense()
        {
            Mode = enMode.AddNew;

            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;
            this.LicenseClassInfo = new clsLicenseClass();
            this.DriverID = -1;
            this.DriverInfo = new clsDrivers();
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.DetainedInfo = new clsDetainedLicenses();
            this.Notes = string.Empty;
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReasons.FirstTime;
            this.CreatedByUserID = -1;
        }

        private clsLicense(int licenseID, int applicationID,int DriverID ,int licenseClassID, DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees, bool isActive, enIssueReasons issueReason, int createdByUserID)
        {
            Mode = enMode.Update;
            
            this.LicenseID = licenseID;
            this.ApplicationID = applicationID;
            this.LicenseClassID = licenseClassID;
            this.DriverID = DriverID;
            this.DriverInfo = clsDrivers.FindDriverByDriverID(DriverID);
            this.LicenseClassInfo = clsLicenseClass.FindByID(LicenseClassID);
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.Notes = notes;
            this.PaidFees = paidFees;
            this.IsActive = isActive;
            this.IssueReason = issueReason;
            this.CreatedByUserID = createdByUserID;
            this.DetainedInfo = clsDetainedLicenses.FindByLicenseID(LicenseID);
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate, this.ExpirationDate,
                this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);

            return(this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate, this.ExpirationDate,
                this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        static public clsLicense FindLicenseByLicenseID(int LicenseID)
        {
            int ApplicationID = -1;
            int LicenseClassID = -1;
            int DriverID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            String Notes = string.Empty;
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 1;
            int CreatedByUserID = -1;

            if(clsLicenseData.GetLicenseInfoByLicenseID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID,
                 IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReasons)IssueReason, CreatedByUserID);
            }
            else
                return null;
        }

        static public int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClass)
        {
            return clsLicenseData.GetActiveLicenseIDbyPersonID(PersonID, LicenseClass);
        }
        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        static public DataTable GetLocalDrivingLicensesByDriverID(int DriverID)
        {
            return clsLicenseData.GetLocalDrivingLicensesByDriverID(DriverID);
        }
        private string _GetIssueReasonText()
        {
            switch (this.IssueReason)
            {
                case enIssueReasons.FirstTime:
                    return "First Time";
                case enIssueReasons.Renew:
                    return "Renew";
                case enIssueReasons.ReplaceLost:
                    return "Replace Lost";
                case enIssueReasons.ReplaceDamaged:
                    return "Replace Damaged";
                default:
                    return "";
            }
        }

        public clsLicense RenewDrivingLicense(string Notes, int CreatedByUserID)
        {
            clsApplication application = new clsApplication();

            application.ApplicantPersonID = this.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationTypeID;
            application.LastStatusDate = DateTime.Now;
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees;
            application.CreatedByUserID = CreatedByUserID;

            if(!application.Save())
            {
                return null;
            }

            clsLicense License = new clsLicense();
            License.DriverID = this.DriverID;
            License.ApplicationID = application.ApplicationID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.PaidFees;
            License.IsActive = true;
            License.IssueReason = enIssueReasons.Renew;
            License.CreatedByUserID = CreatedByUserID;

            if(!License.Save()) 
            { return null; }

            DeactivateCurrentLicense();

            return License;

        }

        public clsLicense Replace(enIssueReasons issueReasons, int CreatedByUserID)
        {
            //clsApplication.enApplicationType AppType = (issueReasons == enIssueReasons.ReplaceLost) ? clsApplication.enApplicationType.ReplaceLostDrivingLicense : clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;

            clsApplication application = new clsApplication();
            application.ApplicantPersonID = this.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = clsApplicationTypes.Find((int)issueReasons).ApplicationTypeID;
            application.LastStatusDate = DateTime.Now;
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.PaidFees = clsApplicationTypes.Find((int)issueReasons).ApplicationFees;
            application.CreatedByUserID = CreatedByUserID;

            if (!application.Save())
            {
                return null;
            }

            clsLicense License = new clsLicense();
            License.DriverID = this.DriverID;
            License.ApplicationID = application.ApplicationID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = this.ExpirationDate;
            License.Notes = this.Notes;
            License.PaidFees = 0;
            License.IsActive = true;
            License.IssueReason = issueReasons;
            License.CreatedByUserID = CreatedByUserID;

            if (!License.Save())
            { return null; }

            DeactivateCurrentLicense();

            return License;
        }
        static public bool DeactivateLicense(int LicneseID)
        {
            return clsLicenseData.DeactivateLicense(LicneseID);
        }

        public bool DeactivateCurrentLicense()
        {
            return clsLicenseData.DeactivateLicense(this.LicenseID);
        }

        public bool IsLicenseExpired()
        {
            return (DateTime.Compare(DateTime.Now, this.ExpirationDate) > 0);
        }

        public int Detain(decimal FineFees, int CreatedByUserID)
        {
            clsDetainedLicenses DetainLicense = new clsDetainedLicenses();
            DetainLicense.LicensesID = this.LicenseID;
            DetainLicense.DetainDate = DateTime.Now;
            DetainLicense.FineFees = FineFees;
            DetainLicense.CreatedByUserID = CreatedByUserID;

            if (DetainLicense.Save())
            {
                return DetainLicense.DetainID;
            }
            return -1;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {
            clsApplication application = new clsApplication();

            application.ApplicantPersonID = this.DriverInfo.PersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationTypeID = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationTypeID;
            application.LastStatusDate = DateTime.Now;
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            application.CreatedByUserID = ReleasedByUserID;

            if(!application.Save()) { return false; }

            ApplicationID = application.ApplicationID;

            clsDetainedLicenses DetainedLicense = clsDetainedLicenses.FindByLicenseID(this.LicenseID);

            return DetainedLicense.ReleaseDetainedLicense(ApplicationID, ReleasedByUserID);
        }

        public bool IsLicenseDetained()
        {
            return clsDetainedLicenses.IsLicenseDetained(this.LicenseID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.AddNew;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateLicense();
                default:
                    return false;

            }
        }
    }
}
