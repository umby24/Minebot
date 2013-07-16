using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class incStat
    {
        public incStat(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readInt();
            socket.readInt();
        }
    }
}
