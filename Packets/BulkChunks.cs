using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C_Minebot.Classes;
using System.IO;
using System.IO.Compression;
using ICSharpCode.SharpZipLib;
using System.Threading;

namespace C_Minebot.Packets
{
    // Note: This packet is all sorts of broken.

    // Step 1: Decompress entire compressed stream
    // Step 2: pull number of blocks, optionally with lighting from decompressed data

    class BulkChunks
    {
        public BulkChunks(Wrapped.Wrapped socket, Form1 mainform) {

            short columncount = socket.readShort();
            int Datalength = socket.readInt();
            bool skylight = socket.readBool();

            byte[] packdata = socket.readByteArray(Datalength);
            byte[] decompressed;


            byte[] trim = new byte[Datalength - 2];
            Chunk[] chunks = new Chunk[columncount];

            // Shoutout to BjorN64 for the simplification of this.
            Array.Copy(packdata, 2, trim, 0, Datalength - 2);

            // Decompress the data

            Classes.Decompressor dc = new Classes.Decompressor(trim);
            decompressed = dc.decompress();

            // Checksum time..

            
            for (int i = 0; columncount > i; i++) {
                // Need to store this data so it's not lost as well..
                int x = socket.readInt();
                int z = socket.readInt();
                short pbitmap = socket.readShort();
                short abitmap = socket.readShort();

                chunks[i] = new Chunk(x, z, pbitmap, abitmap, skylight);

                decompressed = chunks[i].getData(decompressed);
                chunks[i].parseBlocks();
                mainform.Chunks.Add(chunks[i]);

            }

            //mainform.puts("begin parse.." + DateTime.Now.ToString());
            //Thread thisthread = new Thread(parsethem);
            //thisthread.Start(mainform);
            //mainform.puts("Parsed.." + DateTime.Now.ToString());
        }

        void parsethem() {


        }
    }
}
