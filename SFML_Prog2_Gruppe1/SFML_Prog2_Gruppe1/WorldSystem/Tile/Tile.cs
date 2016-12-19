using SFML.System;
using SFML.Graphics;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.WorldSystem
{
    /// <summary>
    /// Different types of normal tiles.
    /// </summary>
    public enum NormalTileType
    {
        Grass = 1,
        GrassTop = 2,
        GrassBot = 3,
        GrassL = 4,
        GrassR = 5
    }

    /// <summary>
    /// Different types of collidable tiles.
    /// </summary>
    public enum CollisionTileType
    {
        Wall = 1,
        Ruin = 2,
        Tree1 = 3,
        Tree2 = 4
    }

    /// <summary>
    /// Abstract tile-class as parent for normal and collidable tiles.
    /// </summary>
    public abstract class Tile
    {
        public const int Width = 32;
        public const int Height = 32;

        protected Texture texture;
        protected Sprite sprite;
        protected Vector2f position;

        /// <summary>
        /// Getter for the position of a tile.
        /// </summary>
        public Vector2f Position
        {
            get { return position; }
        }

        /// <summary>
        /// Getter for the boundary rectangle of a tile.
        /// </summary>
        public FloatRect Rectangle
        {
            get { return sprite.GetGlobalBounds(); }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Tile()
        {
        }

        /// <summary>
        /// Initializes the sprite and the texture for the tile.
        /// </summary>
        virtual public void Initialize()
        {
            sprite = new Sprite();
            texture = new Texture(32, 32);
        }

        /// <summary>
        /// Draws the tile sprites.
        /// </summary>
        virtual public void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(sprite);
        }
    }
}
