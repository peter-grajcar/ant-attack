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
        
        private Boy _boy;
        
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
            
            _boy = new Boy(_map);
            _boy.Position = new Vector3(10, 10, 0);
            _map.AddEntity(_boy);
        }

        public void OnTick(object sender, EventArgs e)
        {
            _boy.Update();
            
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
