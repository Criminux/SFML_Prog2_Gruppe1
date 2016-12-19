using System;
using SFML.System;
using SFML.Graphics;
using SFML.Audio;
using SFML_Prog2_Gruppe1.WorldSystem;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.CharacterSystem
{
    /// <summary>
    /// Blueprint for all the characters in the game.
    /// </summary>
    public abstract class Character
    {
        
        protected int stamina;
        protected int health;
        protected int damage;
        protected int armor;
        protected Vector2f position;
        protected Vector2f velocity;

        protected Sound stepSound;
        protected Sound shootSound;
        protected Sound diamondSound;
        protected SoundBuffer walkBuffer;
        protected SoundBuffer shootBuffer;
        protected SoundBuffer diamondBuffer;

        protected Clock stepTimer;
        

        protected Animation IdleAnimation;

        protected Animation WalkLeftAnimation;
        protected Animation WalkRightAnimation;
        protected Animation WalkUpAnimation;
        protected Animation WalkDownAnimation;

        protected Animation AttackLeftAnimation;
        protected Animation AttackRightAnimation;
        protected Animation AttackDownAnimation;
        protected Animation AttackUpAnimation;

        protected Animation ToDrawAnimation;

        protected Texture Idle;

        protected Texture WalkLeft;
        protected Texture WalkRight;
        protected Texture WalkUp;
        protected Texture WalkDown;

        protected Texture AttackLeft;
        protected Texture AttackRight;
        protected Texture AttackUp;
        protected Texture AttackDown;

        protected AnimationStates currentAnimationState;
        
        /// <summary>
        /// Property to get and set the current animation state.
        /// </summary>
        public AnimationStates CurrentAnimationState
        {
            get { return currentAnimationState; }
            set { currentAnimationState = value; }
        }

        /// <summary>
        /// Property to get the player health.
        /// </summary>
        public int Health
        {
            get { return health; }
        }

        /// <summary>
        /// Property to get the boundaries for the rectangle.
        /// </summary>
        public FloatRect Bounds
        {
            get { return ToDrawAnimation.Sprite.GetGlobalBounds(); }
        }

        /// <summary>
        ///Standard velocity of 0 is getting assigned for every character.
        /// </summary>
        public Character()
        {
            velocity = new Vector2f(0, 0);
        }

        /// <summary>
        /// Characters start with the walk down animation.
        /// </summary>
        protected void Initialize()
        {
            ToDrawAnimation = WalkDownAnimation;
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
        /// Method to update character-velocity, position and possible collisions. Calls the active animation method at the end.
        /// </summary>
        /// <param name="room">Tilemap of active room.</param>
        public virtual void Update(Room room)
        {
            if (this is Player || this is EnemyNPC)
            {
                if (!(velocity.X == 0 && velocity.Y == 0))
                {
                    WalkLeftAnimation.Update();
                    WalkRightAnimation.Update();
                    WalkUpAnimation.Update();
                    WalkDownAnimation.Update();
                }
                else
                {
                    if(this is Player) stepSound.Stop();
                }
                AttackLeftAnimation.Update();
                AttackUpAnimation.Update();
                AttackRightAnimation.Update();
                AttackDownAnimation.Update();
            }

            if (this is QuestNPC)
            {
                IdleAnimation.Update();
            }
            
            ApplyVelocity();
            ApplyPosition();

            HandleCollisions(room.Tilemap);

            Velocity = new Vector2f(0, 0);

            SetActiveAnimation();
        }

        /// <summary>
        /// Determines which animation to draw.
        /// </summary>
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
                    ToDrawAnimation = AttackLeftAnimation;
                    break;
                case AnimationStates.AttackRight:
                    ToDrawAnimation = AttackRightAnimation;
                    break;
                case AnimationStates.AttackUp:
                    ToDrawAnimation = AttackUpAnimation;
                    break;
                case AnimationStates.AttackDown:
                    ToDrawAnimation = AttackDownAnimation;
                    break;
                case AnimationStates.Idle:
                    ToDrawAnimation = IdleAnimation;
                    break;
            }
        }

        /// <summary>
        /// Draws the character sprites to render window.
        /// </summary>
        public virtual void Draw()
        {
            ToDrawAnimation.Draw(position);
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
                Vector2f depth = CollisionUtil.CalculateCollisionDepth(ToDrawAnimation.Sprite.GetGlobalBounds(), tile.Rectangle);

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
           ToDrawAnimation.Sprite.Position = position;
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