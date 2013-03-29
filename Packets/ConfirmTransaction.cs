using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets {
    class ConfirmTransaction {

        public ConfirmTransaction(Wrapped.Wrapped socket, Form1 mainform) {
            // Should only be sent if we actually receive one with a "false" for accepted.
            byte windowID = socket.readByte();
            short actionNumber = socket.readShort();
            bool accepted = socket.readBool();

            if (accepted == false) {
                socket.writeByte(0x6A);
                socket.writeByte(windowID);
                socket.writeShort(actionNumber);
                socket.writeBool(accepted);
            }
        }
    }
}
