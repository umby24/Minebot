using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace C_Minebot.Classes
{
    public class Decompressor
    {
        // ZLib Decompressor.
        byte[] thisdata;

        public Decompressor(byte[] data)
        {
            thisdata = data;
        }

        public byte[] decompress()
        {
            using(var compressedStream = new MemoryStream(thisdata))
            using(var zipStream = new DeflateStream(compressedStream,CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                var buffer = new byte[4096];
                int read;

                while ((read = zipStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    resultStream.Write(buffer, 0, read);
                }

                return resultStream.ToArray();
            }
        }
    }
}
