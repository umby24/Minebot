using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class EntRelmove
    {
        public EntRelmove(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readInt();
            socket.readByte();
            socket.readByte();
            socket.readByte();
        }

    }
}
