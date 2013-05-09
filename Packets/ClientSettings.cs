using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class ClientSettings
    {
        public ClientSettings(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.writeByte(0xcc);
            socket.writeString("en_US");
            socket.writeByte(1);
            socket.writeByte(4);
            socket.writeByte(2);
            socket.writeBool(false);
        }
    }
}
