using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C_Minebot
{
    public partial class Form1 : Form
    {
        #region Variables
        public Color windowColor;
        public Color TextColor;
        public Color TextAColor;
        public Boolean flatten;
        public Boolean colorize = true;
        public Boolean onlineMode;
        public List<string> admins;
        #region Network
        public string username;
        public string sessionId;
        public networkHandler nh;
        #endregion
        #region Encryption
        public byte[] sharedkey = new byte[16];
        public string ServerID;
        public byte[] PublicKey;
        public byte[] token;
        public string serverHash;
        #endregion
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I have developed this program entirely in secret due to the massive amount" + Environment.NewLine + "of bitching from everyone that VB looks like shit. So here you go, C# Minebot. Enjoy. Or don't.", "About C# Minebot");
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings whatever = new Settings();
            whatever.myform = this;
            whatever.Show();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load interface Customizations
             loadColors();
            //Load bot allowed administrators.
             RegistryControl Reg = new RegistryControl();
            admins = new List<string>();
            string AdminString = Reg.GetSetting("SH", "Minebot SMP", "admins", "").ToString();
            string[] mysplits = AdminString.Split('|');
            for (int i = 0; i < mysplits.Length; i++)
            {
                if (mysplits[i] != "")
                {
                    admins.Add(mysplits[i]);
                }
            }
            admins.Add("Minebot");
            puts("All settings loaded. Welcome to minebot.");
        }
    
        #region FormHelpers
        public void loadColors()
        {
            //Retrieve color settings from registry.
            RegistryControl reg = new RegistryControl();
            windowColor = Color.FromArgb(int.Parse(reg.GetSetting("SH", "Minebot SMP", "bcr", 255).ToString()), int.Parse(reg.GetSetting("SH", "Minebot SMP", "bcg", 255).ToString()), int.Parse(reg.GetSetting("SH", "Minebot SMP", "bcb", 255).ToString()));

            TextColor = Color.FromArgb(int.Parse(reg.GetSetting("SH", "Minebot SMP", "ter", 0).ToString()), int.Parse(reg.GetSetting("SH", "Minebot SMP", "teg", 0).ToString()), int.Parse(reg.GetSetting("SH", "Minebot SMP", "teb", 0).ToString()));

            TextAColor = Color.FromArgb(int.Parse(reg.GetSetting("SH", "Minebot SMP", "bgr", 192).ToString()), int.Parse(reg.GetSetting("SH", "Minebot SMP", "bgg", 192).ToString()), int.Parse(reg.GetSetting("SH", "Minebot SMP", "bgb", 192).ToString()));

            flatten = Boolean.Parse(reg.GetSetting("SH", "Minebot SMP", "flat", "false").ToString());
            colorize = Boolean.Parse(reg.GetSetting("SH", "Minebot SMP", "colored", "true").ToString());
            if (flatten == true)
            {
                btnSend.FlatStyle = FlatStyle.Flat;
            }
            else
            {
                btnSend.FlatStyle = FlatStyle.Standard;
            }
            //Set the colors from this form, the other forms will pull from this form to color themselves.
            this.BackColor = windowColor;
            this.ForeColor = TextColor;
            btnSend.ForeColor = TextColor;
            btnSend.BackColor = windowColor;
            this.lstOnline.BackColor = TextAColor;
            lstOnline.ForeColor = TextColor;
            console.BackColor = TextAColor;
            console.ForeColor = TextColor;
            chat.BackColor = TextAColor;
            chat.ForeColor = TextColor;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private delegate void putss(string text);

        public void puts(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new putss(puts), text);
            }
            else
            {
                console.AppendText(Environment.NewLine + text);
                console.Select(console.Text.Length, console.Text.Length);
                console.ScrollToCaret();
            }
        }
        public void beginconnect(string UN,string password, string ip, string port,bool online)
        {
            onlineMode = online;
            puts("Logging in to Minecraft.net...");
            if (online)
            {
                Minecraft_Net_Interaction netinter = new Minecraft_Net_Interaction();
                string Response = netinter.login2(UN, password);
                if (Response.Contains(":"))
                {
                    //version:Deprecated:UN:sessionID:UID
                    string[] items = Response.Split(':');
                    username = items[2];
                    sessionId = items[3];
                    nh = new networkHandler(ip, port,this);
                    nh.start();
                }
                else
                {
                    MessageBox.Show(Response);
                }
            }
            else
            {
                username = UN;
                sessionId = "1337";
                nh = new networkHandler(ip, port,this);
                nh.start();
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            nh.socket.writeByte(0);
        }
    }
}
