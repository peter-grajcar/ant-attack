using System.Windows.Forms;

namespace AntAttack
{
    public class Controller
    {
        
        public static bool DidMove { get; set;  }
        
        public static void Control(Human human)
        {
            DidMove = false;
            
            Vector3 d = new Vector3(0, 0, 0);
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
                case Keys.Space:
                    Renderer.Orientation = Renderer.Orientation == Renderer.Direction.NorthEast
                        ? Renderer.Direction.SouthEast
                        : Renderer.Direction.NorthEast;
                    break;
            }
        }
    }
}