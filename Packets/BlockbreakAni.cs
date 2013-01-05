using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class BlockbreakAni
    {
        public BlockbreakAni(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readInt();
            socket.readInt();
            socket.readInt();
            socket.readInt();
            socket.readByte();
        }
    }
}
