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
    public partial class Settings : Form
    {
        List<string> favs;
        public Form1 myform;
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            favs = new List<string>();
            RegistryControl reg = new RegistryControl();

            linkLabel1.Links.Add(0, linkLabel1.Text.Length, linkLabel1.Text);
            linkLabel2.Links.Add(0, linkLabel2.Text.Length, linkLabel2.Text);

            checkBox2.Checked = myform.flatten;
            txtIP.Text = (string)reg.GetSetting("SH","Minebot SMP","IP","smp.mcsteamed.net");
            txtPort.Text = (string)reg.GetSetting("SH", "Minebot SMP", "Port", 25566);
            txtUN.Text = (string)reg.GetSetting("SH", "Minebot SMP", "Username", "Minebot");
            txtPW.Text = (string)reg.GetSetting("SH", "Minebot SMP", "Password", "pie");
            cbOnline.Checked = bool.Parse((string)reg.GetSetting("SH", "Minebot SMP", "Online", true));
           // Load in admins from main form
            for (int i = 0; i < (myform.admins.Count); i++)
            {
                lstAdmins.Items.Add(myform.admins[i]);
            }
            try
            {
                txtIrcIP.Text = (string)reg.GetSetting("SH", "Minebot SMP", "ircIP", "irc.esper.net");

                txtIRCPort.Text = (string)reg.GetSetting("SH", "Minebot SMP", "ircPort", 6667);
                txtIrcChan.Text = (string)reg.GetSetting("SH", "Minebot SMP", "ircChan", "#bot");
                txtIrcNick.Text = (string)reg.GetSetting("SH", "Minebot SMP", "ircName", "VBMimebot");

                string Favorites = (string)reg.GetSetting("SH", "Minebot SMP", "Fav", "");
                if (Favorites.Contains("=") && Favorites.Contains("|"))
                {
                    string[] Favsplit = Favorites.Split('=');
                    for (int i = 0; i < (Favsplit.Length); i++)
                    {
                        if (Favsplit[i] != "")
                        {
                            favs.Add(Favsplit[i]);
                            lstFav.Items.Add(Favsplit[i].Split('|')[0]);
                        }

                    }
                }
            }
            catch
            {
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            myform.windowColor = colorDialog1.Color;
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "bcr", colorDialog1.Color.R.ToString());
            Reg.SaveSetting("SH", "Minebot SMP", "bcg", colorDialog1.Color.G.ToString());
            Reg.SaveSetting("SH", "Minebot SMP", "bcb", colorDialog1.Color.B.ToString());
            myform.loadColors();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            myform.windowColor = colorDialog1.Color;
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "bgr", colorDialog1.Color.R.ToString());
            Reg.SaveSetting("SH", "Minebot SMP", "bgg", colorDialog1.Color.G.ToString());
            Reg.SaveSetting("SH", "Minebot SMP", "bgb", colorDialog1.Color.B.ToString());
            myform.loadColors();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            myform.windowColor = colorDialog1.Color;
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "ter", colorDialog1.Color.R.ToString());
            Reg.SaveSetting("SH", "Minebot SMP", "teg", colorDialog1.Color.G.ToString());
            Reg.SaveSetting("SH", "Minebot SMP", "teb", colorDialog1.Color.B.ToString());
            myform.loadColors();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "flat", checkBox2.Checked.ToString());
            myform.loadColors();
        }

        private void cbOnline_CheckedChanged(object sender, EventArgs e)
        {
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "Online", cbOnline.Checked.ToString());
        }
        private void lstFav_DoubleClick(object Sender, EventArgs e)
        {
            string selected = lstFav.SelectedItem.ToString();
            for (int i = 0; i < favs.Count; i++)
            {
                if (favs[i].Split('|')[0] == selected)
                {
                    txtIP.Text = favs[i].Split('|')[1];
                    txtPort.Text = favs[i].Split('|')[2].Replace("=","");
                }
            }
        }

        private void btnPingFav_Click(object sender, EventArgs e)
        {

        }

        private void btnRemFav_Click(object sender, EventArgs e)
        {

        }

        private void btnAddFav_Click(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            myform.beginconnect(txtUN.Text,txtPW.Text,txtIP.Text,txtPort.Text,cbOnline.Checked);
            this.Close();
        }

        private void txtIrcIP_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddAdmin_Click(object sender, EventArgs e)
        {

        }

        private void btnRemAdmin_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
