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
    public class UIManager
    {
        private Texture uiBar;
        private Sprite sprite;

        private Texture fullHeart;
        private Texture emptyHeart;

        private int health;
        private List<Sprite> heartSprites;

        /// <summary>
        /// UI Manager constructor
        /// </summary>
        public UIManager()
        {
            uiBar = new Texture("UI/UIBar.png");
            sprite = new Sprite(uiBar);
            sprite.Position = new Vector2f(0, 640);

            fullHeart = new Texture("UI/FullHeart.png");
            emptyHeart = new Texture("UI/EmptyHeart.png");

            heartSprites = new List<Sprite>();

            heartSprites.Add(new Sprite());
            heartSprites[0].Position = new Vector2f(16, 680);
            heartSprites.Add(new Sprite());
            heartSprites[1].Position = new Vector2f(80, 680);
            heartSprites.Add(new Sprite());
            heartSprites[2].Position = new Vector2f(144, 680);
            heartSprites.Add(new Sprite());
            heartSprites[3].Position = new Vector2f(208, 680);
            heartSprites.Add(new Sprite());
            heartSprites[4].Position = new Vector2f(272, 680);
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <param name="health">Health of the Player.</param>
        public void Update(int health)
        {
            this.health = health;

            if (health >= 1) heartSprites[0].Texture = fullHeart;
            else heartSprites[0].Texture = emptyHeart;
            if (health >= 2) heartSprites[1].Texture = fullHeart;
            else heartSprites[1].Texture = emptyHeart;
            if (health >= 3) heartSprites[2].Texture = fullHeart;
            else heartSprites[2].Texture = emptyHeart;
            if (health >= 4) heartSprites[3].Texture = fullHeart;
            else heartSprites[3].Texture = emptyHeart;
            if (health >= 5) heartSprites[4].Texture = fullHeart;
            else heartSprites[4].Texture = emptyHeart;
        }

        /// <summary>
        /// Draws the UI.
        /// </summary>
        public void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(sprite);
            
            foreach (Sprite tempSprite in heartSprites)
            {
                ProjectRenderWindow.GetRenderWindowInstance().Draw(tempSprite); 
            }
            
        }
    }
}
