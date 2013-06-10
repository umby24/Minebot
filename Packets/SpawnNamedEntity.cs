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
            int EID = socket.readInt();
            string Name = socket.readString();
            int X = socket.readInt();
            int Y = socket.readInt();
            int Z = socket.readInt();
            byte Yaw = socket.readByte();
            byte Pitch = socket.readByte();
            short heldItem = socket.readShort();
            functions parser = new functions();
            parser.readMetadata(socket);

            Classes.Entity newPlayer = new Classes.Entity(EID, Name, X, Y, Z, Yaw, Pitch, heldItem);
            mainform.Entitys.Add(newPlayer);
        }
    }
}
