using System.Drawing;

namespace AntAttack
{
    public interface IRenderable
    {
        Bitmap GetTexture(Renderer.Direction direction);
    }
}