using System;
using System.Drawing;
using System.Windows.Forms;

namespace AntAttack
{
    public class Boy : Entity
    {
        public enum State { Standing, Running, Jumping, Falling }
        
        public State CurrentState { get; set; }
        
        public Boy(Map map) : base(map)
        {
            CurrentState = State.Standing;
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

        public override void Update()
        {
            Vector3 d = new Vector3(0, 0, 0);
            switch (Keyboard.KeyPressed)
            {
                case Keys.Up: 
                    d.X = -1;
                    Direction = 2;
                    break;
                case Keys.Down:
                    d.X = 1;
                    Direction = 0;
                    break;
                case Keys.Left:
                    d.Y = 1;
                    Direction = 3;
                    break;
                case Keys.Right:
                    d.Y = -1;
                    Direction = 1;
                    break;
                case Keys.C:
                    d.Z = 1;
                    break;
                case Keys.Space:
                    Renderer.Orientation = Renderer.Orientation == Renderer.Direction.NorthEast
                        ? Renderer.Direction.SouthEast
                        : Renderer.Direction.NorthEast;
                    break;
                default:
                    if(Position.Z > 0 && _map.Get(Position.X, Position.Y, Position.Z - 1) == Map.Air)
                        d = new Vector3(0, 0, -1);
                    break;
            }

            if (_map.Move(this, Position + d))
            {
                if (d.Z > 0)
                    CurrentState = State.Jumping;
                if (d.Z < 0)
                    CurrentState = State.Falling;
                else
                    CurrentState = State.Running;
            }
            else
                CurrentState = State.Standing;
        }
    }
}