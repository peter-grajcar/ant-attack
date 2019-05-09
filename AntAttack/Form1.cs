using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            
            _graphics = CreateGraphics();
            _renderer = new Renderer(_graphics);
            _map = new Map("AntAttack/Resources/map.txt");
        }

        public void OnTick(object sender, EventArgs e)
        {
            _renderer.RenderMap(_map);
            
            Vector2 d = new Vector2(0, 0);
            switch (_keyPressed)
            {
                    case Keys.Up: 
                        d.Y = 5;
                        break;
                    case Keys.Down:
                        d.Y = -5;
                        break;
                    case Keys.Left: 
                        d.X = 5;
                        break;
                    case Keys.Right:
                        d.X = -5;
                        break;
            }

            _renderer.Position += d;
            _keyPressed = Keys.None;
        }

        private Keys _keyPressed;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            _keyPressed = keyData;
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
