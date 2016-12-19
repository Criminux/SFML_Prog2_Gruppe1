using SFML.System;
using SFML.Graphics;

namespace SFML_Prog2_Gruppe1.WorldSystem
{
    /// <summary>
    /// Collision tile, where all characters will collide with.
    /// </summary>
    class CollisionTile : Tile
    {
        /// <summary>
        /// Constructor for collidable tile.
        /// </summary>
        /// <param name="position">Position.</param>
        public CollisionTile(Vector2f position, CollisionTileType type) : base()
        {
            Initialize();

            switch (type)
            {
                case CollisionTileType.Wall:
                    texture = new Texture("WorldSystem/Tile/CollisionTile.png");
                    break;

                case CollisionTileType.Ruin:
                    texture = new Texture("WorldSystem/Tile/CollisionTile_ruin.png");
                    break;

                case CollisionTileType.Tree1:
                    texture = new Texture("WorldSystem/Tile/CollisionTile_tree1.png");
                    break;

                case CollisionTileType.Tree2:
                    texture = new Texture("WorldSystem/Tile/CollisionTile_tree2.png");
                    break;
            }
            this.position = position;

            sprite.Texture = texture;
            sprite.Position = position;
        }
    }
}
