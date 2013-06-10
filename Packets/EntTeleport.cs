using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class EntTeleport
    {
        public EntTeleport(Wrapped.Wrapped socket, Form1 Mainform)
        {
            int EID = socket.readInt();
            int X = socket.readInt();
            int Y = socket.readInt();
            int Z = socket.readInt();
            byte yaw = socket.readByte();
            byte pitch = socket.readByte();
            functions helper = new functions();

            Classes.Entity thisEnt = helper.getEntbyID(EID, Mainform);

            if (thisEnt != null) {
                thisEnt.X = X ;
                thisEnt.Y = Y;
                thisEnt.Z = Z;
                thisEnt.yaw = yaw;
                thisEnt.pitch = pitch;

                if (Mainform.following == true && thisEnt.name == Mainform.fname) {
                    Mainform.location[0] = thisEnt.X / 32;
                    Mainform.location[1] = thisEnt.Y / 32;
                    Mainform.location[2] = thisEnt.Z / 32;
                    Mainform.location[3] = thisEnt.Y / 32 + 1;
                }
            }
        }
    }
}
