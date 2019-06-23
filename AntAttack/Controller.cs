using System.Windows.Forms;

namespace AntAttack
{
    public static class Controller
    {
        public static bool DidMove { get; set;  }
        
        public static void Control(Human human)
        {
            DidMove = false;
            
            switch (Keyboard.KeyPressed)
            {
                case Keys.M: 
                    human.TurnLeft();
                    break;
                case Keys.Oemcomma:
                    human.TurnRight();
                    break;
                case Keys.V:
                    DidMove |= human.MoveForward();
                    break;
                case Keys.C:
                    DidMove |= human.Jump();
                    break;
                case Keys.G:
                    human.ThrowGrenade();
                    break;
                case Keys.Space:
                    AntAttack.Renderer.Orientation = AntAttack.Renderer.Orientation == Renderer.Direction.NorthEast
                        ? Renderer.Direction.SouthEast
                        : Renderer.Direction.NorthEast;
                    break;
            }
            
            Keyboard.KeyPressed = Keys.None;
            AntAttack.Renderer.Centre.X = human.Position.X;
            AntAttack.Renderer.Centre.Y = human.Position.Y;
        }
    }
}