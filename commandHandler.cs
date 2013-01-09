using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot
{
    class commandHandler
    {
        Wrapped.Wrapped Socket;
        Form1 Mainform;

        public commandHandler(Wrapped.Wrapped socket, Form1 mainform, string message)
        {
            functions func = new functions();

            Socket = socket;
            Mainform = mainform;

            string[] args = message.Split(' ');
            string username = func.strip_codes(args[0].Replace("<", "").Replace(">", "").Replace(":", "").Replace(" ", ""));
            
            if (username.ToLower() == mainform.username.ToLower())
                return;

            if (Mainform.admins.Contains(username))
            {
                if (args[1].StartsWith("+"))
                {
                    switch (args[1].Replace("+", "").ToLower())
                    {
                        case "say":
                            Say(message);
                            break;
                        case "irc":
                            Irc(message);
                            break;
                        case "lol":
                            lol();
                            break;
                    }
                }
            }
        }

        void Say(string text)
        {
            string[] args = text.Split(new char[1] {' '},3);

            Packets.chatMessage Message = new Packets.chatMessage(true, Socket, Mainform, args[2]);
        }
        void Irc(string text)
        {
            string[] args = text.Split(new char[1] { ' ' }, 3);

            int mode;
            int.TryParse(args[2].Replace(" ", ""), out mode);
            switch (mode)
            {
                case 0:
                    if (Mainform.ircmode > 0)
                        Mainform.stopIRC();
                    Mainform.ircmode = 0;
                    break;
                case 1:
                    if (0 >= Mainform.ircmode)
                        Mainform.startIRC();
                    else
                        Mainform.ircmessage("IRC to Server only Enabled.");
                    Mainform.ircmode = 1;
                    break;
                case 2:
                    if (0 >= Mainform.ircmode)
                        Mainform.startIRC();
                    else
                        Mainform.ircmessage("Server to IRC only enabled.");
                    Mainform.ircmode = 2;
                    break;
                case 3:
                    if (0 >= Mainform.ircmode)
                        Mainform.startIRC();
                    else
                        Mainform.ircmessage("Bi-directional mode enabled.");
                    Mainform.ircmode = 3;
                    break;
            }

        }
        void lol()
        {
            Mainform.send("JOIN " + Mainform.channel);
            Mainform.ircmessage("Current mode: " + Mainform.ircmode);
        }
    }
}
