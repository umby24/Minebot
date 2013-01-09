using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

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
            if (lstFav.SelectedItem == null)
                return;
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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            myform.beginConnect(txtIP.Text, txtPort.Text, txtUN.Text, txtPW.Text, cbOnline.Checked);
            this.Close();
        }

        private void txtIP_TextChanged(object sender, EventArgs e)
        {
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "IP", txtIP.Text);
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "Port", txtPort.Text);
        }

        private void txtUN_TextChanged(object sender, EventArgs e)
        {
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "Username", txtUN.Text);
        }

        private void txtPW_TextChanged(object sender, EventArgs e)
        {
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "Password", txtPW.Text);
        }

        private void btnRemFav_Click(object sender, EventArgs e)
        {
            string name = lstFav.GetItemText(lstFav.SelectedItem);
            if (name == "" || name == null)
            {
                MessageBox.Show("No item selected.");
                return;
            }
            
            RegistryControl reg = new RegistryControl();
            string fullstring;
            string Favorites = (string)reg.GetSetting("SH", "Minebot SMP", "Fav", "");
            string temp = Favorites.Substring(0, Favorites.IndexOf(name));
            

            if (temp.Length != 0)
                fullstring = Favorites.Replace(temp, "");
            else
                fullstring = Favorites;
            
            fullstring = fullstring.Substring(0, fullstring.IndexOf("=") + 1);
            Favorites = Favorites.Replace(fullstring, "");
            reg.SaveSetting("SH", "Minebot SMP", "Fav", Favorites);
            lstFav.Items.Clear();

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

        private void btnAddFav_Click(object sender, EventArgs e)
        {
             // As much as I wanted to stay away from VB Methods, there is no equivelant of this in C#.
            string name = Interaction.InputBox("What is the name for your server?", "Server Name");
            string ip = Interaction.InputBox("What is the server's IP?", "Server IP");
            string port = Interaction.InputBox("What is the server's port?", "Server Port");
            RegistryControl reg = new RegistryControl();

            string Favorites = (string)reg.GetSetting("SH", "Minebot SMP", "Fav", "");
            Favorites += name + "|" + ip + "|" + port + "=";
            reg.SaveSetting("SH", "Minebot SMP", "Fav", Favorites);
            lstFav.Items.Clear();

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

        private void btnAddAdmin_Click(object sender, EventArgs e)
        {
            RegistryControl Reg = new RegistryControl();
            string name = Interaction.InputBox("What is the admin's in-game username?", "Username");
            string admins = (string)Reg.GetSetting("SH", "Minebot SMP", "Admins", "");

            admins += name + "|";
            Reg.SaveSetting("SH", "Minebot SMP", "Admins", admins);
            myform.admins.Clear();
            lstAdmins.Items.Clear();

            string[] mysplits = admins.Split('|');

            for (int i = 0; i < mysplits.Length; i++)
            {
                if (mysplits[i] != "")
                {
                    myform.admins.Add(mysplits[i]);
                    lstAdmins.Items.Add(mysplits[i]);
                }
            }
            if (!myform.admins.Contains("Minebot"))
            {
                myform.admins.Add("Minebot");
                lstAdmins.Items.Add("Minebot");
            }
        }

        private void btnRemAdmin_Click(object sender, EventArgs e)
        {
            RegistryControl Reg = new RegistryControl();
            string selected = (string)lstAdmins.SelectedItem;
            string admins = (string)Reg.GetSetting("SH", "Minebot SMP", "Admins", "");
            if (selected == null)
                return;
           
            admins = admins.Replace(selected + "|", "");
            Reg.SaveSetting("SH", "Minebot SMP", "Admins", admins);
            lstAdmins.Items.Clear();
            myform.admins.Clear();

            string[] mysplits = admins.Split('|');

            for (int i = 0; i < mysplits.Length; i++)
            {
                if (mysplits[i] != "")
                {
                    myform.admins.Add(mysplits[i]);
                    lstAdmins.Items.Add(mysplits[i]);
                }
            }
            if (!myform.admins.Contains("Minebot"))
            {
                myform.admins.Add("Minebot");
                lstAdmins.Items.Add("Minebot");
            }
        }

        private void txtIrcIP_TextChanged(object sender, EventArgs e)
        {
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "ircIP", txtIrcIP.Text);
        }

        private void txtIRCPort_TextChanged(object sender, EventArgs e)
        {
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "ircPort", txtIRCPort.Text);
        }

        private void txtIrcChan_TextChanged(object sender, EventArgs e)
        {
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "ircChan", txtIrcChan.Text);
        }

        private void txtIrcNick_TextChanged(object sender, EventArgs e)
        {
            RegistryControl Reg = new RegistryControl();
            Reg.SaveSetting("SH", "Minebot SMP", "ircName", txtIrcNick.Text);
        }
    }
}
