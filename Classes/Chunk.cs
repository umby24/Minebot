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
        public int aBlocks;
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

            // Generate a number of how many chunks (16 x 16 x 16) are included for this chunk.
            numBlocks = 0;
            aBlocks = 0;

            for (int i = 0; i < 16; i++) {
                if (Convert.ToBoolean(Pbitmap & (1 << i))) {
                    numBlocks++;
                }
            }

            for (int i = 0; i < 16; i++) {
                if (Convert.ToBoolean(Abitmap & (1 << i))) {
                    aBlocks++;
                }
            }

            numBlocks = numBlocks * 4096;
        }

        public void parseBlocks(Form1 Mainform) {
            int offset = 0;

            for (int i = 0; i < 16; i++) {
                if (Convert.ToBoolean(pbitmap & (1 << i))) {

                    byte[] temp = new byte[4096];
                    Array.Copy(blocks, offset, temp, 0, 4096);

                    for (int f = 0; f < 4096; f++) {

                        int BlockX = f & 0x0F;
                        BlockX = BlockX + 16;
                        int BlockY = i * 16 + (f >> 8);
                        int BlockZ = (f & 0xF0) >> 4;
                        BlockZ = BlockZ - 16;
                       // int BlockX = (x * 16) + (f & 0x0F);
                        //int BlockY = (i * 16) + (BlockX >> 8);
                        //int BlockZ = (z * 16) + (f & 0xF0) >> 4;
                        int flat = BlockX + BlockZ * 16 + BlockY * 256;
                        int BlockID = temp[f];

                        MapBlock newBlock = new MapBlock(BlockID, BlockX, BlockY, BlockZ,x,z);
                        Mainform.blocks.Add(newBlock);

                    }
                    offset += 4096;
                }
            }
        }
        
        public byte[] getData(byte[] deCompressed) {

            blocks = new byte[numBlocks];
            byte[] temp;
            int removeable = numBlocks;

            if (lighting == true)
                removeable += (numBlocks / 2);

            removeable += 256;

            Array.Copy(deCompressed, 0, blocks, 0, numBlocks);
            temp = new byte[deCompressed.Length - (numBlocks + removeable)];

            deCompressed.Reverse();
            Array.Copy(deCompressed, temp, temp.Length);
            temp.Reverse();

            return temp;
        }

    }
}
