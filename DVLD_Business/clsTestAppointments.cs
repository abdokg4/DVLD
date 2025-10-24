using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsTestAppointments
    {
        public enum enMode { AddNew = 1, Update = 2 }
        enMode _Mode = enMode.AddNew;
        public int TestAppointmentID { get; set; }

        public clsTestTypes.enTestTypes TestTypeID { get; set; }
        public int LocalDrivingLicenseID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestApplicationInfo { get; set; }

        public int TestID
        {
            get
            {
                return _GetTestID();
            }
        }


        public clsTestAppointments()
        {
            _Mode = enMode.AddNew;

            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestTypes.enTestTypes.VisionTest;
            this.LocalDrivingLicenseID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;
        }

        private clsTestAppointments(int testAppointmentID, clsTestTypes.enTestTypes testTypeID, int localDrivingLicenseID, DateTime appointmentDate, decimal paidFees, int createdByUserID, bool isLocked, int retakeTestApplicationID)
        {
            _Mode = enMode.Update;

            this.TestAppointmentID = testAppointmentID;
            this.TestTypeID = testTypeID;
            this.LocalDrivingLicenseID = localDrivingLicenseID;
            this.AppointmentDate = appointmentDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.IsLocked = isLocked;
            this.RetakeTestApplicationID = retakeTestApplicationID;
            RetakeTestApplicationInfo = clsApplication.Find(RetakeTestApplicationID);
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentsData.AddNewTestAppointment((int)this.TestTypeID, this.LocalDrivingLicenseID, this.AppointmentDate,
                this.PaidFees, this.CreatedByUserID, this.RetakeTestApplicationID);

            return this.TestAppointmentID != 0;
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentsData.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseID, this.AppointmentDate,
                this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }

        static public clsTestAppointments FindTestAppointment(int testAppointmentID)
        {
            int TestType = 1;
            int LocalDrivingLicenseID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            if(clsTestAppointmentsData.GetTestAppointmentInfoByID(testAppointmentID, ref TestType, ref LocalDrivingLicenseID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new clsTestAppointments(testAppointmentID, (clsTestTypes.enTestTypes)TestType, LocalDrivingLicenseID, AppointmentDate,
                    PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            else
                return null;

        }

        static public clsTestAppointments FindLastAppointment(int LocalDrivingLicenseID, clsTestTypes.enTestTypes TestType)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            if (clsTestAppointmentsData.GetLastTestAppointment(LocalDrivingLicenseID,(int)TestType, ref TestAppointmentID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))
            {
                return new clsTestAppointments(TestAppointmentID, (clsTestTypes.enTestTypes)TestType, LocalDrivingLicenseID, AppointmentDate,
                    PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            else
                return null;

        }

        static public DateTime GetMinimumAppointmentDate(int LocalDrivingLicenseID, clsTestTypes.enTestTypes TestType)
        {
            return clsTestAppointmentsData.GetMinimumAppointmentDate(LocalDrivingLicenseID, (int)TestType);
        }
        static public DataTable GetAllApplcationTestAppointmentsPerDataType(int LocalDrivingLicenseApplicationID, byte TestType)
        {
            return clsTestAppointmentsData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, TestType);
        }

        private int _GetTestID()
        {
            return clsTestAppointmentsData.GetTestID(TestAppointmentID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateTestAppointment();
            }
            return false;
        }
    }
}
