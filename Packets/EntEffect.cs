using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class EntEffect
    {
        public EntEffect(Wrapped.Wrapped socket, Form1 Mainform)
        {
            socket.readInt();
            socket.readByte();
            socket.readByte();
            socket.readShort();
        }
    }
}
