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
    public abstract class Character
    {
        public enum CharacterID
        {
            Player = 1,
            EnemyNPC = 2,
            QuestNPC = 3
        }

        protected CharacterID characterType;
        protected int stamina;
        protected int health;
        protected int damage;
        protected int armor;
        protected Vector2f position;
        protected Sprite characterSprite;
        protected Texture characterTexture;
        protected Vector2f velocity;


        public Character()
        {
            characterSprite = new Sprite();
            velocity = new Vector2f(0,0);
            position = new Vector2f(50,50);

            characterTexture = new Texture("Character/Player.png");
            //switch (characterType)
            //{
            //    // Load texture based on ID
            //    case CharacterID.Player:
            //        characterTexture = new Texture("Character/Player.png");
            //        break;
            //    case CharacterID.QuestNPC:
            //        characterTexture = new Texture("Character/Player.png");
            //        break;
            //    case CharacterID.EnemyNPC:
            //        characterTexture = new Texture("Character/Player.png");
            //        break;
            //}

            characterSprite.Texture = characterTexture;
            characterSprite.Position = position;

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
            position.X = position.X + velocity.X;
            position.Y = position.Y + velocity.Y;

            characterSprite.Position = position;

            velocity = new Vector2f(0,0);
        }

        public abstract void Draw();

        // TODO: Method to handle input?
    }
}
