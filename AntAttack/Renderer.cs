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
        
        public Vector3 Centre { get; set; }
        public Direction Orientation { get; set; }
        public int HorizontalSize => _sizeH;
        public int VerticalSize => _sizeV;

        public Colour Overlay { get; set; }



        public Renderer(Graphics graphics, int size)
        {
            _graphics = graphics;
            _graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            
            Centre = new Vector3(0, 0, 0);
            Orientation = Direction.NorthEast;

            _sizeV = size;
            _sizeH = (int) Math.Round(Math.Sqrt(3) / 2 * _sizeV);

            Overlay = Colour.Empty;
        }

        private ulong _overlayTimeout;
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
                        /* Flip coordinates when direction is changed */
                        if (Orientation == Direction.SouthEast)
                            coordinates = new Vector3(y, map.Width - x - 1, z);
                        
                        if (map.Get(coordinates) == Map.Wall)
                        {
                            Vector2 cubePosition = TransformCoordinates(x, y, z);
                            if(IsInBounds(cubePosition))
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

            if (Overlay != Colour.Empty)
            {
                brush = new SolidBrush(Colour.FromArgb(128, Overlay));
                _graphics.FillRectangle(brush, new Rectangle(0, 0, 800, 600));
                if (Time.T - _overlayTimeout > 200)
                    Overlay = Colour.Empty;
            }
            else
            {
                _overlayTimeout = Time.T;
            }
        }

        public void RenderStart()
        {
            _graphics.FillRectangle(Brushes.Cyan, new Rectangle(0,0, 800, 600));
            _graphics.FillRectangle(Brushes.Yellow, new Rectangle(100,100, 600, 400));
            Pen pen = new Pen(Brushes.Magenta, 20);
            _graphics.DrawRectangle(pen, new Rectangle(100,100, 600, 400));

            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            
            Font font = new Font("Comic Sans MS", 8);
            _graphics.DrawString("WELCOME TO ...", font, Brushes.Black, new RectangleF(150, 150, 500, 20));

            if (Time.T % 2000 < 1500)
            {
                font = new Font("Comic Sans MS", 50);
                _graphics.DrawString("ANT ATTACK", font, Brushes.Black, new PointF(400, 240), format);
            }
            
            font = new Font("Comic Sans MS", 12);
            _graphics.DrawString("AFTER A LONG JOURNEY YOU SEE YOUR DESTINATION. THE HIGH WALLED CITY OF ANTESCHER. AS YOU STAND AT THE GATE YOU HEAR A CALL OF DISTRESS. IRRESISTIBLE TO A HERO LIKE YOU. YOU MUST ANSWER IT ...\nGOOD LUCK!", font, Brushes.Black, new RectangleF(150, 300, 500, 150));

            Brush brush1 = (Time.T % 1000 > 500) ? Brushes.LightGray : Brushes.Black;
            Brush brush2 = (Time.T % 1000 > 500) ? Brushes.Black : Brushes.LightGray;
            _graphics.FillRectangle(brush1, new Rectangle(200,490, 400, 20));
           font = new Font("Comic Sans MS", 10);
            _graphics.DrawString("G i r l   o r   B o y   ( g   /   b ) ?", font, brush2, new PointF(400, 500), format);
        }

        public void RenderStats()
        {
            _graphics.FillRectangle(Brushes.Cyan, new Rectangle(0,0, 800, 600));
            _graphics.FillRectangle(Brushes.Yellow, new Rectangle(100,100, 600, 400));
            Pen pen = new Pen(Brushes.Magenta, 20);
            _graphics.DrawRectangle(pen, new Rectangle(100,100, 600, 400));
            
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            
            Font font = new Font("Comic Sans MS", 8);
            _graphics.FillRectangle(Brushes.Black, new Rectangle(250,150, 300, 50));
            _graphics.DrawString("* * * *  ANT ATTACK  * * * *\n* * * *  SCORE CARD * * * *", font, Brushes.LightGray, new PointF(400, 175), format);
            
            _graphics.DrawString("LIVES SAVED:", font, Brushes.Blue, new PointF(150, 250));
            _graphics.DrawString("TIME LEFT:", font, Brushes.Blue, new PointF(150, 300));
            
            _graphics.DrawString(AntAttack.CurrentLevel.ToString(), font, Brushes.Blue, new PointF(550, 250));
            _graphics.DrawString((AntAttack.TimeLeft / 1000).ToString(), font, Brushes.Blue, new PointF(550, 300));
            
            Brush brush1 = (Time.T % 1000 > 500) ? Brushes.LightGray : Brushes.Black;
            Brush brush2 = (Time.T % 1000 > 500) ? Brushes.Black : Brushes.LightGray;
            
            font = new Font("Comic Sans MS", 10);
            _graphics.FillRectangle(brush1, new Rectangle(200,490, 400, 20));
            _graphics.DrawString("PRESS ANY KEY ", font, brush2, new PointF(400, 500), format);
        }

        public void RenderEnd(bool lost)
        {
            _graphics.FillRectangle(Brushes.Cyan, new Rectangle(0,0, 800, 600));
            _graphics.FillRectangle(Brushes.Yellow, new Rectangle(100,100, 600, 400));
            Pen pen = new Pen(Brushes.Magenta, 20);
            _graphics.DrawRectangle(pen, new Rectangle(100,100, 600, 400));

            if (lost)
            {
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
                Font font = new Font("Comic Sans MS", 50);
                _graphics.DrawString("GAME OVER", font, Brushes.Red, new PointF(400, 210), format);
                
                Brush brush1 = (Time.T % 1000 > 500) ? Brushes.LightGray : Brushes.Black;
                Brush brush2 = (Time.T % 1000 > 500) ? Brushes.Black : Brushes.LightGray;
            
                font = new Font("Comic Sans MS", 10);
                _graphics.FillRectangle(brush1, new Rectangle(200,490, 400, 20));
                _graphics.DrawString("PRESS ANY KEY ", font, brush2, new PointF(400, 500), format);
            }
            else
            {
                Font font = new Font("Comic Sans MS", 8);
                _graphics.DrawString("YOU ARE A REAL", font, Brushes.Black, new RectangleF(150, 150, 500, 20));

                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
                font = new Font("Comic Sans MS", 50);
                _graphics.DrawString("HERO", font, Brushes.Red, new PointF(400, 210), format);

                font = new Font("Comic Sans MS", 8);
                _graphics.DrawString(
                    "AT LEAST YOUR MISSION IS OVER! YOUR NEW FRIENDS WISH TO THANK YOU FOR SAVING THEIR LIVES.", font,
                    Brushes.Black, new RectangleF(150, 250, 500, 200));
                
                _graphics.DrawString("LIVES SAVED:", font, Brushes.Blue, new PointF(150, 400));
                _graphics.DrawString("TIME LEFT:", font, Brushes.Blue, new PointF(150, 450));
            
                _graphics.DrawString(AntAttack.CurrentLevel.ToString(), font, Brushes.Blue, new PointF(550, 400));
                _graphics.DrawString((AntAttack.TimeLeft / 1000).ToString(), font, Brushes.Blue, new PointF(550, 450));

            }
        }

        private string _text = "";
        private int _length;
        private ulong _lastTick;
        public void SetMessage(string text)
        {
            _text = text;
            _length = 0;
            AntAttack.Paused = true;
        }
        
        public bool FinishedMessage()
        {
            return _text.Length == 0;
        }
        
        public void RenderMessage()
        {
            if (Time.T - _lastTick > 50)
            {
                if (_length < _text.Length + 10)
                {
                    _length++;
                }
                else
                {
                    _text = "";
                    AntAttack.Paused = false;
                }

                _lastTick = Time.T;
            }
            
            Font font = new Font("Comic Sans MS", 20);
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            
            _graphics.DrawString(_text.Substring(0, Math.Min(_text.Length,_length)), font, Brushes.Red, new PointF(400, 150), format);
        }

        
        
        private bool _blink;
        private ulong _lastBlink;
        public void RenderGui(Human h1, Human h2)
        {
            Font font = new Font("Comic Sans MS", 16);
            _graphics.FillRectangle(Brushes.Black, 0, 390, 800, 210);
            _graphics.FillRectangle(Brushes.Magenta, 0, 420, 800, 30);
            _graphics.FillRectangle(Brushes.Magenta, 0, 540, 800, 30);
            
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            
            _graphics.DrawString("AMMO", font, Brushes.LightGray, new PointF(100, 555), format);
            _graphics.DrawString((h1 is Girl) ? "GIRL" : "BOY", font, Brushes.LightGray, new PointF(250, 555), format);
            _graphics.DrawString((h1 is Girl) ? "BOY" : "GIRL", font, Brushes.LightGray, new PointF(400, 555), format);
            _graphics.DrawString("TIME", font, Brushes.LightGray, new PointF(550, 555), format);
            _graphics.DrawString("SCAN", font, Brushes.LightGray, new PointF(700, 555), format);
            
            format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Near;
            
            /* Ammo */
            _graphics.FillRectangle(Brushes.Blue, 50, 450, 100, 30);
            _graphics.FillRectangle(Brushes.LightGray, 50, 480, 100, 30);
            _graphics.FillRectangle(Brushes.Blue, 50, 510, 100, 30);
            _graphics.DrawString(h2.Ammo.ToString(), font, Brushes.Black, new PointF(50, 495), format);
            
            /* Girl's Health */
            _graphics.FillRectangle(Brushes.Blue, 200, 450, 100, 30);
            _graphics.FillRectangle(Brushes.LightGray, 200, 480, 100, 30);
            _graphics.FillRectangle(Brushes.Blue, 200, 510, 100, 30);
            _graphics.DrawString(h1.Health.ToString(), font, Brushes.Black, new PointF(200, 495), format);
            
            /* Boy's Health */
            _graphics.FillRectangle(Brushes.Blue, 350, 450, 100, 30);
            _graphics.FillRectangle(Brushes.LightGray, 350, 480, 100, 30);
            _graphics.FillRectangle(Brushes.Blue, 350, 510, 100, 30);
            _graphics.DrawString(h2.Health.ToString(), font, Brushes.Black, new PointF(350, 495), format);
            
            /* Time */
            _graphics.FillRectangle(Brushes.Blue, 500, 450, 100, 30);
            _graphics.FillRectangle(Brushes.LightGray, 500, 480, 100, 30);
            _graphics.FillRectangle(Brushes.Blue, 500, 510, 100, 30);
            _graphics.DrawString((AntAttack.TimeLeft / 1000).ToString(), font, Brushes.Black, new PointF(500, 495), format);

            /* Scanner */
            double c = 1000.0 / Math.Min(Vector3.Dist(h1.Position, h2.Position) , 50);
            if (_blink && Time.T - _lastBlink > 1000 - c)
            {
                _blink = false;
                _lastBlink = Time.T;
            }
            else if (!_blink && Vector3.Dist(h1.Position, h2.Position) > 1 && Time.T - _lastBlink > c)
            {
                _blink = true;
                _lastBlink = Time.T;
            }

            if(_blink)
                _graphics.FillRectangle(Brushes.Red, 650, 450, 100, 90);
            else
                _graphics.FillRectangle(Brushes.Lime, 650, 450, 100, 90);
        }

        
        
        public Vector2 TransformCoordinates(Vector3 pos)
        {
            return TransformCoordinates(pos.X, pos.Y, pos.Z);
        }
        
        public Vector2 TransformCoordinates(int x, int y, int z)
        {
            if (Orientation == Direction.NorthEast)
            {
                x -= Centre.X;
                y -= Centre.Y;
                z -= Centre.Z;
            }
            else
            {
                x -= AntAttack.Map.Width - Centre.Y - 1;
                y -= Centre.X;
                z -= Centre.Z;
            }

        return new Vector2(400, 250) + new Vector2(
                x * _sizeH - y * _sizeH,
                (x * _sizeV) / 2 + (y*_sizeV)/2 - z * _sizeV
            );
        }

        
        private bool IsInBounds(Vector2 v)
        {
            return (v.X >= 0 - _sizeH && v.X <= 800 + _sizeH) && (v.Y >= 0 - _sizeV && v.Y <= 600 + _sizeV);
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
            _graphics.DrawImage(img, new Rectangle(pos.X - _sizeH, pos.Y - _sizeV, _sizeV + _sizeV/2, _sizeV + _sizeV/2));
            //_graphics.FillEllipse(new SolidBrush(Colour.Red), pos.X - 2, pos.Y - 2, 4, 4);
        }
    }
}