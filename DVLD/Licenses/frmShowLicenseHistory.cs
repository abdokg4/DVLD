using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmShowLicenseHistory : Form
    {
        private int _PersonID = -1;
        public frmShowLicenseHistory()
        {
            InitializeComponent();
        }

        public frmShowLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void _LoadData()
        {

        }

        private void ctrlLicenseHistory1_Load(object sender, EventArgs e)
        {

        }

        private void frmShowLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFilter1.DisableFilter();
                ctrlLicenseHistory1.LoadInfoByPersonID(_PersonID);
            }

            else
            {
                ctrlPersonCardWithFilter1.Enabled=true;
                ctrlPersonCardWithFilter1.FilterFocus();
            }

        }
    }
}
