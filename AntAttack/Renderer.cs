using System.Drawing;

namespace AntAttack
{
    public class Renderer
    {
        private Graphics _graphics;

        public Vector2 Position { get; set; }
        
        public Renderer(Graphics graphics)
        {
            _graphics = graphics;
        }

        public void Render(Map map)
        {
            Brush brush = new SolidBrush(Color.LightGray);
            _graphics.FillRectangle(brush, new Rectangle(0,0, 800, 600));
            
            DrawCube(new Vector2(100, 100), 20);
            DrawCube(new Vector2(100, 80), 20);
            DrawCube(new Vector2(120, 160), 20);
            DrawCube(new Vector2(60, 200), 20);
        }

        private void DrawCube(Vector2 pos, int size)
        {
            Brush brush;
            
            brush = new SolidBrush(Color.DimGray);
            Point[] side1 = {
                new Point(pos.X, pos.Y), 
                new Point(pos.X, pos.Y + size),
                new Point(pos.X - (int) (0.86 * size), pos.Y + size/2),
                new Point(pos.X - (int) (0.86 * size), pos.Y - size/2),
            };
            _graphics.FillPolygon(brush, side1);
            
            brush = new SolidBrush(Color.Gray);
            Point[] side2 = {
                new Point(pos.X, pos.Y), 
                new Point(pos.X, pos.Y + size),
                new Point(pos.X + (int) (0.86 * size), pos.Y + size/2),
                new Point(pos.X + (int) (0.86 * size), pos.Y - size/2),
            };
            _graphics.FillPolygon(brush, side2);
            
            brush = new SolidBrush(Color.Silver);
            Point[] side3 = {
                new Point(pos.X, pos.Y),
                new Point(pos.X + (int) (0.86 * size), pos.Y - size/2),
                new Point(pos.X, pos.Y - size),
                new Point(pos.X - (int) (0.86 * size), pos.Y - size/2),
            };
            _graphics.FillPolygon(brush, side3);
        }
    }
}