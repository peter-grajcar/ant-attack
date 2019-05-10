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
            _renderer = new Renderer(_graphics, 20);
            _map = new Map("AntAttack/Resources/map.txt");
        }

        public void OnTick(object sender, EventArgs e)
        {
            _renderer.RenderMap(_map);
            
            Vector2 d = new Vector2(0, 0);
            switch (_keyPressed)
            {
                    case Keys.Up: 
                        d.Y = _renderer.VerticalSize/2;
                        break;
                    case Keys.Down:
                        d.Y = -_renderer.VerticalSize/2;
                        break;
                    case Keys.Left: 
                        d.X = _renderer.HorizontalSize/2;
                        break;
                    case Keys.Right:
                        d.X = -_renderer.HorizontalSize/2;
                        break;
                    case Keys.Space:
                        _renderer.Orientation = _renderer.Orientation == Renderer.Direction.NorthEast
                            ? Renderer.Direction.SouthEast
                            : Renderer.Direction.NorthEast;
                        break;
            }

            _renderer.Centre += d;
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
