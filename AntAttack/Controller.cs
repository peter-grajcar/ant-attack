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
                case Keys.G:
                    human.ThrowGrenade();
                    break;
                case Keys.Space:
                    Form1.Renderer.Orientation = Form1.Renderer.Orientation == Renderer.Direction.NorthEast
                        ? Renderer.Direction.SouthEast
                        : Renderer.Direction.NorthEast;
                    break;
            }
            
            // TODO: Fix dynamic camera
            // Dynamic camera
            /* Vector2 pos = Form1.Renderer.TransformCoordinates(human.Position);
            Vector3 diff = new Vector3(0, 0, 0);
            if(pos.Y < 100)
                diff.Y = -1;
            else if(pos.Y > 500)
                diff.Y = 1;
            if(pos.X < 100)
                diff.X = -1;
            else if(pos.X > 700)
                diff.X = 1;
            Form1.Renderer.Centre += diff; */
            
            // Following camera
            Form1.Renderer.Centre.X = human.Position.X;
            Form1.Renderer.Centre.Y = human.Position.Y;
        }
    }
}