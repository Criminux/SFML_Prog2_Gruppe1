using SFML.System;
using SFML.Graphics;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.WorldSystem
{
    /// <summary>
    /// Item class.
    /// </summary>
    public class Item
    {
        private Texture texture;
        private Sprite sprite;
        private Vector2f position;

        private Spawn spawn;

        /// <summary>
        /// Spawn of the item.
        /// </summary>
        public Spawn Spawn
        {
            get { return spawn; }
            set { spawn = value; }
        }

        /// <summary>
        /// Bounds of the item.
        /// </summary>
        public FloatRect Bounds
        {
            get { return sprite.GetGlobalBounds(); }
        }

        /// <summary>
        /// Instantiate a new Item.
        /// </summary>
        /// <param name="position">Position in the room.</param>
        public Item(Vector2f position)
        {
            texture = new Texture("WorldSystem/Item.png");
            this.position = position;

            sprite = new Sprite(texture);
            sprite.Position = position;
        }

        /// <summary>
        /// Draws the item.
        /// </summary>
        public void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(sprite);
        }
    }
}
