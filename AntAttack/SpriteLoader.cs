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
            Ammo, Granade, Explosion, Airplane
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
                    break;
                case Sprite.BoyFall:
                    break;
                case Sprite.BoyJump:
                    break;
                case Sprite.BoyTrip:
                    break;
                case Sprite.Girl:
                    rect = new Rectangle(direction*spriteSize,3*spriteSize, spriteSize, spriteSize);
                    break;
                case Sprite.GirlRun:
                    break;
                case Sprite.GirlFall:
                    break;
                case Sprite.GirlJump:
                    break;
                case Sprite.GirlTrip:
                    break;
                case Sprite.Ammo:
                    break;
                case Sprite.Granade:
                    break;
                case Sprite.Explosion:
                    break;
                case Sprite.Airplane:
                    break;
            }
            return _sprites.Clone(rect, PixelFormat.DontCare);
        }
    }
}