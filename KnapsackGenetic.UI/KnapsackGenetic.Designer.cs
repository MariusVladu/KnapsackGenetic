namespace KnapsackGenetic.UI
{
    partial class KnapsackGenetic
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonNextGeneration = new System.Windows.Forms.Button();
            this.chartAverageScore = new ScottPlot.FormsPlot();
            this.labelGenerationInfo = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.inputGenerationsNumber = new System.Windows.Forms.NumericUpDown();
            this.chartBestScore = new ScottPlot.FormsPlot();
            ((System.ComponentModel.ISupportInitialize)(this.inputGenerationsNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNextGeneration
            // 
            this.buttonNextGeneration.Location = new System.Drawing.Point(12, 12);
            this.buttonNextGeneration.Name = "buttonNextGeneration";
            this.buttonNextGeneration.Size = new System.Drawing.Size(113, 23);
            this.buttonNextGeneration.TabIndex = 0;
            this.buttonNextGeneration.Text = "Next Generation";
            this.buttonNextGeneration.UseVisualStyleBackColor = true;
            this.buttonNextGeneration.Click += new System.EventHandler(this.buttonNextGeneration_Click);
            // 
            // chartAverageScore
            // 
            this.chartAverageScore.Location = new System.Drawing.Point(13, 41);
            this.chartAverageScore.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chartAverageScore.Name = "chartAverageScore";
            this.chartAverageScore.Size = new System.Drawing.Size(568, 327);
            this.chartAverageScore.TabIndex = 1;
            // 
            // labelGenerationInfo
            // 
            this.labelGenerationInfo.AutoSize = true;
            this.labelGenerationInfo.Location = new System.Drawing.Point(603, 56);
            this.labelGenerationInfo.Name = "labelGenerationInfo";
            this.labelGenerationInfo.Size = new System.Drawing.Size(108, 15);
            this.labelGenerationInfo.TabIndex = 2;
            this.labelGenerationInfo.Text = "Current Generation";
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(506, 12);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 3;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(353, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Generations";
            // 
            // inputGenerationsNumber
            // 
            this.inputGenerationsNumber.Location = new System.Drawing.Point(429, 12);
            this.inputGenerationsNumber.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.inputGenerationsNumber.Name = "inputGenerationsNumber";
            this.inputGenerationsNumber.Size = new System.Drawing.Size(71, 23);
            this.inputGenerationsNumber.TabIndex = 5;
            this.inputGenerationsNumber.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // chartBestScore
            // 
            this.chartBestScore.Location = new System.Drawing.Point(13, 374);
            this.chartBestScore.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chartBestScore.Name = "chartBestScore";
            this.chartBestScore.Size = new System.Drawing.Size(568, 327);
            this.chartBestScore.TabIndex = 6;
            // 
            // KnapsackGenetic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 736);
            this.Controls.Add(this.chartBestScore);
            this.Controls.Add(this.inputGenerationsNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.labelGenerationInfo);
            this.Controls.Add(this.chartAverageScore);
            this.Controls.Add(this.buttonNextGeneration);
            this.Name = "KnapsackGenetic";
            this.Text = "KnapsackGenetic";
            ((System.ComponentModel.ISupportInitialize)(this.inputGenerationsNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNextGeneration;
        private ScottPlot.FormsPlot chartAverageScore;
        private System.Windows.Forms.Label labelGenerationInfo;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown inputGenerationsNumber;
        private ScottPlot.FormsPlot chartBestScore;
    }
}

