using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
        public List<MapBlock> tBlocks;

        public Chunk(int X, int Z, short Pbitmap, short Abitmap, bool inLighting)
        {
            lighting = inLighting;
            pbitmap = Pbitmap;
            abitmap = Abitmap;
            x = X;
            z = Z;
            tBlocks = new List<MapBlock>();

            // Generate a number of how many chunks (16 x 16 x 16) are included for this chunk.
            numBlocks = 0;
            aBlocks = 0;

            for (int i = 0; i < 16; i++) {
                if (Convert.ToBoolean(Pbitmap & (1 << i))) {
                    numBlocks++; // "Sections"
                }
            }

            for (int i = 0; i < 16; i++) {
                if (Convert.ToBoolean(Abitmap & (1 << i))) {
                    aBlocks++;
                }
            }

            // Number of sections * blocks per section = blocks in this "Chunk"
            numBlocks = numBlocks * 4096;
        }

        public void parseBlocks() {
            // Push off to a thread so it returns immediatly and doesn't slow down the networking.

            Thread blockParser = new Thread(loadArray);
            blockParser.Start();
        }

        void loadArray() {
            int offset = 0;

            for (int i = 0; i < 16; i++) {
                if (Convert.ToBoolean(pbitmap & (1 << i))) {

                    // Break into individual blocks for easier manipulation
                    byte[] temp = new byte[4096];
                    Array.Copy(blocks, offset, temp, 0, 4096);

                    for (int f = 0; f < 4096; f++) {
                        // It's all about the parenthesis..

                        int BlockX = x * 16 + (f & 0x0F);
                        int BlockY = i * 16 + (f >> 8);
                        int BlockZ = z * 16 + ((f & 0xF0) >> 4);
                        int BlockID = temp[f];

                        MapBlock newBlock = new MapBlock(BlockID, BlockX, BlockY, BlockZ, x, z);
                        tBlocks.Add(newBlock);
                    }
                    offset += 4096;
                }
            }
        }

        public void updateBlock(int Bx, int By, int Bz, int id) {
            // Updates the block in this chunk.
            // TODO: Ensure that the block being updated is in this chunk.
            // Even though chances of that exception throwing are tiny.

            MapBlock oldBlock = null;

            foreach (MapBlock b in tBlocks) {
                if (b.x == Bx && b.y == By && b.z == Bz) {
                    oldBlock = b;
                    break;
                }
            }

            if (oldBlock != null)
                tBlocks.Remove(oldBlock);

            tBlocks.Add(new MapBlock(id, Bx, By, Bz, x, z));
        }

        public byte[] getData(byte[] deCompressed) {
            // Loading chunks, network handler hands off the decompressed bytes
            // This function takes its portion, and returns what's left.

            blocks = new byte[numBlocks];
            byte[] temp;
            int removeable = numBlocks;

            if (lighting == true)
                removeable += (numBlocks / 2);

            removeable += 256;

            Array.Copy(deCompressed, 0, blocks, 0, numBlocks);
            temp = new byte[deCompressed.Length - (numBlocks + removeable)];

            Array.Copy(deCompressed, (numBlocks + removeable), temp, 0, temp.Length);

            return temp;
        }

    }
}
