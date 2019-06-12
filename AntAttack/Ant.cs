using System;
using System.Collections.Generic;
using System.Drawing;

namespace AntAttack
{
    public class Ant : Entity
    {

        public enum State { Standing, Running };
        
        
        public State CurrentState { get; set; }
        public Entity Target { get; set; }
        

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

        private ulong lastMove = 0;
        public override void Update()
        {
            if (Time.T - lastMove < 200)
                return;
            
            Vector3 v = FindPath();

            if (v != Position)
            {
                int dir = Array.FindIndex(_forward, u => (u == v - Position));
                
                //TODO: fix turning
                if(Direction - dir > 0)
                    TurnRight();
                else if(Direction - dir < 0)
                    TurnLeft();
                else
                    MoveForward();

                lastMove = Time.T;
            }
        }
        
        public bool MoveForward()
        {
            Vector3 f = Position + _forward[Direction];
            if (Form1.Map.IsOnMap(f) && Form1.Map.Get(f) == Map.Air)
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

        private Vector3 FindPath()
        {
            if (Target == null)
                return Position;
            
            int[,] path = new int[Form1.Map.Width, Form1.Map.Height];


            int dist = 1;
            Queue<Vector2> queue = new Queue<Vector2>();
            
            Vector2 v = new Vector2(Target.Position.X, Target.Position.Y);
            queue.Enqueue(v);
            path[v.X, v.Y] = dist;
            
            while (queue.Count != 0 && path[Position.X, Position.Y] == 0)
            {
                v = queue.Dequeue();
                dist = path[v.X, v.Y];
                if (dist > 10)
                    return Position;
                
                foreach (Vector3 u in _forward)
                {
                    Vector2 w = new Vector2(v.X + u.X, v.Y + u.Y);
                    Vector3 w3 = new Vector3(w.X, w.Y, 0);
                    if (Form1.Map.IsOnMap(w3) && path[w.X, w.Y] == 0 && 
                        (Form1.Map.Get(w3) == Map.Air || w3 == Position))
                    {
                        queue.Enqueue(w);
                        path[w.X, w.Y] = dist + 1;
                    }
                }

            }
                

            dist = path[Position.X, Position.Y];
            foreach (Vector3 u in _forward)
            {
                Vector2 w = new Vector2(Position.X + u.X, Position.Y + u.Y);
                Vector3 w3 = new Vector3(w.X, w.Y, 0);
                if(Form1.Map.IsOnMap(w3))
                {
                    if (path[w.X, w.Y] == dist - 1)
                        return Position + u;
                }
            }
            
            return Position;
        }
    }
}