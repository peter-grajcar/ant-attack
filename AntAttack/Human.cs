using System;
using System.Collections.Generic;

namespace AntAttack
{
    public abstract class Human : Entity
    {
        
        public enum State { Standing, Running, Jumping, Falling, Paralysed }
        
        public State CurrentState { get; set; }
        public int Health { get; set; }
        public int Ammo { get; set; }
        public bool Controllable { get; set; }
        public bool WasHit { get; set; }

        public Human Follow { get; set; }

        protected Human()
        {
            CurrentState = State.Standing;
            Health = 20;
            Ammo = 20;
            Controllable = false;
            Follow = null;
            WasHit = false;
        }

        private ulong _lastMove;
        private int _lastHealth;
        public override void Update()
        {
            WasHit = _lastHealth > Health;
            
            if (Paralysed > 0)
            {
                CurrentState = State.Paralysed;
                Paralysed--;
                return;
            }
            
            
            if (Controllable)
            {
                if (Time.T - _lastMove < 150)
                    return;
                
                Controller.Control(this);
                if (!Controller.DidMove)
                    Controller.DidMove |= Fall();
                
                if(!Controller.DidMove)
                    CurrentState = State.Standing;
                else
                    _lastMove = Time.T;
            }
            else
            {
                bool didMove = false;
                if (Time.T - _lastMove < 150)
                    return;
                
                if (Follow == null)
                {
                    foreach (Entity entity in AntAttack.Map.Entities)
                    {
                        if (entity != this && entity is Human human &&
                            Vector3.Dist(human.Position, Position) <= 2)
                        {
                            Follow = human;
                            AntAttack.Renderer.SetMessage("MY " + (this is Girl ? "HERO" : "HEROINE") + "!\n\nTAKE ME AWAY\nFROM ALL OF THIS!");
                        }
                    }
                    
                }
                else
                {
                    Vector3 v = FindPath();
                    if (Vector3.Dist(Position, Follow.Position) <= 2)
                        v = Position;

                    if (v.Z > Position.Z)
                    {
                        didMove |= Jump();
                    }
                    else if (v.Z == Position.Z && v != Position)
                    {
                        int dir = Array.FindIndex(Forward, u => (u == v - Position));

                        if (Direction - dir > 0)
                            didMove |= TurnRight();
                        else if (Direction - dir < 0)
                            didMove |= TurnLeft();
                        else
                            didMove |= MoveForward();
                    }
                }

                if(!didMove)
                    didMove |= Fall();

                if (!didMove)
                    CurrentState = Follow == null ? State.Paralysed : State.Standing;
                else
                    _lastMove = Time.T;
            }

            _lastHealth = Health;
        }

        public bool MoveForward()
        {
            if (Position.Z == 0 || AntAttack.Map.Get(Position - new Vector3(0, 0, 1)) != Map.Air 
                                || CurrentState == State.Jumping)
            {
                Vector3 f = Position + Forward[Direction];
                if (AntAttack.Map.IsOnMap(f) &&AntAttack.Map.Get(f) == Map.Air)
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
            if ((Position.Z == 0 || AntAttack.Map.Get(Position - new Vector3(0, 0, 1)) != Map.Air) 
                && CurrentState != State.Jumping 
                && AntAttack.Map.Get(Position + new Vector3(0, 0, 1)) == Map.Air)
            {
                Position += new Vector3(0, 0, 1);
                CurrentState = State.Jumping;
                
                return true;
            }

            return false;
        }

        public bool ThrowGrenade()
        {
            if (Ammo > 0 && AntAttack.Map.Get(Position + Forward[Direction]) == Map.Air)
            {
                Ammo--;
                Grenade grenade = new Grenade();
                grenade.Position = Position + Forward[Direction];
                AntAttack.Map.AddEntity(grenade);
                grenade.Velocity = Forward[Direction];
                return true;
            }
            return false;
        }
            

        private bool Fall()
        {
            if(Position.Z > 0 && AntAttack.Map.Get(Position - new Vector3(0, 0, 1)) == Map.Air)
            {
                Position -= new Vector3(0, 0, 1);
                CurrentState = State.Falling;
                
                return true;
            }

            return false;
        }

        private Vector3 FindPath()
        {
            if (Follow == null)
                return Position;
            
            int[,,] path = new int[AntAttack.Map.Width, AntAttack.Map.Height, AntAttack.Map.Depth];


            int dist = 1;
            Queue<Vector3> queue = new Queue<Vector3>();
            
            queue.Enqueue(Follow.Position);
            path[Follow.Position.X, Follow.Position.Y, Follow.Position.Z] = dist;
            
            Vector3[] directions =
            {
                new Vector3(1, 0, 0),
                new Vector3(-1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, -1, 0),
                new Vector3(0, 0, 1),
                new Vector3(0, 0, -1),
            };
            
            while (queue.Count != 0 && path[Position.X, Position.Y, Position.Z] == 0)
            {
                Vector3 v = queue.Dequeue();
                dist = path[v.X, v.Y, v.Z];
                if (dist > 50)
                    return Position;
                
                foreach (Vector3 u in directions)
                {
                    Vector3 w = v + u;
                    if (AntAttack.Map.IsOnMap(w) && path[w.X, w.Y, w.Z] == 0 && 
                        (AntAttack.Map.Get(w) == Map.Air || w == Position))
                    {
                        queue.Enqueue(w);
                        path[w.X, w.Y, w.Z] = dist + 1;
                    }
                }

            }
            
            dist = path[Position.X, Position.Y, Position.Z];
            foreach (Vector3 u in directions)
            {
                Vector3 w = Position + u;
                if(AntAttack.Map.IsOnMap(w))
                {
                    if (path[w.X, w.Y, w.Z] == dist - 1)
                        return Position + u;
                }
            }
            
            return Position;
        }
    }
}