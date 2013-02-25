using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Classes
{
    class Chunk
    {
        public int x;
        public int z;
        public int numBlocks;
        public short pbitmap;
        public short abitmap;
        public byte[] blocks;
        public bool lighting = false;

        public Chunk(int X, int Z, short Pbitmap, short Abitmap, bool inLighting)
        {
            
            lighting = inLighting;
            pbitmap = Pbitmap;
            abitmap = Abitmap;
            x = X;
            z = Z;

        }
    }
}
