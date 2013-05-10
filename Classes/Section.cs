using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Classes {
    public class Section {

        public byte[] blocks;
        public byte y;

        public Section(byte Y) {
            y = Y;
            blocks = new byte[4096];
        }

        public void setBlock(int x, int y, int z, int id) {
            int index = x + (z * 16) + (y * 256);
            blocks[index] = (byte)id;
        }

        public MapBlock getBlock(int x, int y, int z) {
            int index = x + (z * 16) + (y * 16 * 16);
            MapBlock thisBlock = new MapBlock((int)blocks[index], x, y, z, (int)Math.Floor(decimal.Divide(x, 16)), (int)Math.Floor(decimal.Divide(z, 16)));
         
            return thisBlock;
        }

    }
}
