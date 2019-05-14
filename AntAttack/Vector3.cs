namespace AntAttack
{
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
    }
}