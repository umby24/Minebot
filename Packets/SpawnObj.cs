using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class SpawnObj
    {
        public SpawnObj(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readInt();
            socket.readByte();
            socket.readInt();
            socket.readInt();
            socket.readInt();
            socket.readByte();
            socket.readByte();
            // --
            int value = socket.readInt();

            if (value != 0)
            {
            socket.readShort();
            socket.readShort();
            socket.readShort();
            }
        }
    }
}
