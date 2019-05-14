using System.Drawing;
using System.Dynamic;

namespace AntAttack
{
    
    public abstract class Entity : IRenderable
    {
        private int _direction = 0;
        
        public int Direction
        {
            get => _direction;
            set => _direction = value % 4;
        }
        public Vector3 Position { get; set; } = new Vector3(0,0, 0);
        
        public abstract Bitmap GetTexture();

        public abstract void Update();
    }
}