namespace stonefail
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            Game = new PictureBox();
            Timegame = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)Game).BeginInit();
            SuspendLayout();
            // 
            // Game
            // 
            Game.BorderStyle = BorderStyle.Fixed3D;
            Game.Location = new Point(154, 42);
            Game.Name = "Game";
            Game.Size = new Size(818, 485);
            Game.TabIndex = 0;
            Game.TabStop = false;
            // 
            // Timegame
            // 
            Timegame.AutoSize = true;
            Timegame.Font = new Font("Tahoma", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            Timegame.Location = new Point(154, 554);
            Timegame.Name = "Timegame";
            Timegame.Size = new Size(244, 58);
            Timegame.TabIndex = 1;
            Timegame.Text = "Time: 0 s";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 25;
            timer1.Tick += Timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(1224, 671);
            Controls.Add(Timegame);
            Controls.Add(Game);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)Game).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox Game;
        private Label Timegame;
        private System.Windows.Forms.Timer timer1;
    }
}
