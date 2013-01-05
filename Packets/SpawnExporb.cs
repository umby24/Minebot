using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class SpawnExporb
    {
        public SpawnExporb(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readInt();
            socket.readInt();
            socket.readInt();
            socket.readInt();
            socket.readShort();
        }
    }
}
