using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Fuzzy;

namespace MainForm
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void Main(string[] args)
        {
            WritePourboire(7, 9);
            WritePourboire(2, 9);
            WritePourboire(5, 6);
            WritePourboire(2, 3);
            Console.ReadLine();
        }

        public static void ToValenceAndArousal(List<double> EMG, List<double> ECG, List<double> EDA) //noteQualiteService, float noteNourriture)
        {
            #region Input (Qualité de service)
            var lvQualiteService = new LinguisticVariable("QualiteService", 0, 10);

            var fsMauvais = new FuzzySet("Mauvais", new TrapezoidalFunction(0, 5, TrapezoidalFunction.EdgeType.Right));
            var fsBon = new FuzzySet("Bon", new TrapezoidalFunction(0, 5, 10));
            var fsExcellent = new FuzzySet("Excellent", new TrapezoidalFunction(5, 10, TrapezoidalFunction.EdgeType.Left));

            lvQualiteService.AddLabel(fsMauvais);
            lvQualiteService.AddLabel(fsBon);
            lvQualiteService.AddLabel(fsExcellent);
            #endregion

            #region Input (Nourriture)
            var lvNourriture = new LinguisticVariable("Nourriture", 0, 10);

            var fsExecrable = new FuzzySet("Execrable", new TrapezoidalFunction(1, 3, TrapezoidalFunction.EdgeType.Right));
            var fsDelicieux = new FuzzySet("Delicieux", new TrapezoidalFunction(7, 9, TrapezoidalFunction.EdgeType.Left));

            lvNourriture.AddLabel(fsExecrable);
            lvNourriture.AddLabel(fsDelicieux);
            #endregion

            #region Output (Valence)
            var lvValence = new LinguisticVariable("Valence", 0, 30);

            var fsVeryLow = new FuzzySet("VeryLow", new TrapezoidalFunction(0, 5, 10));
            var fsLow = new FuzzySet("Low", new TrapezoidalFunction(0, 5, 10));
            var fsMidLow = new FuzzySet("MidLow", new TrapezoidalFunction(10, 15, 20));
            var fsMid = new FuzzySet("Mid", new TrapezoidalFunction(10, 15, 20));
            var fsMidHigh = new FuzzySet("MidHigh", new TrapezoidalFunction(20, 25, 30));
            var fsHigh = new FuzzySet("High", new TrapezoidalFunction(20, 25, 30));
            var fsVeryHigh = new FuzzySet("High", new TrapezoidalFunction(20, 25, 30));

            lvValence.AddLabel(fsFaible);
            lvValence.AddLabel(fsMoyen);
            lvValence.AddLabel(fsEleve);
            #endregion

            #region Output (Arousal)
            var lvArousal = new LinguisticVariable("Valence", 0, 30);

            var fsFaible = new FuzzySet("Faible", new TrapezoidalFunction(0, 5, 10));
            var fsMoyen = new FuzzySet("Moyen", new TrapezoidalFunction(10, 15, 20));
            var fsEleve = new FuzzySet("Eleve", new TrapezoidalFunction(20, 25, 30));

            lvValence.AddLabel(fsFaible);
            lvValence.AddLabel(fsMoyen);
            lvValence.AddLabel(fsEleve);
            #endregion

            #region Système Inference
            // Base de données pour les variables linguistiques
            // Nourriture(Execrable, Delicieux) 0 - 10
            // QualiteService(Mauvais, Bon, Excellent) 0 - 10
            // Pourboire(Faible, Moyen, Eleve) 0 - 30
            var fuzzyDb = new Database();
            fuzzyDb.AddVariable(lvNourriture);
            fuzzyDb.AddVariable(lvQualiteService);
            fuzzyDb.AddVariable(lvPourboire);

            // Creation system inference
            // Initialise la methode de défuzzification : centre de gravité
            var inferenceSys = new InferenceSystem(fuzzyDb, new CentroidDefuzzifier(1000));
            // Ajoute des regles
            inferenceSys.NewRule("Rule 1", "IF QualiteService IS Mauvais OR Nourriture IS Execrable THEN Pourboire IS Faible");
            inferenceSys.NewRule("Rule 2", "IF QualiteService IS Bon THEN Pourboire IS Moyen");
            inferenceSys.NewRule("Rule 3", "IF QualiteService IS Excellent OR Nourriture IS Delicieux THEN Pourboire IS Eleve");

            If(GSR is high)                 then(arousal is high).
            If(GSR is mid - high)           then(arousal is mid - high).
            If(GSR is mid - low)            then(arousal is mid - low).
            If(GSR is low)                  then(arousal is low).
            If(HR is low)                   then(arousal is low).
            If(HR is high)                  then(arousal is high).
            If(GSR is low) and(HR is high)  then(arousal is mid - low).
            If(GSR is high) and(HR is low)  then(arousal is mid - high).
            If(EMGfrown is high)            then(valence is very low).
            If(EMGfrown is mid)             then(valence is low).
            If(EMGsmile is mid)             then(valence is high).
            If(EMGsmile is high)            then(valence is very high).
            If(EMGsmile is low) and(EMGfrown is low) then(valence is neutral).
            If(EMGsmile is high) and(EMGfrown is low) then(valence is very high).
            If(EMGsmile is high) and(EMGfrown is mid) then(valence is high).
            If(EMGsmile is low) and(EMGfrown is high) then(valence is very low).
            If(EMGsmile is mid) and(EMGfrown is high) then(valence is low).
            If(EMGsmile is low) and(EMGfrown is low) and(HR is low) then(valence is low).
            If(EMGsmile is low) and(EMGfrown is low) and(HR is high) then(valence is high).
            If(GSR is high) and(HR is mid) then(arousal is high).
            If(GSR is mid - high) and(HR is mid) then(arousal is mid - high).
            If(GSR is mid - low) and(HR is mid) then(arousal is mid - low)

            #endregion

            #region Exemple
            // Initialise les données d'entrées
            inferenceSys.SetInput("QualiteService", noteQualiteService);
            inferenceSys.SetInput("Nourriture", noteNourriture);

            // Evalue la donnée de sortie : Pourboire
            var resPourboire = -1f;
            try
            {
                resPourboire = inferenceSys.Evaluate("Pourboire");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erreur : {0}", ex.Message));
            }
            Console.WriteLine("Nourriture: {0}  + QualiteService : {1} = Pourboire : {2}",
                noteNourriture, noteQualiteService, resPourboire);
            #endregion
        }

    }
}
