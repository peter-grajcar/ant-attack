using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AntAttack
{
    // God save the Queen and help us all!
    using Colour = Color;

    public class Renderer
    {
        public enum Direction
        {
            NorthEast,
            SouthEast
        };


        private Graphics _graphics;
        private int _sizeV; // Horizontal size
        private int _sizeH; // Vertical size



        public Vector2 Centre { get; set; }
        public static Direction Orientation { get; set; }
        public int HorizontalSize => _sizeH;
        public int VerticalSize => _sizeV;
        
        
        
        public Renderer(Graphics graphics, int size)
        {
            _graphics = graphics;
            _graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            
            Centre = new Vector2(400, 0);
            Orientation = Direction.NorthEast;

            _sizeV = size;
            _sizeH = (int) Math.Round(Math.Sqrt(3) / 2 * _sizeV);
        }

        
        public void RenderMap(Map map)
        {
            // Background
            Brush brush = new SolidBrush(Colour.LightGray);
            _graphics.FillRectangle(brush, new Rectangle(0,0, 800, 600));
            
            // Walls
            for (int z = 0; z < map.Depth; z++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    for (int x = 0; x < map.Width; x++)
                    {
                        Vector3 coordinates = new Vector3(x, y, z);
                        if (Orientation == Direction.SouthEast)
                            coordinates = new Vector3(y, map.Width - x - 1, z);
                        
                        if (map.Get(coordinates) == Map.Wall)
                        {
                            Vector2 cubePosition = TransformCoordinates(x, y, z);
                            if(isInBounds(cubePosition))
                                DrawCube(cubePosition);
                        }
                        else if (map.Get(coordinates) == Map.Entity)
                        {
                            foreach (Entity entity in map.Entities)
                            {
                                if (entity.Position == coordinates)
                                {
                                    Vector2 pos = TransformCoordinates(x, y, z);
                                    DrawEntity(entity, pos);
                                }   
                            }
                        }
                    }
                }
            }
            
            
        }

        public Vector2 TransformCoordinates(int x, int y, int z)
        {
            return this.Centre + new Vector2(
                x * _sizeH - y * _sizeH,
                (x * _sizeV) / 2 + (y*_sizeV)/2 - z * _sizeV
            );
        }

        private bool isInBounds(Vector2 v)
        {
            return (v.X >= 0 && v.X <= 800) && (v.Y >= 0 && v.Y <= 600);
        }

        private void DrawCube(Vector2 pos)
        {
            Brush brush1, brush2, brush3;
            HatchStyle style = HatchStyle.Percent10;
            
            brush1 = new HatchBrush(style, Colour.Black, Colour.DimGray);
            brush2 = new HatchBrush(style, Colour.Gray, Colour.Black);
            brush3 = new HatchBrush(style, Colour.Black, Colour.Silver);
            
            Point[] side1 = {
                new Point(pos.X, pos.Y), 
                new Point(pos.X, pos.Y + _sizeV),
                new Point(pos.X - _sizeH, pos.Y + _sizeV/2),
                new Point(pos.X - _sizeH, pos.Y - _sizeV/2),
            };
            Point[] side2 = {
                new Point(pos.X, pos.Y), 
                new Point(pos.X, pos.Y + _sizeV),
                new Point(pos.X + _sizeH, pos.Y + _sizeV/2),
                new Point(pos.X + _sizeH, pos.Y - _sizeV/2),
            };
            Point[] side3 = {
                new Point(pos.X, pos.Y),
                new Point(pos.X + _sizeH, pos.Y - _sizeV/2),
                new Point(pos.X, pos.Y - _sizeV),
                new Point(pos.X - _sizeH, pos.Y - _sizeV/2),
            };
            
            _graphics.FillPolygon(Orientation == Direction.NorthEast ? brush1 : brush2, side1);
            _graphics.FillPolygon(Orientation == Direction.NorthEast ? brush2 : brush1, side2);
            _graphics.FillPolygon(brush3, side3);
        }

        private void DrawEntity(Entity entity, Vector2 pos)
        {
            Image img = entity.GetTexture(Orientation);
            Brush brush = new TextureBrush(img);
            _graphics.DrawImage(img, new Rectangle(pos.X - _sizeH, pos.Y - _sizeV, _sizeV + _sizeV/2, _sizeV + _sizeV/2));
            //_graphics.FillEllipse(new SolidBrush(Colour.Red), pos.X - 2, pos.Y - 2, 4, 4);
        }
    }
}