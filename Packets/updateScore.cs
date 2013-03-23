using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets {
    class updateScore {
        // Server-To-Client only

        public updateScore(Wrapped.Wrapped Socket, Form1 Mainform) {

            string itemName = Socket.readString();
            byte update = Socket.readByte();
            string scoreName = Socket.readString();
            int value = Socket.readInt();
        }
    }
}
