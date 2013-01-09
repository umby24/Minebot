using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace C_Minebot.Packets
{
    class chatMessage
    {
        Wrapped.Wrapped socket;
        Form1 mainform;
        string Message;

        public chatMessage(bool outgoing, Wrapped.Wrapped sock, Form1 myform)
        {
            socket = sock;
            mainform = myform;

            if (outgoing)
            {
                send();
            }
            else
            {
                handle();
            }
        }

        public chatMessage(bool outgoing, Wrapped.Wrapped sock, Form1 myform, string message)
        {
            socket = sock;
            mainform = myform;
            Message = message;

            if (outgoing)
            {
                send();
            }
            else
            {
                handle();
            }
        }

        private void handle()
        {
            functions func = new functions();

            Message = socket.readString();
            commandHandler ch = new commandHandler(socket, mainform, func.strip_codes(Message));
            handleColors(Message);
            if (mainform.ircmode == 2 || mainform.ircmode == 3)
            {
                string[] args = Message.Split(' ');
                string username = args[0].Replace("<", "").Replace(">", "").Replace(":", "").Replace(" ", "");
                username = func.strip_codes(username);
                if (!(username == mainform.username && Message.Contains("IRC: <")))
                 mainform.ircmessage(mainform.translate_colors(Message));
            }
        }

        private void send()
        {
            socket.writeByte(3);
            socket.writeString(Message);
        }



        public void handleColors(string text)
        {
            if (!text.Contains("§"))
            {
                mainform.putsc(text, Color.White);
                return;
            }

            int count = CharCount(text, "§");

            mainform.putsc("", Color.Aqua);
            //---
            while (count != 0)
            {
                if (text.StartsWith("§") == false && count > 0)
                {
                    string temp = text.Substring(0, text.IndexOf("§"));
                    text = text.Substring(text.IndexOf("§"), text.Length - text.IndexOf("§"));
                    mainform.putsc(temp, Color.White, true);
                }
                else
                {
                    string code = text.Substring(text.IndexOf("§") + 1, 1);
                    code = code.ToLower();

                    if (count == 1)
                    {
                        string temp = text.Substring(text.IndexOf("§") + 2, text.Length - text.IndexOf("§") - 2);

                        switch (code)
                        {
                            case "0":
                                mainform.putsc(temp, Color.FromArgb(0, 0, 0), true);
                                break;
                            case "1":
                                mainform.putsc(temp, Color.FromArgb(0, 0, 170), true);
                                break;
                            case "2":
                                mainform.putsc(temp, Color.FromArgb(0, 170, 0), true);
                                break;
                            case "3":
                                mainform.putsc(temp, Color.FromArgb(0, 170, 170), true);
                                break;
                            case "4":
                                mainform.putsc(temp, Color.FromArgb(170, 0, 0), true);
                                break;
                            case "5":
                                mainform.putsc(temp, Color.FromArgb(170, 0, 170), true);
                                break;
                            case "6":
                                mainform.putsc(temp, Color.FromArgb(255, 170, 0), true);
                                break;
                            case "7":
                                mainform.putsc(temp, Color.FromArgb(170, 170, 170), true);
                                break;
                            case "8":
                                mainform.putsc(temp, Color.FromArgb(85, 85, 85), true);
                                break;
                            case "9":
                                mainform.putsc(temp, Color.FromArgb(85, 85, 255), true);
                                break;
                            case "a":
                                mainform.putsc(temp, Color.FromArgb(85, 255, 85), true);
                                break;
                            case "b":
                                mainform.putsc(temp, Color.FromArgb(85, 255, 255), true);
                                break;
                            case "c":
                                mainform.putsc(temp, Color.FromArgb(255, 85, 85), true);
                                break;
                            case "d":
                                mainform.putsc(temp, Color.FromArgb(255, 85, 255), true);
                                break;
                            case "e":
                                mainform.putsc(temp, Color.FromArgb(255, 255, 85), true);
                                break;
                            case "f":
                                mainform.putsc(temp, Color.FromArgb(255, 255, 255), true);
                                break;
                        }

                        count--;
                    }
                    else
                    {
                        string temp = text.Substring(text.IndexOf("§") + 2, text.Length - text.IndexOf("§") - 2);
                        string temp2;

                        if (temp.Contains("§"))
                            temp2 = temp.Substring(0, temp.IndexOf("§"));
                        else
                            temp2 = temp;

                        if (temp != "")
                            if (temp2 != "")
                                temp = temp.Substring(temp2.Length, (temp.Length - temp2.Length));
                        switch (code)
                        {
                            case "0":
                                mainform.putsc(temp2, Color.FromArgb(0, 0, 0), true);
                                break;
                            case "1":
                                mainform.putsc(temp2, Color.FromArgb(0, 0, 170), true);
                                break;
                            case "2":
                                mainform.putsc(temp2, Color.FromArgb(0, 170, 0), true);
                                break;
                            case "3":
                                mainform.putsc(temp2, Color.FromArgb(0, 170, 170), true);
                                break;
                            case "4":
                                mainform.putsc(temp2, Color.FromArgb(170, 0, 0), true);
                                break;
                            case "5":
                                mainform.putsc(temp2, Color.FromArgb(170, 0, 170), true);
                                break;
                            case "6":
                                mainform.putsc(temp2, Color.FromArgb(255, 170, 0), true);
                                break;
                            case "7":
                                mainform.putsc(temp2, Color.FromArgb(170, 170, 170), true);
                                break;
                            case "8":
                                mainform.putsc(temp2, Color.FromArgb(85, 85, 85), true);
                                break;
                            case "9":
                                mainform.putsc(temp2, Color.FromArgb(85, 85, 255), true);
                                break;
                            case "a":
                                mainform.putsc(temp2, Color.FromArgb(85, 255, 85), true);
                                break;
                            case "b":
                                mainform.putsc(temp2, Color.FromArgb(85, 255, 255), true);
                                break;
                            case "c":
                                mainform.putsc(temp2, Color.FromArgb(255, 85, 85), true);
                                break;
                            case "d":
                                mainform.putsc(temp2, Color.FromArgb(255, 85, 255), true);
                                break;
                            case "e":
                                mainform.putsc(temp2, Color.FromArgb(255, 255, 85), true);
                                break;
                            case "f":
                                mainform.putsc(temp2, Color.FromArgb(255, 255, 255), true);
                                break;
                            default:
                                mainform.putsc(temp2, Color.FromArgb(255, 255, 255), true);
                                break;
                        }
                        count--;
                        if (temp.Contains("§"))
                        {
                            text = temp;
                            
                        }
                    }
                }

            }
        }

        public int CharCount(string Origstring, string Chars)
        {
            int Count = 0;

            for (int i = 0; i < Origstring.Length - 1; i++)
            {
                if (Origstring.Substring(i, 1) == Chars)
                    Count++;
            }

            return Count;
        }
    }
}
