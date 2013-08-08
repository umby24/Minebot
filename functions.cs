using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C_Minebot.Classes;

namespace C_Minebot {
    class functions {
        #region "Slot"
        public void writeSlot(Item item, Wrapped.Wrapped socket) {
            if (item == null)
                socket.writeShort(-1);

            socket.writeShort((short)item.itemID);
            socket.writeByte(item.itemCount);
            socket.writeShort(item.itemDamage);
            socket.writeShort(-1);
        }
        public void readSlot(Wrapped.Wrapped socket, bool inventory = false, Form1 Mainform = null, short slot = 500) {
            // Read's slot data off the socket, and if the options are provided, will also add to the bot's inventory.

            bool delete = false;
            Classes.Item existing = null;

            // Just in case.
            if (inventory == true & Mainform == null) {
                throw new Exception("If inventory is true, you must include a main form.");
            }
            if ((inventory == true) & slot == 500) {
                throw new Exception("If inventory is true, you must include which slot to add to.");
            }

            // Delete the existing inventory data if it already exists. 
            if (inventory == true) {
                foreach (Classes.Item b in Mainform.inventory) {
                    if (b.slot == slot) {
                        delete = true;
                        existing = b;
                        break;
                    }
                }

                if (delete == true)
                    Mainform.inventory.Remove(existing);
            }

            int blockID = socket.readShort();

            if (blockID == -1)
                return;

            byte itemCount = socket.readByte();
            short damage = socket.readShort();
            int NBTLength = socket.readShort();

            if (NBTLength == -1) {
                if (inventory == true)
                    Mainform.inventory.Add(new Classes.Item(blockID, itemCount, damage, slot));
                return;
            }

            socket.readByteArray(NBTLength);


            if (inventory == true)
                Mainform.inventory.Add(new Classes.Item(blockID, itemCount, damage, slot));

        }
        public void otherSlot(Wrapped.Wrapped socket, Form1 Mainform, int EID, int slot) {
            Entity thisEnt = getEntbyID(EID, Mainform); // Entity we are modifying

            int blockID = socket.readShort();

            if (blockID == -1) {
                switch (slot) {
                    case 0:
                        thisEnt.held = new Item(0, 1, 0, 0);
                        break;
                    case 1:
                        thisEnt.boots = new Item(0, 1, 0, 0);
                        break;
                    case 2:
                        thisEnt.leggins = new Item(0, 1, 0, 0);
                        break;
                    case 3:
                        thisEnt.chestplate = new Item(0, 1, 0, 0);
                        break;
                    case 4:
                        thisEnt.helmet = new Item(0, 1, 0, 0);
                        break;
                }
                return;
            }

            byte itemCount = socket.readByte();
            short damage = socket.readShort();
            int NBTLength = socket.readShort();

            if (NBTLength == -1) {
                switch (slot) {
                    case 0:
                        thisEnt.held = new Item(blockID, itemCount, damage, (short)slot);
                        break;
                    case 1:
                        thisEnt.boots = new Item(blockID, itemCount, damage, (short)slot);
                        break;
                    case 2:
                        thisEnt.leggins = new Item(blockID, itemCount, damage, (short)slot);
                        break;
                    case 3:
                        thisEnt.chestplate = new Item(blockID, itemCount, damage, (short)slot);
                        break;
                    case 4:
                        thisEnt.helmet = new Item(blockID, itemCount, damage, (short)slot);
                        break;
                }
                return;
            }

            socket.readByteArray(NBTLength);

            switch (slot) {
                case 0:
                    thisEnt.held = new Item(blockID, itemCount, damage, (short)slot);
                    break;
                case 1:
                    thisEnt.boots = new Item(blockID, itemCount, damage, (short)slot);
                    break;
                case 2:
                    thisEnt.leggins = new Item(blockID, itemCount, damage, (short)slot);
                    break;
                case 3:
                    thisEnt.chestplate = new Item(blockID, itemCount, damage, (short)slot);
                    break;
                case 4:
                    thisEnt.helmet = new Item(blockID, itemCount, damage, (short)slot);
                    break;
            }

        }
        public Classes.Item returnSlot(Wrapped.Wrapped socket) {
            int blockID = socket.readShort();

            if (blockID == -1)
                return null;

            byte itemCount = socket.readByte();
            short damage = socket.readShort();
            int NBTLength = socket.readShort();

            if (NBTLength == -1) {
                return new Classes.Item(blockID, itemCount, damage, 0);
            }

            socket.readByteArray(NBTLength);

            return new Classes.Item(blockID, itemCount, damage, 0);
        }
        #endregion

