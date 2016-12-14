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
        protected Vector2f velocity;

        protected Animation animation;
        protected Texture spriteSheet;

        protected Animation WalkLeftAnimation;
        protected Animation WalkRightAnimation;
        protected Animation WalkUpAnimation;
        protected Animation WalkDownAnimation;

        protected Animation AttackLeftAnimation;
        protected Animation AttackRightAnimation;
        protected Animation AttackDownAnimation;
        protected Animation AttackUpAnimation;

        protected Animation ToDrawAnimation;

        protected Texture WalkLeft;
        protected Texture WalkRight;
        protected Texture WalkUp;
        protected Texture WalkDown;

        protected Texture AttackLeft;
        protected Texture AttackRight;
        protected Texture AttackUp;
        protected Texture AttackDown;

        protected AnimationStates currentAnimationState;
        
        public int Health
        {
            get { return health; }
        }
        public FloatRect Bounds
        {
            get { return ToDrawAnimation.Sprite.GetGlobalBounds(); }
        }

        /// <summary>
        /// Character sprites and standard velocity of 0 are getting assigned.
        /// </summary>
        public Character()
        {
            spriteSheet = new Texture("Character/CharMove.png");
            animation = new Animation(spriteSheet, 4, 1, 32, 32, 100);
            animation.Sprite.Texture = spriteSheet;
            velocity = new Vector2f(0, 0);
            ToDrawAnimation = animation; //TODO: animation weg

        }

        /// <summary>
        /// Getter and setter for movement.
        /// </summary>
        public Vector2f Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        /// <summary>
        /// Getter and setter for position.
        /// </summary>
        public Vector2f Position
        {
            get { return position; }
            set { position = value; }
        }
        
        /// <summary>
        /// Method to update character-velocity and possible collisions.
        /// </summary>
        /// <param name="room">Tilemap of active room.</param>
        public virtual void Update(Room room)
        {
            animation.Update();
            animation.Sprite.Position = position;

            ToDrawAnimation = animation;


            if (this is Player || this is EnemyNPC)
            {
                WalkLeftAnimation.Update();
                WalkRightAnimation.Update();
                WalkUpAnimation.Update();
                WalkDownAnimation.Update();
            }
            

            ApplyVelocity();
            ApplyPosition();

            HandleCollisions(room.Tilemap);

            if(this is Player) Console.WriteLine("Position after Collision: " + position.ToString());

            ApplyPosition();

            Velocity = new Vector2f(0, 0);

            SetActiveAnimation();
        }

        private void SetActiveAnimation()
        {
            switch (currentAnimationState)
            {
                case AnimationStates.UnspecifiedState:
                    ToDrawAnimation = WalkDownAnimation;
                    break;
                case AnimationStates.WalkLeft:
                    ToDrawAnimation = WalkLeftAnimation;
                    break;
                case AnimationStates.WalkRight:
                    ToDrawAnimation = WalkRightAnimation;
                    break;
                case AnimationStates.WalkUp:
                    ToDrawAnimation = WalkUpAnimation;
                    break;
                case AnimationStates.WalkDown:
                    ToDrawAnimation = WalkDownAnimation;
                    break;
                case AnimationStates.AttackLeft:
                    break;
                case AnimationStates.AttackRight:
                    break;
                case AnimationStates.AttackUp:
                    break;
                case AnimationStates.AttackDown:
                    break;
               
            }
        }

        /// <summary>
        /// Draws the character sprites to render window.
        /// </summary>
        public virtual void Draw()
        {
            if (this is Player ||this is EnemyNPC)
            {
                ToDrawAnimation.Draw(position, false);
            }


        }
        
        /// <summary>
        /// Handles collision of the character.
        /// </summary>
        /// <param name="room">
        /// Tilemap of the active room.
        /// </param>
        private void HandleCollisions(Tile[,] room)
        {
            int indexX = (int)Math.Round((position.X / 32));
            int indexY = (int)Math.Round((position.Y / 32));


            for (int x = -1; x < 2; x++)
            {
                if (!(indexX + x < 0 || indexX + x > 39 || indexY < 0 || indexY > 19))
                {
                    Tile tileToCheck = room[indexX + x, indexY];
                    HandleCollisionForTile(tileToCheck, depth => position.X += depth.X);
                }
            }

            for (int y = -1; y < 2; y++)
            {
                if (!(indexX < 0 || indexX > 39 || indexY + y < 0 || indexY + y > 19))
                {
                    Tile tileToCheck = room[indexX, indexY + y];
                    HandleCollisionForTile(tileToCheck, depth => position.Y += depth.Y);
                }
            }

            if (!(indexX - 1 < 0 || indexX - 1 > 39 || indexY - 1 < 0 || indexY - 1 > 19)) HandleCollisionForTile(room[indexX - 1, indexY - 1], depth => position += depth);
            if (!(indexX - 1 < 0 || indexX - 1 > 39 || indexY + 1 < 0 || indexY + 1 > 19)) HandleCollisionForTile(room[indexX - 1, indexY + 1], depth => position += depth);
            if (!(indexX + 1 < 0 || indexX + 1 > 39 || indexY - 1 < 0 || indexY - 1 > 19)) HandleCollisionForTile(room[indexX + 1, indexY - 1], depth => position += depth);
            if (!(indexX + 1 < 0 || indexX + 1 > 39 || indexY + 1 < 0 || indexY + 1 > 19)) HandleCollisionForTile(room[indexX + 1, indexY + 1], depth => position += depth);
        }

        /// <summary>
        /// Handles collision for one specific tile.
        /// </summary>
        /// <param name="tile">
        /// Tile to check.
        /// </param>
        /// <param name="depthApplyer">
        /// Delegate method for X and Y correction.
        /// </param>
        private void HandleCollisionForTile(Tile tile, CollisionDepthApplyer depthApplyer)
        {
            if (tile is CollisionTile)
            {
                Console.WriteLine("charSprite: " + ToDrawAnimation.Sprite.GetGlobalBounds().ToString());
                Vector2f depth = CollisionUtil.CalculateCollisionDepth(ToDrawAnimation.Sprite.GetGlobalBounds(), tile.Rectangle);
                Console.WriteLine("Depth:" + depth.ToString());

                if (depth != new Vector2f(0f, 0f))
                {
                    depthApplyer(depth);
                    ApplyPosition();
                }

            }
        }

        /// <summary>
        /// Sets position and applies it.
        /// </summary>
        /// <param name="position">
        /// Position of the character.
        /// </param>
        protected void SetAndApplyPosition(Vector2f position)
        {
            this.position = position;
            ApplyPosition();
        }

        /// <summary>
        /// Applies the position on the character sprite.
        /// </summary>
        private void ApplyPosition()
        {
            animation.Sprite.Position = position;
        }

       /// <summary>
       /// Applies the velocity onto the position.
       /// </summary>
        private void ApplyVelocity()
        {
            position += velocity;
        }

        /// <summary>
        /// Delegate Method for applying depth on X and Y axis.
        /// </summary>
        /// <param name="depth">Correction on X or Y axis.</param>
        private delegate void CollisionDepthApplyer(Vector2f depth);
    }
}
