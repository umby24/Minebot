using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace C_Minebot.Classes {
    public class Chunk {

        public int x, z, numBlocks, aBlocks;
        public short pbitmap, abitmap;
        public byte[] blocks;
        public bool lighting, groundup = false;
        public List<Section> sections;

        public Chunk(int X, int Z, short Pbitmap, short Abitmap, bool inLighting, bool Groundup) {
            // Create chunk sections.
            groundup = Groundup;
            lighting = inLighting;
            pbitmap = Pbitmap;
            abitmap = Abitmap;
            x = X;
            z = Z;
            sections = new List<Section>();

            numBlocks = 0;
            aBlocks = 0;

            for (int i = 0; i < 16; i++) {
                if (Convert.ToBoolean(Pbitmap & (1 << i))) {
                    numBlocks++; // "Sections"
                    sections.Add(new Section((byte)i));
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

        public void populate() {
            // Seperate thread no longer required due to new optizations.

            int offset = 0;

            for (int i = 0; i < 16; i++) {
                if (Convert.ToBoolean(pbitmap & (1 << i))) {

                    byte[] temp = new byte[4096];
                    Array.Copy(blocks, offset, temp, 0, 4096);
                    Section mySection = sections[i];

                    mySection.blocks = temp;
                    offset += 4096;
                }
            }
        }

        public int getBlockId(int Bx, int By, int Bz) {
            Section thisSection = GetSectionByNumber(By);
            return thisSection.getBlock(getXinSection(Bx), GetPositionInSection(By), getZinSection(Bz)).ID;
        }

        public MapBlock getBlock(int Bx, int By, int Bz) {
            Section thisSection = GetSectionByNumber(By);
            return thisSection.getBlock(getXinSection(Bx), GetPositionInSection(By), getZinSection(Bz));
        }

        public void updateBlock(int Bx, int By, int Bz, int id) {
            // Updates the block in this chunk.
            // TODO: Ensure that the block being updated is in this chunk.
            // Even though chances of that exception throwing are tiny.

            Section thisSection = GetSectionByNumber(By);
            thisSection.setBlock(getXinSection(Bx), GetPositionInSection(By), getZinSection(Bz), id);

        }

        public byte[] getData(byte[] deCompressed) {
            // Loading chunks, network handler hands off the decompressed bytes
            // This function takes its portion, and returns what's left.

            blocks = new byte[numBlocks];
            byte[] temp;
            int removeable = numBlocks;

            if (lighting == true)
                removeable += (numBlocks / 2);

            if (groundup)
                removeable += 256;

            Array.Copy(deCompressed, 0, blocks, 0, numBlocks);
            temp = new byte[deCompressed.Length - (numBlocks + removeable)];

            Array.Copy(deCompressed, (numBlocks + removeable), temp, 0, temp.Length);

            populate(); // Populate all of our sections with the bytes we just aquired.

            return temp;
        }

        #region Helping Methods
            private Section GetSectionByNumber(int blockY) {
            Section thisSection = null;

            foreach (Section y in sections) {
                if (y.y == blockY / 16) {
                    thisSection = y;
                    break;
                }
            }

            if (thisSection == null) { // Add a new section, if it doesn't exist yet.
                thisSection = new Section((byte)(blockY / 16));
                sections.Add(thisSection);
            }

            return thisSection;
        }
            private int getXinSection(int BlockX) {
            return BlockX - (x * 16);
        }
            private int GetPositionInSection(int blockY) {
            return blockY & (16 - 1); // Credits: SirCmpwn Craft.net
        }
            private int getZinSection(int BlockZ) {
            return BlockZ - (z * 16);
        }
        #endregion
    }
}
