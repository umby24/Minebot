using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Winsock_Orcas;
using System.Text.RegularExpressions;
using System.Collections;
using C_Minebot.Classes;

namespace C_Minebot
{
    public partial class Form1 : Form
    {
        #region Variables

        #region Bot Specific
        public string prefix = "+";
        public bool setopen = false;
        public bool muted = false;
        public bool onlineMode;
        public Color windowColor, TextColor, TextAColor;
        public Boolean flatten;
        public Boolean colorize = true;
        public List<string> admins;
        public List<Classes.Item> inventory = new List<Classes.Item>();
        #endregion

        #region Server

        public short health, hunger, selectedSlot;
        public long time, worldAge;
        public int[] spawnPoint;
        public int EntityID;
        public bool onground;
        public float[] position;
        public double[] location;
        public string ServerID, serverHash, sessionId, sip, sport;
      //  public MapBlock[] blocks;
        public List<MapBlock> blocks = new List<MapBlock>();
        public List<Chunk> Chunks = new List<Chunk>();
        #endregion

        #region Encryption
        public byte[] PublicKey, token;
        public byte[] sharedkey = new byte[16];
        #endregion

        #region Network
        public string username;
        public networkHandler nh;
        #endregion

        #region IRC Bot

        public Winsock_Orcas.Winsock IRCSock;
        public int ircmode = 0;
        public int ircPort;
        public string channel, ircname, ircIP;
        public bool canTalk = false;

        #endregion

        #endregion
        #region Colorized Chatbox
        public const int EM_GETLINECOUNT = 0xBA;
        public const int EM_LINESCROLL = 0xB6;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        #endregion

        public Form1()
        {
            InitializeComponent();
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

            // Load IRC settings
            try
            {
                ircIP = (string)Reg.GetSetting("SH", "Minebot SMP", "ircIP", "irc.esper.net");

                int.TryParse((string)Reg.GetSetting("SH", "Minebot SMP", "ircPort", 6667), out ircPort);
                channel = (string)Reg.GetSetting("SH", "Minebot SMP", "ircChan", "#bot");
                ircname = (string)Reg.GetSetting("SH", "Minebot SMP", "ircName", "VBMinebot");
            }
            catch (Exception f)
            {
                MessageBox.Show("An error occured while loading settings. " + f.Message);
            }

            // Load command prefix
            RegistryControl reg = new RegistryControl();
            prefix = (string)reg.GetSetting("SH", "Minebot SMP", "prefix", "+");

            admins.Add("Minebot");

            putsc("=_=_=_= C# Minebot =_=_=_=", Color.Yellow);
            putsc("=+=+=+= Version 1.0 =+=+=+=", Color.Blue);
            putsc("======= by Umby24 ========", Color.Red);
            putsc("-------- All settings loaded ---------", Color.Orange);

        }
        private void Form1_Close(object sender, EventArgs e)
        {
            if (nh != null)
            {
                Packets.Kick leaving = new Packets.Kick(true, nh.socket, this);
                System.Threading.Thread.Sleep(200);
                nh.stop();
            }
            
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

            lstOnline.Items.Clear();
            username = UN;
            onlineMode = online;
            sip = IP;
            sport = port;

            if (online && sessionId == null)
            {
                puts("Logging in to Minecraft.net...");

                Minecraft_Net_Interaction Login = new Minecraft_Net_Interaction();

                string Response = Login.Login(UN, PW);

                if (Response.Contains(':'))
                {
                    string[] mysplit = Response.Split(':');
                    username = mysplit[2];
                    sessionId = mysplit[3];
                    puts("Done.");
                }
                else
                {
                    if (sessionId != null)
                    {
                        puts("Error logging in to Minecraft.net!");
                        puts(Response);
                    }
                    else
                    {
                        puts("Attempting to log in with old credentials..");
                    }
                }
            }
            else
                sessionId = "epicw1n";

            //Begin connection to server.
            nh = new networkHandler(IP, port, this);
            nh.start();
        }
        #endregion
        #region Threadsafe

        private delegate void putss(string text);
        private delegate void putsc_(string text, Color tColor, bool append);

