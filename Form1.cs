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

        #region Bot Specific
        public bool onlineMode;
        public Color windowColor;
        public Color TextColor;
        public Color TextAColor;
        public Boolean flatten;
        public Boolean colorize = true;
        public List<string> admins;
        #endregion

        #region Server

        public short health;
        public short hunger;
        public long time;
        public long worldAge;
        public int[] spawnPoint;
        public int EntityID;
        public short selectedSlot;
        public bool onground;
        public float[] position;
        public double[] location;
        public string ServerID;
        public string serverHash;
        public string sessionId;
        #endregion

        #region Encryption
        public byte[] PublicKey;
        public byte[] token;
        public byte[] sharedkey = new byte[16];
        #endregion

        #region Network
        public string username;
        public networkHandler nh;
        #endregion

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the official successor to VB Minebot, written so I could learn C#, and to get this project up again, but cleaner.", "About C# Minebot");
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
            // Load interface Customizations
             loadColors();

            // Load bot allowed administrators.

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
            puts("All settings loaded, welcome to Minebot.");
        }

        private void Form1_Close(object sender, EventArgs e)
        {
            if (nh.started)
            nh.stop();
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
        public void beginConnect(string IP, string port, string UN, string PW, bool online)
        {
            username = UN;
            onlineMode = online;
            if (online)
            {
                puts("Logging in to Minecraft.net...");
                Minecraft_Net_Interaction Login = new Minecraft_Net_Interaction();
                string Response = Login.Login(UN,PW);
                if (Response.Contains(':'))
                {
                    string[] mysplit = Response.Split(':');
                    username = mysplit[2];
                    sessionId = mysplit[3];
                    puts("Done.");
                }
                else
                {
                    puts("Error logging in to Minecraft.net!");
                    puts(Response);
                }
            }

            //Begin connection to server.
            nh = new networkHandler(IP, port, this);
            nh.start();
        }
        #endregion
        #region Threadsafe

        private delegate void putss(string text);

        public void puts(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new putss(puts), text);
            }
            else
            {
                console.AppendText(text + Environment.NewLine);
                console.Select(console.Text.Length, console.Text.Length);
                console.ScrollToCaret();
            }
        }

        public void add(string name)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new putss(add), name);
            }
            else
            {
                lstOnline.Items.Add(name);
            }
        }

        public void remove(string name)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new putss(remove), name);
            }
            else
            {
                lstOnline.Items.Remove(name);
            }
        }
        #endregion

        private void btnSend_Click(object sender, EventArgs e)
        {
            Packets.chatMessage chatmess = new Packets.chatMessage(true, nh.socket, this, chat.Text);
            chat.Clear();
        }
    }
}
