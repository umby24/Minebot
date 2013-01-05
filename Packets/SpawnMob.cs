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
            Minecraft_Net_Interaction parser = new Minecraft_Net_Interaction();
            parser.readMetadata(socket);
        }
    }
}
