using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.CharacterSystem
{
    public class Animation
    {
        private Texture spriteSheet;
        private Sprite sprite;
        private int currentFrame;
        private int animationSpriteCountX;
        private int animationSpriteCountY;
        private int animationTime;
        private float currentAnimationTime;
        private int animationSpriteHeight;
        private int animationSpriteWidth;
        private List<IntRect> animationSprites;
        private Clock clock;


        public Sprite Sprite
        {
            get { return sprite; }
        }

        public Animation(Texture spriteSheet, int countX, int countY, int width, int height, int time)
        {
            this.spriteSheet = spriteSheet;
            this.animationSpriteCountX = countX;
            this.animationSpriteCountY = countY;
            this.animationSpriteWidth = width;
            this.animationSpriteHeight = height;
            this.animationTime = time;
            this.currentAnimationTime = 0.0f;
            this.currentFrame = 0;
            this.animationSprites = new List<IntRect>();

            clock = new Clock();
            clock.Restart();

            sprite = new Sprite();

            Load();
        }

        protected void Load()
        {
            //Loads the sprites from the spritesheets
            IntRect rect = new IntRect();
            for (int y = 0; y < animationSpriteCountY; y++)
            {
                for (int x = 0; x < animationSpriteCountX; x++)
                {
                    rect.Left = animationSpriteWidth * x;
                    rect.Top = animationSpriteHeight * y;
                    rect.Width = animationSpriteWidth;
                    rect.Height = animationSpriteHeight;

                    animationSprites.Add(rect);
                }
            }
        }
        
        public void Update()
        {
            //Updates the characters in relation to the game time and the frames
            int deltaTime = clock.ElapsedTime.AsMilliseconds();

            currentAnimationTime -= deltaTime;

            if (currentAnimationTime <= 0.0f)
            {
                currentFrame++;

                if (currentFrame >= animationSprites.Count)
                {
                    currentFrame = 0;
                }
                currentAnimationTime = animationTime;
            }
            clock.Restart();
        }

        public void Draw(Vector2f position, bool InvertHorizontal)
        {
            //Draws the sprites and flips them if needed

            sprite = new Sprite(spriteSheet, animationSprites[currentFrame]);
            sprite.Position = position;
            

            if (InvertHorizontal)
            {
                sprite.Scale = new Vector2f(-1f, 1);
                ProjectRenderWindow.GetRenderWindowInstance().Draw(sprite);
            }

            ProjectRenderWindow.GetRenderWindowInstance().Draw(sprite);
        }
    }
}
