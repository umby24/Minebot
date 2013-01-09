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
            // This packet is complicated, so I will comment the process.
            // Let's get the data off the line first..
            string serverID = sock.readString();
            short keyLength = sock.readShort();
            short verifyLength;
            byte[] key;
            byte[] token;
            key = sock.readByteArray(keyLength);
            verifyLength = sock.readShort();
            token = sock.readByteArray(verifyLength);
            
            //Here, we need some random bytes to use as a shared key with the server.

            RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(myform.sharedkey);

            // AsnKeyParser is a part of the cryptography.dll, which is simply a compiled version
            // of SMProxy's Cryptography.cs, with the server side parts stripped out.
            // You pass it the key data and ask it to parse, and it will 
            // Extract the server's public key, then parse that into RSA for us.

            AsnKeyParser keyParser = new AsnKeyParser(key);
            RSAParameters Dekey = keyParser.ParseRSAPublicKey();

            // Now we create an encrypter, and encrypt the token sent to us by the server
            // as well as our newly made shared key (Which can then only be decrypted with the server's private key)
            // and we send it to the server.
            RSACryptoServiceProvider cryptoService = new RSACryptoServiceProvider();
            cryptoService.ImportParameters(Dekey);
            byte[] EncryptedSecret = cryptoService.Encrypt(myform.sharedkey,false);
            byte[] EncryptedVerfy = cryptoService.Encrypt(token,false);

            // I pass this information back up (Unencrypted) to the main form.
            // This allows me to have it ready for when I need this later.

            myform.ServerID = serverID;
            myform.token = token;
            myform.PublicKey = key;
            

            if (serverID != "-" && myform.onlineMode)
            {
                // Verify with Minecraft.net, if need be.
                // At this point, the server requires a hash containing the server id,
                // shared key, and original public key. So we make this, and then pass to Minecraft.net

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
                    myform.sessionId = null;
                }


            } else {
                // Skip Verification, user is not online.
                myform.puts("Skipping verification.");
            }

            // Sets up the socket for encryption, but does not enable it yet.
            sock.InitEncryption(myform.sharedkey);

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
