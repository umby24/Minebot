using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C_Minebot.Classes;
using System.IO;
using System.IO.Compression;

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
            int decompressedSize = 0;
            bool skylight = socket.readBool();
            byte[] packdata = socket.readByteArray(Datalength);
            byte[] decompressed;
            byte[] trim = new byte[Datalength - 2];
            byte[] temp;
            Chunk[] chunks = new Chunk[columncount];

            for (int i = 0; columncount > i; i++)
            {
                // Need to store this data so it's not lost as well..
               int chunksize = 0;
               int x = socket.readInt();
               int z = socket.readInt();
               int numblocks = 0;
               short pbitmap = socket.readShort();
               short abitmap = socket.readShort();
               //byte[] chunkdata;
               //byte[] temp;

               BitArray primary = new BitArray(BitConverter.GetBytes(pbitmap));

               for (int f = 0; f < primary.Count; f++)
               {
                   if (primary.Get(f) == true)
                   {
                       chunksize += 1;
                   }
               }

               numblocks = chunksize * 4096;
               chunksize = chunksize * 12544;

                // Half-byte per block for skylight data

               if (skylight == true)
                   chunksize += (int)(numblocks * 0.5);

               decompressedSize += chunksize;

               Chunk mychunk = new Chunk(x, z, pbitmap, abitmap, skylight);
               mychunk.numBlocks = numblocks;
               mychunk.blocks = new byte[chunksize];

               //chunkdata = new byte[chunksize];

               //Array.Copy(packdata, chunkdata, chunksize);

               //temp = new byte[packdata.Length - chunksize];

               //Array.Copy(packdata, chunksize, temp, 0, packdata.Length - chunksize);
               //packdata = temp;

               //Chunk newChunk = new Chunk(chunkdata,x,z,pbitmap,abitmap,skylight);
               chunks[i] = mychunk;

            }

            // At this point, we should have accumulated the sizes of each chunk area, and thus calculated the decompressed size.
            // With that information, we can decompress the whole stream.

            // Remove first two bytes to array
            Array.Reverse(packdata);
            Array.Copy(packdata, trim, Datalength - 2);
            Array.Reverse(trim);

            // Decompress the data

            DeflateStream decompresser = new DeflateStream(new MemoryStream(trim), CompressionMode.Decompress);
            decompressed = new byte[decompressedSize];
            decompresser.Read(decompressed, 0, decompressedSize);

            // Now that it's decompressed, we can go back through each chunk and assign it the appropriate ammount of data.

            foreach (Chunk b in chunks)
            {
                Array.Copy(decompressed, b.blocks, b.blocks.Length);

                temp = new byte[decompressed.Length - b.blocks.Length];

                Array.Copy(decompressed, b.blocks.Length, temp, 0, decompressed.Length - b.blocks.Length);
                decompressed = temp;
                parseChunk(b);
            }
        }
        void parseChunk(Chunk toparse)
        {
            // Get all of the blocks, and load them into memory.
            for (byte i = 0; i < 16; i++)
            {
                if (Convert.ToBoolean(toparse.pbitmap & (1 << i)))
                {
                    for (int f= 0; f < 4096; f++)
                    {
                        int blockX = (toparse.x * 16) + (f & 0x0F);
                        int blockY = (i * 16) + (blockX >> 8);
                        int blockZ = (toparse.z * 16) + (f & 0xF0) >> 4;
                        int blockID = toparse.blocks[f];

                    }
                }
            }
        }
    }
}
