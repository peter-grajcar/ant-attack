using System;
using System.Drawing;
using System.Windows.Forms;

namespace AntAttack
{
    public class Boy : Human
    {
        
        public Boy(Map map) : base(map)
        {
            
        }
        
        public override Bitmap GetTexture(Renderer.Direction direction)
        {
            int dir = (Direction + (direction == Renderer.Direction.SouthEast ? 3 : 0)) % 4;
            switch (CurrentState)
            {
                case State.Standing:
                    return SpriteLoader.GetSpriteLoader().GetSprite(SpriteLoader.Sprite.Boy, dir);
                case State.Running:
                    return SpriteLoader.GetSpriteLoader().GetSprite(SpriteLoader.Sprite.BoyRun, dir);
                case State.Jumping:
                    return SpriteLoader.GetSpriteLoader().GetSprite(SpriteLoader.Sprite.BoyJump, dir);
                case State.Falling:
                    return SpriteLoader.GetSpriteLoader().GetSprite(SpriteLoader.Sprite.BoyFall, dir);
                default:
                    return null;
            }
                
            
        }
    }
}