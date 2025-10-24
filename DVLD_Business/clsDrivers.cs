using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsDrivers
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsDrivers()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.PersonInfo = null;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;
        }

        private clsDrivers(int driverID, int personID,int createdByUserID, DateTime createdDate)
        {
            Mode = enMode.Update;

            DriverID = driverID;
            PersonID = personID;
            PersonInfo = clsPerson.Find(PersonID);
            CreatedByUserID = createdByUserID;
            CreatedDate = createdDate;
        }

        private bool _AddNewDriver()
        {
            this.DriverID = clsDriversData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return (this.DriverID != -1);
        }

        private bool _UpdateDriver()
        {
            return clsDriversData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        }

        static public clsDrivers FindDriverByDriverID(int driverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if(clsDriversData.FindDriverByDriverID(driverID,ref PersonID,ref CreatedByUserID,ref CreatedDate))
            {
                return new clsDrivers(driverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        static public clsDrivers FindDriverByPersonID(int PersonID)
        {
            int driverID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriversData.FindDriverByPersonID(ref driverID, PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDrivers(driverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }

        static public DataTable GetAllDrivers()
        {
            return clsDriversData.GetAllDrivers();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewDriver())
                    {
                    Mode = enMode.AddNew;
                    return true;
                    }
                    else
                        return false;
      
                case enMode.Update:
                    return _UpdateDriver();
                default:
                    return false;
                   
            }
        }
    }
}
