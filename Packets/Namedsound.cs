using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class Namedsound
    {
        public Namedsound(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readString();
            socket.readInt();
            socket.readInt();
            socket.readInt();
            socket.readFloat();
            socket.readByte();
        }
    }
}
