using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json.Linq;
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
            var root = JObject.Parse(Message);

                foreach (KeyValuePair<String, JToken> app in root) {
                    var appName = app.Key;

                    switch (appName) {
                        case "text":
                            message = app.Value.ToString();
                            if (message.Contains(" "))
                                name = func.strip_codes(message.Substring(0, message.IndexOf(" ")).Replace("<", "").Replace(">", "").Replace(":", "").Replace(" ", ""));
                            type = "chat";
                            break;
                        case "translate":
                            type = app.Value.ToString();
                            break;
                        case "using":
                            name = app.Value.First.ToString();
                            message = app.Value.Last.ToString();
                            break;
                        case "color":
                            color = app.Value.ToString();
                            break;
                        case "italic":
                            if (app.Value.ToString() == "true")
                                style = "italic";
                            break;
                    }
                    if (type == "chat.type.admin") {
                        if (app.Value.HasValues == true) {
                            name = app.Value[0].ToString();
                            JObject thisObj = JObject.Parse(app.Value[1].ToString());

                            foreach (KeyValuePair<String, JToken> part in thisObj) {
                                var topName = part.Key;

                                switch (topName) {
                                    case "translate":
                                        type = part.Value.ToString();
                                        break;
                                    case "using":
                                        message = part.Value.First.ToString();
                                        name = part.Value.Last.ToString();
                                        break;
                                }
                            }
                        }
                    }
                }

                // Now with the JSON Parsed, we have to add some special message cases.
                switch (type) {
                    case "multiplayer.player.joined":
                        message = name + " Joined the server";
                        break;
                    case "multiplayer.player.left":
                        message = name + " Left the server";
                        break;
                    case "death.attack.outOfWorld":
                        message = name + " fell out of the world!";
                        break;
                    case "death.attack.explosion.player":
                        message = name + " was blown up by " + message;
                        break;
                    case "death.attack.mob":
                        message = name + " was killed by a " + message;
                        break;
                    case "chat.type.text":
                        message = name + ": " + message;
                        commandHandler ch = new commandHandler(socket, mainform, name, message);
                        break;
                    case "chat":
                        commandHandler h = new commandHandler(socket, mainform, name, message);
                        break;
                    case "chat.type.emote":
                        message = "§d" + name + " " + message;
                        break;
                    case "chat.type.announcement":
                        message = "§d[" + name + "]:§f " + message;
                        break;
                    case "commands.tp.success":
                        message = name + " teleported to " + message;
                        break;
                    case "commands.op.success":
                        message = name + " was promoted to Op";
                        break;
                    default:
                        message = Message;
                        break;
                }
                if (color != "")
                    message = convertCode(message, color);

            
            handleColors(message,style);
            if (mainform.ircmode == 2 || mainform.ircmode == 3)
            {
                string[] args = message.Split(' ');
                string username = args[0].Replace("<", "").Replace(">", "").Replace(":", "").Replace(" ", "");
                username = func.strip_codes(username);
                if (!(username == mainform.username && message.Contains("IRC: <")))
                 mainform.ircmessage(mainform.translate_colors(message));
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



        public void handleColors(string text, string Style)
        {
            if (!text.Contains("§"))
            {
                mainform.putsc(text, Color.White,Style);
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
                    mainform.putsc(temp, Color.White, true,Style);
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
                                mainform.putsc(temp, Color.FromArgb(0, 0, 0), true, Style);
                                break;
                            case "1":
                                mainform.putsc(temp, Color.FromArgb(0, 0, 170), true, Style);
                                break;
                            case "2":
                                mainform.putsc(temp, Color.FromArgb(0, 170, 0), true, Style);
                                break;
                            case "3":
                                mainform.putsc(temp, Color.FromArgb(0, 170, 170), true, Style);
                                break;
                            case "4":
                                mainform.putsc(temp, Color.FromArgb(170, 0, 0), true, Style);
                                break;
                            case "5":
                                mainform.putsc(temp, Color.FromArgb(170, 0, 170), true, Style);
                                break;
                            case "6":
                                mainform.putsc(temp, Color.FromArgb(255, 170, 0), true, Style);
                                break;
                            case "7":
                                mainform.putsc(temp, Color.FromArgb(170, 170, 170), true, Style);
                                break;
                            case "8":
                                mainform.putsc(temp, Color.FromArgb(85, 85, 85), true, Style);
                                break;
                            case "9":
                                mainform.putsc(temp, Color.FromArgb(85, 85, 255), true, Style);
                                break;
                            case "a":
                                mainform.putsc(temp, Color.FromArgb(85, 255, 85), true, Style);
                                break;
                            case "b":
                                mainform.putsc(temp, Color.FromArgb(85, 255, 255), true, Style);
                                break;
                            case "c":
                                mainform.putsc(temp, Color.FromArgb(255, 85, 85), true, Style);
                                break;
                            case "d":
                                mainform.putsc(temp, Color.FromArgb(255, 85, 255), true, Style);
                                break;
                            case "e":
                                mainform.putsc(temp, Color.FromArgb(255, 255, 85), true, Style);
                                break;
                            case "f":
                                mainform.putsc(temp, Color.FromArgb(255, 255, 255), true, Style);
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
                                mainform.putsc(temp2, Color.FromArgb(0, 0, 0), true, Style);
                                break;
                            case "1":
                                mainform.putsc(temp2, Color.FromArgb(0, 0, 170), true, Style);
                                break;
                            case "2":
                                mainform.putsc(temp2, Color.FromArgb(0, 170, 0), true, Style);
                                break;
                            case "3":
                                mainform.putsc(temp2, Color.FromArgb(0, 170, 170), true, Style);
                                break;
                            case "4":
                                mainform.putsc(temp2, Color.FromArgb(170, 0, 0), true, Style);
                                break;
                            case "5":
                                mainform.putsc(temp2, Color.FromArgb(170, 0, 170), true, Style);
                                break;
                            case "6":
                                mainform.putsc(temp2, Color.FromArgb(255, 170, 0), true, Style);
                                break;
                            case "7":
                                mainform.putsc(temp2, Color.FromArgb(170, 170, 170), true, Style);
                                break;
                            case "8":
                                mainform.putsc(temp2, Color.FromArgb(85, 85, 85), true, Style);
                                break;
                            case "9":
                                mainform.putsc(temp2, Color.FromArgb(85, 85, 255), true, Style);
                                break;
                            case "a":
                                mainform.putsc(temp2, Color.FromArgb(85, 255, 85), true, Style);
                                break;
                            case "b":
                                mainform.putsc(temp2, Color.FromArgb(85, 255, 255), true, Style);
                                break;
                            case "c":
                                mainform.putsc(temp2, Color.FromArgb(255, 85, 85), true, Style);
                                break;
                            case "d":
                                mainform.putsc(temp2, Color.FromArgb(255, 85, 255), true, Style);
                                break;
                            case "e":
                                mainform.putsc(temp2, Color.FromArgb(255, 255, 85), true, Style);
                                break;
                            case "f":
                                mainform.putsc(temp2, Color.FromArgb(255, 255, 255), true, Style);
                                break;
                            default:
                                mainform.putsc(temp2, Color.FromArgb(255, 255, 255), true, Style);
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
