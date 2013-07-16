using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets {
    class entProps {
        public entProps(Wrapped.Wrapped socket, Form1 mainform) {

            int entId = socket.readInt();
            int propCount = socket.readInt();

            for (int b = 0; b < propCount; b++) {
                string key = socket.readString();
                double value = socket.readDouble();
                short listLength = socket.readShort();

                for (int i = 0; i < listLength; i++) {
                    long msb = socket.readLong();
                    long lsb = socket.readLong();
                    double ammount = socket.readDouble();
                    byte op = socket.readByte();
                }
            }

        }
    }
}
