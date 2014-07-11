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
using System.Runtime.InteropServices;
using System.IO;

using libMC.NET.Client;

namespace CBOT {
    public partial class mainForm : Form {
        public bool Connected;
        public MinecraftClient Client;
        public Settings SettingsForm;
        #region Colorized Chatbox
        public const int EM_GETLINECOUNT = 0xBA;
        public const int EM_LINESCROLL = 0xB6;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        #endregion
        #region Form Helpers
        private delegate void putss(string text);
        private delegate void putsc_(string text, Color tColor, bool append, string Style);

        public void putsc(string text, Color tColor, bool append, string Style = "") {
            if (this.InvokeRequired) {
                this.Invoke(new putsc_(putsc), text, tColor, append, Style);
            } else {
                if (append == true) {
                    int intLines = SendMessage(boxConsole.Handle, EM_GETLINECOUNT, 0, 0);
                    boxConsole.AppendText(text);
                    boxConsole.Select(boxConsole.Text.Length - text.Length, text.Length);
                    boxConsole.SelectionColor = tColor;

                    if (Style == "italic")
                        boxConsole.SelectionFont = new Font("Cambria", 12, FontStyle.Italic);
                    if (Style == "bold")
                        boxConsole.SelectionFont = new Font("Cambria", 12, FontStyle.Bold);
                    if (Style == "strike")
                        boxConsole.SelectionFont = new Font("Cambria", 12, FontStyle.Strikeout);

                    int linesToAdd = (SendMessage(boxConsole.Handle, EM_GETLINECOUNT, 0, 0) - intLines);
                    SendMessage(boxConsole.Handle, EM_LINESCROLL, 0, linesToAdd);
                    return;
                }
                int aintLines = SendMessage(boxConsole.Handle, EM_GETLINECOUNT, 0, 0);
                boxConsole.AppendText(Environment.NewLine);
                boxConsole.AppendText(text);
                boxConsole.Select(boxConsole.Text.Length - text.Length, text.Length);
                boxConsole.SelectionColor = tColor;

                if (Style == "italic")
                    boxConsole.SelectionFont = new Font("Cambria", 12, FontStyle.Italic);
                if (Style == "bold")
                    boxConsole.SelectionFont = new Font("Cambria", 12, FontStyle.Bold);
                if (Style == "strike")
                    boxConsole.SelectionFont = new Font("Cambria", 12, FontStyle.Strikeout);

                int blinesToAdd = (SendMessage(boxConsole.Handle, EM_GETLINECOUNT, 0, 0) - aintLines);
                SendMessage(boxConsole.Handle, EM_LINESCROLL, 0, blinesToAdd);
            }

        }
        public void putsc(string text, Color tColor, string Style = "") {
            bool append = false;
            if (this.InvokeRequired) {
                this.Invoke(new putsc_(putsc), text, tColor, append, Style);
            } else {
                int aintLines = SendMessage(boxConsole.Handle, EM_GETLINECOUNT, 0, 0);
                boxConsole.AppendText(Environment.NewLine);
                boxConsole.AppendText(text);
                boxConsole.Select(boxConsole.Text.Length - text.Length, text.Length);
                boxConsole.SelectionColor = tColor;
                if (Style == "italic")
                    boxConsole.SelectionFont = new Font("Cambria", 12, FontStyle.Italic);
                int blinesToAdd = (SendMessage(boxConsole.Handle, EM_GETLINECOUNT, 0, 0) - aintLines);
                SendMessage(boxConsole.Handle, EM_LINESCROLL, 0, blinesToAdd);
            }
        }
        public void puts(string text) {
            if (this.InvokeRequired) {
                try {
                    this.Invoke(new putss(puts), text);
                } catch (ObjectDisposedException) { }
            } else {
                boxConsole.AppendText(Environment.NewLine + text);
                boxConsole.Select(boxConsole.Text.Length, boxConsole.Text.Length);
                boxConsole.ScrollToCaret();
            }
        }
        #endregion

        public mainForm() {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e) {
            putsc("Welcome to Minebot!", Color.Blue, "bold");
            putsc("by Umby24", Color.Green, "italic");
        }

        #region Button Presses
        private void connectToolStripMenuItem_Click(object sender, EventArgs e) {
            if (SettingsForm != null)
                return;

            SettingsForm = new Settings(this);
            SettingsForm.Show();
        }

        private void reconnectToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Connected)
                Client.Disconnect();

            lstPlayers.Items.Clear();
            boxConsole.Clear();

