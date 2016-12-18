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
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.WorldSystem
{
    public class Item
    {
        private Texture texture;
        private Sprite sprite;
        private Vector2f position;

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

        public void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(sprite);
        }
    }
}
