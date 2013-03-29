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
        Thread excersist;

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
                        case "ircjoin":
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
                        case "pos":
                            pos();
                            break;
                        case "omfg":
                            omfg(false);
                            break;
                        case "omfgstop":
                            omfg(true);
                            break;
                        case "block":
                            whatblock(args);
                            break;
                    }
                }
            }
        }


        void dropStack(string[] args) {
            functions lookup = new functions();
            Classes.Item thisitem = null;

            foreach (Classes.Item b in Mainform.inventory) {
                if (b.slot == (short.Parse(args[2]))) {
                    thisitem = b;
                    break;
                }
            }

            Packets.ClickWindow click = new Packets.ClickWindow(Socket, Mainform, short.Parse(args[2]), 0, 0, thisitem);
            Packets.ClickWindow click2 = new Packets.ClickWindow(Socket, Mainform, -999, 0, 0, thisitem);
        }
        void whatblock(string[] args)
        {
            Mainform.puts("'" + args[1] + "'");

            int thisX = int.Parse(args[2]);
            int thisy = int.Parse(args[3]);
            int thisZ = int.Parse(args[4]);

            foreach (Classes.Chunk c in Mainform.Chunks)
            { //  & (c.z <= thisZ) & (thisZ <= (c.z + 16))
                if (c.x == 4 & c.z == -2)
                {
                    Mainform.puts("asdasdasd");

                    for (byte i = 0; i < 16; i++)
                    {
                        if (Convert.ToBoolean(c.pbitmap & (1 << i)))
                        {
                            for (int f = 0; f < 4096; f++)
                            {

                                // in classic; (z * mapy + y) * mapx + x
                                int blockXa = c.x * 16 + (f & 0x0F);
                                int blockX = f & 0x0F;
                                int blockY = i * 16 + (f >> 8);
                                int blockZ = (f & 0xF0) >> 4;

                                int blockYa = i * 16 + (blockXa >> 8);
                                int blockZa = c.z * 16 + (f & 0xF0) >> 4;
                                int blockID = c.blocks[(blockX + blockZ * 16 + blockY * 256)]; //  return x + z*depth + y*ystep;
                                // depth = 16, width = 16, ystep = width * depth ( 256)

                                if (((int)blockXa == (int)thisX))
                                {
                                    if ((int)blockYa == (int)thisy)
                                    {
                                        if ((int)blockZa == (int)thisZ)
                                        {
                                    Mainform.puts("Got it! it is " + blockID.ToString());
                                        }
                                    }
                                }
                                //MapBlock myBlock = new MapBlock(blockID, blockX, blockY, blockZ);

                                //if (Mainform.blocks == null)
                                //    Mainform.blocks = new MapBlock[] { myBlock };
                                //else
                                //{
                                //    MapBlock[] temp = Mainform.blocks;
                                //    Mainform.blocks = new MapBlock[temp.Length + 1];
                                //    Array.Copy(temp, Mainform.blocks, temp.Length);
                                //    Mainform.blocks[temp.Length] = myBlock;
                                //}

                            }
                        }
                    }
                }
            }
            //foreach (Classes.MapBlock b in Mainform.blocks)
            //{
            //    if (b.x.ToString() == args[1] & b.y.ToString() == args[2] & b.z.ToString() == args[3])
            //    {
            //        Packets.chatMessage chat = new Packets.chatMessage(true, Socket, Mainform, "Block: " + b.ID.ToString());
            //    }
            //}
        }

        void holdChange(string[] args) {
            if (short.Parse(args[2]) < 0 || short.Parse(args[2]) > 9)
                return;
            Mainform.selectedSlot = short.Parse(args[2]);
            Packets.HeldItemChange hic = new Packets.HeldItemChange(true, Socket, Mainform);
        }

        void ircjoin()
        {
            Mainform.send("JOIN " + Mainform.channel);
        }
        void omfg(bool stop)
        {
            // <SinZ>in a thread, for(i=0,360) do pitch=i, sleep(1) end
            if (stop)
            {
                excersist.Abort();
            }
            else
            {
                excersist = new Thread(datthread);
                excersist.Start();
            }
        }
        void datthread()
        {
            while (true)
            {
                for (int i = 0; 361 > i; i++)
                {
                    Mainform.position[1] = (float)i;
                    Packets.PlayerLook look = new Packets.PlayerLook(Socket, Mainform);
                    Thread.Sleep(100);
                }
            }
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
    }
}
