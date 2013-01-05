using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class ChunkData
    {
        public ChunkData(Wrapped.Wrapped socket, Form1 Mainform)
        {
            socket.readInt();
            socket.readInt();
            socket.readBool();
            socket.readShort();
            socket.readShort();
            int size = socket.readInt();
            socket.readByteArray(size);
        }
    }
}
