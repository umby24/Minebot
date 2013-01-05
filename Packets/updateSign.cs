using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class updateSign
    {
        public updateSign(Wrapped.Wrapped socket, Form1 Mainform)
        {
            socket.readInt();
            socket.readShort();
            socket.readInt();
            socket.readString();
            socket.readString();
            socket.readString();
            socket.readString();
        }
    }
}
