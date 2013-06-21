using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class CloseWindow
    {
        public CloseWindow(Wrapped.Wrapped socket, Form1 mainform, bool outgoing = false)
        {
            if (outgoing)
            {
                socket.writeByte(0x65);
                socket.writeByte(0);
            }
            else
            {
                socket.readByte();
            }
            
        }
    }
}
