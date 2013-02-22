using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C_Minebot.Packets;

namespace C_Minebot
{
    class packetHandler
    {
        Wrapped.Wrapped sock;
        Form1 mainform;

        public packetHandler(int id,Wrapped.Wrapped socket,Form1 mform)
        {
            sock = socket;
            mainform = mform;
            Handle(id);
        }

        void Handle(int id)
        {
            switch (id)
            {
                case 0:
                    keepAlive imalive = new keepAlive(sock, mainform);
                    break;
                case 1:
                    Login_Response response = new Login_Response(sock, mainform);
                    break;
                case 2:
                    Handshake myhand = new Handshake(false, sock, mainform);
                    break;
                case 3:
                     chatMessage chat = new chatMessage(false, sock, mainform);
                    break;
                case 4:
                    timeUpdate time = new timeUpdate(sock, mainform);
                    break;
                case 5:
                    entityEquipment equip = new entityEquipment(sock, mainform);
                    break;
                case 6:
                    spawnPosition spawn = new spawnPosition(sock, mainform);
                    break;
                case 8:
                    updateHealth health = new updateHealth(sock, mainform);
                    break;
                case 9:
                    Respawn lol = new Respawn(sock, mainform);
                    break;
                case 13:
                    PPaL pal = new PPaL(false, sock, mainform);
                    break;
                case 16:
                    HeldItemChange hic = new HeldItemChange(false, sock, mainform);
                    break;
                case 17:
                    useBed ub = new useBed(sock, mainform);
                    break;
                case 18:
                    Animation ani = new Animation(false, sock, mainform);
                    break;
                case 20:
                    SpawnNamedEntity nes = new SpawnNamedEntity(sock, mainform);
                    break;
                case 22:
                    CollectItem ci = new CollectItem(sock, mainform);
                    break;
                case 23:
                    SpawnObj obj = new SpawnObj(sock, mainform);
                    break;
                case 24:
                    SpawnMob mob = new SpawnMob(sock, mainform);
                    break;
                case 25:
                    SpawnPainting paint = new SpawnPainting(sock, mainform);
                    break;
                case 26:
                    SpawnExporb exp = new SpawnExporb(sock, mainform);
                    break;
                case 28:
                    EntVelocity velocity = new EntVelocity(sock, mainform);
                    break;
                case 29:
                    DestroyEnt destroy = new DestroyEnt(sock, mainform);
                    break;
                case 30:
                    Entity entity = new Entity(sock, mainform);
                    break;
                case 31:
                    EntRelmove imovin = new EntRelmove(sock, mainform);
                    break;
                case 32:
                    Entlook ilookin = new Entlook(sock, mainform);
                    break;
                case 33:
                    EntlookRelmove longname = new EntlookRelmove(sock, mainform);
                    break;
                case 34:
                    EntTeleport enderman = new EntTeleport(sock, mainform);
                    break;
                case 35:
                    Enthead myhairbacknforth = new Enthead(sock, mainform);
                    break;
                case 38:
                    EntStatus stateous = new EntStatus(sock, mainform);
                    break;
                case 39:
                    AttachEnt weee = new AttachEnt(sock, mainform);
                    break;
                case 40:
                    EntMetadata ewww = new EntMetadata(sock, mainform);
                    break;
                case 41:
                    EntEffect notuniverse = new EntEffect(sock, mainform);
                    break;
                case 42:
                    RemoveEntEffect tehuniverse = new RemoveEntEffect(sock, mainform);
                    break;
                case 43:
                    SetExp ihasenchant = new SetExp(sock, mainform);
                    break;
                case 51:
                    ChunkData ichunkurmom = new ChunkData(sock, mainform);
                    break;
                case 52:
                    MultiBlockChange ichangealot = new MultiBlockChange(sock, mainform);
                    break;
                case 53:
                    BlockChange ichange = new BlockChange(sock, mainform);
                    break;
                case 54:
                    BlockAction waht = new BlockAction(sock, mainform);
                    break;
                case 55:
                    BlockbreakAni animations = new BlockbreakAni(sock, mainform);
                    break;
                case 56:
                    BulkChunks alotofdata = new BulkChunks(sock, mainform);
                    break;
                case 60:
                    Explosion boom = new Explosion(sock, mainform);
                    break;
                case 61:
                    Sound isawesome = new Sound(sock, mainform);
                    break;
                case 62:
                    Namedsound namedatsound = new Namedsound(sock, mainform);
                    break;
                case 70:
                    changeGameState rainin = new changeGameState(sock, mainform);
                    break;
                case 71:
                    SpawnGlobalEntity thunda = new SpawnGlobalEntity(sock, mainform);
                    break;
                case 100:
                    OpenWindow ow = new OpenWindow(sock, mainform);
                    break;
                case 101:
                    CloseWindow closedat = new CloseWindow(false,sock, mainform);
                    break;
                case 103:
                    SetSlot doubles = new SetSlot(sock, mainform);
                    break;
                case 104:
                    setWindowItems swi = new setWindowItems(sock, mainform);
                    break;
                case 105:
                    updateWindowProperty uwp = new updateWindowProperty(sock, mainform);
                    break;
                case 106:
                    mainform.puts("Parse packet 0x6A!");
                    break;
                case 107:
                    mainform.puts("Parse packet 0x6B!");
                    break;
                case 130:
                    updateSign ud = new updateSign(sock, mainform);
                    break;
                case 131:
                    itemData itemd = new itemData(sock, mainform);
                    break;
                case 132:
                    updateTileEntity ute = new updateTileEntity(sock, mainform);
                    break;
                case 200:
                    incStat istat = new incStat(sock, mainform);
                    break;
                case 201:
                    PlayerListItem pli = new PlayerListItem(sock, mainform);
                    break;
                case 202:
                    PlayerAbilities pa = new PlayerAbilities(sock, mainform);
                    break;
                case 203:
                    TabComplete tc = new TabComplete(sock, mainform);
                    break;
                case 250:
                    PluginMessage pm = new PluginMessage(sock, mainform);
                    break;
                case 252:
                    EncResponse Response = new EncResponse(false, sock, mainform);
                    break;
                case 253:
                    EncRequest Request = new EncRequest(false, sock, mainform);
                    break;
                case 255:
                    Kick kicked = new Kick(false, sock, mainform);
                    break;
                default:
                    mainform.puts("new packet found! " + Convert.ToString(id));
                    sock._stream.Close();
                    break;
            }
        }
    }
}