        public void moveTo(int x, int y, int z, Form1 Mainform) {
            if (Math.Abs(Mainform.location[0] - x) < 10 && Math.Abs(Mainform.location[1] - y) < 10 && Math.Abs(Mainform.location[2] - z) < 10) {
                Mainform.location[0] = x;
                Mainform.location[1] = y;
                Mainform.location[2] = z;
                Mainform.location[3] = Mainform.location[1] + 1;
                Packets.PlayerPosition pp = new Packets.PlayerPosition(Mainform.nh.socket, Mainform);
                return;
            } else {
                Mainform.moving = true;
                while (Mainform.moving) {
                    while ((int)Mainform.location[1] != y) {
                        if (Mainform.location[1] < y) {
                            Mainform.location[1] += 1;
                            Mainform.location[3] = Mainform.location[1] + 1;
                            Packets.PlayerPosition pp = new Packets.PlayerPosition(Mainform.nh.socket, Mainform);
                        } else {
                            Mainform.location[1] -= 1;
                            Mainform.location[3] = Mainform.location[1] + 1;
                            Packets.PlayerPosition pp = new Packets.PlayerPosition(Mainform.nh.socket, Mainform);
                        }
                    }
                    while ((int)Mainform.location[0] != x) {
                        if (Mainform.location[0] < x) {
                            Mainform.location[0] += 1;
                            Packets.PlayerPosition pp = new Packets.PlayerPosition(Mainform.nh.socket, Mainform);
                        } else {
                            Mainform.location[0] -= 1;
                            Packets.PlayerPosition pp = new Packets.PlayerPosition(Mainform.nh.socket, Mainform);
                        }
                    }
                    while ((int)Mainform.location[2] != z) {
                        if (Mainform.location[2] < z) {
                            Mainform.location[2] += 1;
                            Packets.PlayerPosition pp = new Packets.PlayerPosition(Mainform.nh.socket, Mainform);
                        } else {
                            Mainform.location[2] -= 1;
                            Packets.PlayerPosition pp = new Packets.PlayerPosition(Mainform.nh.socket, Mainform); 
                        }
                    }
                    Mainform.moving = false;
                }
            }
        }
        public void readMetadata(Wrapped.Wrapped socket) {
            // read metadata from socket
            do {

                byte item = socket.readByte();

                if (item == 127) break;
                int index = item & 0x1F;
                int type = item >> 5;

                switch (type) {
                    case 0:
                        socket.readByte();
                        break;
                    case 1:
                        socket.readShort();
                        break;
                    case 2:
                        socket.readInt();
                        break;
                    case 3:
                        socket.readFloat();
                        break;
                    case 4:
                        socket.readString();
                        break;
                    case 5:
                        readSlot(socket);
                        break;
                    case 6:
                        socket.readInt();
                        socket.readInt();
                        socket.readInt();
                        break;

                }
            } while (true);
        }

        public string strip_codes(string text) {
            // Strips the color codes from text.
            string smessage = text;

            if (smessage.Contains("§")) {

                smessage = smessage.Replace("§0", "");
                smessage = smessage.Replace("§1", "");
                smessage = smessage.Replace("§2", "");
                smessage = smessage.Replace("§3", "");
                smessage = smessage.Replace("§4", "");
                smessage = smessage.Replace("§5", "");
                smessage = smessage.Replace("§6", "");
                smessage = smessage.Replace("§7", "");
                smessage = smessage.Replace("§8", "");
                smessage = smessage.Replace("§9", "");
                smessage = smessage.Replace("§a", "");
                smessage = smessage.Replace("§b", "");
                smessage = smessage.Replace("§c", "");
                smessage = smessage.Replace("§d", "");
                smessage = smessage.Replace("§e", "");
                smessage = smessage.Replace("§f", "");
                smessage = smessage.Replace("§A", "");
                smessage = smessage.Replace("§B", "");
                smessage = smessage.Replace("§C", "");
                smessage = smessage.Replace("§D", "");
                smessage = smessage.Replace("§E", "");
                smessage = smessage.Replace("§F", "");

            }

            return smessage;
        }

        public string getitembyslot(int slot, Form1 mainform) {
            // Return "Item" object from a slot number.
            string item = "";

            foreach (Classes.Item b in mainform.inventory) {
                if (b.slot == slot) {
                    item = b.itemname;
                    break;
                }
            }
            return item;
        }

