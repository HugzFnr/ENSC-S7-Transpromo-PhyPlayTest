using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            DateTime[] tab = new DateTime[60000000];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Write(DateTime.Now.Millisecond);
        }
    }
}
