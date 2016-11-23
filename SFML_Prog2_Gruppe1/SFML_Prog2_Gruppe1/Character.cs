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
            //position = new Vector2f(200,200);
        }

        // Get and Setter for movement
        public Vector2f Velocity
        {
            get { return velocity; }
            set{ velocity = value; }
            
        }

        public virtual void Update(Tile[,] room)
        {
            Console.WriteLine("Velocity pre Collision: " + velocity.ToString());
            Console.WriteLine("Position pre Collision: " + position.ToString());

            position += velocity;

            HandleCollisions(room);

            Console.WriteLine("Velocity after Collision: " + velocity.ToString());
            Console.WriteLine("Position after Collision: " + position.ToString());

            characterSprite.Position = position;

            Velocity = new Vector2f(0, 0);
        }

        public virtual void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(characterSprite);
        }

        private void HandleCollisions(Tile[,] room)
        {
            int indexX = (int)Math.Round((position.X / 32));
            int indexY = (int)Math.Round((position.Y / 32));
            
            try
            {
                for (int x = -1; x < 2; x++)
                {
                    Tile tempTile = room[MathUtil.Clamp<int>(indexX + x, 0, room.GetUpperBound(0)), indexY];
                    if (tempTile is CollisionTile)
                    {
                        Vector2f depth = CollisionUtil.CalculateCollisionDepth(characterSprite.GetGlobalBounds(), tempTile.Rectangle);
                        Console.WriteLine("Depth:" + depth.ToString());

                        position.X += depth.X;
                        characterSprite.Position = position;
                        ProjectRenderWindow.GetRenderWindowInstance().Draw(characterSprite);

                    }
                }
                for (int y = -1; y < 2; y++)
                {
                    Tile tempTile = room[indexX, MathUtil.Clamp<int>(indexY + y, 0, room.GetUpperBound(1))];
                    if (tempTile is CollisionTile)
                    {
                        Vector2f depth = CollisionUtil.CalculateCollisionDepth(characterSprite.GetGlobalBounds(), tempTile.Rectangle);
                        Console.WriteLine("Depth:" + depth.ToString());

                        position.Y += depth.Y;
                        characterSprite.Position = position;
                        ProjectRenderWindow.GetRenderWindowInstance().Draw(characterSprite);

                    }
                }
            }catch(IndexOutOfRangeException e)
            {
                //In case player gets out of the room, he respawns
                position = new Vector2f(100, 100);
                characterSprite.Position = position;
            }
            
        }
    }
}
