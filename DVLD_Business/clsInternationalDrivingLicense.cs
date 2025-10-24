using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsInternationalDrivingLicense : clsApplication
    {
        enum enMode { AddNew = 1, Update = 2}
        enMode Mode = enMode.AddNew;
        public int InternationalDrivingLicenseID { get; set; }
        public int DriverID { get; set; }
        public clsDrivers DriverInfo { get; set; }
        public int IssuedUsingLocalDrivingLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public clsInternationalDrivingLicense()
        {
            this.ApplicationTypeID = (int)enApplicationType.NewInternationalLicense;

            this.InternationalDrivingLicenseID = -1;
            this.DriverID = -1;
            this.DriverInfo = new clsDrivers();
            this.IssuedUsingLocalDrivingLicenseID = -1;
            this.IssueDate  = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;

            Mode = enMode.AddNew;
        }

        private clsInternationalDrivingLicense(int applicationID, int applicantPersonID, DateTime applicationDate, DateTime lastStatusDate, enApplicationStatus applicationStatus, decimal paidFees, int createdByUserID,
                                    int InternationalDrivingLicenseID, int DriverID, int IssuedUsingLocalDrivingLicenseID,
                                    DateTime IssueDate, DateTime ExpirationDate, bool IsActive)
        {
            base.ApplicationID = applicationID;
            base.ApplicantPersonID = applicantPersonID;
            base.ApplicationDate = applicationDate;
            base.LastStatusDate = lastStatusDate;
            base.ApplicationStatus = applicationStatus;
            base.ApplicationTypeID = (int)enApplicationType.NewInternationalLicense;
            base.PaidFees = paidFees;
            base.CreatedByUserID = createdByUserID;

            this.InternationalDrivingLicenseID = InternationalDrivingLicenseID;
            this.DriverID = DriverID;
            this.DriverInfo = clsDrivers.FindDriverByDriverID(this.DriverID);
            this.IssuedUsingLocalDrivingLicenseID = IssuedUsingLocalDrivingLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalDrivingLicenseID = clsInternationalLicenseData.AddInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalDrivingLicenseID,
                this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return (this.InternationalDrivingLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(this.InternationalDrivingLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalDrivingLicenseID,
                this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);
        }

        static public clsInternationalDrivingLicense FindByInternationalDrivingLicenseID(int InternationalDrivingLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int IssuedUsingLocalDrivingLicenseID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            bool IsActive = false;
            int CreatedByUserID = -1;

            if(clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalDrivingLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalDrivingLicenseID,
                ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                clsApplication Application = clsApplication.Find(ApplicationID);

                return new clsInternationalDrivingLicense(ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, Application.LastStatusDate,
                   Application.ApplicationStatus, Application.PaidFees, Application.CreatedByUserID, InternationalDrivingLicenseID, DriverID,
                   IssuedUsingLocalDrivingLicenseID, IssueDate, ExpirationDate, IsActive);
            }
            else
                return null;

        }

        static public int GetActiveInternationalLicenseIDByPersonID(int PersonID)
        {
            return clsInternationalLicenseData.GetInternationalDrivingLicenseIDByPersonID(PersonID);
        }

        static public DataTable GetInternationalDrivingLicensesByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.GetAllInternationalDrivingLicensesByDriverID(DriverID);
        }

        static public DataTable GetAllInternationalDrivingLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalDrivingLicenses();
        }

        public bool Save()
        {
            base._Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateInternationalLicense();

            }
            return false;
        }

    }
}
