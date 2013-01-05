using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class SetExp
    {
        public SetExp(Wrapped.Wrapped socket, Form1 Mainform)
        {
            socket.readFloat();
            socket.readShort();
            socket.readShort();
        }
    }
}
