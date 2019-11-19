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
            //DateTime[] tab = new DateTime[60000000];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Write(DateTime.Now.Millisecond);
        }
        public LiveSequencer liveSequencer
        {
            get;
            set;
        }
        /// <summary>
        /// Suspend l'acquisition et stocke les données.
        /// </summary>
        public void Pause()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Arrête le programme.
        /// </summary>
        public void Stop()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Démarre le programme d'acquisition des données en créant un LiveSequencer et en lui donnant en argument les bons paramètres (nomenclature, intervalle et modèle).
        /// </summary>
        public void Start()
        {
            throw new System.NotImplementedException();
        }
    }
}
