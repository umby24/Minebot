using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Classes
{
    public class Chunk
    {
        public int x;
        public int z;
        public int numBlocks;
        public short pbitmap;
        public short abitmap;
        public byte[] blocks;
        public byte[] blighting;
        public bool lighting = false;

        public Chunk(int X, int Z, short Pbitmap, short Abitmap, bool inLighting)
        {
            
            lighting = inLighting;
            pbitmap = Pbitmap;
            abitmap = Abitmap;
            x = X;
            z = Z;

        }

    //    public void parseData()
    //    {
    //        int thisx = x << 4;
    //        int thisz = z << 4;

    //        split(thisx, thisz);
    //    }
    //    public void split(int x, int z)
    //    {
    //        int offset = 0;
    //        int len = 256 >> 4;
    //        int lastBlock = 0;

    //        for (int i = 0; i < len; i++)
    //        {

    //        }
    //    }
    }
}
