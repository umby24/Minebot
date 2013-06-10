using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class EntlookRelmove
    {
        public EntlookRelmove(Wrapped.Wrapped socket, Form1 mainform)
        {
            int EID = socket.readInt();
            sbyte dx = (sbyte)socket.readByte();
            sbyte dy = (sbyte)socket.readByte();
            sbyte dz = (sbyte)socket.readByte();
            byte yaw = socket.readByte();
            byte pitch = socket.readByte();
            functions helper = new functions();

            Classes.Entity thisEnt = helper.getEntbyID(EID, mainform);

            if (thisEnt != null) {
                thisEnt.yaw = yaw;
                thisEnt.pitch = pitch;
                thisEnt.X += dx;
                thisEnt.Y += dy;
                thisEnt.Z += dz;


                if (mainform.following == true && thisEnt.name == mainform.fname) {
                    mainform.location[0] = thisEnt.X / 32;
                    mainform.location[1] = thisEnt.Y / 32;
                    mainform.location[2] = thisEnt.Z /32;
                    mainform.location[3] = thisEnt.Y / 32 + 1;
                }
            }
        }
    }
}
