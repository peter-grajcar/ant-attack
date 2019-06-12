using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntAttack
{
    public partial class Form1 : Form
    {
        private Graphics _graphics;
        public static Renderer Renderer;
        public static Map Map;

        private Boy boy;
        private Girl girl;
        
        public Form1()
        {
            InitializeComponent();
            timer.Enabled = true;
            ConsoleUtil.init();
            
            
            Bitmap canvasBmp = new Bitmap(800, 600);
            canvas.Image = canvasBmp;
            
            _graphics = Graphics.FromImage(canvasBmp);
            Renderer = new Renderer(_graphics, 30);
            Map = new Map("AntAttack/Resources/map.txt");
            
            Renderer.Centre.Y += 100;
            
            // TODO: Load entities from map file
            boy = new Boy();
            boy.Position = new Vector3(39, 20, 0);
            boy.Controllable = true;
            Map.AddEntity(boy);
            Renderer.Centre = boy.Position;
            
            girl = new Girl();
            girl.Position = new Vector3(9, 25, 4);
            Map.AddEntity(girl);
            
            Ant ant = new Ant();
            ant.Position = new Vector3(10, 20,0);
            ant.Target = boy;
            Map.AddEntity(ant);
            
        }

        public void OnTick(object sender, EventArgs e)
        {
            Map.CreateAndDestroyEntities();
            
            foreach (Entity entity in Map.Entities)
            {
                entity.Update();
            }
            
            Keyboard.KeyPressed = Keys.None;
            Renderer.RenderMap(Map);
            Renderer.RenderGUI(boy, girl);
            canvas.Refresh();
            Time.Tick();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keyboard.KeyPressed = keyData;
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
