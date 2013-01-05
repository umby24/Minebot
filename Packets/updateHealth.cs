using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class updateHealth
    {

        public updateHealth(Wrapped.Wrapped sock, Form1 myform)
        {
            myform.health = sock.readShort();
            myform.hunger = sock.readShort();
            sock.readFloat();
            if (0 >= myform.health)
            {
                //TODO: Respawn.
            }
        }


    }
}
