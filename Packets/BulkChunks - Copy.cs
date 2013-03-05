using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C_Minebot.Classes;
using System.IO;
using System.IO.Compression;
using ICSharpCode.SharpZipLib;

namespace C_Minebot.Packets
{
    // Step 1: Decompress entire compressed stream
    // Step 2: pull number of blocks, optionally with lighting from decompressed data

    class BulkChunks
    {
        public BulkChunks(Wrapped.Wrapped socket, Form1 mainform)
        {
            short columncount = socket.readShort();
            int Datalength = socket.readInt();
            bool skylight = socket.readBool();

            byte[] packdata = socket.readByteArray(Datalength);
            byte[] decompressed;

            
            byte[] trim = new byte[Datalength - 2];
            Chunk[] chunks = new Chunk[columncount];

            Array.Reverse(packdata);
            Array.Copy(packdata, trim, Datalength - 2);
            Array.Reverse(trim);

            // Decompress the data

            Classes.Decompressor dc = new Classes.Decompressor(trim);
            decompressed = dc.decompress();

            for (int i = 0; columncount > i; i++)
            {
                // Need to store this data so it's not lost as well..
               int x = socket.readInt();
               int z = socket.readInt();
               short pbitmap = socket.readShort();
               short abitmap = socket.readShort();

               Chunk mychunk = new Chunk(x, z, pbitmap, abitmap, skylight);

               chunks[i] = mychunk;

            }

            int offset = 0;

            foreach (Chunk b in chunks)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (Convert.ToBoolean(b.pbitmap & (1 << i)))
                    {
                        byte[] newBlocks = new byte[4096];
                        byte[] temp = b.blocks;

                        Array.Copy(decompressed, offset, newBlocks, 0, 4096);

                        if (b.blocks == null)
                            b.blocks = newBlocks;
                        else
                        {
                            b.blocks = new byte[temp.Length + 4096];
                            temp.CopyTo(b.blocks, 0);
                            newBlocks.CopyTo(b.blocks, temp.Length);
                        }
                        b.numBlocks += 4096;
                        offset += 4096;
                    }
                }

                mainform.Chunks.Add(b);
                if (b.abitmap != 0)
                {
                    throw new Exception();
                }
                // we need to adjust the offset in compensation for additional metadata included with every chunk..
                int additional = 0;
                int Nibbleinfo = b.numBlocks / 2;
                int totalSections = b.numBlocks / 4096;


                if (skylight == true)
                    additional = (totalSections * Nibbleinfo) + (totalSections * Nibbleinfo) + (totalSections * Nibbleinfo);
                else
                    additional = (totalSections * Nibbleinfo) + (totalSections * Nibbleinfo);

                additional += 256;

                offset += additional;
                //Array.Copy(decompressed, b.blocks, b.blocks.Length);

                //temp = new byte[decompressed.Length - b.blocks.Length];

                //Array.Copy(decompressed, b.blocks.Length, temp, 0, decompressed.Length - b.blocks.Length);
                //decompressed = temp;
                //mainform.Chunks.Add(b);
                // parseChunk(b,mainform);
            }
        }

        void parseChunk(Chunk toparse, Form1 thisform)
        {
            // Get all of the blocks, and load them into memory.
            for (byte i = 0; i < 16; i++)
            {
                if (Convert.ToBoolean(toparse.pbitmap & (1 << i)))
                {
                    for (int f= 0; f < 4096; f++)
                    {
                        int blockX = (toparse.x * 16) + (f & 0x0F); // f & 0x0f
                        int blockY = (i * 16) + (blockX >> 8); // i*16 + (f >> 8)
                        int blockZ = (toparse.z * 16) + (f & 0xF0) >> 4; // (f & F0) >> 4
                        int blockID = toparse.blocks[f];

                        MapBlock myBlock = new MapBlock(blockID, blockX, blockY, blockZ);

                        if (thisform.blocks == null)
                            thisform.blocks = new MapBlock[] { myBlock };
                        else
                        {
                            MapBlock[] temp = thisform.blocks;
                            thisform.blocks = new MapBlock[temp.Length + 1];
                            Array.Copy(temp, thisform.blocks, temp.Length);
                            thisform.blocks[temp.Length] = myBlock;
                        }
                    }
                }
            }
        }
    }
}
