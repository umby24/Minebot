namespace CBOT {
    partial class Settings {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnPingServers = new System.Windows.Forms.Button();
            this.btnRemoveServer = new System.Windows.Forms.Button();
            this.btnAddServer = new System.Windows.Forms.Button();
            this.lvlFavicon = new System.Windows.Forms.Label();
            this.IconBox = new System.Windows.Forms.PictureBox();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.lstServers = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.chkStorePassword = new System.Windows.Forms.CheckBox();
            this.grpQuickConnect = new System.Windows.Forms.GroupBox();
            this.txtQuickPort = new System.Windows.Forms.TextBox();
            this.txtQuickIP = new System.Windows.Forms.TextBox();
            this.lblQuickPort = new System.Windows.Forms.Label();
            this.lblQuickIP = new System.Windows.Forms.Label();
            this.btnQuickBookmark = new System.Windows.Forms.Button();
            this.btnQuickConnect = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.chkOnlineMode = new System.Windows.Forms.CheckBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblMinebot = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.grpQuickConnect.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(651, 284);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnConnect);
            this.tabPage1.Controls.Add(this.btnPingServers);
            this.tabPage1.Controls.Add(this.btnRemoveServer);
            this.tabPage1.Controls.Add(this.btnAddServer);
            this.tabPage1.Controls.Add(this.lvlFavicon);
            this.tabPage1.Controls.Add(this.IconBox);
            this.tabPage1.Controls.Add(this.lblPlayers);
            this.tabPage1.Controls.Add(this.lstServers);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(643, 258);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Servers";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(8, 140);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(485, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnPingServers
            // 
            this.btnPingServers.Location = new System.Drawing.Point(8, 227);
            this.btnPingServers.Name = "btnPingServers";
            this.btnPingServers.Size = new System.Drawing.Size(485, 23);
            this.btnPingServers.TabIndex = 6;
            this.btnPingServers.Text = "Ping Servers";
            this.btnPingServers.UseVisualStyleBackColor = true;
            this.btnPingServers.Click += new System.EventHandler(this.btnPingServers_Click);
            // 
            // btnRemoveServer
            // 
            this.btnRemoveServer.Location = new System.Drawing.Point(8, 198);
            this.btnRemoveServer.Name = "btnRemoveServer";
            this.btnRemoveServer.Size = new System.Drawing.Size(485, 23);
            this.btnRemoveServer.TabIndex = 5;
            this.btnRemoveServer.Text = "Remove Server";
            this.btnRemoveServer.UseVisualStyleBackColor = true;
            this.btnRemoveServer.Click += new System.EventHandler(this.btnRemoveServer_Click);
            // 
            // btnAddServer
            // 
            this.btnAddServer.Location = new System.Drawing.Point(8, 169);
            this.btnAddServer.Name = "btnAddServer";
            this.btnAddServer.Size = new System.Drawing.Size(485, 23);
            this.btnAddServer.TabIndex = 4;
            this.btnAddServer.Text = "Add Server";
            this.btnAddServer.UseVisualStyleBackColor = true;
            this.btnAddServer.Click += new System.EventHandler(this.btnAddServer_Click);
            // 
            // lvlFavicon
            // 
            this.lvlFavicon.AutoSize = true;
            this.lvlFavicon.Location = new System.Drawing.Point(499, 3);
            this.lvlFavicon.Name = "lvlFavicon";
            this.lvlFavicon.Size = new System.Drawing.Size(48, 13);
            this.lvlFavicon.TabIndex = 3;
            this.lvlFavicon.Text = "Favicon:";
            // 
            // IconBox
            // 
            this.IconBox.Location = new System.Drawing.Point(499, 17);
            this.IconBox.Name = "IconBox";
            this.IconBox.Size = new System.Drawing.Size(136, 86);
            this.IconBox.TabIndex = 2;
            this.IconBox.TabStop = false;
            // 
            // lblPlayers
            // 
            this.lblPlayers.AutoSize = true;
            this.lblPlayers.Location = new System.Drawing.Point(496, 116);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(44, 13);
            this.lblPlayers.TabIndex = 1;
            this.lblPlayers.Text = "Players:";
            // 
            // lstServers
            // 
            this.lstServers.FormattingEnabled = true;
            this.lstServers.Location = new System.Drawing.Point(8, 8);
            this.lstServers.Name = "lstServers";
            this.lstServers.Size = new System.Drawing.Size(485, 121);
            this.lstServers.TabIndex = 0;
            this.lstServers.SelectedIndexChanged += new System.EventHandler(this.lstServers_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.chkStorePassword);
            this.tabPage2.Controls.Add(this.grpQuickConnect);
            this.tabPage2.Controls.Add(this.txtPassword);
            this.tabPage2.Controls.Add(this.txtUsername);
            this.tabPage2.Controls.Add(this.chkOnlineMode);
            this.tabPage2.Controls.Add(this.lblPassword);
            this.tabPage2.Controls.Add(this.lblUsername);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(643, 258);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "User Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(508, 26);
            this.label1.TabIndex = 9;
            this.label1.Text = "Note: \'Remember Credentials\' does not store your password. If you log in from ano" +
    "ther location or Minecraft\r\nclient, then you will be asked to re-enter your pass" +
    "word to create your credentials again.";
            // 
            // chkStorePassword
            // 
            this.chkStorePassword.AutoSize = true;
            this.chkStorePassword.Location = new System.Drawing.Point(8, 78);
            this.chkStorePassword.Name = "chkStorePassword";
            this.chkStorePassword.Size = new System.Drawing.Size(132, 17);
            this.chkStorePassword.TabIndex = 8;
            this.chkStorePassword.Text = "Remember Credentials";
            this.chkStorePassword.UseVisualStyleBackColor = true;
            // 
            // grpQuickConnect
            // 
            this.grpQuickConnect.Controls.Add(this.txtQuickPort);
            this.grpQuickConnect.Controls.Add(this.txtQuickIP);
            this.grpQuickConnect.Controls.Add(this.lblQuickPort);
            this.grpQuickConnect.Controls.Add(this.lblQuickIP);
            this.grpQuickConnect.Controls.Add(this.btnQuickBookmark);
            this.grpQuickConnect.Controls.Add(this.btnQuickConnect);
            this.grpQuickConnect.Location = new System.Drawing.Point(419, 9);
            this.grpQuickConnect.Name = "grpQuickConnect";
            this.grpQuickConnect.Size = new System.Drawing.Size(216, 104);
            this.grpQuickConnect.TabIndex = 7;
            this.grpQuickConnect.TabStop = false;
            this.grpQuickConnect.Text = "Quick Connect";
            // 
            // txtQuickPort
            // 
            this.txtQuickPort.Location = new System.Drawing.Point(19, 39);
            this.txtQuickPort.Name = "txtQuickPort";
            this.txtQuickPort.Size = new System.Drawing.Size(146, 20);
            this.txtQuickPort.TabIndex = 11;
            // 
            // txtQuickIP
            // 
            this.txtQuickIP.Location = new System.Drawing.Point(19, 18);
            this.txtQuickIP.Name = "txtQuickIP";
            this.txtQuickIP.Size = new System.Drawing.Size(146, 20);
            this.txtQuickIP.TabIndex = 10;
            // 
            // lblQuickPort
            // 
            this.lblQuickPort.AutoSize = true;
            this.lblQuickPort.Location = new System.Drawing.Point(171, 42);
            this.lblQuickPort.Name = "lblQuickPort";
            this.lblQuickPort.Size = new System.Drawing.Size(26, 13);
            this.lblQuickPort.TabIndex = 9;
            this.lblQuickPort.Text = "Port";
            // 
            // lblQuickIP
            // 
            this.lblQuickIP.AutoSize = true;
            this.lblQuickIP.Location = new System.Drawing.Point(171, 21);
            this.lblQuickIP.Name = "lblQuickIP";
            this.lblQuickIP.Size = new System.Drawing.Size(17, 13);
            this.lblQuickIP.TabIndex = 8;
            this.lblQuickIP.Text = "IP";
            // 
            // btnQuickBookmark
            // 
            this.btnQuickBookmark.Location = new System.Drawing.Point(54, 75);
            this.btnQuickBookmark.Name = "btnQuickBookmark";
            this.btnQuickBookmark.Size = new System.Drawing.Size(75, 23);
            this.btnQuickBookmark.TabIndex = 7;
            this.btnQuickBookmark.Text = "Bookmark";
            this.btnQuickBookmark.UseVisualStyleBackColor = true;
            this.btnQuickBookmark.Click += new System.EventHandler(this.btnQuickBookmark_Click);
            // 
            // btnQuickConnect
            // 
            this.btnQuickConnect.Location = new System.Drawing.Point(135, 75);
            this.btnQuickConnect.Name = "btnQuickConnect";
            this.btnQuickConnect.Size = new System.Drawing.Size(75, 23);
            this.btnQuickConnect.TabIndex = 6;
            this.btnQuickConnect.Text = "Connect";
            this.btnQuickConnect.UseVisualStyleBackColor = true;
            this.btnQuickConnect.Click += new System.EventHandler(this.btnQuickConnect_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(8, 29);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(127, 20);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(8, 6);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(127, 20);
            this.txtUsername.TabIndex = 4;
            // 
            // chkOnlineMode
            // 
            this.chkOnlineMode.AutoSize = true;
            this.chkOnlineMode.Location = new System.Drawing.Point(8, 55);
            this.chkOnlineMode.Name = "chkOnlineMode";
            this.chkOnlineMode.Size = new System.Drawing.Size(129, 17);
            this.chkOnlineMode.TabIndex = 2;
            this.chkOnlineMode.Text = "Login to Minecraft.net";
            this.chkOnlineMode.UseVisualStyleBackColor = true;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(141, 32);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(141, 9);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username";
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(643, 258);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Bot Settings";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(643, 258);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Scripts";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lblInfo);
            this.tabPage4.Controls.Add(this.lblMinebot);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(643, 258);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "About";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(434, 73);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(185, 117);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "by Umby24\r\n\r\n- Powered by libMC.NET [by Umby24]\r\n\r\n- Version 1.0\r\n\r\n- Supports Mi" +
    "necraft 1.7.2\r\n\r\n- Lua scripts powered by NLua";
            // 
            // lblMinebot
            // 
            this.lblMinebot.AutoSize = true;
            this.lblMinebot.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinebot.Location = new System.Drawing.Point(181, 0);
            this.lblMinebot.Name = "lblMinebot";
            this.lblMinebot.Size = new System.Drawing.Size(261, 73);
            this.lblMinebot.TabIndex = 0;
            this.lblMinebot.Text = "Minebot";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 284);
            this.Controls.Add(this.tabControl1);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.grpQuickConnect.ResumeLayout(false);
            this.grpQuickConnect.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnPingServers;
        private System.Windows.Forms.Button btnRemoveServer;
        private System.Windows.Forms.Button btnAddServer;
        private System.Windows.Forms.Label lvlFavicon;
        private System.Windows.Forms.PictureBox IconBox;
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.ListBox lstServers;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.CheckBox chkOnlineMode;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblMinebot;
        private System.Windows.Forms.Button btnQuickConnect;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.GroupBox grpQuickConnect;
        private System.Windows.Forms.Label lblQuickPort;
        private System.Windows.Forms.Label lblQuickIP;
        private System.Windows.Forms.Button btnQuickBookmark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkStorePassword;
        private System.Windows.Forms.TextBox txtQuickPort;
        private System.Windows.Forms.TextBox txtQuickIP;
        private System.Windows.Forms.TabPage tabPage5;
    }
}