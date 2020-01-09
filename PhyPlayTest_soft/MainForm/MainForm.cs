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
        //INPUT 1
        List<double> EMG = new List<double>();
        List<double> ECG = new List<double>();
        List<double> EDA = new List<double>();

        //OUTPUT 1 & INPUT 2
        List<double> Valence = new List<double>();
        List<double> Arousal = new List<double>();

        //OUTPUT 2
        List<double> Boredom = new List<double>();
        List<double> Challenge = new List<double>();
        List<double> Excitement = new List<double>();
        List<double> Frustration = new List<double>();
        List<double> Fun = new List<double>();


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
            EMG.Add(800);EMG.Add(750);
            ECG.Add(180);ECG.Add(160);
            EDA.Add(80);EDA.Add(73);  
            //Outputs
            
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

            //Pour traitement adapté à chaque signal
            int meanEMG = Mean(EMG);
            int meanHR = Mean(ECG);
            int meanGSR = Mean(EDA);
            int sEMG = (int) Math.Sqrt(Var(EMG));
            int sHR = (int)Math.Sqrt(Var(ECG));
            int sGSR = (int)Math.Sqrt(Var(EDA));

            #region Input (EMG)
            var lvEMG = new LinguisticVariable("EMG", 0, meanEMG+10*sEMG);

            var fsLowEMG = new FuzzySet("Low", new TrapezoidalFunction(0, meanEMG-5*sEMG, meanEMG-2*sEMG));
            var fsMidEMG = new FuzzySet("Mid", new TrapezoidalFunction(meanEMG - 2 * sEMG, meanEMG, meanEMG +2 * sEMG));
            var fsHighEMG = new FuzzySet("High", new TrapezoidalFunction(meanEMG + 2 * sEMG, meanEMG +5*sEMG, meanEMG + 10 * sEMG));

            lvEMG.AddLabel(fsLowEMG);
            lvEMG.AddLabel(fsMidEMG);
            lvEMG.AddLabel(fsHighEMG);
            #endregion


            #region Input (EDA/GSR)
            var lvGSR = new LinguisticVariable("GSR", 0, meanGSR + 10 * sGSR);

            var fsLowGSR = new FuzzySet("Low", new TrapezoidalFunction(0, meanGSR - 5 * sGSR, meanGSR - 2 * sGSR));
            var fsMidLowGSR = new FuzzySet("MidLow", new TrapezoidalFunction(meanGSR - 2 * sGSR, meanGSR, meanGSR + 2 * sGSR));
            var fsMidGSR = new FuzzySet("Mid", new TrapezoidalFunction(meanGSR - 2 * sGSR, meanGSR, meanGSR + 2 * sGSR));
            var fsMidHighGSR = new FuzzySet("MidHigh", new TrapezoidalFunction(meanGSR - 2 * sGSR, meanGSR, meanGSR + 2 * sGSR));
            var fsHighGSR = new FuzzySet("High", new TrapezoidalFunction(meanGSR - 2 * sGSR, meanGSR, meanGSR + 2 * sGSR));

            lvGSR.AddLabel(fsLowGSR);
            lvGSR.AddLabel(fsMidLowGSR);
            lvGSR.AddLabel(fsMidGSR);
            lvGSR.AddLabel(fsMidHighGSR);
            lvGSR.AddLabel(fsHighGSR);
            #endregion


            #region Input (ECG/HR)
            var lvHR = new LinguisticVariable("HR", 0, meanHR + 10 * sHR);

            var fsLowHR = new FuzzySet("Low", new TrapezoidalFunction(0, meanHR - 5 * sHR, meanHR - 2 * sHR));
            var fsMidHR = new FuzzySet("Mid", new TrapezoidalFunction(meanHR - 2 * sHR, meanHR, meanHR + 2 * sHR));
            var fsHighHR = new FuzzySet("High", new TrapezoidalFunction(meanHR + 2 * sHR, meanHR + 5 * sHR, meanHR + 10 * sHR));

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

        public void FromValenceArousalToEmotions(List<double> Valence, List<double> Arousal)
        {
            //OK ne pas toucher
            #region Input (Valence)
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
            #region Input (Arousal)
            var lvArousal = new LinguisticVariable("Arousal", 0, 30);

            lvArousal.AddLabel(fsVeryLow);
            lvArousal.AddLabel(fsLow);
            lvArousal.AddLabel(fsMidLow);
            lvArousal.AddLabel(fsMid);
            lvArousal.AddLabel(fsMidHigh);
            lvArousal.AddLabel(fsHigh);
            lvArousal.AddLabel(fsVeryHigh);
            #endregion


            //il y a l'output à faire, je sais pas exactement comment il faut faire, à checker en dessous c'est peut etre de la connerie que j'ai fait
            #region Output (Boredom)
            var lvBoredom = new LinguisticVariable("Boredom", 0, 30);

            //var fsVeryLow = new FuzzySet("VeryLow", new TrapezoidalFunction(0, 3, 6));
            //var fsLow = new FuzzySet("Low", new TrapezoidalFunction(6, 7, 8));
            //var fsMidLow = new FuzzySet("MidLow", new TrapezoidalFunction(8, 9, 10));
            //var fsMid = new FuzzySet("Mid", new TrapezoidalFunction(10, 15, 20));
            //var fsMidHigh = new FuzzySet("MidHigh", new TrapezoidalFunction(20, 22, 24));
            //var fsHigh = new FuzzySet("High", new TrapezoidalFunction(24, 25, 26));
            //var fsVeryHigh = new FuzzySet("VeryHigh", new TrapezoidalFunction(26, 28, 30));

            lvBoredom.AddLabel(fsVeryLow);
            lvBoredom.AddLabel(fsLow);
            lvBoredom.AddLabel(fsMidLow);
            lvBoredom.AddLabel(fsMid);
            lvBoredom.AddLabel(fsMidHigh);
            lvBoredom.AddLabel(fsHigh);
            lvBoredom.AddLabel(fsVeryHigh);
            #endregion
            #region Output (Challenge)
            var lvChallenge = new LinguisticVariable("Challenge", 0, 30);

            //var fsVeryLow = new FuzzySet("VeryLow", new TrapezoidalFunction(0, 3, 6));
            //var fsLow = new FuzzySet("Low", new TrapezoidalFunction(6, 7, 8));
            //var fsMidLow = new FuzzySet("MidLow", new TrapezoidalFunction(8, 9, 10));
            //var fsMid = new FuzzySet("Mid", new TrapezoidalFunction(10, 15, 20));
            //var fsMidHigh = new FuzzySet("MidHigh", new TrapezoidalFunction(20, 22, 24));
            //var fsHigh = new FuzzySet("High", new TrapezoidalFunction(24, 25, 26));
            //var fsVeryHigh = new FuzzySet("VeryHigh", new TrapezoidalFunction(26, 28, 30));

            lvChallenge.AddLabel(fsVeryLow);
            lvChallenge.AddLabel(fsLow);
            lvChallenge.AddLabel(fsMidLow);
            lvChallenge.AddLabel(fsMid);
            lvChallenge.AddLabel(fsMidHigh);
            lvChallenge.AddLabel(fsHigh);
            lvChallenge.AddLabel(fsVeryHigh);
            #endregion
            #region Output (Excitement)
            var lvExcitement = new LinguisticVariable("Excitement", 0, 30);

            //var fsVeryLow = new FuzzySet("VeryLow", new TrapezoidalFunction(0, 3, 6));
            //var fsLow = new FuzzySet("Low", new TrapezoidalFunction(6, 7, 8));
            //var fsMidLow = new FuzzySet("MidLow", new TrapezoidalFunction(8, 9, 10));
            //var fsMid = new FuzzySet("Mid", new TrapezoidalFunction(10, 15, 20));
            //var fsMidHigh = new FuzzySet("MidHigh", new TrapezoidalFunction(20, 22, 24));
            //var fsHigh = new FuzzySet("High", new TrapezoidalFunction(24, 25, 26));
            //var fsVeryHigh = new FuzzySet("VeryHigh", new TrapezoidalFunction(26, 28, 30));

            lvExcitement.AddLabel(fsVeryLow);
            lvExcitement.AddLabel(fsLow);
            lvExcitement.AddLabel(fsMidLow);
            lvExcitement.AddLabel(fsMid);
            lvExcitement.AddLabel(fsMidHigh);
            lvExcitement.AddLabel(fsHigh);
            lvExcitement.AddLabel(fsVeryHigh);
            #endregion
            #region Output (Frustation)
            var lvFrustration = new LinguisticVariable("Frustration", 0, 30);

            //var fsVeryLow = new FuzzySet("VeryLow", new TrapezoidalFunction(0, 3, 6));
            //var fsLow = new FuzzySet("Low", new TrapezoidalFunction(6, 7, 8));
            //var fsMidLow = new FuzzySet("MidLow", new TrapezoidalFunction(8, 9, 10));
            //var fsMid = new FuzzySet("Mid", new TrapezoidalFunction(10, 15, 20));
            //var fsMidHigh = new FuzzySet("MidHigh", new TrapezoidalFunction(20, 22, 24));
            //var fsHigh = new FuzzySet("High", new TrapezoidalFunction(24, 25, 26));
            //var fsVeryHigh = new FuzzySet("VeryHigh", new TrapezoidalFunction(26, 28, 30));

            lvFrustration.AddLabel(fsVeryLow);
            lvFrustration.AddLabel(fsLow);
            lvFrustration.AddLabel(fsMidLow);
            lvFrustration.AddLabel(fsMid);
            lvFrustration.AddLabel(fsMidHigh);
            lvFrustration.AddLabel(fsHigh);
            lvFrustration.AddLabel(fsVeryHigh);
            #endregion
            #region Output (Fun)
            var lvFun = new LinguisticVariable("Fun", 0, 30);

            //var fsVeryLow = new FuzzySet("VeryLow", new TrapezoidalFunction(0, 3, 6));
            //var fsLow = new FuzzySet("Low", new TrapezoidalFunction(6, 7, 8));
            //var fsMidLow = new FuzzySet("MidLow", new TrapezoidalFunction(8, 9, 10));
            //var fsMid = new FuzzySet("Mid", new TrapezoidalFunction(10, 15, 20));
            //var fsMidHigh = new FuzzySet("MidHigh", new TrapezoidalFunction(20, 22, 24));
            //var fsHigh = new FuzzySet("High", new TrapezoidalFunction(24, 25, 26));
            //var fsVeryHigh = new FuzzySet("VeryHigh", new TrapezoidalFunction(26, 28, 30));

            lvFun.AddLabel(fsVeryLow);
            lvFun.AddLabel(fsLow);
            lvFun.AddLabel(fsMidLow);
            lvFun.AddLabel(fsMid);
            lvFun.AddLabel(fsMidHigh);
            lvFun.AddLabel(fsHigh);
            lvFun.AddLabel(fsVeryHigh);
            #endregion



            #region Système Inference

            var fuzzyDb = new Database();
            //2 inputs : valence et arousal
            fuzzyDb.AddVariable(lvValence);
            fuzzyDb.AddVariable(lvArousal);

            //5 outputs : boredom, challenge, excitement, frustration, and fun
            fuzzyDb.AddVariable(lvBoredom);
            fuzzyDb.AddVariable(lvChallenge);
            fuzzyDb.AddVariable(lvExcitement);
            fuzzyDb.AddVariable(lvFrustration);
            fuzzyDb.AddVariable(lvFun);

            // Creation system inference
            // Initialise la methode de défuzzification : centre de gravité
            var inferenceSys = new InferenceSystem(fuzzyDb, new CentroidDefuzzifier(1000));
            // Ajout des regles
            inferenceSys.NewRule("Rule 23", "IF Arousal IS NOT VeryLow AND Valence IS MidHigh THEN Fun IS Low");
            inferenceSys.NewRule("Rule 24", "IF Arousal IS NOT Low AND Valence IS MidHigh THEN Fun IS Low");
            inferenceSys.NewRule("Rule 25", "IF Arousal IS NOT VeryLow AND Valence IS High THEN Fun IS Mid");
            inferenceSys.NewRule("Rule 26", "IF Valence IS VeryHigh THEN Fun IS High");
            inferenceSys.NewRule("Rule 27", "IF Arousal IS MidHigh AND Valence IS MidLow THEN Challenge IS Low");
            inferenceSys.NewRule("Rule 28", "IF Arousal IS MidHigh AND Valence IS MidHigh THEN Challenge IS Low");
            inferenceSys.NewRule("Rule 29", "IF Arousal IS High AND Valence IS MidLow THEN Challenge IS Mid");
            inferenceSys.NewRule("Rule 30", "IF Arousal IS High AND Valence IS MidHigh THEN Challenge IS Mid");
            inferenceSys.NewRule("Rule 31", "IF Arousal IS VeryHigh AND Valence IS MidLow THEN Challenge IS High");
            inferenceSys.NewRule("Rule 32", "IF Arousal IS VeryHigh AND Valence IS MidHigh THEN Challenge IS High");
            inferenceSys.NewRule("Rule 33", "IF Arousal IS MidLow AND Valence IS MidLow THEN Boredom IS Low");
            inferenceSys.NewRule("Rule 34", "IF Arousal IS MidLow AND Valence IS Low THEN Boredom IS Mid");
            inferenceSys.NewRule("Rule 35", "IF Arousal IS Low AND Valence IS Low THEN Boredom IS Mid");
            inferenceSys.NewRule("Rule 36", "IF Arousal IS Low AND Valence IS MidLow THEN Boredom IS Mid");
            inferenceSys.NewRule("Rule 37", "IF Arousal IS MidLow AND Valence IS VeryLow THEN Boredom IS High");
            inferenceSys.NewRule("Rule 38", "IF Arousal IS Low AND Valence IS VeryLow THEN Boredom IS High");
            inferenceSys.NewRule("Rule 39", "IF Arousal IS VeryLow AND Valence IS VeryLow THEN Boredom IS High");
            inferenceSys.NewRule("Rule 40", "IF Arousal IS VeryLow AND Valence IS Low THEN Boredom IS High");
            inferenceSys.NewRule("Rule 41", "IF Arousal IS VeryLow AND Valence IS MidLow THEN Boredom IS High");
            inferenceSys.NewRule("Rule 42", "IF Arousal IS VeryHigh AND Valence IS MidLow THEN Frustration IS High");
            inferenceSys.NewRule("Rule 43", "IF Arousal IS MidHigh AND Valence IS Low THEN Frustration IS Mid");
            inferenceSys.NewRule("Rule 44", "IF Arousal IS High AND Valence IS Low THEN Frustration IS Mid");
            inferenceSys.NewRule("Rule 45", "IF Arousal IS High AND Valence IS MidLow THEN Frustration IS Mid");
            inferenceSys.NewRule("Rule 46", "IF Arousal IS MidHigh AND Valence IS VeryLow THEN Frustration IS High");
            inferenceSys.NewRule("Rule 47", "IF Arousal IS High AND Valence IS VeryLow THEN Frustration IS High");
            inferenceSys.NewRule("Rule 48", "IF Arousal IS VeryHigh AND Valence IS VeryLow THEN Frustration IS High");
            inferenceSys.NewRule("Rule 49", "IF Arousal IS VeryHigh AND Valence IS Low THEN Frustration IS High");
            inferenceSys.NewRule("Rule 50", "IF Arousal IS VeryHigh AND Valence IS MidLow THEN Frustration IS High");
            inferenceSys.NewRule("Rule 51", "IF Arousal IS VeryLow AND Valence IS VeryLow THEN Challenge IS VeryLow");
            inferenceSys.NewRule("Rule 52", "IF Arousal IS Low AND Valence IS VeryLow THEN Challenge IS VeryLow");
            inferenceSys.NewRule("Rule 53", "IF Valence IS High THEN Challenge IS VeryLow THEN Boredom IS VeryLow THEN Frustration IS VeryLow");
            inferenceSys.NewRule("Rule 54", "IF Valence IS VeryHigh THEN Challenge IS VeryLow");
            inferenceSys.NewRule("Rule 54b","IF Valence IS VeryHigh THEN Boredom IS VeryLow ");
            inferenceSys.NewRule("Rule 54c","IF Valence IS VeryHigh THEN Frustration IS VeryLow ");
            inferenceSys.NewRule("Rule 55", "IF Valence IS MidHigh THEN Boredom IS VeryLow AND Frustration IS VeryLow");
            inferenceSys.NewRule("Rule 56", "IF Arousal IS VeryLow THEN Challenge IS VeryLow AND Frustration IS VeryLow");
            inferenceSys.NewRule("Rule 57", "IF Arousal IS Low THEN Challenge IS VeryLow AND Frustration IS VeryLow");
            inferenceSys.NewRule("Rule 58", "IF Arousal IS MidLow THEN Challenge IS VeryLow AND Frustration IS VeryLow");
            inferenceSys.NewRule("Rule 59", "IF Arousal IS MidHigh THEN Boredom IS VeryLow");
            inferenceSys.NewRule("Rule 60", "IF Arousal IS High THEN Boredom IS VeryLow");
            inferenceSys.NewRule("Rule 61", "IF Arousal IS VeryHigh THEN Boredom IS VeryLow");
            inferenceSys.NewRule("Rule 62", "IF Arousal IS VeryLow AND Valence IS MidHigh THEN Fun IS VeryLow");
            inferenceSys.NewRule("Rule 63", "IF Arousal IS Low AND Valence IS MidHigh THEN Fun IS VeryLow");
            inferenceSys.NewRule("Rule 64", "IF Arousal IS VeryLow AND Valence IS High THEN Fun IS Low");
            inferenceSys.NewRule("Rule 65", "IF Valence IS MidLow THEN Fun IS VeryLow");
            inferenceSys.NewRule("Rule 66", "IF Arousal IS VeryLow AND Valence IS High THEN Boredom IS Low");
            inferenceSys.NewRule("Rule 67", "IF Arousal IS Low AND Valence IS MidHigh THEN Boredom IS Low");
            inferenceSys.NewRule("Rule 68", "IF Arousal IS VeryLow AND Valence IS MidHigh THEN Boredom IS Mid");
            inferenceSys.NewRule("Rule 69", "IF Arousal IS VeryHigh AND Valence IS VeryLow THEN Challenge IS Mid");
            inferenceSys.NewRule("Rule 70", "IF Arousal IS VeryHigh AND Valence IS VeryHigh THEN Challenge IS Mid");
            inferenceSys.NewRule("Rule 71", "IF Arousal IS High AND Valence IS Low THEN Challenge IS Low");
            inferenceSys.NewRule("Rule 72", "IF Arousal IS High AND Valence IS High THEN Challenge IS Low");
            inferenceSys.NewRule("Rule 73", "IF Arousal IS VeryHigh AND Valence IS Low THEN Challenge IS High");
            inferenceSys.NewRule("Rule 74", "IF Arousal IS VeryHigh AND Valence IS High THEN Challenge IS High");
            inferenceSys.NewRule("Rule 75", "IF Arousal IS MidHigh AND Valence IS MidHigh THEN Excitement IS Low");
            inferenceSys.NewRule("Rule 76", "IF Arousal IS High AND Valence IS MidHigh THEN Excitement IS Mid");
            inferenceSys.NewRule("Rule 77", "IF Arousal IS High AND Valence IS High THEN Excitement IS Mid");
            inferenceSys.NewRule("Rule 78", "IF Arousal IS MidHigh AND Valence IS High THEN Excitement IS Mid");
            inferenceSys.NewRule("Rule 79", "IF Arousal IS VeryHigh AND Valence IS MidHigh THEN Excitement IS High");
            inferenceSys.NewRule("Rule 80", "IF Arousal IS VeryHigh AND Valence IS High THEN Excitement IS High");
            inferenceSys.NewRule("Rule 81", "IF Arousal IS VeryHigh AND Valence IS VeryHigh THEN Excitement IS High");
            inferenceSys.NewRule("Rule 82", "IF Arousal IS High AND Valence IS VeryHigh THEN Excitement IS High");
            inferenceSys.NewRule("Rule 83", "IF Arousal IS MidHigh AND Valence IS VeryHigh THEN Excitement IS High");
            inferenceSys.NewRule("Rule 83", "IF Arousal IS MidLow THEN Excitement IS VeryLow");
            inferenceSys.NewRule("Rule 84", "IF Arousal IS MidLow THEN Excitement IS VeryLow");
            inferenceSys.NewRule("Rule 85", "IF Arousal IS Low THEN Excitement IS VeryLow");
            inferenceSys.NewRule("Rule 86", "IF Arousal IS VeryLow THEN Excitement IS VeryLow");
            inferenceSys.NewRule("Rule 87", "IF Valence IS VeryLow THEN Excitement IS VeryLow");
            inferenceSys.NewRule("Rule 88", "IF Valence IS Low THEN Excitement IS VeryLow");
            inferenceSys.NewRule("Rule 89", "IF Valence IS MidLow THEN Excitement IS VeryLow");



            #endregion

            #region Exemple
            //Pour toutes les valeurs des listes
            float valValence = -1, valArousal = -1;
            for (int i = 0; i < Valence.Count; i++)
            {
                valValence = (float)Valence[i];
                valArousal = (float)Arousal[i];

                // Initialise les données d'entrées
                inferenceSys.SetInput("Valence", valValence);
                inferenceSys.SetInput("Arousal", valArousal);

                // Evalue les données de sortie : Boredom, Challenge, Excitement, Frustration, Fun
                var resBoredom = -1f;
                var resChallenge = -1f;
                var resExcitement = -1f;
                var resFrustration = -1f;
                var resFun = -1f;

                try
                {
                    resBoredom = inferenceSys.Evaluate("Boredom");
                    resChallenge = inferenceSys.Evaluate("Challenge");
                    resExcitement = inferenceSys.Evaluate("Excitement");
                    resFrustration = inferenceSys.Evaluate("Frustration");
                    resFun = inferenceSys.Evaluate("Fun");
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Erreur : {0}", ex.Message));
                }
                ResLogFlou2Label.Text = "Valence: " + valValence + " + Arousal: " + valArousal + " = Boredom: " + resBoredom + ", Challenge: " + resChallenge + ", Excitement: "+resExcitement+", Frustration: "+resFrustration+", Fun: "+resFun;
                ResLogFlou2Label.Refresh();
                //Stockage des résultats dans la liste adéquate au résultat
                Boredom.Add((double)resBoredom);
                Challenge.Add((double)resChallenge);
                Excitement.Add((double)resExcitement);
                Frustration.Add((double)resFrustration);
                Fun.Add((double)resFun);
            }
            #endregion
        }

        public static void DataWriter(List<double> Boredom, List<double> Challenge, List<double> Excitement, List<double> Frustration, List<double> Fun )
        {
            try
            {
                string fichierCible = "PhyPlayTest.txt";

                //Création d'une instance de StreamWriter pour permettre l'ecriture de notre fichier cible
                //StreamWriter monStreamWriter = new StreamWriter(fichierCible);
                StreamWriter myStreamWriter = File.AppendText(fichierCible);
                myStreamWriter.WriteLine(String.Format("Time(ms), Boredom, Challenge, Excitement, Frustration, Fun"));

                for (int i = 0; i < Boredom.Count(); i++)
                {
                    myStreamWriter.WriteLine(String.Format( "{0}, {1}, {2}, {3}, {4}, {5}",
                        i+1, Boredom.First(), Challenge.First(), Excitement.First(), Frustration.First(), Fun.First()));
                    Boredom.Remove(Boredom.First());
                    Challenge.Remove(Boredom.First());
                    Excitement.Remove(Boredom.First());
                    Frustration.Remove(Boredom.First());
                    Fun.Remove(Boredom.First());
                }
                
                //Fermeture du StreamWriter 
                myStreamWriter.Close();
            }
            catch (Exception ex)
            {
                // Code exécuté en cas d'exception 
                Console.Write("Une erreur est survenue au cours de l'opération :");
                Console.WriteLine(ex.Message);
            }
        }


        public int Mean(List<Double> liste)
        {
            double sum = 0;
            for (int i = 0; i < liste.Count; ++i)
            {
                sum += liste[i];
            }
            return (int)(sum / liste.Count);
        }
        public int Var(List<Double> liste)
        {
            double mean = Mean(liste);
            double sum = 0;

            for (int i = 0; i < liste.Count; ++i)
            {
                sum += (liste[i]-mean)* (liste[i] - mean);
            }
            return (int)(sum / liste.Count);
        }
        public int Min(List<Double> liste)
        {
            double min = 0;
            for (int i = 0; i < liste.Count; ++i)
            {
                if (liste[i] < min)
                    min = liste[i];
            }
            return (int)(min) >= 0 ? (int)(min) : 0;
        }
        public int Max(List<Double> liste)
        {
            double max = 0;
            for (int i = 0; i < liste.Count; ++i)
            {
                if (liste[i] > max)
                    max = liste[i];
            }
            return (int)(max) >= 0 ? (int)(max) : 0;
        }

        private void ResLogFlou2Label_Click(object sender, EventArgs e)
        {
            FromValenceArousalToEmotions(Valence, Arousal);
        }
    }
}
