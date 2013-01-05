using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class Entity
    {
        public Entity(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readInt();
        }
    }
}
