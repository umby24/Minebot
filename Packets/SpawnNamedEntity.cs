using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class SpawnNamedEntity
    {
        public SpawnNamedEntity(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readInt();
            socket.readString();
            socket.readInt();
            socket.readInt();
            socket.readInt();
            socket.readByte();
            socket.readByte();
            socket.readShort();
            Minecraft_Net_Interaction parser = new Minecraft_Net_Interaction();
            parser.readMetadata(socket);
        }
    }
}
