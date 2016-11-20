namespace TwitchLiveCounter
{
    partial class TwitchLiveCounter
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
            this.getTwitchUsers = new System.Windows.Forms.Button();
            this.twitchUsernameTextBox = new System.Windows.Forms.TextBox();
            this.followerListView = new System.Windows.Forms.ListView();
            this.Usernames = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.updateNowButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.totalFollowersLabel = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.maximizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.contextMenuStripNotifyIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // getTwitchUsers
            // 
            this.getTwitchUsers.Location = new System.Drawing.Point(118, 51);
            this.getTwitchUsers.Name = "getTwitchUsers";
            this.getTwitchUsers.Size = new System.Drawing.Size(107, 23);
            this.getTwitchUsers.TabIndex = 0;
            this.getTwitchUsers.Text = "Get Twitch Users";
            this.getTwitchUsers.UseVisualStyleBackColor = true;
            this.getTwitchUsers.Click += new System.EventHandler(this.getTwitchUsers_Click);
            // 
            // twitchUsernameTextBox
            // 
            this.twitchUsernameTextBox.Location = new System.Drawing.Point(12, 53);
            this.twitchUsernameTextBox.Name = "twitchUsernameTextBox";
            this.twitchUsernameTextBox.Size = new System.Drawing.Size(100, 20);
            this.twitchUsernameTextBox.TabIndex = 1;
            this.twitchUsernameTextBox.TextChanged += new System.EventHandler(this.twitchUsernameTextBox_TextChanged);
            // 
            // followerListView
            // 
            this.followerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Usernames});
            this.followerListView.FullRowSelect = true;
            this.followerListView.GridLines = true;
            this.followerListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.followerListView.Location = new System.Drawing.Point(12, 104);
            this.followerListView.Name = "followerListView";
            this.followerListView.Size = new System.Drawing.Size(213, 130);
            this.followerListView.TabIndex = 2;
            this.followerListView.UseCompatibleStateImageBehavior = false;
            this.followerListView.View = System.Windows.Forms.View.Details;
            // 
            // updateNowButton
            // 
            this.updateNowButton.Location = new System.Drawing.Point(12, 290);
            this.updateNowButton.Name = "updateNowButton";
            this.updateNowButton.Size = new System.Drawing.Size(213, 23);
            this.updateNowButton.TabIndex = 3;
            this.updateNowButton.Text = "Update Now";
            this.updateNowButton.UseVisualStyleBackColor = true;
            this.updateNowButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Twitch Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "List of Twitch Users to Poll for Live status";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 255);
            this.textBox2.Name = "textBox2";
            this.textBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox2.Size = new System.Drawing.Size(30, 20);
            this.textBox2.TabIndex = 6;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Update Interval in Minutes (Default 5)";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(236, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllSettingsToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // clearAllSettingsToolStripMenuItem
            // 
            this.clearAllSettingsToolStripMenuItem.Name = "clearAllSettingsToolStripMenuItem";
            this.clearAllSettingsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.clearAllSettingsToolStripMenuItem.Text = "Clear All Settings";
            this.clearAllSettingsToolStripMenuItem.Click += new System.EventHandler(this.clearAllSettingsToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            // 
            // totalFollowersLabel
            // 
            this.totalFollowersLabel.AutoSize = true;
            this.totalFollowersLabel.Location = new System.Drawing.Point(12, 237);
            this.totalFollowersLabel.Name = "totalFollowersLabel";
            this.totalFollowersLabel.Size = new System.Drawing.Size(84, 13);
            this.totalFollowersLabel.TabIndex = 9;
            this.totalFollowersLabel.Text = "Total Followers: ";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStripNotifyIcon;
            this.notifyIcon.Visible = true;
            // 
            // contextMenuStripNotifyIcon
            // 
            this.contextMenuStripNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.maximizeToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.contextMenuStripNotifyIcon.Name = "contextMenuStripNotifyIcon";
            this.contextMenuStripNotifyIcon.Size = new System.Drawing.Size(114, 48);
            // 
            // maximizeToolStripMenuItem
            // 
            this.maximizeToolStripMenuItem.Name = "maximizeToolStripMenuItem";
            this.maximizeToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.maximizeToolStripMenuItem.Text = "Restore";
            this.maximizeToolStripMenuItem.Click += new System.EventHandler(this.maximizeToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // TwitchLiveCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 327);
            this.Controls.Add(this.totalFollowersLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.updateNowButton);
            this.Controls.Add(this.followerListView);
            this.Controls.Add(this.twitchUsernameTextBox);
            this.Controls.Add(this.getTwitchUsers);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TwitchLiveCounter";
            this.Text = "Twitch Live Counter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TwitchLiveCounter_FormClosing);
            this.Load += new System.EventHandler(this.TwitchLiveCounter_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStripNotifyIcon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getTwitchUsers;
        private System.Windows.Forms.TextBox twitchUsernameTextBox;
        private System.Windows.Forms.ListView followerListView;
        private System.Windows.Forms.Button updateNowButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ColumnHeader Usernames;
        private System.Windows.Forms.Label totalFollowersLabel;
        private System.Windows.Forms.ToolStripMenuItem clearAllSettingsToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem maximizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.Timer updateTimer;
    }
}

