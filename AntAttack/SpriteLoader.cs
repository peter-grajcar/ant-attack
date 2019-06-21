using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AntAttack
{
    public class SpriteLoader
    {
        public const int spriteSize = 16;
        
        private static SpriteLoader _spriteLoader;
        private Bitmap _sprites;

        public enum Sprite
        {
            Ant, AntRun,
            Boy, BoyRun, BoyFall, BoyJump, BoyTrip,
            Girl, GirlRun, GirlFall, GirlJump, GirlTrip,
            Ammo, Grenade, Airplane
        }

        private SpriteLoader()
        {
            _sprites = LoadBitmap();
        }
        
        public static SpriteLoader GetSpriteLoader()
        {
            if(_spriteLoader == null)
                _spriteLoader = new SpriteLoader();
            return _spriteLoader;
        }
        
        private Bitmap LoadBitmap()
        {
            /*
             * Bitmap.FromFile does not work on MacOS
             */
            if (Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                Stream stream = new FileStream("AntAttack/Resources/sprites.data", FileMode.Open);
                Bitmap bmp = new Bitmap(192, 80);
                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        byte a = (byte) stream.ReadByte();
                        byte r = (byte) stream.ReadByte();
                        byte g = (byte) stream.ReadByte();
                        byte b = (byte) stream.ReadByte();
                        bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                    }
                }
                stream.Close();
            
                return bmp; 
            }
            return (Bitmap) Bitmap.FromFile("AntAttack/Resources/sprites.gif");
        }

        public Bitmap GetSprite(Sprite sprite, int direction)
        {
            Rectangle rect = new Rectangle(0,0,0,0);
            switch (sprite)
            {
                case Sprite.Ant:
                    rect = new Rectangle(direction*spriteSize,0, spriteSize, spriteSize);
                    break;
                case Sprite.AntRun:
                    rect = new Rectangle((4 + direction)*spriteSize,0, spriteSize, spriteSize);
                    break;
                case Sprite.Boy:
                    rect = new Rectangle(direction*spriteSize,spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.BoyRun:
                    rect = new Rectangle(direction*spriteSize,2*spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.BoyFall:
                    rect = new Rectangle((4 + direction)*spriteSize,spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.BoyJump:
                    rect = new Rectangle((4 + direction)*spriteSize,2*spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.BoyTrip:
                    rect = new Rectangle((8 + direction)*spriteSize,2*spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.Girl:
                    rect = new Rectangle(direction*spriteSize,3*spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.GirlRun:
                    rect = new Rectangle(direction*spriteSize,4*spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.GirlFall:
                    rect = new Rectangle((4 + direction)*spriteSize,3*spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.GirlJump:
                    rect = new Rectangle((4 + direction)*spriteSize,4*spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.GirlTrip:
                    rect = new Rectangle((8 + direction)*spriteSize,4*spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.Ammo:
                    rect = new Rectangle((8 + direction)*spriteSize,3*spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.Grenade:
                    rect = new Rectangle((8 + direction)*spriteSize,spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.Airplane:
                    rect = new Rectangle((8 + direction)*spriteSize,0, spriteSize, spriteSize);
                    break;
            }
            return _sprites.Clone(rect, PixelFormat.DontCare);
        }
    }
}