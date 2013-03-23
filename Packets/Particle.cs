using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets {
    class Particle {

        public Particle(Wrapped.Wrapped Socket, Form1 mainform) {
             // Server to client only
            string particleName = Socket.readString();
            float X = Socket.readFloat();
            float Y = Socket.readFloat();
            float Z = Socket.readFloat();
            float OffsetX = Socket.readFloat();
            float OffsetY = Socket.readFloat();
            float OffsetZ = Socket.readFloat();
            float particleSpeed = Socket.readFloat();
            int number = Socket.readInt();
        }
    }
}
