using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Vortex;
using Vortex.Drawing;
using C_Minebot.Classes;

namespace C_Minebot {
    public partial class ChunkViewer : Form {

        SingleContextDevice _Device;
        Canvas2D _Canvas;
        Texture Tileset;
        Point offset;
        int size = 20;
        int halfsize = 10;
        Bitmap chunkRender;
        Form1 mainForm;

        public ChunkViewer(Form1 mainform) {
            InitializeComponent();
            this.Show();
            _Device = new SingleContextDevice(this.pictureBox1.Handle);
            _Canvas = _Device.Context.Canvas;
            Tileset = new Texture(Properties.Resources.tilesetHQ);
            mainForm = mainform;
            while (true) {
                //call drawrendeR()
                //Application.doevents()
            }
        }

        private void drawRender() {
            int blocksdrawn;
            List<Entity> renderEntitys = mainForm.Entitys;
            Queue<Chunk> RenderChunks = new Queue<Chunk>();
            List<Chunk> nearChunks = new List<Chunk>();

            foreach (Chunk curChunk in mainForm.Chunks) {
                if (Math.Sqrt((curChunk.x - (int)mainForm.location[0]) ^ 2 + (curChunk.z - (int)mainForm.location[2]) ^ 2) <= 3 * 16) {
                    nearChunks.Add(curChunk);
                }
            }

            if (nearChunks.Count == 0) { return; }

            Chunk lowestchunk = nearChunks[0];

            while (nearChunks.Count > 0) {
                foreach (Chunk CurChunk in nearChunks) {
                    if (((CurChunk.x - (int)mainForm.location[0]) + (CurChunk.z - (int)mainForm.location[2])) < ((lowestchunk.x - (int)mainForm.location[0]) + (lowestchunk.z - (int)mainForm.location[2])))
                        lowestchunk = CurChunk;
                }

                RenderChunks.Enqueue(lowestchunk);
                nearChunks.Remove(lowestchunk);

                if (nearChunks.Count > 0) {
                    lowestchunk = nearChunks[0];
                }
            }

            if (_Device.BeginScene()) {
   
            
            }
        }
        private void ChunkViewer_Load(object sender, EventArgs e) {

        }

        private void pictureBox1_Click(object sender, EventArgs e) {

        }
    }
}
