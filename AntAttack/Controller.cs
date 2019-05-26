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
                    Vector2 oldPos = Form1.Renderer.TransformCoordinates(human.Position);
                    DidMove |= human.MoveForward();
                    Vector2 diff = oldPos - Form1.Renderer.TransformCoordinates(human.Position);
                    Form1.Renderer.Centre += diff;
                    break;
                case Keys.C:
                    DidMove |= human.Jump();
                    break;
                case Keys.G:
                    human.ThrowGrenade();
                    break;
                case Keys.Space:
                    Form1.Renderer.Orientation = Form1.Renderer.Orientation == Renderer.Direction.NorthEast
                        ? Renderer.Direction.SouthEast
                        : Renderer.Direction.NorthEast;
                    break;
            }
        }
    }
}