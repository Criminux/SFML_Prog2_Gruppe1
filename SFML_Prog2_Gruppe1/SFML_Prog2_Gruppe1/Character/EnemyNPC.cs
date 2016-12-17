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

namespace SFML_Prog2_Gruppe1
{
    public class EnemyNPC : Character
    {
        const float MovementSpeed = 3;
        const int RandomMovementChangeDelay = 1;
        private Clock movementDelay;
        private Direction movementState;



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

            WalkLeft = new Texture("Character/EnemyNPCWalkLeft.png");
            WalkRight = new Texture("Character/EnemyNPCWalkRight.png");
            WalkUp = new Texture("Character/EnemyNPCWalkUp.png");
            WalkDown = new Texture("Character/EnemyNPCWalkDown.png");

            WalkLeftAnimation = new Animation(WalkLeft, 9, 1, 32, 32, 100);
            WalkRightAnimation = new Animation(WalkRight, 9, 1, 32, 32, 100);
            WalkUpAnimation = new Animation(WalkUp, 9, 1, 32, 32, 100);
            WalkDownAnimation = new Animation(WalkDown, 9, 1, 32, 32, 100);
            IdleAnimation = new Animation(WalkDown, 9, 1, 32, 32, 100);


            AttackLeft = new Texture("Character/EnemyNPCAttackLeft.png");
            AttackRight = new Texture("Character/EnemyNPCAttackRight.png");
            AttackUp = new Texture("Character/EnemyNPCAttackUp.png");
            AttackDown = new Texture("Character/EnemyNPCAttackDown.png");

            AttackLeftAnimation = new Animation(AttackLeft, 6, 1, 32, 32, 100);
            AttackRightAnimation = new Animation(AttackRight, 6, 1, 32, 32, 100);
            AttackUpAnimation = new Animation(AttackUp, 6, 1, 32, 32, 100);
            AttackDownAnimation = new Animation(AttackDown, 6, 1, 32, 32, 100);

            currentAnimationState = AnimationStates.Idle;

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
        public override void Update(Room room)
        {
            MoveRandom();


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
                currentAnimationState = AnimationStates.WalkUp;
            }
            if (movementState == Direction.DOWN)
            {
                Velocity = new Vector2f(0, MovementSpeed);
                currentAnimationState = AnimationStates.WalkDown;
            }
            if (movementState == Direction.LEFT)
            {
                Velocity = new Vector2f(-MovementSpeed, 0);
                currentAnimationState = AnimationStates.WalkLeft;
            }
            if (movementState == Direction.RIGHT)
            {
                Velocity = new Vector2f(MovementSpeed, 0);
                currentAnimationState = AnimationStates.WalkRight;
            }
        }
    }
}
