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
            int EID = socket.readInt();
            byte type = socket.readByte();
            int X = socket.readInt();
            int Y = socket.readInt();
            int Z = socket.readInt();
            byte Pitch = socket.readByte();
            byte headPitch = socket.readByte();
            byte Yaw = socket.readByte();
            socket.readShort();
            socket.readShort();
            socket.readShort();
            
            functions parser = new functions();
            parser.readMetadata(socket);

            Classes.Entity newMob = new Classes.Entity(EID, ((functions.entityId)type).ToString(), X, Y, Z, Yaw, Pitch, 0);
            Mainform.Entitys.Add(newMob);
        }
    }
}
