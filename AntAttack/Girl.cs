using System.Drawing;

namespace AntAttack
{
    public class Girl : Human
    {

        public Girl(Map map) : base(map)
        {
            
        }

        public override Bitmap GetTexture(Renderer.Direction direction)
        {
            return null;
        }

        public override void Update()
        {
           
        }
    }
}