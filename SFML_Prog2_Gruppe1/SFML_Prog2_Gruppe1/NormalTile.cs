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

            //texture = new Texture("Tile/NormalTile.png");
            switch(type)
            {
                case NormalTileType.Grass:
                    texture = new Texture("Tile/NormalTile.png");
                    break;

                case NormalTileType.GrassTop:
                    texture = new Texture("Tile/NormalTile_top.png");
                    break;

                case NormalTileType.GrassBot:
                    texture = new Texture("Tile/NormalTile_bottom.png");
                    break;

                case NormalTileType.GrassL:
                    texture = new Texture("Tile/NormalTile_left.png");
                    break;

                case NormalTileType.GrassR:
                    texture = new Texture("Tile/NormalTile_right.png");
                    break;
            }

            this.position = position;

            sprite.Texture = texture;
            sprite.Position = position;
        }

    }
}
