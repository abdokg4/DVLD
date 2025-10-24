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
    public partial class frmShowApplicationInfo : Form
    {
        private int _LdlAppID;
        public frmShowApplicationInfo(int LdlAppID)
        {
            _LdlAppID = LdlAppID;
            InitializeComponent();
        }

        private void frmShowApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlApplicationInfo1.LoadInfo(_LdlAppID);
        }
    }
}
