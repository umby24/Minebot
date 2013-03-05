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
        byte[] thisdata;

        public Decompressor(byte[] data)
        {
            thisdata = data;
        }

        public byte[] decompress()
        {
            //DeflateStream decompresser = new DeflateStream(new MemoryStream(thisdata), CompressionMode.Decompress);
            //int size = 0;
            //int next = decompresser.ReadByte();
            //byte[] decompressed = null;

            //while (next != -1)
            //{
            //    size++;

            //    if (decompressed == null)
            //        decompressed = new byte[1] { (byte)next };
            //    else
            //    {
            //        byte[] temp = decompressed;
            //        decompressed = new byte[size];
            //        Array.Copy(temp, decompressed, size - 1);
            //        decompressed[size - 1] = (byte)next;
            //    }
                
            //    next = decompresser.ReadByte();

            //}

            //return decompressed;
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
