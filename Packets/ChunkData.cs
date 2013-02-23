using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace C_Minebot.Packets
{
    class ChunkData
    {
        public ChunkData(Wrapped.Wrapped socket, Form1 Mainform)
        {
            int decompressedSize = 0;
            int x = socket.readInt();
            int z = socket.readInt();
            bool groundup = socket.readBool();
            short pbitmap = socket.readShort();
            short abitmap = socket.readShort();
            int size = socket.readInt();
            int numBlocks = 0;
            byte[] data = socket.readByteArray(size);
            byte[] trim = new byte[size - 2];
            byte[] decompressed;
            byte[] blocks;
            BitArray primary = new BitArray(BitConverter.GetBytes(pbitmap));
            BitArray add = new BitArray(BitConverter.GetBytes(abitmap));

            // Determine what sections are included

            for (int i = 0; i > primary.Count; i++)
            {
                if (primary[i] == true)
                {
                    decompressedSize += 1;
                }
            }

            numBlocks = decompressedSize * 4096;
            blocks = new byte[numBlocks];

            if (groundup == false)
                decompressedSize = decompressedSize * 12288; // 16 x 16 x 16 column, (4096), x 3 for metadata, light, skylight, and add array
            else
                decompressedSize = decompressedSize * 12544; // + 256 for biome data

            // Remove first two bytes to array
            Array.Reverse(data);
            Array.Copy(data, trim, size - 2);
            Array.Reverse(trim);

            // Decompress the data

            DeflateStream decompresser = new DeflateStream(new MemoryStream(trim), CompressionMode.Decompress);
            decompressed = new byte[decompressedSize];
            decompresser.Read(decompressed, 0, decompressedSize);

            // Get all of the blocks, and load them into memory.
            for (byte i = 0; i < 16; i++)
            {
                int what = (pbitmap & (1 << i));
                if (true)
                {

                }
            }
            
        }
    }
}
