namespace Game1.Minigames.QuizAfvalRace
{
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AntwoordA = new System.Windows.Forms.TextBox();
            this.KeuzeA = new System.Windows.Forms.Button();
            this.KeuzeB = new System.Windows.Forms.Button();
            this.AntwoordB = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.KeuzeC = new System.Windows.Forms.Button();
            this.AntwoordC = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // VraagBox
            // 
            this.VraagBox.Location = new System.Drawing.Point(273, 62);
            this.VraagBox.Multiline = true;
            this.VraagBox.Name = "VraagBox";
            this.VraagBox.Size = new System.Drawing.Size(661, 90);
            this.VraagBox.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Game1.Properties.Resources.deur;
            this.pictureBox1.Location = new System.Drawing.Point(97, 254);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(234, 227);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // AntwoordA
            // 
            this.AntwoordA.Location = new System.Drawing.Point(116, 269);
            this.AntwoordA.Multiline = true;
            this.AntwoordA.Name = "AntwoordA";
            this.AntwoordA.Size = new System.Drawing.Size(196, 49);
            this.AntwoordA.TabIndex = 4;
            // 
            // KeuzeA
            // 
            this.KeuzeA.Location = new System.Drawing.Point(158, 444);
            this.KeuzeA.Name = "KeuzeA";
            this.KeuzeA.Size = new System.Drawing.Size(118, 24);
            this.KeuzeA.TabIndex = 5;
            this.KeuzeA.Text = "Open deur";
            this.KeuzeA.UseVisualStyleBackColor = true;
            // 
            // KeuzeB
            // 
            this.KeuzeB.Location = new System.Drawing.Point(527, 444);
            this.KeuzeB.Name = "KeuzeB";
            this.KeuzeB.Size = new System.Drawing.Size(118, 24);
            this.KeuzeB.TabIndex = 8;
            this.KeuzeB.Text = "Open deur";
            this.KeuzeB.UseVisualStyleBackColor = true;
            // 
            // AntwoordB
            // 
            this.AntwoordB.Location = new System.Drawing.Point(481, 269);
            this.AntwoordB.Multiline = true;
            this.AntwoordB.Name = "AntwoordB";
            this.AntwoordB.Size = new System.Drawing.Size(196, 49);
            this.AntwoordB.TabIndex = 7;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Game1.Properties.Resources.deur;
            this.pictureBox2.Location = new System.Drawing.Point(462, 254);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(236, 227);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // KeuzeC
            // 
            this.KeuzeC.Location = new System.Drawing.Point(912, 444);
            this.KeuzeC.Name = "KeuzeC";
            this.KeuzeC.Size = new System.Drawing.Size(118, 24);
            this.KeuzeC.TabIndex = 11;
            this.KeuzeC.Text = "Open deur";
            this.KeuzeC.UseVisualStyleBackColor = true;
            // 
            // AntwoordC
            // 
            this.AntwoordC.Location = new System.Drawing.Point(871, 269);
            this.AntwoordC.Multiline = true;
            this.AntwoordC.Name = "AntwoordC";
            this.AntwoordC.Size = new System.Drawing.Size(196, 49);
            this.AntwoordC.TabIndex = 10;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Game1.Properties.Resources.deur;
            this.pictureBox3.Location = new System.Drawing.Point(848, 254);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(233, 227);
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(91, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 32);
            this.label1.TabIndex = 12;
            this.label1.Text = "00:00";
            // 
            // Start
            // 
            this.Start.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.Location = new System.Drawing.Point(504, 179);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(150, 39);
            this.Start.TabIndex = 13;
            this.Start.Text = "Start!";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(976, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Game1.Properties.Resources._93407604_a0e4ae527d_m;
            this.ClientSize = new System.Drawing.Size(1220, 508);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KeuzeC);
            this.Controls.Add(this.AntwoordC);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.KeuzeB);
            this.Controls.Add(this.AntwoordB);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.KeuzeA);
            this.Controls.Add(this.AntwoordA);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.VraagBox);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox VraagBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox AntwoordA;
        private System.Windows.Forms.Button KeuzeA;
        private System.Windows.Forms.Button KeuzeB;
        private System.Windows.Forms.TextBox AntwoordB;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button KeuzeC;
        private System.Windows.Forms.TextBox AntwoordC;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label label2;
    }
}