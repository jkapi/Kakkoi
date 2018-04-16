namespace Game1.Minigames.QuizAfvalRace
{
    /// <summary>
    /// 
    /// </summary>
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.VraagBox = new System.Windows.Forms.TextBox();
            this.Box1 = new System.Windows.Forms.PictureBox();
            this.AntwoordA = new System.Windows.Forms.TextBox();
            this.KeuzeA = new System.Windows.Forms.Button();
            this.KeuzeB = new System.Windows.Forms.Button();
            this.AntwoordB = new System.Windows.Forms.TextBox();
            this.Box2 = new System.Windows.Forms.PictureBox();
            this.KeuzeC = new System.Windows.Forms.Button();
            this.AntwoordC = new System.Windows.Forms.TextBox();
            this.Box3 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.TimerLabel = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Button();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.LevensLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.KeuzeA_Goed = new System.Windows.Forms.PictureBox();
            this.KeuzeA_Fout = new System.Windows.Forms.PictureBox();
            this.KeuzeB_Fout = new System.Windows.Forms.PictureBox();
            this.KeuzeB_Goed = new System.Windows.Forms.PictureBox();
            this.C_Fout = new System.Windows.Forms.PictureBox();
            this.KeuzeC_Goed = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Box1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Box2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Box3)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KeuzeA_Goed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeuzeA_Fout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeuzeB_Fout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeuzeB_Goed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.C_Fout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeuzeC_Goed)).BeginInit();
            this.SuspendLayout();
            // 
            // VraagBox
            // 
            this.VraagBox.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VraagBox.Location = new System.Drawing.Point(377, 102);
            this.VraagBox.Multiline = true;
            this.VraagBox.Name = "VraagBox";
            this.VraagBox.Size = new System.Drawing.Size(661, 67);
            this.VraagBox.TabIndex = 0;
            // 
            // Box1
            // 
            this.Box1.Image = global::Game1.Properties.Resources.deur;
            this.Box1.Location = new System.Drawing.Point(201, 271);
            this.Box1.Name = "Box1";
            this.Box1.Size = new System.Drawing.Size(234, 227);
            this.Box1.TabIndex = 1;
            this.Box1.TabStop = false;
            // 
            // AntwoordA
            // 
            this.AntwoordA.Location = new System.Drawing.Point(221, 286);
            this.AntwoordA.Multiline = true;
            this.AntwoordA.Name = "AntwoordA";
            this.AntwoordA.Size = new System.Drawing.Size(196, 49);
            this.AntwoordA.TabIndex = 4;
            // 
            // KeuzeA
            // 
            this.KeuzeA.Location = new System.Drawing.Point(262, 461);
            this.KeuzeA.Name = "KeuzeA";
            this.KeuzeA.Size = new System.Drawing.Size(118, 24);
            this.KeuzeA.TabIndex = 5;
            this.KeuzeA.Text = "Open deur";
            this.KeuzeA.UseVisualStyleBackColor = true;
            this.KeuzeA.Click += new System.EventHandler(this.KeuzeA_Click);
            // 
            // KeuzeB
            // 
            this.KeuzeB.Location = new System.Drawing.Point(631, 461);
            this.KeuzeB.Name = "KeuzeB";
            this.KeuzeB.Size = new System.Drawing.Size(118, 24);
            this.KeuzeB.TabIndex = 8;
            this.KeuzeB.Text = "Open deur";
            this.KeuzeB.UseVisualStyleBackColor = true;
            this.KeuzeB.Click += new System.EventHandler(this.KeuzeB_Click);
            // 
            // AntwoordB
            // 
            this.AntwoordB.Location = new System.Drawing.Point(585, 286);
            this.AntwoordB.Multiline = true;
            this.AntwoordB.Name = "AntwoordB";
            this.AntwoordB.Size = new System.Drawing.Size(196, 49);
            this.AntwoordB.TabIndex = 7;
            // 
            // Box2
            // 
            this.Box2.Image = global::Game1.Properties.Resources.deur;
            this.Box2.Location = new System.Drawing.Point(566, 271);
            this.Box2.Name = "Box2";
            this.Box2.Size = new System.Drawing.Size(236, 227);
            this.Box2.TabIndex = 6;
            this.Box2.TabStop = false;
            // 
            // KeuzeC
            // 
            this.KeuzeC.Location = new System.Drawing.Point(1016, 461);
            this.KeuzeC.Name = "KeuzeC";
            this.KeuzeC.Size = new System.Drawing.Size(118, 24);
            this.KeuzeC.TabIndex = 11;
            this.KeuzeC.Text = "Open deur";
            this.KeuzeC.UseVisualStyleBackColor = true;
            this.KeuzeC.Click += new System.EventHandler(this.KeuzeC_Click);
            // 
            // AntwoordC
            // 
            this.AntwoordC.Location = new System.Drawing.Point(975, 286);
            this.AntwoordC.Multiline = true;
            this.AntwoordC.Name = "AntwoordC";
            this.AntwoordC.Size = new System.Drawing.Size(196, 49);
            this.AntwoordC.TabIndex = 10;
            // 
            // Box3
            // 
            this.Box3.Image = global::Game1.Properties.Resources.deur;
            this.Box3.Location = new System.Drawing.Point(952, 271);
            this.Box3.Name = "Box3";
            this.Box3.Size = new System.Drawing.Size(233, 227);
            this.Box3.TabIndex = 9;
            this.Box3.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimerLabel.Location = new System.Drawing.Point(195, 79);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(92, 32);
            this.TimerLabel.TabIndex = 12;
            this.TimerLabel.Text = "00:00";
            // 
            // Start
            // 
            this.Start.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.Location = new System.Drawing.Point(608, 196);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(150, 39);
            this.Start.TabIndex = 13;
            this.Start.Text = "Start!";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.button1_Click);
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreLabel.Location = new System.Drawing.Point(1082, 79);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(86, 23);
            this.ScoreLabel.TabIndex = 14;
            this.ScoreLabel.Text = "Score:0";
            // 
            // LevensLabel
            // 
            this.LevensLabel.AutoSize = true;
            this.LevensLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevensLabel.Location = new System.Drawing.Point(1082, 125);
            this.LevensLabel.Name = "LevensLabel";
            this.LevensLabel.Size = new System.Drawing.Size(103, 23);
            this.LevensLabel.TabIndex = 15;
            this.LevensLabel.Text = "Levens: 3";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Game1.Properties.Resources._93407604_a0e4ae527d_m;
            this.panel1.Controls.Add(this.KeuzeA);
            this.panel1.Controls.Add(this.AntwoordA);
            this.panel1.Controls.Add(this.LevensLabel);
            this.panel1.Controls.Add(this.VraagBox);
            this.panel1.Controls.Add(this.ScoreLabel);
            this.panel1.Controls.Add(this.Start);
            this.panel1.Controls.Add(this.TimerLabel);
            this.panel1.Controls.Add(this.KeuzeC);
            this.panel1.Controls.Add(this.AntwoordC);
            this.panel1.Controls.Add(this.AntwoordB);
            this.panel1.Controls.Add(this.KeuzeB);
            this.panel1.Controls.Add(this.Box2);
            this.panel1.Controls.Add(this.KeuzeB_Fout);
            this.panel1.Controls.Add(this.KeuzeB_Goed);
            this.panel1.Controls.Add(this.Box1);
            this.panel1.Controls.Add(this.KeuzeA_Fout);
            this.panel1.Controls.Add(this.KeuzeA_Goed);
            this.panel1.Controls.Add(this.Box3);
            this.panel1.Controls.Add(this.C_Fout);
            this.panel1.Controls.Add(this.KeuzeC_Goed);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 535);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // KeuzeA_Goed
            // 
            this.KeuzeA_Goed.Image = global::Game1.Properties.Resources.Deur3;
            this.KeuzeA_Goed.Location = new System.Drawing.Point(206, 271);
            this.KeuzeA_Goed.Name = "KeuzeA_Goed";
            this.KeuzeA_Goed.Size = new System.Drawing.Size(234, 227);
            this.KeuzeA_Goed.TabIndex = 17;
            this.KeuzeA_Goed.TabStop = false;
            this.KeuzeA_Goed.Visible = false;
            // 
            // KeuzeA_Fout
            // 
            this.KeuzeA_Fout.Image = global::Game1.Properties.Resources.deur1;
            this.KeuzeA_Fout.Location = new System.Drawing.Point(206, 271);
            this.KeuzeA_Fout.Name = "KeuzeA_Fout";
            this.KeuzeA_Fout.Size = new System.Drawing.Size(229, 224);
            this.KeuzeA_Fout.TabIndex = 16;
            this.KeuzeA_Fout.TabStop = false;
            this.KeuzeA_Fout.Visible = false;
            // 
            // KeuzeB_Fout
            // 
            this.KeuzeB_Fout.Image = global::Game1.Properties.Resources.deur1;
            this.KeuzeB_Fout.Location = new System.Drawing.Point(566, 271);
            this.KeuzeB_Fout.Name = "KeuzeB_Fout";
            this.KeuzeB_Fout.Size = new System.Drawing.Size(236, 224);
            this.KeuzeB_Fout.TabIndex = 18;
            this.KeuzeB_Fout.TabStop = false;
            this.KeuzeB_Fout.Visible = false;
            // 
            // KeuzeB_Goed
            // 
            this.KeuzeB_Goed.Image = global::Game1.Properties.Resources.Deur3;
            this.KeuzeB_Goed.Location = new System.Drawing.Point(566, 271);
            this.KeuzeB_Goed.Name = "KeuzeB_Goed";
            this.KeuzeB_Goed.Size = new System.Drawing.Size(236, 224);
            this.KeuzeB_Goed.TabIndex = 19;
            this.KeuzeB_Goed.TabStop = false;
            this.KeuzeB_Goed.Visible = false;
            // 
            // C_Fout
            // 
            this.C_Fout.Image = global::Game1.Properties.Resources.deur1;
            this.C_Fout.Location = new System.Drawing.Point(952, 271);
            this.C_Fout.Name = "C_Fout";
            this.C_Fout.Size = new System.Drawing.Size(233, 224);
            this.C_Fout.TabIndex = 20;
            this.C_Fout.TabStop = false;
            this.C_Fout.Visible = false;
            // 
            // KeuzeC_Goed
            // 
            this.KeuzeC_Goed.Image = global::Game1.Properties.Resources.Deur3;
            this.KeuzeC_Goed.Location = new System.Drawing.Point(952, 271);
            this.KeuzeC_Goed.Name = "KeuzeC_Goed";
            this.KeuzeC_Goed.Size = new System.Drawing.Size(233, 227);
            this.KeuzeC_Goed.TabIndex = 21;
            this.KeuzeC_Goed.TabStop = false;
            this.KeuzeC_Goed.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Game1.Properties.Resources._93407604_a0e4ae527d_m;
            this.ClientSize = new System.Drawing.Size(1423, 580);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Box1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Box2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Box3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KeuzeA_Goed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeuzeA_Fout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeuzeB_Fout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeuzeB_Goed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.C_Fout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeuzeC_Goed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox VraagBox;
        private System.Windows.Forms.PictureBox Box1;
        private System.Windows.Forms.TextBox AntwoordA;
        private System.Windows.Forms.Button KeuzeA;
        private System.Windows.Forms.Button KeuzeB;
        private System.Windows.Forms.TextBox AntwoordB;
        private System.Windows.Forms.PictureBox Box2;
        private System.Windows.Forms.Button KeuzeC;
        private System.Windows.Forms.TextBox AntwoordC;
        private System.Windows.Forms.PictureBox Box3;
        private System.Windows.Forms.Label TimerLabel;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Label LevensLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox KeuzeA_Fout;
        private System.Windows.Forms.PictureBox KeuzeA_Goed;
        private System.Windows.Forms.PictureBox KeuzeB_Fout;
        private System.Windows.Forms.PictureBox KeuzeB_Goed;
        private System.Windows.Forms.PictureBox C_Fout;
        private System.Windows.Forms.PictureBox KeuzeC_Goed;
    }
}