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

using SFML_Prog2_Gruppe1.Util;
using SFML_Prog2_Gruppe1.WorldSystem;

namespace SFML_Prog2_Gruppe1.CharacterSystem
{
    public class EnemyNPC : Character
    {
        const float MovementSpeed = 3;
        const int RandomMovementChangeDelay = 1;
        private Clock movementDelay;
        private Direction movementState;
        private Spawn spawn;

        public Spawn Spawn
        {
            get { return spawn; }
            set { spawn = value; }
        }


        private Clock attackCooldown;
        private const float attackCooldownLenght = 600;


        /// <summary>
        /// Applies basic stats to the enemy NPC. Loads correct texture and applies starting position.
        /// </summary>
        public EnemyNPC() : base()
        {
            characterType = CharacterID.EnemyNPC;
            health = 25;
            stamina = 25;
            damage = 10;
            armor = 10;

            WalkLeft = new Texture("CharacterSystem/EnemyNPCWalkLeft.png");
            WalkRight = new Texture("CharacterSystem/EnemyNPCWalkRight.png");
            WalkUp = new Texture("CharacterSystem/EnemyNPCWalkUp.png");
            WalkDown = new Texture("CharacterSystem/EnemyNPCWalkDown.png");

            WalkLeftAnimation = new Animation(WalkLeft, 9, 1, 32, 32, 100);
            WalkRightAnimation = new Animation(WalkRight, 9, 1, 32, 32, 100);
            WalkUpAnimation = new Animation(WalkUp, 9, 1, 32, 32, 100);
            WalkDownAnimation = new Animation(WalkDown, 9, 1, 32, 32, 100);
            IdleAnimation = new Animation(WalkDown, 9, 1, 32, 32, 100);


            AttackLeft = new Texture("CharacterSystem/EnemyNPCAttackLeft.png");
            AttackRight = new Texture("CharacterSystem/EnemyNPCAttackRight.png");
            AttackUp = new Texture("CharacterSystem/EnemyNPCAttackUp.png");
            AttackDown = new Texture("CharacterSystem/EnemyNPCAttackDown.png");

            AttackLeftAnimation = new Animation(AttackLeft, 6, 1, 32, 32, 100);
            AttackRightAnimation = new Animation(AttackRight, 6, 1, 32, 32, 100);
            AttackUpAnimation = new Animation(AttackUp, 6, 1, 32, 32, 100);
            AttackDownAnimation = new Animation(AttackDown, 6, 1, 32, 32, 100);

            currentAnimationState = AnimationStates.Idle;
            attackCooldown = new Clock();

            Initialize();

            SetAndApplyPosition(new Vector2f(350, 350));
            movementDelay = new Clock();
        }

        /// <summary>
        /// Calls the MoveRandom function and updates the base.
        /// </summary>
        /// <param name="room">
        /// Current room.
        /// </param>
        public void Update(Room room, Vector2f playerPosition)
        {
            float distance = 300;
            float distancesquared = ((playerPosition.X - position.X) * (playerPosition.X - position.X) +
            (playerPosition.Y - position.Y) * (playerPosition.Y - position.Y));;

            if (distancesquared <= distance * distance)
            {
                Vector2f ChasePath;
                Vector2f direction = playerPosition - position;
                ChasePath = new Vector2f(MathUtil.Clamp<float>(direction.X,-MovementSpeed,MovementSpeed), MathUtil.Clamp<float>(direction.Y, -MovementSpeed, MovementSpeed));
                
                if ((ChasePath.X != 0) && (ChasePath.Y != 0))
                {
                    Velocity = new Vector2f(ChasePath.X / (float)Math.Sqrt(2), ChasePath.Y / (float)Math.Sqrt(2));
                }
                else
                {
                    Velocity = ChasePath;
                }
            }
            else
            {
                MoveRandom();
            }
            base.Update(room);
        }

        /// <summary>
        /// Enables the NPC to move random along the x and y axis.
        /// </summary>
        private void MoveRandom()
        {
            if(movementDelay.ElapsedTime.AsSeconds() > RandomMovementChangeDelay)
            {
                Random rand = new Random(DateTime.Now.Millisecond);
                int randNumber = rand.Next(0, 4);

                if (randNumber == 0) movementState = Direction.UP;
                if (randNumber == 1) movementState = Direction.DOWN;
                if (randNumber == 2) movementState = Direction.LEFT;
                if (randNumber == 3) movementState = Direction.RIGHT;

                movementDelay.Restart();
            }

            if (movementState == Direction.UP)
            { 
                Velocity = new Vector2f(0, -MovementSpeed);
                if(attackCooldown.ElapsedTime.AsMilliseconds() >= 600) currentAnimationState = AnimationStates.WalkUp;
            }
            if (movementState == Direction.DOWN)
            {
                Velocity = new Vector2f(0, MovementSpeed);
                if (attackCooldown.ElapsedTime.AsMilliseconds() >= 600) currentAnimationState = AnimationStates.WalkDown;
            }
            if (movementState == Direction.LEFT)
            {
                Velocity = new Vector2f(-MovementSpeed, 0);
                if (attackCooldown.ElapsedTime.AsMilliseconds() >= 600) currentAnimationState = AnimationStates.WalkLeft;
            }
            if (movementState == Direction.RIGHT)
            {
                Velocity = new Vector2f(MovementSpeed, 0);
                if (attackCooldown.ElapsedTime.AsMilliseconds() >= 600) currentAnimationState = AnimationStates.WalkRight;
            }
        }


        public void ResetAttackCooldown()
        {
            attackCooldown.Restart();
        }
    }
}
