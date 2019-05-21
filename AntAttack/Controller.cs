using System.Windows.Forms;

namespace AntAttack
{
    public class Controller
    {
        
        public static void Control(Human human)
        {
            Vector3 d = new Vector3(0, 0, 0);
            switch (Keyboard.KeyPressed)
            {
                case Keys.Up: 
                    d.X = -1;
                    human.Direction = 2;
                    break;
                case Keys.Down:
                    d.X = 1;
                    human.Direction = 0;
                    break;
                case Keys.Left:
                    d.Y = 1;
                    human.Direction = 3;
                    break;
                case Keys.Right:
                    d.Y = -1;
                    human.Direction = 1;
                    break;
                case Keys.C:
                    d.Z = 1;
                    break;
                case Keys.Space:
                    Renderer.Orientation = Renderer.Orientation == Renderer.Direction.NorthEast
                        ? Renderer.Direction.SouthEast
                        : Renderer.Direction.NorthEast;
                    break;
            }

            if (human.CurrentState == Human.State.Jumping)
                d.Z = 0;
            else if(human.Position.Z > 0 && human.GetMap().Get(human.Position.X, human.Position.Y, human.Position.Z - 1) == Map.Air)
                d = new Vector3(0, 0, -1);

            if (human.GetMap().Move(human, human.Position + d))
            {
                if (d.Z > 0)
                    human.CurrentState = Human.State.Jumping;
                else if (d.Z < 0)
                    human.CurrentState = Human.State.Falling;
                else if(human.CurrentState != Human.State.Running)
                    human.CurrentState = Human.State.Running;
                else
                    human.CurrentState = Human.State.Standing;
            }
            else
                human.CurrentState = Human.State.Standing;
        }
    }
}