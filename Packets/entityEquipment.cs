using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class entityEquipment
    {
        Wrapped.Wrapped socket;
        Form1 mainform;

        public entityEquipment(Wrapped.Wrapped sock, Form1 myform)
        {
            socket = sock;
            mainform = myform;

            handle();
        }

        void handle()
        {
            // The Entity we're updating..

            int EID = socket.readInt();

            // Which slot we're updating (0 = held, 1-4 = armour slots)

            short slot = socket.readShort();

            //Have to handle the item now.

            if (EID == mainform.EntityID) {
                functions slotHandler = new functions();
                slotHandler.readSlot(socket, true, mainform, slot);
            } else {
                functions slotHandler = new functions();
                slotHandler.otherSlot(socket, mainform, EID, slot);
            }
        }

    }
}
