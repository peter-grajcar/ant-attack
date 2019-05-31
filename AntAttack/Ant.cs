using System;
using System.Drawing;

namespace AntAttack
{
    public class Ant : Entity
    {

        public enum State { Standing, Running };
        
        
        public State CurrentState { get; set; }
        public Entity Target { get; set; }
        

        public override Bitmap GetTexture(Renderer.Direction direction)
        {
            int dir = (Direction + (direction == Renderer.Direction.SouthEast ? 3 : 0)) % 4;
            switch (CurrentState)
            {
                case State.Standing:
                    return SpriteLoader.GetSpriteLoader().GetSprite(SpriteLoader.Sprite.Ant, dir);
                case State.Running:
                    return SpriteLoader.GetSpriteLoader().GetSprite(SpriteLoader.Sprite.AntRun, dir);
                default:
                    return null;
            }
        }

        public override void Update()
        {
            //TODO: implement ant AI
        }
    }
}