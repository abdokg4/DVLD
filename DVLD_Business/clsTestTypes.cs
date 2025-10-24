using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsTestTypes
    {
        public enum enTestTypes {VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        public clsTestTypes.enTestTypes TestTypeID { set; get; }
        public string TestTypeTitle { get; set; }

        public string TestTypeDescription { get; set; }
        public decimal TestFees { get; set; }

        public clsTestTypes()
        {
            this.TestTypeID = enTestTypes.VisionTest;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestFees = 0;
        }
        private clsTestTypes(enTestTypes TestTypeID, string TestTypeTitle,string TestTypeDescription, decimal TestFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestFees = TestFees;
        }

        static public clsTestTypes Find(enTestTypes TestTypeID)
        {
            string TestTypeTitle = "";
            string TestTypeDescription = "";
            decimal TestFees = 0;
            

            if (clsTestTypesData.GetTestTypeByID((int)TestTypeID, ref TestTypeTitle,ref TestTypeDescription ,ref TestFees))
            {
                return new clsTestTypes(TestTypeID, TestTypeTitle,TestTypeDescription ,TestFees);
            }
            else
            {
                return null;
            }
        }
        private bool _EditTestType()
        {
            return clsTestTypesData.EditTestType((int)this.TestTypeID, this.TestTypeTitle,this.TestTypeDescription ,this.TestFees);
        }

        public bool Save()
        {
            return _EditTestType();
        }

        static public DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }
    }
}
