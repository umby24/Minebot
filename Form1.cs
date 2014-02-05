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

using libMC.NET;
using libMC.NET.Packets.Play.ServerBound;

using CBOT.Classes;

namespace CBOT {
    public partial class mainForm : Form {
        #region Variables
        public Thread luaThread;
        public LuaWrapper luaHandler;
        public bool connected = false;
        public Minecraft MinecraftServer;
        public string prefix = "+";
        #region Follow Command
        public bool Following = false;
        public int Follow_ID = 0;
        #endregion
        #region Commands
        public Dictionary<string, Command> Commands;
        public List<string> AccessList;
        #endregion

        #region Colorized Chatbox
        public const int EM_GETLINECOUNT = 0xBA;
        public const int EM_LINESCROLL = 0xB6;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        #endregion

        #endregion

        public mainForm() {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e) {
            puts("Welcome to Minebot!");

            luaHandler = new LuaWrapper(this); // -- initalize Lua

            luaThread = new Thread(luaHandler.Lua_Main); // -- Begin script loading/reloading loop.
            luaThread.Start();

            puts("Lua initilized");

            Commands = new Dictionary<string, Command> {
                {"+luarun", new LuaRun()},
                {"+getblock", new GetBlock()},
                {"+hold", new Hold()},
                {"+say", new Say()},
                {"+follow", new Follow()}
            }; // -- Initilize bot command databases.

            // -- Initilize bot accesslist, and load it from file.

            AccessList = new List<string>();

            settingsReader SR = new settingsReader("admin.txt", true);

            if (!File.Exists("admin.txt")) {
                SR.settings = new Dictionary<string, string>();
                SR.settings.Add("admins", "");
                SR.saveSettings();
            }

            SR.readSettings();

            if (SR.settings["admins"] != null) {
                string[] admins = SR.settings["admins"].Split('|');
                foreach (string b in admins) {
                    AccessList.Add(b);
                }
            }

            puts("Access list loaded");
        }
        void mainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
            if (connected)
                MinecraftServer.Disconnect();

            luaThread.Abort();
        }

        #region Button Clicks
        private void connectToolStripMenuItem_Click(object sender, EventArgs e) {
            Settings settingsWindow = new Settings(this);
            settingsWindow.Show();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e) {
            if (connected) {
                MinecraftServer.Disconnect();
                DeregisterHandlers();
                connected = false;
                lstPlayers.Items.Clear();
                puts("Disconnected from server.");
            }
        }

        private void reconnectToolStripMenuItem_Click(object sender, EventArgs e) {
            if (connected)
                MinecraftServer.Disconnect();

            lstPlayers.Items.Clear();
            boxConsole.Clear();

            if (MinecraftServer != null)
                MinecraftServer.Connect();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            //if (connected)
            //    MinecraftServer.Disconnect();

            //this.Close();
            MinecraftServer.ThisPlayer.location.x = MinecraftServer.ThisPlayer.location.x + 2;
            PlayerPosition pp = new PlayerPosition(ref MinecraftServer);
        }
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
        #region Helping Functions

        /// <summary>
        /// Registers all event handlers for the bot.
        /// </summary>
        public void RegisterHandlers() {
            MinecraftServer.Message += MinecraftServer_message;
            MinecraftServer.PlayerListitemAdd += MinecraftServer_playerListitemAdd;
            MinecraftServer.PlayerListitemRemove += MinecraftServer_playerListitemRemove;
            MinecraftServer.LoginSuccess += MinecraftServer_loginSuccess;
            MinecraftServer.SetPlayerHealth += MinecraftServer_setPlayerHealth;
            // MinecraftServer.DebugMessage += MinecraftServer_DebugMessage;
            MinecraftServer.InfoMessage += MinecraftServer_InfoMessage;
            MinecraftServer.PlayerKicked += MinecraftServer_PlayerKicked;
            MinecraftServer.EntityRelMove += MinecraftServer_entityRelMove;
            MinecraftServer.EntityTeleport += MinecraftServer_entityTeleport;

            putsc("Handlers Registered!", Color.Green);
        }

        void MinecraftServer_entityTeleport(int Entity_ID, int X, int Y, int Z) {
            if (Following && (Entity_ID == Follow_ID)) {
                MinecraftServer.ThisPlayer.location.x = X;
                MinecraftServer.ThisPlayer.location.y = Y;
                MinecraftServer.ThisPlayer.location.z = Z;

                var PlayerPosition = new libMC.NET.Packets.Play.ServerBound.PlayerPositionAndLook(ref MinecraftServer);
            }
        }

        void MinecraftServer_entityRelMove(int Entity_ID, int Change_X, int Change_Y, int Change_Z) {
            if (Following && (Entity_ID == Follow_ID)) {
                MinecraftServer.ThisPlayer.location.x += Change_X;
                MinecraftServer.ThisPlayer.location.y += Change_Y;
                MinecraftServer.ThisPlayer.location.z += Change_Z;

                var PlayerPosition = new libMC.NET.Packets.Play.ServerBound.PlayerPositionAndLook(ref MinecraftServer);
            }
        }

