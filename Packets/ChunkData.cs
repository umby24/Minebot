using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C_Minebot.Classes;

namespace C_Minebot.Packets
{
    class ChunkData
    {
        public ChunkData(Wrapped.Wrapped socket, Form1 Mainform)
        {
            int x = socket.readInt();
            int z = socket.readInt();
            bool groundup = socket.readBool();
            short pbitmap = socket.readShort();
            short abitmap = socket.readShort();
            int size = socket.readInt();
            byte[] data = socket.readByteArray(size);
            byte[] trim = new byte[size - 2];
            byte[] decompressed;


            // Remove first two bytes to array
            Array.Copy(data, 2, trim, 0, size - 2);

            // Decompress the data

            Classes.Decompressor DC = new Decompressor(trim);
            decompressed = DC.decompress();

            if (pbitmap == 0) {
                // Unload chunk, save ALL the ram!
                Classes.Chunk thischunk = null;

                foreach (Chunk f in Mainform.Chunks) {
                    if (f.x == x && f.z == z) {
                        thischunk = f;
                        break;
                    }
                }

                if (thischunk != null) 
                    Mainform.Chunks.Remove(thischunk);
            }

            Chunk myChunk = new Chunk(x, z, pbitmap, abitmap, true); // Skylight assumed true..
            decompressed = myChunk.getData(decompressed);

            // Parse the received blocks.
            myChunk.parseBlocks();

            Mainform.Chunks.Add(myChunk); // Add to main form for use later.
        }
    }
}
