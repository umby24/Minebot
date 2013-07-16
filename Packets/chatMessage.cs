using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;
using System.IO;

namespace C_Minebot.Packets
{
    class chatMessage
    {
        Wrapped.Wrapped socket;
        Form1 mainform;
        string Message;

        public chatMessage(Wrapped.Wrapped sock, Form1 myform, bool outgoing = false)
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

        public chatMessage(Wrapped.Wrapped sock, Form1 myform, string message, bool outgoing = false)
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
            string name = "", message = "", type = "", color = "", style = "";
            Message = socket.readString();

            JsonTextReader json = new JsonTextReader(new StringReader(Message));
            while (json.Read()) {
                if (json.Value != null) {
                    switch (json.Path) {
                        case "using[0]":
                            name = (string)json.Value;
                            break;
                        case "using[1]":
                            message = (string)json.Value;
                            break;
                        case "translate":
                            if (json.Value.ToString() != "translate") 
                                type = (string)json.Value;
                            break;
                        case "color":
                            if (json.Value.ToString() != "translate")
                                color = (string)json.Value;
                            break;
                    }
                }
            }
            
           // Now with the JSON Parsed, we have to add some special message cases.
            switch (type) {
                case "multiplayer.player.joined":
                    message = name + " Joined the server";
                    break;
                case "death.attack.outOfWorld":
                    message = name + " fell out of the world!";
                    break;
                case "death.attack.explosion.player":
                    message = name + " was blown up by " + message;
                    break;
                case "chat.type.text":
                    message = name + ": " + message;
                    commandHandler ch = new commandHandler(socket, mainform, name, message);
                    break;
                case "chat.type.emote":
                    message = "§d" + name + " " + message;
                    break;
                default:
                    message = "You need to parse " + type + "; " + name + " + " + message;
                    break;
            }
            if (color != "")
                message = convertCode(message, color);

            
            handleColors(message);
            if (mainform.ircmode == 2 || mainform.ircmode == 3)
            {
                string[] args = message.Split(' ');
                string username = args[0].Replace("<", "").Replace(">", "").Replace(":", "").Replace(" ", "");
                username = func.strip_codes(username);
                if (!(username == mainform.username && message.Contains("IRC: <")))
                 mainform.ircmessage(mainform.translate_colors(Message));
            }
        }

        private string convertCode(string text, string color) {
            switch (color.ToLower()) {
                case "black":
                    text = "§0" + text;
                    break;
                case "dark blue":
                    text = "§1" + text;
                    break;
                case "dark green":
                    text = "§2" + text;
                    break;
                case "dark cyan":
                    text = "§3" + text;
                    break;
                case "dark red":
                    text = "§4" + text;
                    break;
                case "purple":
                    text = "§5" + text;
                    break;
                case "gold":
                    text = "§6" + text;
                    break;
                case "gray":
                    text = "§7" + text;
                    break;
                case "dark gray":
                    text = "§8" + text;
                    break;
                case "blue":
                    text = "§9" + text;
                    break;
                case "bright green":
                    text = "§a" + text;
                    break;
                case "cyan":
                    text = "§b" + text;
                    break;
                case "red":
                    text = "§c" + text;
                    break;
                case "pink":
                    text = "§d" + text;
                    break;
                case "yellow":
                    text = "§e" + text;
                    break;
                case "white":
                    text = "§f" + text;
                    break;
            }
            return text;
        }
        private void send()
        {
            if (mainform.muted == false)
            {
                socket.writeByte(3);
                socket.writeString(Message);
            }
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
