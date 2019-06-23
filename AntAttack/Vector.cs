using System;
using System.Text;

namespace AntAttack
{
    public class Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        
        public static Vector2 operator +(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X + v.X, u.Y + v.Y);
        }
        public static Vector2 operator -(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X - v.X, u.Y - v.Y);
        }
    }
    
    public class Vector3
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector3(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        
        public static Vector3 operator +(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        }
        public static Vector3 operator -(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        }

        public static bool operator ==(Vector3 u, Vector3 v)
        {
            return (u.X == v.X && u.Y == v.Y && u.Z == v.Z);
        }

        public static bool operator !=(Vector3 u, Vector3 v)
        {
            return !(u == v);
        }

        public Vector3 copy()
        {
            return new Vector3(X, Y, Z);
        }
        
        public static int Dist(Vector3 u, Vector3 v)
        {
            return Math.Abs(u.X - v.X) + Math.Abs(u.Y - v.Y) + Math.Abs(u.Z - v.Z);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[")
                .Append(X)
                .Append(", ")
                .Append(Y)
                .Append(", ")
                .Append(Z)
                .Append("]");
            return builder.ToString();
        }
    }
}