namespace MainForm
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
            this.InfoLabel = new System.Windows.Forms.Label();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenFile.Location = new System.Drawing.Point(13, 206);
            this.buttonOpenFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(495, 133);
            this.buttonOpenFile.TabIndex = 0;
            this.buttonOpenFile.Text = "1. Ouvrir le fichier OpenSignals";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(26, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "PhyPlayTest";
            // 
            // ResLogFlou1Label
            // 
            this.ResLogFlou1Label.AutoSize = true;
            this.ResLogFlou1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResLogFlou1Label.Location = new System.Drawing.Point(493, 87);
            this.ResLogFlou1Label.Name = "ResLogFlou1Label";
            this.ResLogFlou1Label.Size = new System.Drawing.Size(594, 29);
            this.ResLogFlou1Label.TabIndex = 2;
            this.ResLogFlou1Label.Text = "2. Cliquer ici pour faire un calcul de Valence et Arousal";
            this.ResLogFlou1Label.Click += new System.EventHandler(this.Label2_Click);
            // 
            // ResLogFlou2Label
            // 
            this.ResLogFlou2Label.AutoSize = true;
            this.ResLogFlou2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResLogFlou2Label.Location = new System.Drawing.Point(493, 452);
            this.ResLogFlou2Label.Name = "ResLogFlou2Label";
            this.ResLogFlou2Label.Size = new System.Drawing.Size(505, 29);
            this.ResLogFlou2Label.TabIndex = 3;
            this.ResLogFlou2Label.Text = "3. Cliquer ici pour faire un calcul des emotions";
            this.ResLogFlou2Label.Click += new System.EventHandler(this.ResLogFlou2Label_Click);
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoLabel.Location = new System.Drawing.Point(148, 370);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(186, 29);
            this.InfoLabel.TabIndex = 4;
            this.InfoLabel.Text = "Ouvrez le fichier";
            // 
            // SaveBtn
            // 
            this.SaveBtn.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveBtn.Location = new System.Drawing.Point(1011, 206);
            this.SaveBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(495, 133);
            this.SaveBtn.TabIndex = 5;
            this.SaveBtn.Text = "4. Sauvegarder les Emotions";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(1519, 565);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.InfoLabel);
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
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.Button SaveBtn;
    }
}

