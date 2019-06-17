using System.Drawing;
using System.Windows.Forms;

namespace AntAttack
{
    public class Grenade : Entity
    {
        private const int distance = 3;
        private int _timer;
        
        public Vector3 Velocity { get; set; }

        public Grenade()
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
                Form1.Map.RemoveEntity(this);
                foreach (Entity entity in Form1.Map.Entities)
                {
                    if (entity != this && Vector3.Dist(entity.Position, Position) <= 2)
                    {
                        if(entity is Ant)
                            Form1.Map.RemoveEntity(entity);
                        else if (entity is Human)
                            ((Human) entity).Health--;
                    }
                }
                return;
            }
            
            if (_timer >= distance)
            {
                Velocity = new Vector3(0, 0, 0);
            }
            
            if (_timer > 0)
            {
                if (Form1.Map.Get(Position + Velocity) != Map.Air)
                    Velocity = new Vector3(0, 0, 0);
                
                if (Form1.Map.Get(Position + Velocity - new Vector3(0, 0, 1)) == Map.Air)
                    Velocity.Z = -1;
                
                Position += Velocity;
                Velocity.Z = 0;
            }
            
            _timer++;
        }
    }
}