using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using SMProxy;

namespace C_Minebot.Packets
{
    class EncRequest
    {
        Wrapped.Wrapped sock;
        Form1 myform;
        public EncRequest(bool outgoing, Wrapped.Wrapped socket,Form1 asdf)
        {
            sock = socket;
            myform = asdf;
            if (outgoing) {            
                send();
            } else {
                handle();
            }
        }
        void handle()
        {
            string serverID = sock.readString();
            short keyLength = sock.readShort();
            short verifyLength;
            byte[] key;
            byte[] token;
            key = sock.readByteArray(keyLength);
            verifyLength = sock.readShort();
            token = sock.readByteArray(verifyLength);
            //-------------------\\
            RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(myform.sharedkey);
            AsnKeyParser keyParser = new AsnKeyParser(key);
            RSAParameters Dekey = keyParser.ParseRSAPublicKey();
            RSACryptoServiceProvider cryptoService = new RSACryptoServiceProvider();
            cryptoService.ImportParameters(Dekey);
            byte[] EncryptedSecret = cryptoService.Encrypt(myform.sharedkey,false);
            byte[] EncryptedVerfy = cryptoService.Encrypt(token,false);
            //--------------------\\
            myform.ServerID = serverID;
            myform.token = token;
            myform.PublicKey = key;
            //-------------------\\

            if (serverID != "-" && myform.onlineMode)
            {
                // Verify with Minecraft.net
                List<byte> hashlist = new List<byte>();
                hashlist.AddRange(System.Text.Encoding.ASCII.GetBytes(serverID));
                hashlist.AddRange(myform.sharedkey);
                hashlist.AddRange(key);
                byte[] hashData = hashlist.ToArray();
                string hash = JavaHexDigest(hashData);
                myform.serverHash = hash;
                Minecraft_Net_Interaction verify = new Minecraft_Net_Interaction();
                if (!verify.VerifyName(myform.username, myform.sessionId, hash))
                {
                    myform.puts("Failed to verify name with minecraft.net");
                    sock._stream.Close();
                }


            } else {
                // Skip Verification
                myform.puts("Skipping verification.");
            }

            sock.crypto = cryptoService; // This allows us to let the socket do the encryption work for us. Easy.

            myform.puts("Verification done!");

            // Respond to server.
            EncResponse Response = new EncResponse(true, sock, myform,EncryptedVerfy,EncryptedSecret);
        }
        void send()
        {

        }

        private static string GetHexString(byte[] p)
        {
            string result = "";
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i] < 0x10)
                    result += "0";
                result += p[i].ToString("x"); // Converts to hex string
            }
            return result;
        }

        private static byte[] TwosCompliment(byte[] p) // little endian
        {
            int i;
            bool carry = true;
            for (i = p.Length - 1; i >= 0; i--)
            {
                p[i] = unchecked((byte)~p[i]);
                if (carry)
                {
                    carry = p[i] == 0xFF;
                    p[i]++;
                }
            }
            return p;
        }

        public static string JavaHexDigest(byte[] data)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(data);
            bool negative = (hash[0] & 0x80) == 0x80;
            if (negative) // check for negative hashes
                hash = TwosCompliment(hash);
            // Create the string and trim away the zeroes
            string digest = GetHexString(hash).TrimStart('0');
            if (negative)
                digest = "-" + digest;
            return digest;
        }
    }
}
