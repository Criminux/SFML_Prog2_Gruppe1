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
            position = new Vector2f(200,200);

        }

        // Get and Setter for movement
        public Vector2f Velocity
        {
            get { return velocity; }
            set{ velocity = value; }
            
        }

        public virtual void Update(Tile[,] room)
        {
            HandleCollisions(room);

            position.X = position.X + velocity.X;
            position.Y = position.Y + velocity.Y;

            characterSprite.Position = position;

            Velocity = new Vector2f(0, 0);
        }

        public virtual void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(characterSprite);
        }

        private void HandleCollisions(Tile[,] room)
        {
            int indexX = (int)((position.X / 32) - 1);
            int indexY = (int)((position.Y / 32) - 1);

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    //TODO: Refactor try/Catch -> Collision Logic
                    try
                    {
                        Tile tempTile = room[indexX + i, indexY + j];
                        if (tempTile is CollisionTile)
                        {
                            if (tempTile.Rectangle.Intersects(characterSprite.TextureRect))
                            {
                                Console.WriteLine("Die Dinger kollidieren!!!!!!!!!0000");
                                //velocity = new Vector2f(0, 0);
                            }
                        }

                    }
                    catch (Exception e) { }
                }
            }

        }
    }
}
