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
    public class Projectile
    {
        //TODO: Add destruction timer
        private Texture projectileTexture;
        private Sprite projectileSprite;

        private Vector2f position;
        private Vector2f velocity;

        private Clock destructionTimer;

        public Clock DestructionTimer
        {
            get { return destructionTimer; }
        }

        public FloatRect Bounds
        {
            get { return projectileSprite.GetGlobalBounds(); }
        }



        public Projectile(Vector2f position, Vector2f velocity)
        {
            projectileTexture = new Texture("Character/Projectile.png");
            projectileSprite = new Sprite(projectileTexture);

            destructionTimer = new Clock();

            this.position = position;
            this.velocity = velocity;

            projectileSprite.Position = position;
        }

        public void Update()
        {
            position += velocity;
            projectileSprite.Position = position;
        }

        public void Draw()
        {
                ProjectRenderWindow.GetRenderWindowInstance().Draw(projectileSprite);
        }


    }
}
