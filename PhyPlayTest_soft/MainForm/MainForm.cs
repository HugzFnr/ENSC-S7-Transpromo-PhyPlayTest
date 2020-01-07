using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Fuzzy;

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

        private void Label2_Click(object sender, EventArgs e)
        {
            //Inputs
            List<double> EMG = new List<double>();
            EMG.Add(3);
            List<double> ECG = new List<double>();
            ECG.Add(2);
            List<double> EDA = new List<double>();
            EDA.Add(1);            
            //Outputs
            List<double> Valence = new List<double>();
            List<double> Arousal = new List<double>();
            ToValenceAndArousal(EMG, ECG, EDA, Valence, Arousal);

        }

        /// <summary>
        /// Premier traitement en logique flou permettant d'obtenir les variables Valence et Arousal pour toute liste mesurée
        /// </summary>
        /// <param name="EMG"></param>
        /// <param name="ECG"></param>
        /// <param name="EDA"></param>
        public void ToValenceAndArousal(List<double> EMG, List<double> ECG, List<double> EDA, List<double> Valence, List<double> Arousal)
        {


            #region Input (EMG)
            var lvEMG = new LinguisticVariable("EMG", 0, 30);

            var fsLowEMG = new FuzzySet("Low", new TrapezoidalFunction(0, 5, 10));
            var fsMidEMG = new FuzzySet("Mid", new TrapezoidalFunction(10, 15, 20));
            var fsHighEMG = new FuzzySet("High", new TrapezoidalFunction(20, 25, 30));

            lvEMG.AddLabel(fsLowEMG);
            lvEMG.AddLabel(fsMidEMG);
            lvEMG.AddLabel(fsHighEMG);
            #endregion


            #region Input (EDA/GSR)
            var lvGSR = new LinguisticVariable("GSR", 0, 30);

            var fsLowGSR = new FuzzySet("Low", new TrapezoidalFunction(0, 3, 6));
            var fsMidLowGSR = new FuzzySet("MidLow", new TrapezoidalFunction(6, 8, 10));
            var fsMidGSR = new FuzzySet("Mid", new TrapezoidalFunction(10, 15, 20));
            var fsMidHighGSR = new FuzzySet("MidHigh", new TrapezoidalFunction(20, 22, 24));
            var fsHighGSR = new FuzzySet("High", new TrapezoidalFunction(24, 27, 30));

            lvGSR.AddLabel(fsLowGSR);
            lvGSR.AddLabel(fsMidLowGSR);
            lvGSR.AddLabel(fsMidGSR);
            lvGSR.AddLabel(fsMidHighGSR);
            lvGSR.AddLabel(fsHighGSR);
            #endregion


            #region Input (ECG/HR)
            var lvHR = new LinguisticVariable("HR", 0, 30);

            var fsLowHR = new FuzzySet("Low", new TrapezoidalFunction(0, 5, 10));
            var fsMidHR = new FuzzySet("Mid", new TrapezoidalFunction(10, 15, 20));
            var fsHighHR = new FuzzySet("High", new TrapezoidalFunction(20, 25, 30));

            lvHR.AddLabel(fsLowHR);
            lvHR.AddLabel(fsMidHR);
            lvHR.AddLabel(fsHighHR);
            #endregion


            #region Output (Valence)
            var lvValence = new LinguisticVariable("Valence", 0, 30);

            var fsVeryLow = new FuzzySet("VeryLow", new TrapezoidalFunction(0, 3, 6));
            var fsLow = new FuzzySet("Low", new TrapezoidalFunction(6, 7, 8));
            var fsMidLow = new FuzzySet("MidLow", new TrapezoidalFunction(8, 9, 10));
            var fsMid = new FuzzySet("Mid", new TrapezoidalFunction(10, 15, 20));
            var fsMidHigh = new FuzzySet("MidHigh", new TrapezoidalFunction(20, 22, 24));
            var fsHigh = new FuzzySet("High", new TrapezoidalFunction(24, 25, 26));
            var fsVeryHigh = new FuzzySet("VeryHigh", new TrapezoidalFunction(26, 28, 30));

            lvValence.AddLabel(fsVeryLow);
            lvValence.AddLabel(fsLow);
            lvValence.AddLabel(fsMidLow);
            lvValence.AddLabel(fsMid);
            lvValence.AddLabel(fsMidHigh);
            lvValence.AddLabel(fsHigh);
            lvValence.AddLabel(fsVeryHigh);
            #endregion

            #region Output (Arousal)
            var lvArousal = new LinguisticVariable("Arousal", 0, 30);

            lvArousal.AddLabel(fsVeryLow);
            lvArousal.AddLabel(fsLow);
            lvArousal.AddLabel(fsMidLow);
            lvArousal.AddLabel(fsMid);
            lvArousal.AddLabel(fsMidHigh);
            lvArousal.AddLabel(fsHigh);
            lvArousal.AddLabel(fsVeryHigh);
            #endregion

            #region Système Inference
            // Base de données pour les variables linguistiques
            //Liste des inputs :
            // EMG(Low, Mid, High) 0 - 30
            // GSR(Low, Mid, High) 0 - 30
            // HR (Low, Mid, High) 0 - 30

            //Liste des outputs :
            // Valence(VeryLow, Low, MidLow, Mid, MidHigh, High, VeryHigh) 0 - 30
            // Arousal(VeryLow, Low, MidLow, Mid, MidHigh, High, VeryHigh) 0 - 30
            var fuzzyDb = new Database();
            fuzzyDb.AddVariable(lvEMG);
            fuzzyDb.AddVariable(lvGSR);
            fuzzyDb.AddVariable(lvHR);

            fuzzyDb.AddVariable(lvValence);
            fuzzyDb.AddVariable(lvArousal);

            // Creation system inference
            // Initialise la methode de défuzzification : centre de gravité
            var inferenceSys = new InferenceSystem(fuzzyDb, new CentroidDefuzzifier(1000));
            // Ajout des regles
            inferenceSys.NewRule("Rule 1", "IF GSR IS High THEN Arousal IS High");
            inferenceSys.NewRule("Rule 2", "IF GSR IS MidHigh THEN Arousal IS MidHigh ");
            inferenceSys.NewRule("Rule 3", "IF GSR IS MidLow THEN Arousal IS MidLow");
            inferenceSys.NewRule("Rule 4", "IF GSR IS Low THEN Arousal IS Low");
            inferenceSys.NewRule("Rule 5", "IF HR IS Low THEN Arousal IS Low");
            inferenceSys.NewRule("Rule 6", "IF HR IS High THEN Arousal IS High");
            inferenceSys.NewRule("Rule 7", "IF GSR IS Low  AND HR IS High THEN Arousal IS MidLow");
            inferenceSys.NewRule("Rule 8", "IF GSR IS High  AND HR IS Low THEN Arousal IS MidHigh");
            inferenceSys.NewRule("Rule 9", "IF EMG IS High  THEN Valence IS VeryLow");
            inferenceSys.NewRule("Rule 10", "IF EMG IS Mid THEN Valence IS Low");
            //inferenceSys.NewRule("Rule 11", "IF EMGsmile IS Mid THEN Valence IS High");
            //inferenceSys.NewRule("Rule 12", "IF EMGsmile IS High THEN Valence IS very High");
            inferenceSys.NewRule("Rule 14", "IF EMG IS Low  THEN Valence IS VeryHigh");
            inferenceSys.NewRule("Rule 15", "IF EMG IS Mid  THEN Valence IS High");
            inferenceSys.NewRule("Rule 16", "IF EMG IS High  THEN Valence IS VeryLow");
            inferenceSys.NewRule("Rule 17", "IF EMG IS High  THEN Valence IS Low");
            inferenceSys.NewRule("Rule 18", "IF EMG IS Low  AND HR IS Low  THEN Valence IS Low");
            inferenceSys.NewRule("Rule 19", "IF EMG IS Low  AND HR IS High  THEN Valence IS High");
            inferenceSys.NewRule("Rule 20", "IF GSR IS High  AND HR IS Mid  THEN Arousal IS High");
            inferenceSys.NewRule("Rule 21", "IF GSR IS MidHigh  AND HR IS Mid  THEN Arousal IS MidHigh");
            inferenceSys.NewRule("Rule 22", "IF GSR IS MidLow  AND HR IS Mid  THEN Arousal IS MidLow");

            #endregion

            #region Exemple
            //Pour toutes les valeurs des listes
            float valEMG = -1, valGSR = -1, valHR = -1;
            for (int i = 0; i < EMG.Count; i++)
            {
                valEMG = (float)EMG[i];
                valGSR = (float)EDA[i];
                valHR = (float)ECG[i];

                // Initialise les données d'entrées
                inferenceSys.SetInput("EMG", valEMG);
                inferenceSys.SetInput("GSR", valGSR);
                inferenceSys.SetInput("HR", valHR);

                // Evalue les données de sortie : Valence, Arousal
                var resValence = -1f;
                var resArousal = -1f;
                try
                {
                    resValence = inferenceSys.Evaluate("Valence");
                    resArousal = inferenceSys.Evaluate("Arousal");
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Erreur : {0}", ex.Message));
                }
                ResLogFlou1Label.Text="EMG: "+ valEMG+" + GSR: "+ valGSR + " + HR: "+ valHR + " = Valence: "+ resValence + ", Arousal: "+ resArousal;
                ResLogFlou1Label.Refresh();
                //Stockage des résultats dans la liste adéquate au résultat
                Valence.Add((double)resValence);
                Arousal.Add((double)resArousal);
            }

            
            #endregion
        }
    }
}
