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
        /// <summary>
        /// Constructor for a walkable tile.
        /// </summary>
        /// <param name="position">Position.</param>
        public NormalTile(Vector2f position, NormalTileType type) : base()
        {
            Initialize();

            texture = new Texture("Tile/NormalTile.png");
            switch(type)
            {
                case NormalTileType.Background:
                    texture = new Texture("Tile/NormalTile.png");

                    break;
            }

            this.position = position;

            sprite.Texture = texture;
            sprite.Position = position;
        }

    }
}
