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
    public class NormalTile : Tile
    {
        public NormalTile(Vector2f position) : base()
        {
            texture = new Texture("NormalTile.png");
            this.position = position;

            sprite.Texture = texture;
            sprite.Position = position;
        }

    }
}