        public void putsc(string text, Color tColor, bool append)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new putsc_(putsc), text, tColor, append);
            }
            else {
                if (append == true)
                {
                    int intLines = SendMessage(console.Handle, EM_GETLINECOUNT, 0, 0);
                    console.AppendText(text);
                    console.Select(console.Text.Length - text.Length, text.Length);
                    console.SelectionColor = tColor;
                    int linesToAdd = (SendMessage(console.Handle, EM_GETLINECOUNT, 0, 0) - intLines);
                    SendMessage(console.Handle, EM_LINESCROLL, 0, linesToAdd);
                    return;
                }
                int aintLines = SendMessage(console.Handle, EM_GETLINECOUNT, 0, 0);
                console.AppendText(Environment.NewLine);
                console.AppendText(text);
                console.Select(console.Text.Length - text.Length, text.Length);
                console.SelectionColor = tColor;
                int blinesToAdd = (SendMessage(console.Handle, EM_GETLINECOUNT, 0, 0) - aintLines);
                SendMessage(console.Handle, EM_LINESCROLL, 0, blinesToAdd);
            }

        }
        public void putsc(string text, Color tColor)
        {
            bool append = false;
            if (this.InvokeRequired)
            {
                this.Invoke(new putsc_(putsc), text, tColor, append);
            }
            else
            {
                if (append == true)
                {
                    int intLines = SendMessage(console.Handle, EM_GETLINECOUNT, 0, 0);
                    console.AppendText(text);
                    console.Select(console.Text.Length - text.Length, text.Length);
                    console.SelectionColor = tColor;
                    int linesToAdd = (SendMessage(console.Handle, EM_GETLINECOUNT, 0, 0) - intLines);
                    SendMessage(console.Handle, EM_LINESCROLL, 0, linesToAdd);
                    return;
                }
                int aintLines = SendMessage(console.Handle, EM_GETLINECOUNT, 0, 0);
                console.AppendText(Environment.NewLine);
                console.AppendText(text);
                console.Select(console.Text.Length - text.Length, text.Length);
                console.SelectionColor = tColor;
                int blinesToAdd = (SendMessage(console.Handle, EM_GETLINECOUNT, 0, 0) - aintLines);
                SendMessage(console.Handle, EM_LINESCROLL, 0, blinesToAdd);
            }
        }
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
                if (lstOnline.Items.Contains(name) == false)
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
                if (lstOnline.Items.Contains(name) == false)
                    lstOnline.Items.Remove(name);
            }
        }
        #endregion
        #region IRC

        public void stopIRC()
        {
            IRCSock.Close();
        }
        public void startIRC()
        {
            RegistryControl Reg = new RegistryControl();

            ircIP = (string)Reg.GetSetting("SH", "Minebot SMP", "ircIP", "irc.esper.net");

            int.TryParse((string)Reg.GetSetting("SH", "Minebot SMP", "ircPort", 6667), out ircPort);
            channel = (string)Reg.GetSetting("SH", "Minebot SMP", "ircChan", "#bot");
            ircname = (string)Reg.GetSetting("SH", "Minebot SMP", "ircName", "VBMimebot");

            IRCSock = new Winsock();
            IRCSock.BufferSize = 8192;
            IRCSock.LegacySupport = true;
            IRCSock.Protocol = Winsock_Orcas.WinsockProtocol.Tcp;
            IRCSock.Connected += new Winsock_Orcas.IWinsock.ConnectedEventHandler(IRCSock_Connected);
            IRCSock.DataArrival += new Winsock_Orcas.IWinsock.DataArrivalEventHandler(IRCSock_DataArrival);
            IRCSock.Connect(ircIP, ircPort);

            puts(Environment.NewLine + "Connecting.");
        }
        public void IRCSock_Connected(object sender, WinsockConnectedEventArgs e)
        {
            send("NICK " + ircname);
            send("USER C C C :" + ircname);
            send("MODE " + ircname + " +i");
            send("JOIN " + channel);
            puts("Connected.");
        }
        public string translate_colors(string text)
        {
            string smessage = text;

            if (smessage.Contains("§"))
            {
                smessage = smessage.Replace("§0", ((Char) 3) + "00");
                smessage = smessage.Replace("§1", ((Char)3) + "02");
                smessage = smessage.Replace("§2", ((Char)3) + "03");
                smessage = smessage.Replace("§3", ((Char)3) + "10");
                smessage = smessage.Replace("§4", ((Char)3) + "05");
                smessage = smessage.Replace("§5", ((Char)3) + "06");
                smessage = smessage.Replace("§6", ((Char)3) + "07");
                smessage = smessage.Replace("§7", ((Char)3) + "15");
                smessage = smessage.Replace("§8", ((Char)3) + "14");
                smessage = smessage.Replace("§9", ((Char)3) + "12");
                smessage = smessage.Replace("§a", ((Char)3) + "09");
                smessage = smessage.Replace("§b", ((Char)3) + "11");
                smessage = smessage.Replace("§c", ((Char)3) + "04");
                smessage = smessage.Replace("§d", ((Char)3) + "13");
                smessage = smessage.Replace("§e", ((Char)3) + "08");
                smessage = smessage.Replace("§f", ((Char)3) + "01");
                smessage = smessage.Replace("§A", ((Char)3) + "09");
                smessage = smessage.Replace("§B", ((Char)3) + "11");
                smessage = smessage.Replace("§C", ((Char)3) + "04");
                smessage = smessage.Replace("§D", ((Char)3) + "13");
                smessage = smessage.Replace("§E", ((Char)3) + "08");
                smessage = smessage.Replace("§F", ((Char)3) + "01");
            }

            return smessage;
        }
        public void IRCSock_DataArrival(object sender, WinsockDataArrivalEventArgs e)
        {

            string incoming = Encoding.UTF8.GetString((byte[])IRCSock.Get());
            string host = "";
            string dat = "";
            string message = "";
            string[] splits;
            int index = 0;

            splits = incoming.Split('\r','\n');

            for (int b = 0; (splits.Length - 1) >= b;b++)
            {
                splits[b] = splits[b].Replace("\r\n","");
                splits[b] = splits[b].Replace("\n","");
                splits[b] = splits[b].Replace("\r","");
            }

            do
            {

                incoming = splits[index];

                if (incoming.Contains(" "))
                {

                    if (incoming.Contains("/NAMES list"))
                        canTalk = true;

                    if (incoming.Substring(0, 1) == ":")
                        host = incoming.Substring(1, incoming.IndexOf(" ") - 1);
                    else
                        host = incoming.Substring(0, incoming.IndexOf(" "));

                    dat = incoming.Substring(incoming.IndexOf(" ") + 1, incoming.Length - (incoming.IndexOf(" ") + 1));

                    if (dat.Contains(":"))
                        message = dat.Substring(dat.IndexOf(":") + 1, dat.Length - (dat.IndexOf(":") + 1));

                    if (host == "PING")
                    {
                        send("PONG " + dat);
                        return;
                    }

                    string second = dat.Substring(0, dat.IndexOf(" "));
                    string[] mysplits = dat.Split(new string[] { " " }, 4, StringSplitOptions.None);

                    message = message.Replace("\r\n", "");

                    switch (second)
                    {
                        case "PRIVMSG":
                            if (ircmode == 1 || ircmode == 3 && message.StartsWith("+") == false)
                            {
                                nh.socket.writeByte(3);
                                nh.socket.writeString("IRC: <" + host.Substring(0, host.IndexOf("!")) + "> " + message);
                            }
                            if (message.StartsWith("="))
                            {
                                string cmd = mysplits[2].Substring(1, mysplits[2].Length - 1);

                                switch (cmd.ToLower())
                                {
                                    case "=say":
                                        if (mysplits.Length > 3)
                                        ircmessage(mysplits[3]);
                                        break;
                                    case "=ssay":
                                        Packets.chatMessage mypacket;
                                        if (mysplits.Length > 3)
                                             mypacket = new Packets.chatMessage(true, nh.socket, this, mysplits[3]);
                                        break;
                                    case "=help":
                                        ircmessage("C# Minebot IRC Client, Version 1.0");
                                        ircmessage("Only functional to relay messages between IRC channels and minecraft servers.");
                                        ircmessage("Commands are =say, =ssay, and =help.");
                                        break;
                                }
                            }

                            break;
                        case "376":
                            send("JOIN " + channel);
                            ircmessage("Current mode: " + ircmode);
                            break;
                    }
                }
                index++;
            } while (index == splits.Length - 1);

            incoming = "";
        }

        string stripillegal2(string text)
        {
            string final = "";

            foreach (char b in text)
            {
                if ((Char.IsLetterOrDigit(b) && (b == '!' || b == '@' || b == '#' || b == '$' || b == '%' || b == '*' || b == '(' || b == ')' || b == '=' || b == '-' || b == '_' || b == '+' || b == '/' || b == '\\' || b == '<' || b == '>' || b == '?' || b == '.' || b == ',' || b == ' ')))
                {
                    final += b;
                }
            }

            return final;
        }
        public void send(string msg)
        {
            if (IRCSock == null)
                return;

            if (IRCSock.State == WinsockStates.Connected)
            {
                msg += "\r\n";
                byte[] data = System.Text.ASCIIEncoding.UTF8.GetBytes(msg);
                IRCSock.Send(data);
            }
        }
        public void ircmessage(string text)
        {
            send("PRIVMSG " + channel + " :" + text);
        }
        #endregion
        #region Button Clicks

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the official successor to VB Minebot, written so I could learn C#, and to get this project up again, but cleaner.", "About C# Minebot");
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setopen == false)
            {
                Settings whatever = new Settings();
                whatever.myform = this;
                whatever.Show();
                setopen = true;
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (chat.Text.StartsWith("+"))
            {
                commandHandler ch = new commandHandler(nh.socket, this, "Minebot: " + chat.Text);
                chat.Clear();
                return;
            }

            Packets.chatMessage chatmess = new Packets.chatMessage(true, nh.socket, this, chat.Text);
            chat.Clear();
        }

        private void muteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (muted)
            {
                muted = false;
                muteToolStripMenuItem.Checked = false;
            }
            else
            {
                muted = true;
                muteToolStripMenuItem.Checked = true;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setopen == false)
            {
                Settings whatever = new Settings();
                whatever.myform = this;
                whatever.Show();
                setopen = true;
            }
        }

        private void reconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nh != null)
            {
                nh.stop();
                if (sip != null)
                    beginConnect(sip, sport, username, "asdf", onlineMode);
            }
        }

        private void asdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            functions thisLookup = new functions();
            string build = "";

            for (int i = 0; i < 44; i++) {
                build += i.ToString() + ": " + thisLookup.getitembyslot(i,this) +  "(" + thisLookup.getItemCount(i,this) + ")" + Environment.NewLine;
            }
            MessageBox.Show(build);
        }
        #endregion

        private void parseToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Threading.Thread newthread = new System.Threading.Thread(parsethem);
            newthread.Start();
          //  parsethem();
        }

        private void parsethem() {
            foreach (Chunk b in Chunks) {
                b.parseBlocks(this);
            }
            puts("DONE PARSING");
        }
    }
}
