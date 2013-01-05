using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class Explosion
    {
        public Explosion(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readDouble();
            socket.readDouble();
            socket.readDouble();
            socket.readFloat();
            int count = socket.readInt();
            while (count != 0)
            {
                socket.readByteArray(3);
                count--;
            }
            socket.readFloat();
            socket.readFloat();
            socket.readFloat();
        }
    }
}
