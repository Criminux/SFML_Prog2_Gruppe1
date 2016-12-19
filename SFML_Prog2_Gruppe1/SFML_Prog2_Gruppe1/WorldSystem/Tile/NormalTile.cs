using SFML.System;
using SFML.Graphics;

namespace SFML_Prog2_Gruppe1.WorldSystem
{
    /// <summary>
    /// Normal tile is a walkable tile, the player wont collide with.
    /// </summary>
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
            switch (type)
            {
                case NormalTileType.Grass:
                    texture = new Texture("WorldSystem/Tile/NormalTile.png");
                    break;

                case NormalTileType.GrassTop:
                    texture = new Texture("WorldSystem/Tile/NormalTile_top.png");
                    break;

                case NormalTileType.GrassBot:
                    texture = new Texture("WorldSystem/Tile/NormalTile_bottom.png");
                    break;

                case NormalTileType.GrassL:
                    texture = new Texture("WorldSystem/Tile/NormalTile_left.png");
                    break;

                case NormalTileType.GrassR:
                    texture = new Texture("WorldSystem/Tile/NormalTile_right.png");
                    break;
            }

            this.position = position;

            sprite.Texture = texture;
            sprite.Position = position;
        }

    }
}
