﻿namespace MainForm
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ResLogFlou1Label = new System.Windows.Forms.Label();
            this.ResLogFlou2Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenFile.Location = new System.Drawing.Point(174, 227);
            this.buttonOpenFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(495, 133);
            this.buttonOpenFile.TabIndex = 0;
            this.buttonOpenFile.Text = "Open an OpenSignals file";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(310, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "PhyPlayTest";
            // 
            // ResLogFlou1Label
            // 
            this.ResLogFlou1Label.AutoSize = true;
            this.ResLogFlou1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResLogFlou1Label.Location = new System.Drawing.Point(565, 87);
            this.ResLogFlou1Label.Name = "ResLogFlou1Label";
            this.ResLogFlou1Label.Size = new System.Drawing.Size(569, 29);
            this.ResLogFlou1Label.TabIndex = 2;
            this.ResLogFlou1Label.Text = "Cliquer ici pour faire un calcul de Valence et Arousal";
            this.ResLogFlou1Label.Click += new System.EventHandler(this.Label2_Click);
            // 
            // ResLogFlou2Label
            // 
            this.ResLogFlou2Label.AutoSize = true;
            this.ResLogFlou2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResLogFlou2Label.Location = new System.Drawing.Point(565, 144);
            this.ResLogFlou2Label.Name = "ResLogFlou2Label";
            this.ResLogFlou2Label.Size = new System.Drawing.Size(480, 29);
            this.ResLogFlou2Label.TabIndex = 3;
            this.ResLogFlou2Label.Text = "Cliquer ici pour faire un calcul des emotions";
            this.ResLogFlou2Label.Click += new System.EventHandler(this.ResLogFlou2Label_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(1332, 430);
            this.Controls.Add(this.ResLogFlou2Label);
            this.Controls.Add(this.ResLogFlou1Label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOpenFile);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "PhyPlayTest ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ResLogFlou1Label;
        private System.Windows.Forms.Label ResLogFlou2Label;
    }
}