        public int getItemCount(int slot, Form1 mainform) {
            // Return number of items from a certain inventory slot in bot inventory

            int count = 0;

            foreach (Classes.Item b in mainform.inventory) {
                if (b.slot == slot) {
                    count = b.itemCount;
                    break;
                }
            }

            return count;
        }

        public bool isNumeric(string test) {
            int thisOut;
            return (int.TryParse(test, out thisOut));
        }

        #region entity Help
        public void deleteEnt(int EID, Form1 mainform) {
            Classes.Entity blank = null;

            foreach (Classes.Entity b in mainform.Entitys) {
                if (b.EID == EID) {
                    blank = b;
                    break;
                }
            }

            if (blank != null)
                mainform.Entitys.Remove(blank);

        }
        public Entity getEntbyID(int EID, Form1 mainform) {
            Entity blank = null;

            foreach (Entity b in mainform.Entitys) {
                if (b.EID == EID) {
                    blank = b;
                    break;
                }
            }

            return blank;
        }
        public enum entityId {
            Creeper = 50,
            Skeleton,
            Spider,
            GiantZombie,
            Zombie,
            Slime,
            Ghast,
            ZombiePigman,
            Enderman,
            CaveSpider,
            Silverfish,
            Blaze,
            MagmaCube,
            EnderDragon,
            Wither,
            Bat,
            Witch,
            Pig = 90,
            Sheep,
            Cow,
            Chicken,
            Squid,
            Wolf,
            Mooshroom,
            Snowman,
            Ocelot,
            IronGolem,
            Villager = 120
        }

        #endregion


        public enum blockitemid {
            Air,
            Stone,
            Grass,
            Dirt,
            CobbleStone,
            Planks,
            Sapling,
            BedRock,
            Water,
            StationaryWater,
            Lava,
            StationaryLava,
            Sand,
            Gravel,
            GoldOre,
            IronOre,
            CoalOre,
            Wood,
            Leaves,
            Sponge,
            Glass,
            LapisLazuliOre,
            LapisLazuliBlock,
            Dispenser,
            SandStone,
            NoteBlock,
            Bed,
            PoweredRail,
            DetectorRail,
            StickyPison,
            Web,
            TallGrass,
            DeadBush,
            Piston,
            PistonExtension,
            Wool,
            Blackstuff,
            Dandelion,
            Rose,
            BrownMushroom,
            RedMushroom,
            GoldBlock,
            IronBlock,
            DoubleSlabs,
            Slabs,
            BrickBlock,
            TNT,
            BookShelf,
            MossStone,
            Obsidion,
            Torch,
            Fire,
            MonsterSpawner,
            OakwoodStairs,
            Chest,
            RedStoneWire,
            DiamondOre,
            DiamondBlock,
            CraftingTable,
            Seeds_Planted,
            Farmland,
            Furnace,
            BurningFurnace,
            SignPost,
            WoodenDoor,
            Ladders,
            Rails,
            CobbleStoneStairs,
            WallSign,
            Lever,
            StonePresurePlate,
            IronDoor,
            WoodenPressurePlate,
            RedStoneOre,
            GlowingRedStoneOre,
            RedStoneTorchoff,
            RedStoneTorchon,
            StoneButton,
            Snow,
            Ice,
            SnowBlock,
            Cactus,
            ClayBlock,
            SugarCane,
            JukeBox,
            Fence,
            Pumpkin,
            Netherrack,
            SoulSand,
            GlowStoneBlock,
            Portal,
            JackOLantern,
            CakeBlock,
            RedStoneRepeateroff,
            RedStoneRepeateron,
            LockedChest,
            Trapdoor,
            MonsterEgg,
            StoneBricks,
            HugeBrownMushroom,
            HugeRedMushroom,
            IronBars,
            GlassPane,
            Melon,
            PumpkinStem,
            MelonStem,
            Vines,
            FenceGate,
            BrickStairs,
            StoneBrickStairs,
            Mycelium,
            LilyPad,
            NetherBrickBlock,
            NetherBrickFence,
            NetherBrickStairs,
            NetherWart,
            EnchantmentTable,
            BrewingStand,
            Cauldron,
            EndPortal,
            EndPortalFrame,
            EndStone,
            DragonEgg,
            RedstoneLampoff,
            RedstoneLampon,
            WoodenDoubleSlab,
            WoodenSlab,
            Cocoa,
            SandstoneStairs,
            EmeraldOre,
            EnderChest,
            TripwireHook,
            Tripwire,
            EmeraldBlock,
            SpruceStairs,
            BirchStairs,
            JungleStairs,
            CommandBlock,
            Beacon,
            CobblestoneWall,
            FlowerPot,
            Carrots,
            Potatos,
            WoodenButton,
            MobHead,
            Anvil,
            TrappedChest,
            LightweightPressurePlate,
            HeavyPressurePlate,
            RedstoneComparatoroff,
            RedstoneComparatorOn,
            DaylightSensor,
            BlockofRedstone,
            NetherQuartzOre,
            Hopper,
            QuartzBlock,
            QuartzStairs,
            ActivatorRail,
            Dropper,
            StainedClay,

