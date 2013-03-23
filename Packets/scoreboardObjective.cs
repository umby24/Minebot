using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets {
    class scoreboardObjective {
        // Server-to-Client only

        public scoreboardObjective(Wrapped.Wrapped Socket, Form1 Mainform) {
            
            string objectiveName = Socket.readString();
            string objectiveValue = Socket.readString();
            byte create = Socket.readByte();

        
        }
    }
}
