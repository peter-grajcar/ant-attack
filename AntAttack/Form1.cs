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
            _map = new Map();
        }

        public void OnTick(object sender, EventArgs e)
        {
            _renderer.Render(_map);
        }
    }
}
