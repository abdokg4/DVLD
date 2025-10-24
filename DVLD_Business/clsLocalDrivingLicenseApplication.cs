using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        new public enum enMode  {AddNew = 1, Update = 2};

        enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID {  get; set; }

        public int LicenseClassID { get; set; }

        public clsLicenseClass LicenseClass { get; set; }

        public string PersonFullName
        {
            get
            {
                return base.PersonInfo.FullName();
            }

        }

        public clsLocalDrivingLicenseApplication()
        {
            Mode = enMode.AddNew;

            LocalDrivingLicenseApplicationID = 0;
            LicenseClassID = 0;
            LicenseClass = new clsLicenseClass();
        }

        private clsLocalDrivingLicenseApplication(int applicationID, int applicantPersonID, DateTime applicationDate, int applicationTypeID, DateTime lastStatusDate, enApplicationStatus applicationStatus, decimal paidFees, int createdByUserID, 
            int LocalDrivingLicenseApplicationID, int LicenseClassID)
        {
          Mode = enMode.Update;

            this.ApplicationID = applicationID;
            this.ApplicantPersonID = applicantPersonID;
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.LastStatusDate = lastStatusDate;
            this.ApplicationStatus = applicationStatus;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClass = clsLicenseClass.FindByID(LicenseClassID);
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);

            return this.LocalDrivingLicenseApplicationID != -1;
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        static public clsLocalDrivingLicenseApplication FindByLdlID(int LdlID)
        {
            int ApplicationID = 0;
            int ClassLicenseID = 0;

            bool isFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationByID(LdlID, ref ApplicationID, ref ClassLicenseID);


            if (isFound)
            {
                clsApplication application = clsApplication.Find(ApplicationID);

                return new clsLocalDrivingLicenseApplication(application.ApplicationID, application.ApplicantPersonID, application.ApplicationDate, application.ApplicationTypeID,
                    application.LastStatusDate, application.ApplicationStatus, application.PaidFees, application.CreatedByUserID, LdlID, ClassLicenseID);
            }
            else
            {
                return null;
            }
        }

        static public clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LdlID = -1;
            int LicenseClassID = -1;

            bool isFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationByApplicationID(ref LdlID, ApplicationID,ref LicenseClassID);

            
                if (isFound)
                {
                    clsApplication application = clsApplication.Find(ApplicationID);

                    return new clsLocalDrivingLicenseApplication(application.ApplicationID, application.ApplicantPersonID, application.ApplicationDate, application.ApplicationTypeID,
                        application.LastStatusDate, application.ApplicationStatus, application.PaidFees, application.CreatedByUserID, LdlID, LicenseClassID);
                }
                else
                {
                    return null;
                }
        }

        
        public int GetPassedTests()
        {
           return clsTestData.GetPassedTests(this.LocalDrivingLicenseApplicationID);
        }

        static public int GetPassedTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTests(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return this.GetPassedTests() == 3;
        }

        public int TotalTrialsPerTest(clsTestTypes.enTestTypes TestType)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsAmountPerTest(this.LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        public bool DoesAttendTestType(clsTestTypes.enTestTypes TestType)
        {
            return (TotalTrialsPerTest(TestType) > 0);
        }

        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

        static public int TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypes TestType)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsAmountPerTest(LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        static public bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplication)
        {
            int AppID = clsLocalDrivingLicenseApplication.FindByLdlID(LocalDrivingLicenseApplication).ApplicationID;
            if (clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplication))
            {
                return clsApplication.DeleteApplication(AppID);
            }
            return false;
        }
        static public DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLdlApplications();
        }

        public bool DidPassTestType(clsTestTypes.enTestTypes TestType)
        {
            return clsLocalDrivingLicenseApplicationData.DidPassTestType(this.LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        static public bool DidPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypes TestType)
        {
            return clsLocalDrivingLicenseApplicationData.DidPassTestType(LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        public bool DoesHaveActiveApplicationWithTestType(clsTestTypes.enTestTypes TestType)
        {
            return clsLocalDrivingLicenseApplicationData.DoesHaveActiveTestWithTestType(this.LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        static public bool DoesHaveActiveApplicationWithTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypes TestType)
        {
            return clsLocalDrivingLicenseApplicationData.DoesHaveActiveTestWithTestType(LocalDrivingLicenseApplicationID, (byte)TestType);
        }

        public int IssueDrivingLicenseForFirstTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDrivers Driver = clsDrivers.FindDriverByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                Driver = new clsDrivers();
                Driver.CreatedByUserID = CreatedByUserID;
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedDate = DateTime.Now;
                if (!Driver.Save())
                {
                    return -1;
                }
            }

            DriverID = Driver.DriverID;

            clsLicense License = new clsLicense();
            License.DriverID = DriverID;
            License.ApplicationID = this.ApplicationID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.FindByID(this.LicenseClassID).DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = clsLicenseClass.FindByID(this.LicenseClassID).ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReasons.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if(License.Save())
            {
                this.Complete();
                return License.LicenseID;
            }
            else
            {
                return -1;
            }
        }
       new public bool Save()
        {
            base._Mode = (clsApplication.enMode)Mode;
            if(!base.Save()) return false;

            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewLocalDrivingLicenseApplication())
                    {
                    Mode = enMode.Update;
                    return true;
                    }
                    else
                        return false;
                    case enMode.Update:
                            return (_UpdateLocalDrivingLicenseApplication());
                        }
            return false;
        }
    }
}
