namespace JumpRun
{
    partial class frm_game
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
            this.player = new System.Windows.Forms.PictureBox();
            this.tmr_game = new System.Windows.Forms.Timer(this.components);
            this.lbl_left = new System.Windows.Forms.Label();
            this.lbl_right = new System.Windows.Forms.Label();
            this.lbl_up = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // player
            // 
            this.player.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.player.Location = new System.Drawing.Point(236, 289);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(50, 100);
            this.player.TabIndex = 0;
            this.player.TabStop = false;
            // 
            // tmr_game
            // 
            this.tmr_game.Enabled = true;
            this.tmr_game.Interval = 10;
            this.tmr_game.Tick += new System.EventHandler(this.tmr_game_Tick);
            // 
            // lbl_left
            // 
            this.lbl_left.AutoSize = true;
            this.lbl_left.Location = new System.Drawing.Point(821, 9);
            this.lbl_left.Name = "lbl_left";
            this.lbl_left.Size = new System.Drawing.Size(24, 15);
            this.lbl_left.TabIndex = 1;
            this.lbl_left.Text = "left";
            // 
            // lbl_right
            // 
            this.lbl_right.AutoSize = true;
            this.lbl_right.Location = new System.Drawing.Point(821, 24);
            this.lbl_right.Name = "lbl_right";
            this.lbl_right.Size = new System.Drawing.Size(32, 15);
            this.lbl_right.TabIndex = 2;
            this.lbl_right.Text = "right";
            // 
            // lbl_up
            // 
            this.lbl_up.AutoSize = true;
            this.lbl_up.Location = new System.Drawing.Point(821, 39);
            this.lbl_up.Name = "lbl_up";
            this.lbl_up.Size = new System.Drawing.Size(21, 15);
            this.lbl_up.TabIndex = 3;
            this.lbl_up.Text = "up";
            // 
            // frm_game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.lbl_up);
            this.Controls.Add(this.lbl_right);
            this.Controls.Add(this.lbl_left);
            this.Controls.Add(this.player);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JumpRun";
            this.Load += new System.EventHandler(this.frm_game_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frm_game_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox player;
        private System.Windows.Forms.Timer tmr_game;
        private Label lbl_left;
        private Label lbl_right;
        private Label lbl_up;
    }
}