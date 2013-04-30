using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class Respawn
    {
        Wrapped.Wrapped socket;
        Form1 mainform;

        public Respawn(Wrapped.Wrapped Socket, Form1 Mainform)
        {
            socket = Socket;
            mainform = Mainform;
            handle();

        }

        void handle()
        {
            int Dimention = socket.readInt();
            byte Difficulty = socket.readByte();
            byte mode = socket.readByte();
            short height = socket.readShort();
            string leveltype = socket.readString();

            mainform.Chunks.Clear(); // Clear out the chunks.
        }
    }
}