        void MinecraftServer_PlayerKicked(string reason) {
            putsc("You have been kicked! Reason: " + reason, Color.Red);
            MinecraftServer.Disconnect();
            DeregisterHandlers();
            connected = false;
            lstPlayers.Items.Clear();
            puts("Disconnected from server.");
        }

        public void DeregisterHandlers() {
            MinecraftServer.Message -= MinecraftServer_message;
            MinecraftServer.PlayerListitemAdd -= MinecraftServer_playerListitemAdd;
            MinecraftServer.PlayerListitemRemove -= MinecraftServer_playerListitemRemove;
            MinecraftServer.LoginSuccess -= MinecraftServer_loginSuccess;
            MinecraftServer.SetPlayerHealth -= MinecraftServer_setPlayerHealth;
            //MinecraftServer.DebugMessage -= MinecraftServer_DebugMessage;
            MinecraftServer.InfoMessage -= MinecraftServer_InfoMessage;
            MinecraftServer.PlayerKicked -= MinecraftServer_PlayerKicked;
            MinecraftServer.EntityRelMove += MinecraftServer_entityRelMove;
            MinecraftServer.EntityTeleport += MinecraftServer_entityTeleport;

            putsc("Handlers DeRegistered!", Color.Red);
        }
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
                        putsc(text.Substring(0, colorIndex), Color.White,true);
                    
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

        #region Event Handlers
        void MinecraftServer_InfoMessage(object sender, string message) {
            putsc("[INFO]: " + message, Color.Green);
        }

        void MinecraftServer_DebugMessage(object sender, string message) {
            putsc("[DEBUG]: " + message, Color.Red);
        }

        void MinecraftServer_setPlayerHealth(float health, short hunger, float saturation) {
            putsc("[DEBUG] Health update! " + health.ToString(), Color.Red);

            if (0 >= health) {
                // -- Respawn
                var RespawnPacket = new ClientStatus(ref MinecraftServer, 0);
                putsc("[DEBUG] Respawned!", Color.Red);
                var PlayerPacket = new Player(ref MinecraftServer);
                var PlayerPacket1 = new Player(ref MinecraftServer);
                var PlayerPacket2 = new Player(ref MinecraftServer);
                var PlayerPacket3 = new Player(ref MinecraftServer);
            }
        }
        void MinecraftServer_message(object sender, string message, string name) {
            if (message.StartsWith(prefix)) {  // -- Handle commands.
                if (!AccessList.Contains(name))
                    return;

                if (!message.Contains(" "))
                    message = message + " ";

                string command = message.Substring(0, message.IndexOf(" "));
                string[] splits = message.Substring(message.IndexOf(" ") + 1, message.Length - (message.IndexOf(" ") + 1)).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string afterMessage = message.Substring(message.IndexOf(" ") + 1, message.Length - (message.IndexOf(" ") + 1));

                if (Commands.ContainsKey(command.ToLower()) == false)
                    ChatMessage.SendChat(MinecraftServer, "Command not found!");
                else {
                    var tCommand = Commands[command.ToLower()];
                    tCommand.run(command, splits, afterMessage, name, this);
                }                   
            }

            if (name != "EVENT")
                HandleColors_2("<" + name + "> " + message);
            else
                HandleColors_2(message);
        }

        void MinecraftServer_loginSuccess(object sender) {
            luaHandler.Run_Lua_Function("LoginSuccess",null);
        }

        void MinecraftServer_playerListitemRemove(string name) {
            if (this.InvokeRequired) {
                this.Invoke(new PlayerListRemove(MinecraftServer_playerListitemRemove), name);
                return;
            }
            luaHandler.Run_Lua_Function("Player_Logout", new[] { name });
            lstPlayers.Items.Remove(name);
        }

        void MinecraftServer_playerListitemAdd(string name, short ping) {
            if (this.InvokeRequired) {
                this.Invoke(new PlayerListAdd(MinecraftServer_playerListitemAdd), name, ping);
                return;
            }

            luaHandler.Run_Lua_Function("Player_Login", new[] { name });
            lstPlayers.Items.Add(name);
        }
        #endregion

        #region Thread-Safe Delegates
        private delegate void PlayerListRemove(string name);
        private delegate void PlayerListAdd(string name, short ping);
        #endregion

        public void AddCommand(string name, string lua_function, string help) {
            name = name.ToLower();
            if (Commands.ContainsKey(prefix + name))
                Commands.Remove(prefix + name);

            ScriptedCommand newCommand = new ScriptedCommand(prefix + name, "Lua:" + lua_function, help);
            Commands.Add(prefix + name, newCommand);
        }
        public void Send_Message(string message) {
            if (connected) {
                ChatMessage.SendChat(MinecraftServer, message);
            }
        }
        private void btnSend_Click(object sender, EventArgs e) {
            ChatMessage.SendChat(MinecraftServer, boxChat.Text);
            boxChat.Clear();
        }
    }
}
