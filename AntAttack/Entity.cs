using System.Drawing;

namespace AntAttack
{
    
    public abstract class Entity : IRenderable
    {
        private int _direction;
        private Vector3 _position = new Vector3(0, 0, 0);
        
        protected readonly Vector3[] Forward = { 
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
                AntAttack.Map.Move(this, value);
                _position = value;
            }
        }
        public int Paralysed { get; set; }
        
        
        public abstract Bitmap GetTexture(Renderer.Direction direction);

        public abstract void Update();


        public bool TurnRight()
        {
            Direction = (Direction + 3) % 4;
            return true;
        }

        public bool TurnLeft()
        {
            Direction = (Direction + 1) % 4;
            return true;
        }
    }
}