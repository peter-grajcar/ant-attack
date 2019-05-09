using System;
using System.Drawing;

namespace AntAttack
{
    // God save the Queen and help us all!
    using Colour = Color;
    
    public class Renderer
    {
        private Graphics _graphics;

        public Vector2 Position { get; set; }
        
        public Renderer(Graphics graphics)
        {
            _graphics = graphics;
            Position = new Vector2(400, 0);
        }

        public void RenderMap(Map map)
        {
            // Background
            Brush brush = new SolidBrush(Colour.LightGray);
            _graphics.FillRectangle(brush, new Rectangle(0,0, 800, 600));
            
            
            // Walls
            int sizeH = 30; // Horizontal size
            double c = Math.Sqrt(3) / 2;
            int sizeV = (int) Math.Round(c * sizeH); // Vertical size
            
            for (int z = 0; z < map.Depth; z++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    for (int x = 0; x < map.Width; x++)
                    {
                        if (map.Get(x, y, z) == Map.Wall)
                        {
                            Vector2 pos = new Vector2(
                                Position.X + x * sizeV - y * sizeV,
                                Position.Y + (x * sizeH) / 2 + (y*sizeH)/2 - z * sizeH
                                );
                            DrawCube(pos, sizeH);
                        }
                    }
                }
            }
            
            // Centre dot (For debugging)
            brush = new SolidBrush(Colour.Red);
            _graphics.FillEllipse(brush, 398, 298, 4, 4);
        }

        private void DrawCube(Vector2 pos, int sizeH)
        {
            Brush brush;
            
            double c = Math.Sqrt(3) / 2;
            int sizeV = (int) Math.Round(c * sizeH); // Vertical size
            
            brush = new SolidBrush(Colour.DimGray);
            Point[] side1 = {
                new Point(pos.X, pos.Y), 
                new Point(pos.X, pos.Y + sizeH),
                new Point(pos.X - sizeV, pos.Y + sizeH/2),
                new Point(pos.X - sizeV, pos.Y - sizeH/2),
            };
            _graphics.FillPolygon(brush, side1);
            
            brush = new SolidBrush(Colour.Gray);
            Point[] side2 = {
                new Point(pos.X, pos.Y), 
                new Point(pos.X, pos.Y + sizeH),
                new Point(pos.X + sizeV, pos.Y + sizeH/2),
                new Point(pos.X + sizeV, pos.Y - sizeH/2),
            };
            _graphics.FillPolygon(brush, side2);
            
            brush = new SolidBrush(Colour.Silver);
            Point[] side3 = {
                new Point(pos.X, pos.Y),
                new Point(pos.X + sizeV, pos.Y - sizeH/2),
                new Point(pos.X, pos.Y - sizeH),
                new Point(pos.X - sizeV, pos.Y - sizeH/2),
            };
            _graphics.FillPolygon(brush, side3);
        }
    }
}