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
        public Vector2 Position { get; set; }
        
        public abstract void Render(Renderer renderer);

        public abstract void Update();
    }
}