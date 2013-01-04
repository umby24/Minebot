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
        public List<string> admins;
        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I have developed this program entirely in secret due to the massive amount" + Environment.NewLine + "of bitching from everyone that VB looks like shit. So here you go, motherfucking shitty ass C# Minebot. Enjoy. Or don't. I really don't give a fuck anymore about this damned project.", "About C# Minebot");
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
        #endregion
    }
}
