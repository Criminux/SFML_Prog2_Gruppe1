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
    abstract class Character
    {
        private enum ID
        {
            Player = 1,
            EnemyNPC = 2,
            QuestNPC = 3
        }

        protected int stamina;
        protected int health;
        protected int damage;
        protected int armor;
        protected Vector2f position;
        protected Sprite characterSprite;
        protected Texture characterTexture;
        protected Vector2f velocity = new Vector2f();


        public Character()
        {
            characterSprite = new Sprite();
            /*
            switch (ID)
            {
                // Load texture based on ID
                case 1:
                    texture = new Texture("");
                    break;
                case 2:
                    texture = new Texture("");
                    break;
                case 3:
                    texture = new Texture("");
                    break;
            }*/

        }

        // Get and Setter for movement
        public virtual void SetVelocity(float x, float y)
        {
            velocity.X = x;
            velocity.Y = y;
        }

        public Vector2f GetVelocity()
        {
            return velocity;
        }

        public virtual void Update()
        {
            velocity = new Vector2f(0,0);
            position += velocity;
        }

        public abstract void Draw();

        // TODO: Method to handle input?
    }
}
