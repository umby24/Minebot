using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot {
    class functions {

        public void readMetadata(Wrapped.Wrapped socket) {
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

        public void readSlot(Wrapped.Wrapped socket, bool inventory = false, Form1 Mainform = null, short slot = 500) {
            // Just in case..
            bool delete = false;
            Classes.Item existing = null;

            if (inventory == true & Mainform == null) {
                throw new Exception("If inventory is true, you must include a main form.");
            }
            if ((inventory == true) & slot == 500) {
                throw new Exception("If inventory is true, you must include which slot to add to.");
            }

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
            return int.TryParse(test, out thisOut);
        }

        //TODO: Add newer items to this enum..
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
            WaterSpring,
            Lava,
            LavaSpring,
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
            WoodenStairs,
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
            HiddenSilverfish,
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
            NetherBrick,
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
        }
    }
}
