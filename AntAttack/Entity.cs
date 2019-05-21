using System.Drawing;
using System.Dynamic;

namespace AntAttack
{
    
    public abstract class Entity : IRenderable
    {
        protected int _direction = 0;
        protected Map _map;
        
        
        public int Direction
        {
            get => _direction;
            set => _direction = value % 4;
        }
        
        public Vector3 Position { get; set; } = new Vector3(0,0, 0);

        
        protected Entity(Map map)
        {
            _map = map;
        }
        
        
        public abstract Bitmap GetTexture(Renderer.Direction direction);

        public abstract void Update();


        public void TurnRight()
        {
            Direction = (Direction + 1) % 4;
        }
        public void TurnLeft()
        {
            Direction = (Direction + 3) % 4;
        }
        public Map GetMap()
        {
            return _map;
        }
    }
}