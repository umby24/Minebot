using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class updateWindowProperty
    {
        public updateWindowProperty(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readByte();
            socket.readShort();
            socket.readShort();
        }
    }
}
