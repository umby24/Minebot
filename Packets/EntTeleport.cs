using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class EntTeleport
    {
        public EntTeleport(Wrapped.Wrapped socket, Form1 Mainform)
        {
            socket.readInt();
            socket.readInt();
            socket.readInt();
            socket.readInt();
            socket.readByte();
            socket.readByte();
        }
    }
}
