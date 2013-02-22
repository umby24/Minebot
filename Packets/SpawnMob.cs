using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class SpawnMob
    {
        public SpawnMob(Wrapped.Wrapped socket, Form1 Mainform)
        {
            socket.readInt();
            socket.readByte();
            socket.readInt();
            socket.readInt();
            socket.readInt();
            socket.readByte();
            socket.readByte();
            socket.readByte();
            socket.readShort();
            socket.readShort();
            socket.readShort();
            functions parser = new functions();
            parser.readMetadata(socket);
        }
    }
}
