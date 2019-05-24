using System.Drawing;

namespace AntAttack
{
    public class Grenade : Entity
    {
        private const int distance = 4;
        private int _timer;
        
        public Vector3 Velocity { get; set; }

        public Grenade(Map map) : base(map)
        {
            _timer = 0;
        }

        public override Bitmap GetTexture(Renderer.Direction direction)
        {
            SpriteLoader loader = SpriteLoader.GetSpriteLoader();
            switch (_timer)
            {
                case distance + 1:
                    return loader.GetSprite(SpriteLoader.Sprite.Grenade, 1);
                case distance + 2:
                    return loader.GetSprite(SpriteLoader.Sprite.Grenade, 2);
                case distance + 3: 
                    return loader.GetSprite(SpriteLoader.Sprite.Grenade, 3);
                default:
                    return loader.GetSprite(SpriteLoader.Sprite.Grenade, 0);
            }
        }

        public override void Update()
        {
            if (_timer == distance + 3)
            {
                _map.RemoveEntity(this);
            }
            else if (_timer > 0 && _timer < distance)
            {
                if (_map.Get(Position + Velocity) != Map.Air)
                    Velocity = new Vector3(0, 0, 0);

                _map.Move(this, Position + Velocity);
            }

            _timer++;
        }
    }
}