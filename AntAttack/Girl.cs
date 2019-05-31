using System.Drawing;

namespace AntAttack
{
    public class Girl : Human
    {
        
        public override Bitmap GetTexture(Renderer.Direction direction)
        {
            int dir = (Direction + (direction == Renderer.Direction.SouthEast ? 3 : 0)) % 4;
            switch (CurrentState)
            {
                case State.Standing:
                    return SpriteLoader.GetSpriteLoader().GetSprite(SpriteLoader.Sprite.Girl, dir);
                case State.Running:
                    return SpriteLoader.GetSpriteLoader().GetSprite(SpriteLoader.Sprite.GirlRun, dir);
                case State.Jumping:
                    return SpriteLoader.GetSpriteLoader().GetSprite(SpriteLoader.Sprite.GirlJump, dir);
                case State.Falling:
                    return SpriteLoader.GetSpriteLoader().GetSprite(SpriteLoader.Sprite.GirlFall, dir);
                default:
                    return null;
            }
        }

       
    }
}