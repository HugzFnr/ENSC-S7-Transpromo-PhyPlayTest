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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Write(DateTime.Now.Millisecond);
        }

        private bool isPaused
        {
            get;
            set;
        }

        private bool isAcquiring
        {
            get;
            set;
        }

        public virtual LiveSequencer liveSequencer
        {
            get;
            set;
        }

        public virtual void Pause()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Stop()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Start()
        {
            throw new System.NotImplementedException();
        }

    }
}
