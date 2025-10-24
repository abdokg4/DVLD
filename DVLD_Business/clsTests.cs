using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsTests
    {
        public enum enMode { AddNew = 1, Update = 2 };
        enMode Mode = enMode.AddNew;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointments TestAppointmentInfo { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTests()
        {
            Mode = enMode.AddNew;

            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestAppointmentInfo = new clsTestAppointments();
            this.TestResult = false;
            this.Notes = string.Empty;
            this.CreatedByUserID = -1;
        }

        private clsTests(int testID, int testAppointmentID, bool testResult, string notes, int createdByUserID)
        {
            this.Mode = enMode.Update;

            this.TestID = testID;
            this.TestAppointmentID = testAppointmentID;
            this.TestAppointmentInfo = clsTestAppointments.FindTestAppointment(TestAppointmentID);
            this.TestResult = testResult;
            this.Notes = notes;
            this.CreatedByUserID = createdByUserID;
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(this.TestAppointmentID,this.TestResult,this.Notes, this.CreatedByUserID);

            return (this.TestID != -1);
        }
        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }

        static public clsTests Find(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = string.Empty;
            int createdByUserID = -1;

            if(clsTestData.GetTestInfo(TestID,ref TestAppointmentID, ref TestResult, ref Notes, ref createdByUserID))
            {
                return new clsTests(TestID,TestAppointmentID,TestResult, Notes, createdByUserID);
            }
            else
                return null;
        }

        static public DataTable GetAllTests()
        {
            return clsTestData.GetAllTests();
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                    case enMode.Update:
                    return _UpdateTest();
            }
            return false;
        }

    }
}
