using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
            
            //if (username.ToLower() == mainform.username.ToLower())
            //    return;

            if (Mainform.admins.Contains(username))
            {
                if (args[1].StartsWith(mainform.prefix))
                {
                    switch (args[1].Replace(mainform.prefix, "").ToLower())
                    {
                        case "say":
                            Say(message);
                            break;
                        case "irc":
                            Irc(message);
                            break;
                        case "hold":
                            holdChange(args);
                            break;
                        case "mute":
                            Mute();
                            break;
                        case "yaw":
                            yaw(message);
                            break;
                        case "pitch":
                            pitch(message);
                            break;
                        case "move":
                            move(message);
                            break;
                        case "drop":
                            dropStack(args);
                            break;
                        case "block":
                          //  findBlock(args);
                            test(args);
                            break;
                        case "pos":
                            pos();
                            break;
                    }
                }
            }
        }


        void test(string[] args) {
            // Attempt on an alternitive block lookup method, for speed's sake..
            functions lookup = new functions();

            if (!lookup.isNumeric(args[2]))
                return;
            if (!lookup.isNumeric(args[3]))
                return;
            if (!lookup.isNumeric(args[4]))
                return;

            int blockX = int.Parse(args[2]);
            int blockY = int.Parse(args[3]);
            int blockZ = int.Parse(args[4]);

            decimal ChunkX = decimal.Divide(blockX, 16);
            decimal ChunkZ = decimal.Divide(blockZ, 16);

            ChunkX = Math.Floor(ChunkX);
            ChunkZ = Math.Floor(ChunkZ);

            Classes.Chunk thisChunk = null;
            Classes.MapBlock thisblock = null;

            foreach (Classes.Chunk b in Mainform.Chunks) {
                if (b.x == ChunkX & b.z == ChunkZ) {
                    thisChunk = b;
                    break;
                }
            }

            foreach (Classes.MapBlock b in thisChunk.tBlocks) {
                if (b.Name == "DiamondBlock") {
                    Mainform.puts("found block");
                    Mainform.puts(b.x.ToString() + " " + b.y.ToString() + " " + b.z.ToString());
                }
                if (b.x == blockX & b.y == blockY & b.z == blockZ) {
                    thisblock = b;
                }
            }

            if (thisblock != null) {
                Packets.chatMessage cm = new Packets.chatMessage(true, Socket, Mainform, "FOUND IT. " + thisblock.Name);
            } else {
                Packets.chatMessage cm = new Packets.chatMessage(true, Socket, Mainform, "Fail :(");
            }

        }

        void findBlock(string[] args) {
            functions lookup = new functions();

            if (!lookup.isNumeric(args[2]))
                return;
            if (!lookup.isNumeric(args[3]))
                return;
            if (!lookup.isNumeric(args[4]))
                return;

            Classes.MapBlock thisblock = null;

            foreach (Classes.MapBlock b in Mainform.blocks) {
                if (b.x == int.Parse(args[2])) {
                    if (b.y == int.Parse(args[3])) {
                        if (b.z == int.Parse(args[4])) {
                            thisblock = b;
                            break;
                        }
                    }

                }
            }

            if (thisblock != null) {
                Packets.chatMessage cm = new Packets.chatMessage(true, Socket, Mainform, "FOUND IT. " + thisblock.Name);

            } else {
                Packets.chatMessage cm = new Packets.chatMessage(true, Socket, Mainform, "Fail :(");
            }
        }
        void dropStack(string[] args) {
            functions lookup = new functions();
            Classes.Item thisitem = null;

            if (!lookup.isNumeric(args[2]))
                return;

            foreach (Classes.Item b in Mainform.inventory) {
                if (b.slot == (short.Parse(args[2]))) {
                    thisitem = b;
                    break;
                }
            }

            if (thisitem == null)
                return;

            Packets.ClickWindow click = new Packets.ClickWindow(Socket, Mainform, short.Parse(args[2]), 0, 0, thisitem);
            Packets.ClickWindow click2 = new Packets.ClickWindow(Socket, Mainform, -999, 0, 0, thisitem); //Slot -999 is outside window click, which drops it.
        }

        void holdChange(string[] args) {

            functions test = new functions();

            if (!test.isNumeric(args[2]))
                return;

            if (short.Parse(args[2]) < 0 || short.Parse(args[2]) > 9)
                return;

            Mainform.selectedSlot = short.Parse(args[2]);
            Packets.HeldItemChange hic = new Packets.HeldItemChange(true, Socket, Mainform);
        }

        void pos()
        {
            Packets.chatMessage chat = new Packets.chatMessage(true, Socket, Mainform, "X: " + Mainform.location[0].ToString() + " Y: " + Mainform.location[1].ToString() + " Z: " + Mainform.location[2].ToString());
        }

        void yaw(string text)
        {
            string[] args = text.Split(new char[1] { ' ' }, 3);
            Mainform.position[0] = float.Parse(args[2]);
            Packets.PlayerLook look = new Packets.PlayerLook(Socket, Mainform);
        }

        void pitch(string text)
        {
            string[] args = text.Split(new char[1] { ' ' }, 3);
            Mainform.position[1] = float.Parse(args[2]);
            Packets.PlayerLook look = new Packets.PlayerLook(Socket, Mainform);
        }

        void move(string text)
        {
            string[] args = text.Split(new char[1] { ' ' }, 4);
            string axis = args[2];

            args[3] = args[3].Replace(" ", "");
            float position = float.Parse(args[3]);

            if (position > 10 || -10 > position)
                return;

            switch (axis.ToLower())
            {
                case "x":
                    Mainform.location[0] += position;
                    break;
                case "y":
                    Mainform.location[1] += position;
                    Mainform.location[3] = Mainform.location[1] + 1;
                    break;
                case "z":
                    Mainform.location[2] += position;
                    break;
            }

            Packets.PlayerPosition pos = new Packets.PlayerPosition(Socket, Mainform);
        }

        void Say(string text)
        {
            string[] args = text.Split(new char[1] {' '},3);
            Packets.chatMessage Message = new Packets.chatMessage(true, Socket, Mainform, args[2]);
        }

        void Mute()
        {
            if (Mainform.muted)
            {
                Mainform.muted = false;
                Packets.chatMessage chat = new Packets.chatMessage(true, Socket, Mainform, "Unmuted.");
            }
            else
            {
                Mainform.muted = true;
                Packets.chatMessage chat = new Packets.chatMessage(true, Socket, Mainform, "Muted.");
            }
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
                    Mainform.puts("IRC mode: 0");
                    break;
                case 1:
                    if (0 >= Mainform.ircmode)
                        Mainform.startIRC();
                    else
                        Mainform.ircmessage("IRC to Server only Enabled.");

                    Mainform.puts("IRC mode: 1");
                    Mainform.ircmode = 1;
                    break;
                case 2:
                    if (0 >= Mainform.ircmode)
                        Mainform.startIRC();
                    else
                        Mainform.ircmessage("Server to IRC only enabled.");

                    Mainform.puts("IRC mode: 2");
                    Mainform.ircmode = 2;
                    break;
                case 3:
                    if (0 >= Mainform.ircmode)
                        Mainform.startIRC();
                    else
                        Mainform.ircmessage("Bi-directional mode enabled.");

                    Mainform.puts("IRC mode: 3");
                    Mainform.ircmode = 3;
                    break;
            }

        }
    }
}
