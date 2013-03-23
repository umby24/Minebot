using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets {
    class teams {
        // Server-to-Client only

        public teams(Wrapped.Wrapped socket, Form1 Mainform) {

            List<string> players;
            string teamDisplayname;
            string teamPrefix;
            string teamSuffix;
            byte friendlyFire;
            short playerCount;

            string teamName = socket.readString();
            byte mode = socket.readByte();

            if (mode == 0 || mode == 2) {

                teamDisplayname = socket.readString();
                teamPrefix = socket.readString();
                teamSuffix = socket.readString();
                friendlyFire = socket.readByte();
            }
            if (mode == 0 || mode == 3 || mode == 4) {

                playerCount = socket.readShort();
                players = new List<string>();

                for (int i = 1; i < playerCount; i++) {
                    players.Add(socket.readString());
                }
            }
        }
    }
}
