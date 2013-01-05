using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class EntVelocity
    {
        public EntVelocity(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readInt();
            socket.readShort();
            socket.readShort();
            socket.readShort();
        }
    }
}
