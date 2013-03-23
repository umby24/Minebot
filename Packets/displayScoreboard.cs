using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets {
    class displayScoreboard {
        // server-to-client only

        public displayScoreboard(Wrapped.Wrapped Socket, Form1 mainform) {

            byte position = Socket.readByte();
            string scoreName = Socket.readString();

        }
    }
}
