using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class SpawnPainting
    {
        public SpawnPainting(Wrapped.Wrapped socket, Form1 Mainform)
        {
            socket.readInt();
            socket.readString();
            socket.readInt();
            socket.readInt();
            socket.readInt();
            socket.readInt();
        }
    }
}
