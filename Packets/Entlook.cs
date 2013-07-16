using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class Entlook
    {
        public Entlook(Wrapped.Wrapped socket, Form1 mainform)
        {
            int EID = socket.readInt();
            byte yaw = socket.readByte();
            byte pitch = socket.readByte();
            functions helper = new functions();

            Classes.Entity thisEnt = helper.getEntbyID(EID,mainform);
            if (thisEnt != null) {
                thisEnt.yaw = yaw;
                thisEnt.pitch = pitch;
            }
        }

    }
}
