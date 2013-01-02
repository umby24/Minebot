using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class Login_Response
    {
        Wrapped.Wrapped sock;
        Form1 myform;
        public Login_Response(Wrapped.Wrapped socket, Form1 asdf)
        {
            sock = socket;
            myform = asdf;
            handle();
        }
        void handle()
        {
            myform.puts("------------------");
            myform.puts("Success.");
            myform.puts("------------------");
        }
    }
}
