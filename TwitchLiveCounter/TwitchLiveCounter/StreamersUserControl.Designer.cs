namespace TwitchLiveCounter {
    partial class StreamersUserControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.user = new System.Windows.Forms.Label();
            this.description = new System.Windows.Forms.Label();
            this.game = new System.Windows.Forms.Label();
            this.viewers = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // user
            // 
            this.user.AutoSize = true;
            this.user.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.user.Location = new System.Drawing.Point(20, 23);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(41, 13);
            this.user.TabIndex = 0;
            this.user.Text = "label1";
            // 
            // description
            // 
            this.description.AutoSize = true;
            this.description.Location = new System.Drawing.Point(20, 44);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(35, 13);
            this.description.TabIndex = 1;
            this.description.Text = "label2";
            // 
            // game
            // 
            this.game.AutoSize = true;
            this.game.Location = new System.Drawing.Point(90, 23);
            this.game.Name = "game";
            this.game.Size = new System.Drawing.Size(35, 13);
            this.game.TabIndex = 2;
            this.game.Text = "label3";
            // 
            // viewers
            // 
            this.viewers.AutoSize = true;
            this.viewers.Location = new System.Drawing.Point(161, 23);
            this.viewers.Name = "viewers";
            this.viewers.Size = new System.Drawing.Size(35, 13);
            this.viewers.TabIndex = 3;
            this.viewers.Text = "label4";
            // 
            // StreamersUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.viewers);
            this.Controls.Add(this.game);
            this.Controls.Add(this.description);
            this.Controls.Add(this.user);
            this.Name = "StreamersUserControl";
            this.Size = new System.Drawing.Size(226, 72);
            this.Load += new System.EventHandler(this.StreamersUserControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label user;
        private System.Windows.Forms.Label description;
        private System.Windows.Forms.Label game;
        private System.Windows.Forms.Label viewers;
    }
}
