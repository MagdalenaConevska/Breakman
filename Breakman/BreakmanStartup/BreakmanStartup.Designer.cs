namespace BreakmanStartup
{
    partial class BreakmanStartup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRecordScores = new System.Windows.Forms.Button();
            this.btnContinueSavedGame = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BreakmanStartup.Properties.Resources.GameTitle;
            this.pictureBox1.Location = new System.Drawing.Point(118, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(581, 116);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Goldenrod;
            this.btnExit.Font = new System.Drawing.Font("Constantia", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnExit.Location = new System.Drawing.Point(237, 405);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(332, 44);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit ";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRecordScores
            // 
            this.btnRecordScores.BackColor = System.Drawing.Color.Goldenrod;
            this.btnRecordScores.Font = new System.Drawing.Font("Constantia", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecordScores.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnRecordScores.Location = new System.Drawing.Point(237, 335);
            this.btnRecordScores.Name = "btnRecordScores";
            this.btnRecordScores.Size = new System.Drawing.Size(332, 44);
            this.btnRecordScores.TabIndex = 6;
            this.btnRecordScores.Text = "See record scores";
            this.btnRecordScores.UseVisualStyleBackColor = false;
            // 
            // btnContinueSavedGame
            // 
            this.btnContinueSavedGame.BackColor = System.Drawing.Color.Goldenrod;
            this.btnContinueSavedGame.Font = new System.Drawing.Font("Constantia", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinueSavedGame.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnContinueSavedGame.Location = new System.Drawing.Point(237, 268);
            this.btnContinueSavedGame.Name = "btnContinueSavedGame";
            this.btnContinueSavedGame.Size = new System.Drawing.Size(332, 44);
            this.btnContinueSavedGame.TabIndex = 5;
            this.btnContinueSavedGame.Text = "Continue saved game";
            this.btnContinueSavedGame.UseVisualStyleBackColor = false;
            this.btnContinueSavedGame.Click += new System.EventHandler(this.btnContinueSavedGame_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.BackColor = System.Drawing.Color.Goldenrod;
            this.btnNewGame.Font = new System.Drawing.Font("Constantia", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewGame.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnNewGame.Location = new System.Drawing.Point(237, 200);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(332, 44);
            this.btnNewGame.TabIndex = 4;
            this.btnNewGame.Text = "Start new game";
            this.btnNewGame.UseVisualStyleBackColor = false;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // BreakmanStartup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(824, 561);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRecordScores);
            this.Controls.Add(this.btnContinueSavedGame);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.pictureBox1);
            this.Name = "BreakmanStartup";
            this.Text = "Breakman";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRecordScores;
        private System.Windows.Forms.Button btnContinueSavedGame;
        private System.Windows.Forms.Button btnNewGame;
    }
}

