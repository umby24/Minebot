using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;

using CBOT.Classes;

namespace CBOT {
    public partial class Settings : Form {
        #region Variables
        private string AccessToken, ClientToken;
        private bool Login = false, Verify = false;
        public Dictionary<string, string> Bookmarks;
        public Dictionary<string, Image> BIcons;
        public Dictionary<string, string> BPlayers;
        mainForm MainGui;
        #endregion

        public Settings(mainForm gui) {
            MainGui = gui;
            InitializeComponent();
        }

        #region Button Clicks
        private void btnQuickConnect_Click(object sender, EventArgs e) {
            if (HelpingFunctions.isNumeric(txtQuickPort.Text) == false) {
                MessageBox.Show("Server port must be a number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtQuickIP.Text.Replace(" ", "") == "" || txtQuickPort.Text.Replace(" ", "") == "") {
                MessageBox.Show("IP and Port must be filled in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (chkOnlineMode.Checked == true) {
                if ((AccessToken != null && ClientToken != null) && (AccessToken != "" && ClientToken != "")) {
                    Verify = true;
                    Login = false;
                } else {
                    Login = true;
                    Verify = false;
                }
            } else {
                Login = false;
                Verify = false;
            }

            ConnectMinecraft(txtQuickIP.Text, int.Parse(txtQuickPort.Text));
        }

        private void btnQuickBookmark_Click(object sender, EventArgs e) {
            if (HelpingFunctions.isNumeric(txtQuickPort.Text) == false) {
                MessageBox.Show("Server port must be a number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtQuickIP.Text.Replace(" ", "") == "" || txtQuickPort.Text.Replace(" ", "") == "") {
                MessageBox.Show("IP and Port must be filled in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string Servername = Interaction.InputBox("Enter the name to bookmark this server under", "Bookmark server");

            if (Servername == "")
                return;

            if (Bookmarks == null)
                Bookmarks = new Dictionary<string, string>();

            Bookmarks.Add(Servername, Servername + "|" + txtQuickIP.Text + "|" + txtQuickPort.Text + "=");

            txtQuickIP.Clear();
            txtQuickPort.Clear();

            SaveSettings();
            ResetList();
        }

        private void btnConnect_Click(object sender, EventArgs e) {
            if (lstServers.SelectedIndex == -1)
                return;

            string ServerName = (string)lstServers.SelectedItem;

            if (ServerName.Contains(" ")) // -- This may include ping information, need to split it if so.
                ServerName = ServerName.Substring(0, ServerName.IndexOf(" "));

            string fullString = Bookmarks[ServerName];
            string IP = fullString.Substring(fullString.IndexOf("|") + 1, fullString.LastIndexOf("|") - (fullString.IndexOf("|") + 1));
            string Port = fullString.Substring(fullString.LastIndexOf("|") + 1, fullString.IndexOf("=") - (fullString.LastIndexOf("|") + 1));

            if (chkOnlineMode.Checked == true) {
                if ((AccessToken != null && ClientToken != null) && (AccessToken != "" && ClientToken != "")) {
                    Verify = true;
                    Login = false;
                } else {
                    Login = true;
                    Verify = false;
                }
            } else {
                Login = false;
                Verify = false;
            }

            ConnectMinecraft(IP, int.Parse(Port));
        }
        private void btnPingServers_Click(object sender, EventArgs e) {
            lstServers.Items.Clear();

            if (BIcons == null)
                BIcons = new Dictionary<string, Image>();

            if (BPlayers == null)
                BPlayers = new Dictionary<string, string>();

            BIcons.Clear();
            BPlayers.Clear();

            foreach (string Server in Bookmarks.Keys) {
                string fullString = Bookmarks[Server];
                string IP = fullString.Substring(fullString.IndexOf("|") + 1, fullString.LastIndexOf("|") - (fullString.IndexOf("|") + 1));
                string Port = fullString.Substring(fullString.LastIndexOf("|") + 1, fullString.IndexOf("=") - (fullString.LastIndexOf("|") + 1));

                ServerPinger Pinger = new ServerPinger(IP, int.Parse(Port), Server);
                Pinger.PingComplete += PingCompleteHandler;
                Pinger.ping();
            }
        }
        private void btnAddServer_Click(object sender, EventArgs e) {
            string ServerName = Interaction.InputBox("Input the name for the server", "Add Bookmark");

            if (ServerName == "")
                return;

            string IP = Interaction.InputBox("Input the IP for the server", "Add Bookmark");

            if (IP == "")
                return;

            string Port = Interaction.InputBox("Input the port for the server", "Add Bookmark", "25565");

            if (Port == "")
                return;

            if (HelpingFunctions.isNumeric(Port) == false) {
                MessageBox.Show("Server port must be a number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Bookmarks.Add(ServerName, ServerName + "|" + IP + "|" + Port + "=");
            SaveSettings();
            ResetList();
        }

        private void lstServers_SelectedIndexChanged(object sender, EventArgs e) {
            if (lstServers.SelectedIndex == -1)
                return;

            string ServerName = (string)lstServers.SelectedItem;

            if (ServerName.Contains(" ")) // -- This may include ping information, need to split it if so.
                ServerName = ServerName.Substring(0, ServerName.IndexOf(" "));

            if (BIcons != null) { 
                if (BIcons.ContainsKey(ServerName))
                    IconBox.Image = BIcons[ServerName];

                if (BPlayers.ContainsKey(ServerName))
                    lblPlayers.Text = "Players:\n" + BPlayers[ServerName];
            }
        }
        private void btnRemoveServer_Click(object sender, EventArgs e) {
            if (lstServers.SelectedIndex == -1)
                return;

            string ServerName = (string)lstServers.SelectedItem;

            if (ServerName.Contains(" ")) // -- This may include ping information, need to split it if so.
                ServerName = ServerName.Substring(0, ServerName.IndexOf(" "));

            Bookmarks.Remove(ServerName);
            SaveSettings();
            ResetList();
        }
        #endregion

        private void Settings_Load(object sender, EventArgs e) {
            // -- Load bot settings
            settingsReader SR = new settingsReader("Settings.ini", true);
            SR.readSettings();

            if (!File.Exists("Settings.ini")) {
                SR.settings.Add("QuickIP", "");
                SR.saveSettings();
            }

            SR.readSettings();
            // -- Server bookmarks
            Bookmarks = new Dictionary<string, string>();

            if (SR.settings.ContainsKey("Bookmarks")) {
                // -- [ServerName]|IP|Port=
                if (!SR.settings["Bookmarks"].Contains("=")) {
                    MessageBox.Show("Settings file formatted incorrectly!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] servers = SR.settings["Bookmarks"].Split(new Char[] {'='}, StringSplitOptions.RemoveEmptyEntries);

                foreach (string i in servers) {
                    string ServerName = i.Substring(0, i.IndexOf("|"));
                    Bookmarks.Add(ServerName, i + "=");
                    lstServers.Items.Add(ServerName);
                }
            }

            // -- Quick Connect info
            if (SR.settings.ContainsKey("QuickIP"))
                txtQuickIP.Text = SR.settings["QuickIP"];

            if (SR.settings.ContainsKey("QuickPort"))
                txtQuickPort.Text = SR.settings["QuickPort"];

            // -- User info

            if (SR.settings.ContainsKey("Username"))
                txtUsername.Text = SR.settings["Username"];

            if (SR.settings.ContainsKey("Verify"))
                chkOnlineMode.Checked = bool.Parse(SR.settings["Verify"]);

            if (SR.settings.ContainsKey("AccessToken")) {
                AccessToken = SR.settings["AccessToken"];
                chkStorePassword.Checked = true;
            }

            if (SR.settings.ContainsKey("ClientToken"))
                ClientToken = SR.settings["ClientToken"];

        }

        private void SaveSettings() {
            settingsReader SR = new settingsReader("Settings.ini",true);
            SR.settings = new Dictionary<string, string>();

            SR.settings.Add("Username", txtUsername.Text);

            if (chkStorePassword.Checked == true) {
                SR.settings.Add("AccessToken", AccessToken);
                SR.settings.Add("ClientToken", ClientToken);
            }

            SR.settings.Add("Verify", chkOnlineMode.Checked.ToString());
            SR.settings.Add("QuickIP", txtQuickIP.Text);
            SR.settings.Add("QuickPort", txtQuickPort.Text);
            SR.settings.Add("Bookmarks", String.Join("", Bookmarks.Values.ToArray()));

            SR.saveSettings();
        }
        private void ConnectMinecraft(string ip, int port) {
            if (MainGui.connected == true)
                MainGui.MinecraftServer.Disconnect();

            if (MainGui.MinecraftServer == null)
                MainGui.MinecraftServer = new libMC.NET.Client.MinecraftClient(ip, port, txtUsername.Text, txtPassword.Text, chkOnlineMode.Checked);

            if (Verify) {
                bool Result = MainGui.MinecraftServer.VerifySession(AccessToken, ClientToken);

                if (!Result)
                    Login = true;
            }

            if (Login) {
                if (txtPassword.Text != "")
                    MainGui.MinecraftServer.Login();
                else {
                    MessageBox.Show("Error verifying credentials, please enter your password to connect.", "LoginError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            AccessToken = MainGui.MinecraftServer.AccessToken;
            ClientToken = MainGui.MinecraftServer.ClientToken;

            MainGui.connected = true;

            MainGui.RegisterHandlers();

            MainGui.MinecraftServer.Connect();

            SaveSettings();

            Thread.Sleep(3);

            this.Close();
        }
        private void ResetList() {
            lstServers.Items.Clear();

            foreach (string s in Bookmarks.Keys) {
                lstServers.Items.Add(s);
            }
        }

        private delegate void PingComplete(object ping);
        private void PingCompleteHandler(object Ping) {
            if (this.InvokeRequired) {
                this.Invoke(new PingComplete(PingCompleteHandler), Ping);
                return;
            }
            ServerPinger FinishedPing = (ServerPinger)Ping;

            lstServers.Items.Add(FinishedPing.serverName + " - " + FinishedPing.PingResponse[0] + "/" + FinishedPing.PingResponse[1] + " (" + FinishedPing.PingResponse[4] + ") " + FinishedPing.msPing + " ms");

            if (BIcons.ContainsKey(FinishedPing.serverName))
                BIcons.Remove(FinishedPing.serverName);

            BIcons.Add(FinishedPing.serverName, FinishedPing.favicon);

            if (FinishedPing.Players != null)
                BPlayers.Add(FinishedPing.serverName, String.Join("\n", FinishedPing.Players));

            FinishedPing.SP.Disconnect();
        }

        private void tabPage1_Click(object sender, EventArgs e) {

        }




    }
}
