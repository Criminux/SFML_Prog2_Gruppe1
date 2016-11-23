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
            velocity = new Vector2f(0, 0);
        }

        // Get and Setter for movement
        public Vector2f Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        protected Vector2f Position
        {
            get { return position; }
            set { position = value; }
        }

        public virtual void Update(Tile[,] room)
        {
            Console.WriteLine("Velocity pre Collision: " + velocity.ToString());
            Console.WriteLine("Position pre Collision: " + position.ToString());

            ApplyVelocity();
            Console.WriteLine("Position past ApplyVelo(): " + position.ToString());
            ApplyPosition();

            HandleCollisions(room);

            Console.WriteLine("Position after Collision: " + position.ToString());

            ApplyPosition();

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
                    Tile tileToCheck = room[indexX + x, indexY];
                    HandleCollisionForTile(tileToCheck, depth => position.X += depth.X);
                }

                for (int y = -1; y < 2; y++)
                {
                    Tile tileToCheck = room[indexX, indexY + y];
                    HandleCollisionForTile(tileToCheck, depth => position.Y += depth.Y);
                }

                HandleCollisionForTile(room[indexX - 1, indexY - 1], depth => position += depth);
                HandleCollisionForTile(room[indexX - 1, indexY + 1], depth => position += depth);
                HandleCollisionForTile(room[indexX + 1, indexY - 1], depth => position += depth);
                HandleCollisionForTile(room[indexX + 1, indexY + 1], depth => position += depth);

            }
            catch (IndexOutOfRangeException e)
            {
                //In case player gets out of the room, he respawns
                SetAndApplyPosition(new Vector2f(100, 100));
            }

        }

        private void HandleCollisionForTile(Tile tile, CollisionDepthApplyer depthApplyer)
        {
            if (tile is CollisionTile)
            {
                Console.WriteLine("charSprite: " + characterSprite.GetGlobalBounds().ToString());
                Vector2f depth = CollisionUtil.CalculateCollisionDepth(characterSprite.GetGlobalBounds(), tile.Rectangle);
                Console.WriteLine("Depth:" + depth.ToString());

                if (depth != new Vector2f(0f, 0f))
                {
                    depthApplyer(depth);
                    ApplyPosition();
                }

            }
        }


        protected void SetAndApplyPosition(Vector2f position)
        {
            this.position = position;
            ApplyPosition();
        }

        private void ApplyPosition()
        {
            characterSprite.Position = position;
        }

        private void ApplyPositionAndRedraw()
        {
            ApplyPosition();
            Draw();
        }

        private void ApplyVelocity()
        {
            position += velocity;
        }

        private delegate void CollisionDepthApplyer(Vector2f depth);
    }
}
