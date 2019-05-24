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
        private Renderer _renderer;
        private Map _map;
        
        public Form1()
        {
            InitializeComponent();
            timer.Enabled = true;
            
            Bitmap canvasBmp = new Bitmap(800, 600);
            canvas.Image = canvasBmp;
            
            _graphics = Graphics.FromImage(canvasBmp);
            _renderer = new Renderer(_graphics, 20);
            _map = new Map("AntAttack/Resources/map.txt");
            
            _renderer.Centre.Y += 100;
            
            Boy boy = new Boy(_map);
            boy.Position = new Vector3(10, 10, 0);
            boy.Controllable = true;
            _map.AddEntity(boy);
            
            Girl girl = new Girl(_map);
            girl.Position = new Vector3(9, 15, 4);
            _map.AddEntity(girl);
        }

        public void OnTick(object sender, EventArgs e)
        {
            _map.CreateAndDestroyEntities();
            
            foreach (Entity entity in _map.Entities)
            {
                entity.Update();
            }
            
            Keyboard.KeyPressed = Keys.None;
            _renderer.RenderMap(_map);
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
