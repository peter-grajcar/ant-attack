using System.Drawing;
using System.Dynamic;

namespace AntAttack
{
    
    public abstract class Entity : IRenderable
    {
        protected int _direction = 0;
        protected Vector3 _position = new Vector3(0, 0, 0);
        protected Map _map;
        
        protected Vector3[] _forward = { 
            new Vector3(1, 0,0),
            new Vector3(0, -1,0),
            new Vector3(-1, 0,0), 
            new Vector3(0, 1,0) 
        };
        
        
        public int Direction
        {
            get => _direction;
            set => _direction = value % 4;
        }  
        public Vector3 Position { 
            get => _position;
            set { 
                _map.Move(this, value);
                _position = value;
            }
        }


        protected Entity(Map map)
        {
            _map = map;
        }
        
        
        public abstract Bitmap GetTexture(Renderer.Direction direction);

        public abstract void Update();


        public void TurnRight()
        {
            Direction = (Direction + 3) % 4;
        }
        public void TurnLeft()
        {
            Direction = (Direction + 1) % 4;
        }
        public Map GetMap()
        {
            return _map;
        }
    }
}