using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C_Minebot.Classes;
using System.Threading;
using C_Minebot.Properties;

namespace C_Minebot {
    public partial class Entity_Tracker : Form {
        Form1 mainForm;
        int zoom = 1;
        int radius = 10;
        bool grid = false;

        public Entity_Tracker() {
            InitializeComponent();
        }

        private void Entity_Tracker_Load(object sender, EventArgs e) {

        }
        private void Entity_tracker_close(object sender, EventArgs e) {
                timer1.Enabled = false;
        }
        public void startTracking(Form1 main) {
            mainForm = main;
            timer1.Enabled = true;
        }

        private void drawLoop() {

                pictureBox1.BackColor = Color.Black;
                Bitmap map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                Graphics g;
                g = Graphics.FromImage(map);

                int cirRadius = (radius * 2) * zoom;
                g.DrawRectangle(new Pen(Color.Blue, (float)3.0), pictureBox1.Width / 2, pictureBox1.Height / 2, 3, 3); // Represents the bot. Constant size.
                g.DrawRectangle(new Pen(Color.White, (float)1.0), (pictureBox1.Width / 2) - (cirRadius / 2), (pictureBox1.Height / 2) - (cirRadius / 2), cirRadius, cirRadius);
                
                // Draw in gray lines to represent blocks.

                if (grid == true) {
                    for (int i = 0; i < (radius * 2); i++) {
                        for (int b = 0; b < (radius * 2); b++) {
                            g.DrawRectangle(new Pen(Color.DarkGray, (float)1.0), ((pictureBox1.Width / 2) - (cirRadius / 2) + (i * zoom)), ((pictureBox1.Height / 2) - (cirRadius / 2) + (b * zoom)), zoom, zoom);
                        }
                    }
                }

                try {
                    foreach (Entity b in mainForm.Entitys) {
                        if (Math.Abs(((b.X / 32) - (int)mainForm.location[0])) < pictureBox1.Width / 2) { // Restrain to within the size of this picturebox.
                            if (Math.Abs(((b.Z / 32) - (int)mainForm.location[2])) < pictureBox1.Height / 2) {

                                // Skip entities 10 blocks above or below us.
                                if ((b.Y / 32) < ((int)mainForm.location[1]) - radius || (b.Y / 32) > ((int)mainForm.location[1] + radius)) { 
                                    continue;
                                }

                                // Filter out for our radius
                                if ((b.X / 32) < ((int)mainForm.location[0]) - radius || (b.X / 32) > ((int)mainForm.location[0] + radius)) {
                                    continue;
                                }
                                if ((b.Z / 32) < ((int)mainForm.location[2]) - radius || (b.Z / 32) > ((int)mainForm.location[2] + radius)) {
                                    continue;
                                }

                                int startingX = pictureBox1.Width / 2;
                                int startingZ = pictureBox1.Height / 2;

                                startingX = startingX + (((b.X / 32) - ((int)mainForm.location[0])) * zoom);
                                startingZ = startingZ + (((b.Z / 32) - ((int)mainForm.location[2])) * zoom);

                                // Draw our pictures to the box
                                switch (b.name) {
                                    case "Creeper":
                                        g.DrawImage(Resources.CreeperFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Skeleton":
                                        g.DrawImage(Resources.SkeletonFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Spider":
                                        g.DrawImage(Resources.SpiderFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "GiantZombie":
                                        g.DrawImage(Resources.ZombieFace, startingX, startingZ, 2 * zoom, 2 * zoom);
                                        break;
                                    case "Zombie":
                                        g.DrawImage(Resources.ZombieFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Slime":
                                        g.DrawImage(Resources.SlimeFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Ghast":
                                        g.DrawImage(Resources.GhastFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "ZombiePigman":
                                        g.DrawImage(Resources.ZombiePigmanFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Enderman":
                                        g.DrawImage(Resources.EndermanFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "CaveSpider":
                                        g.DrawImage(Resources.CaveSpiderFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Silverfish":
                                        g.DrawImage(Resources.SilverfishFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Blaze":
                                        g.DrawImage(Resources.Blaze_Face, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "MagmaCube":
                                        g.DrawImage(Resources.MagmaCubeFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "EnderDragon":
                                        g.DrawImage(Resources.Enderdragon_face_3, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Wither":
                                        g.DrawImage(Resources.WitherHead, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Bat":
                                        g.DrawImage(Resources.BatFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Witch":
                                        g.DrawImage(Resources.Witchface, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Pig":
                                        g.DrawImage(Resources.PigFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Squid":
                                        g.DrawImage(Resources.Squidface, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Wolf":
                                        g.DrawImage(Resources.WolfFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Mooshroom":
                                        g.DrawImage(Resources.MooshroomFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Snowman":
                                        g.DrawImage(Resources.Snowmanface, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Ocelot":
                                        g.DrawImage(Resources.OcelotFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "IronGolem":
                                        g.DrawImage(Resources.IronGolemFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Villager":
                                        g.DrawImage(Resources.VillagerFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Sheep":
                                        g.DrawImage(Resources.SheepFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Cow":
                                        g.DrawImage(Resources.CowFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    case "Chicken":
                                        g.DrawImage(Resources.ChickenFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                    default:
                                        g.DrawImage(Resources.HumanFace, startingX, startingZ, 1 * zoom, 1 * zoom);
                                        break;
                                }
                                
                            }
                        }
                    }

                    setImage(map);
                }
                catch {
                    // Bot tends to throw a bunch of "Cannot continue due to collection being modified" (an entity being added or removed)
                    // So... This catch is just to ignore that little error and continue rendering.
                }
        }

        private delegate void setImage_Safe(Bitmap map);

        private void setImage(Bitmap map) {
            if (this.InvokeRequired) {
                this.Invoke(new setImage_Safe(setImage), map);
                return;
            }

            pictureBox1.Image = map;
            pictureBox1.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            drawLoop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            zoom = trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e) {
            radius = int.Parse(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e) {
            int cirRadius = (radius * 2) * zoom;

            pictureBox1.Width = cirRadius;
            pictureBox1.Height = cirRadius;
        }

        private void button2_Click_1(object sender, EventArgs e) {
            if (grid == true)
                grid = false;
            else
                grid = true;
        }
    }
}
