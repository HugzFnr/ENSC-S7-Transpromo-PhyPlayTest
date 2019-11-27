﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private void button1_Click(object sender, EventArgs e) //Bouton openFile
        {
            {
                //label1.Text = getPath();  //Récupère le path et l'affiche pour le fun ^_^
                string fichier = getPath();
                try
                {
                    using (StreamReader sr = new StreamReader(fichier))
                    {
                        string line;
                        string[] tableau;
                        List<double> EMG = new List<double>();
                        List<double> ECG = new List<double>();
                        List<double> EDA = new List<double>();

                        int i = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if( i > 2)
                            {
                                tableau = line.Split('\t');
                                tableau[5]=tableau[5].Replace(".", ",");
                                //Console.WriteLine(tableau[5]);
                                //Console.WriteLine(double.Parse(tableau[5]));
                                EMG.Add(double.Parse(tableau[5]));
                                ECG.Add(double.Parse(tableau[6]));
                                EDA.Add(double.Parse(tableau[7]));

                            }
                            ++i;                            
                        }
                        foreach (var elem in EMG)
                        {
                            Console.WriteLine(elem + ", ");
                        }
                    }
                }
                
                catch (Exception)
                {
                    Console.WriteLine("The file could not be read.");
                }
                
            }
        }

        public string getPath()
        {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }

            return filePath;
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