            if (Client != null)
                Client.Connect();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Connected) {
                Client.Disconnect();
                DeregisterHandlers();
                Connected = false;
                lstPlayers.Items.Clear();
                puts("Disconnected from server.");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            if (SettingsForm != null)
                SettingsForm.Close();

            if (Connected)
                Client.Disconnect();

            this.Close();
        }
        #endregion
        #region Event Handlers
        public void RegisterHandlers() {
            Client.Message += Client_Message;
            Client.LoginFailure += Client_LoginFailure;
            Client.ErrorMessage += Client_ErrorMessage;
            Client.InfoMessage += Client_InfoMessage;

            Client.JoinedGame += Client_JoinedGame;
            Client.PlayerKicked += Client_PlayerKicked;

            Client.PlayerListitemAdd += Client_PlayerListitemAdd;
            Client.PlayerListitemRemove += Client_PlayerListitemRemove;
            Client.PlayerListitemUpdate += Client_PlayerListitemUpdate;
        }
        
        public void DeregisterHandlers() {
            Client.Message -= Client_Message;
            Client.LoginFailure -= Client_LoginFailure;
            Client.ErrorMessage -= Client_ErrorMessage;
            Client.InfoMessage -= Client_InfoMessage;

            Client.JoinedGame -= Client_JoinedGame;
            Client.PlayerKicked -= Client_PlayerKicked;

            Client.PlayerListitemAdd -= Client_PlayerListitemAdd;
            Client.PlayerListitemRemove -= Client_PlayerListitemRemove;
            Client.PlayerListitemUpdate -= Client_PlayerListitemUpdate;
        }



        void Client_PlayerKicked(string reason) {
            putsc("You were kicked! Reason: " + reason, Color.Red, "bold");
            Client.Disconnect();
            Connected = false;
            DeregisterHandlers();
            lstPlayers.Items.Clear();
        }

        void Client_JoinedGame() {
            putsc("You joined the game!", Color.Green);
        }

        void Client_InfoMessage(object sender, string message) {
            putsc("[Info] " + message, Color.Green);
        }

        void Client_LoginFailure(object sender, string reason) {
            putsc("Login Failure: " + reason, Color.Red, "bold");
        }

        void Client_ErrorMessage(object sender, string message) {
            putsc("An error occured: " + message, Color.Red, "bold");
        }

        void Client_PlayerListitemUpdate(string name, short ping) {
            
        }

        void Client_PlayerListitemRemove(string name) {
            lstPlayers.Items.Remove(name);
        }

        void Client_PlayerListitemAdd(string name, short ping) {
            lstPlayers.Items.Add(name);
        }

        void Client_Message(object sender, string message, string raw) {
            HandleColors_2(message);
        }
        #endregion
        #region Helpers
        private Color GetChatColor(string code) {
            switch (code) {
                case "0":
                    return Color.FromArgb(0, 0, 0);

                case "1":
                    return Color.FromArgb(0, 0, 170);

                case "2":
                    return Color.FromArgb(0, 170, 0);

                case "3":
                    return Color.FromArgb(0, 170, 170);

                case "4":
                    return Color.FromArgb(170, 0, 0);

                case "5":
                    return Color.FromArgb(170, 0, 170);

                case "6":
                    return Color.FromArgb(255, 170, 0);

                case "7":
                    return Color.FromArgb(170, 170, 170);

                case "8":
                    return Color.FromArgb(85, 85, 85);

                case "9":
                    return Color.FromArgb(85, 85, 255);

                case "a":
                    return Color.FromArgb(85, 255, 85);

                case "b":
                    return Color.FromArgb(85, 255, 255);

                case "c":
                    return Color.FromArgb(255, 85, 85);

                case "d":
                    return Color.FromArgb(255, 85, 255);

                case "e":
                    return Color.FromArgb(255, 255, 85);

                case "f":
                    return Color.FromArgb(255, 255, 255);
                default:
                    return Color.FromArgb(255, 255, 255);
            }
        }
        public void HandleColors_2(string text) {
            if (!text.Contains("§")) {
                putsc(text, Color.White);
                return;
            }
            string style = "";

            putsc("", Color.Black); // -- Make a new line

            while (text.Contains("§")) {
                int colorIndex = text.IndexOf("§");
                string code = text.Substring(colorIndex + 1, 1);

                if (colorIndex != 0)
                    putsc(text.Substring(0, colorIndex), Color.White, true);

                text = text.Substring(colorIndex + 2, text.Length - (colorIndex + 2));

                colorIndex = text.IndexOf("§");

                if (colorIndex == -1) {
                    putsc(text, GetChatColor(code.ToLower()), true, style);
                    break;
                }

                putsc(text.Substring(0, colorIndex), GetChatColor(code.ToLower()), true, style);
                text = text.Substring(colorIndex, text.Length - (colorIndex));
            }
        }
        #endregion

        private void btnSend_Click(object sender, EventArgs e) {
            if (Connected) {
                Client.SendChat(boxChat.Text);
                boxChat.Clear();
            }
        }
    }
}
