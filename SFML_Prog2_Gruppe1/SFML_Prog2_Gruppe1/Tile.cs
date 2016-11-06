using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace SFML_Prog2_Gruppe1
{
    public abstract class Tile
    {
        protected Texture texture;
        protected Sprite sprite;
        protected Vector2f position;

        public Tile()
        {
        }

        virtual public void Initialize()
        {
            sprite = new Sprite();
            texture = new Texture(32, 32);
        }

        virtual public void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(sprite);
        }
    }
}
