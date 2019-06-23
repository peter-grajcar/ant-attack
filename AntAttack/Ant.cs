using System;
using System.Collections.Generic;
using System.Drawing;

namespace AntAttack
{
    public class Ant : Entity
    {

        public enum State { Standing, Running };
        
        
        public State CurrentState { get; set; }
        public Human Target { get; set; }
        

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

        private ulong _lastMove;
        private ulong _lastBite;
        public override void Update()
        {
            bool didMove = false;
            if (Time.T - _lastMove < 150)
                return;
            
            if (AntAttack.Map.Get(Position + new Vector3(0, 0, 1)) == Map.Entity)
            {
                Paralysed = 40;
            }
            if (Paralysed > 0)
            {
                Paralysed--;
                return;
            }
            
            Human min  = null;
            foreach (Entity entity in AntAttack.Map.Entities)
            {
                if (entity is Human human)
                {
                    if(human.Position.Z > 0 || (!human.Controllable && human.Follow == null))
                        continue;
                    if(min == null || Vector3.Dist(min.Position, Position) < Vector3.Dist(human.Position, Position))
                        min = human;
                }
            }
            Target = min;

            if (Target == null)
            {
                Random random = new Random();
                int n = random.Next(5);
                switch (n)
                {
                   case 0:
                       TurnLeft();
                       break;
                   case 1:
                       TurnRight();
                       break;
                   default:
                       MoveForward();
                       break;
                }
                return;
            }

            Vector3 v = FindPath(Target.Position);
            
            if (v != Position)
            {
                int dir = Array.FindIndex(Forward, u => (u == v - Position));
                
                if(Direction - dir > 0)
                    didMove |= TurnRight();
                else if(Direction - dir < 0)
                    didMove |= TurnLeft();
                else
                {
                    didMove |= Bite();
                    if(!didMove)
                        didMove |= MoveForward();
                }

                if (didMove)
                    _lastMove = Time.T;
                else
                    CurrentState = Ant.State.Standing;
            }
        }
        
        public bool MoveForward()
        {
            Vector3 f = Position + Forward[Direction];
            if (AntAttack.Map.IsOnMap(f) && AntAttack.Map.Get(f) == Map.Air)
            {
                Position = f;
                if(CurrentState != Ant.State.Running)
                    CurrentState = Ant.State.Running;
                else
                    CurrentState = Ant.State.Standing;
                return true;
            }

            return false;
        }

        public bool Bite()
        {
            if (Time.T - _lastBite > 500 && Target != null && Vector3.Dist(Position, Target.Position) == 1)
            {
                Target.Health--;

                CurrentState = Ant.State.Running;
                _lastBite = Time.T;
                return true;
            }
            return false;
        }

        private Vector3 FindPath(Vector3 to)
        {
            if (Target == null)
                return Position;
            
            int[,] path = new int[AntAttack.Map.Width, AntAttack.Map.Height];


            int dist = 1;
            Queue<Vector2> queue = new Queue<Vector2>();
            
            Vector2 v = new Vector2(to.X, to.Y);
            queue.Enqueue(v);
            path[v.X, v.Y] = dist;
            
            while (queue.Count != 0 && path[Position.X, Position.Y] == 0)
            {
                v = queue.Dequeue();
                dist = path[v.X, v.Y];
                if (dist > 30)
                    return Position;
                
                foreach (Vector3 u in Forward)
                {
                    Vector2 w = new Vector2(v.X + u.X, v.Y + u.Y);
                    Vector3 w3 = new Vector3(w.X, w.Y, 0);
                    if (AntAttack.Map.IsOnMap(w3) && path[w.X, w.Y] == 0 && 
                        (AntAttack.Map.Get(w3) == Map.Air || w3 == Position))
                    {
                        queue.Enqueue(w);
                        path[w.X, w.Y] = dist + 1;
                    }
                }

            }
                

            dist = path[Position.X, Position.Y];
            foreach (Vector3 u in Forward)
            {
                Vector2 w = new Vector2(Position.X + u.X, Position.Y + u.Y);
                Vector3 w3 = new Vector3(w.X, w.Y, 0);
                if(AntAttack.Map.IsOnMap(w3))
                {
                    if (path[w.X, w.Y] == dist - 1)
                        return Position + u;
                }
            }
            
            return Position;
        }
    }
}