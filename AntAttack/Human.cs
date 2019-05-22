namespace AntAttack
{
    public abstract class Human : Entity
    {
        private Vector3[] _forward = { 
            new Vector3(1, 0,0),
            new Vector3(0, -1,0),
            new Vector3(-1, 0,0), 
            new Vector3(0, 1,0) 
        };
        
        public enum State { Standing, Running, Jumping, Falling }
        
        public State CurrentState { get; set; }
        public int Health { get; set; }
        public int Ammo { get; set; }
        public bool Controllable { get; set; }
        
        
        protected Human(Map map) : base(map)
        {
            CurrentState = State.Standing;
            Health = 20;
            Controllable = false;
        }

        public override void Update()
        {

            if (Controllable)
            {
                Controller.Control(this);
                if (!Controller.DidMove)
                    Controller.DidMove |= Fall();
                
                if(!Controller.DidMove)
                    CurrentState = State.Standing;
            }
            else
            {
                bool didMove = false;
                didMove |= Fall();

                if (!didMove)
                    CurrentState = State.Standing;
            }


        }

        public bool MoveForward()
        {
            if (Position.Z == 0 || _map.Get(Position - new Vector3(0, 0, 1)) != Map.Air 
                                || CurrentState == State.Jumping)
            {
                Vector3 f = Position + _forward[Direction];
                if (_map.Get(f) == Map.Air)
                {
                    _map.Move(this, f);
                    Position = f;
                    
                    if(CurrentState != Human.State.Running)
                        CurrentState = Human.State.Running;
                    else
                        CurrentState = Human.State.Standing;

                    return true;
                }
            }

            return false;
        }

        public bool Jump()
        {
            if ((Position.Z == 0 || _map.Get(Position - new Vector3(0, 0, 1)) != Map.Air) 
                && CurrentState != State.Jumping 
                && _map.Get(Position + new Vector3(0, 0, 1)) == Map.Air)
            {
                _map.Move(this, Position + new Vector3(0, 0, 1));
                CurrentState = State.Jumping;
                
                return true;
            }

            return false;
        }

        public bool ThrowGrenade()
        {
            //TODO: implement method
            return false;
        }
            

        private bool Fall()
        {
            if(Position.Z > 0 && _map.Get(Position - new Vector3(0, 0, 1)) == Map.Air)
            {
                _map.Move(this, Position - new Vector3(0, 0, 1));
                CurrentState = State.Falling;
                
                return true;
            }

            return false;
        }
    }
}