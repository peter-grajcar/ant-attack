namespace AntAttack
{
    public abstract class Human : Entity
    {
        
        public enum State { Standing, Running, Jumping, Falling }
        
        public State CurrentState { get; set; }
        public int Health { get; set; }
        public int Ammo { get; set; }
        public bool Controllable { get; set; }
        
        
        protected Human(Map map) : base(map)
        {
            CurrentState = State.Standing;
            Health = 20;
            Ammo = 10;
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
                //TODO: implement human AI
                
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
                if (_map.IsOnMap(f) &&_map.Get(f) == Map.Air)
                {
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
                Position += new Vector3(0, 0, 1);
                CurrentState = State.Jumping;
                
                return true;
            }

            return false;
        }

        public bool ThrowGrenade()
        {
            if (Ammo > 0)
            {
                Ammo--;
                Grenade grenade = new Grenade(_map);
                grenade.Position = Position + _forward[Direction];
                _map.AddEntity(grenade);
                grenade.Velocity = _forward[Direction];
                return true;
            }
            return false;
        }
            

        private bool Fall()
        {
            if(Position.Z > 0 && _map.Get(Position - new Vector3(0, 0, 1)) == Map.Air)
            {
                Position -= new Vector3(0, 0, 1);
                CurrentState = State.Falling;
                
                return true;
            }

            return false;
        }
    }
}