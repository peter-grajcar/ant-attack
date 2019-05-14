using System.Drawing;

namespace AntAttack
{
    public class Boy : Entity
    {
        public Boy()
        {
            
        }
        
        public override Bitmap GetTexture()
        {
            return SpriteLoader.GetSpriteLoader().GetSprite(SpriteLoader.Sprite.Boy, Direction);
        }

        public override void Update()
        {
            
        }
    }
}