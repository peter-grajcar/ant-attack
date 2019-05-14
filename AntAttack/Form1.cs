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
            
            _boy = new Boy();
            _boy.Position = new Vector3(10, 10, 0);
            _map.AddEntity(_boy);
        }

        public void OnTick(object sender, EventArgs e)
        {
            _renderer.RenderMap(_map);
            canvas.Refresh();
            
            Vector3 d = new Vector3(0, 0, 0);
            switch (_keyPressed)
            {
                    case Keys.Up: 
                        d.X = -1;
                        break;
                    case Keys.Down:
                        d.X = 1;
                        break;
                    case Keys.Left:
                        d.Y = 1;
                        break;
                    case Keys.Right:
                        d.Y = -1;
                        break;
                    case Keys.Space:
                        _renderer.Orientation = _renderer.Orientation == Renderer.Direction.NorthEast
                            ? Renderer.Direction.SouthEast
                            : Renderer.Direction.NorthEast;
                        break;
            }

            _map.Move(_boy, _boy.Position + d);
            
            
            _keyPressed = Keys.None;
            
            Time.Tick();
        }

        private Keys _keyPressed;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            _keyPressed = keyData;
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
