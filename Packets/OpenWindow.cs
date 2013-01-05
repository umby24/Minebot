using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class OpenWindow
    {
        public OpenWindow(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readByte();
            socket.readByte();
            socket.readString();
            socket.readByte();
        }
    }
}