            HayBlock = 170,
            Carpet,
            HardenedClay,
            CoalBlock,

            IronShovel = 256,
            IronPickAxe,
            IronAxe,
            FlintAndSteel,
            Apple,
            Bow,
            Arrow,
            Coal,
            Diamond,
            IronIngot,
            GoldIngot,
            IronSword,
            WoodenSword,
            WoodenShovel,
            WoodenPickAxe,
            WoodenAxe,
            StoneSword,
            StoneShovel,
            StonePickaxe,
            StoneAxe,
            DiamondSword,
            DiamondShovel,
            DiamondPickAxe,
            DiamondAxe,
            Stick,
            Bowl,
            MushroomSoup,
            GoldSword,
            GoldShovel,
            GoldPickAxe,
            GoldAxe,
            String_item,
            Feather,
            GunPowder,
            WoodenHoe,
            StoneHoe,
            IronHoe,
            DiamondHoe,
            GoldHoe,
            Seeds,
            Wheat,
            Bread,
            LeatherCap,
            LeatherTunic,
            LeatherPants,
            LeatherBoots,
            ChainHelmet,
            ChainChestplate,
            ChainLeggings,
            ChainBoots,
            IronHelmet,
            IronChestplate,
            IronLeggings,
            IronBoots,
            DiamondHelmet,
            DiamondChestplate,
            DiamondLeggings,
            DiamondBoots,
            GoldHelmet,
            GoldChestplate,
            GoldLeggings,
            GoldBoots,
            Flint,
            RawPorkchop,
            CookedPorkchop,
            Painting,
            GoldenApple,
            Sign,
            WoodenDoor_Item,
            Bucket,
            WaterBucket,
            LavaBucket,
            Minecart,
            Saddle,
            IronCoor,
            RedStone,
            SnowBall,
            Boat,
            Leather,
            Milk,
            ClayBrick,
            Clay,
            SugarCane_Item,
            Paper,
            Book,
            SlimeBall,
            StorageMinecart,
            PoweredMinecart,
            Egg,
            Compass,
            FishingRod,
            Clock,
            GlowStoneDust,
            RawFish,
            CookedFish,
            Dye,
            Bone,
            Sugar,
            Cake,
            Bed_Item,
            RedstoneRepeater,
            Cookie,
            Map,
            Shears,
            MelonSlice,
            PumpkinSeeds,
            MelonSeeds,
            RawBeef,
            Steak,
            RawChicken,
            CookedChicken,
            RottenFlesh,
            EnderPearl,
            BlazeRod,
            GhastTear,
            GoldNugget,
            NetherWart_item,
            Potions,
            GlassBottle,
            SpiderEye,
            FermentedSpiderEye,
            BlazePowder,
            MagmaCream,
            BrewingStand_Item,
            Cauldron_Item,
            EyeofEnder,
            GlisteringMelon,
            SpawnEgg,
            EnchantingBottle,
            FireCharge,
            BookNQuill,
            WrittenBook,
            Emerald_item,
            ItemFrame,
            FlowerPot_item,
            Carrot,
            Potato,
            BakedPotato,
            PoisonPotato,
            EmptyMap,
            GoldenCarrot,
            MobHead_item,
            CarrotonStick,
            NetherStar,
            PumpkinPie,
            FireworkRocket,
            FireworkStar,
            EnchantedBook,
            RedstoneComparator,
            NetherBrick,
            netherQuartz,
            TNTCart,
            HopperCart,
            IronHorseArmor,
            GoldHorseArmor,
            DiamondHorseArmor,
            Lead,
            NameTag,

            Disk13 = 2256,
            CatDisk,
            BlocksDisk,
            ChirpDisk,
            FarDisk,
            MallDisk,
            MellohiDisk,
            StalDisk,
            StradDisk,
            WardDisk,
            Disk11,
            waitDisk
        }

    }
}
