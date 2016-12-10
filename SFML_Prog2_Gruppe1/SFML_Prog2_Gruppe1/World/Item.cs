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
    public class Item
    {
        private Texture texture;
        private Sprite sprite;
        private Vector2f position;

        /// <summary>
        /// Instantiate a new Item.
        /// </summary>
        /// <param name="position">Position in the room.</param>
        public Item(Vector2f position)
        {
            texture = new Texture("World/Item.png");
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
