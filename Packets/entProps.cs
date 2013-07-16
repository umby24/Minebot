using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets {
    class entProps {
        public entProps(Wrapped.Wrapped Socket, Form1 mainform) {
            int entId = Socket.readInt();
            int propCount = Socket.readInt();
            string key = Socket.readString();
            double value = Socket.readDouble();
            short listLength = Socket.readShort();
            
            for (int i = 0; i < listLength; i++) {
                long msb = Socket.readLong();
                long lsb = Socket.readLong();
                double ammount = Socket.readDouble();
                byte op = Socket.readByte();
            }
        }
    }
}
