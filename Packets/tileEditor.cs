using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets {
    class tileEditor {
        public tileEditor(Wrapped.Wrapped socket, Form1 mainForm) {
            socket.readByte();
            socket.readInt();
            socket.readInt();
            socket.readInt();
        
        }
    }
}
