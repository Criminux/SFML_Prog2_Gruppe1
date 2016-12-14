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
    public class Player : Character
    {
        
        public delegate void RoomChangeEventHandler(Direction direction);
        public delegate void QuestEventHandler();
        public delegate void EnemyEventHandler();
        public delegate void ItemEventHandler();

        public event RoomChangeEventHandler roomChangeEvent;
        public event QuestEventHandler QuestEvent;
        public event EnemyEventHandler EnemyEvent;
        public event ItemEventHandler ItemEvent;

        const float MovementSpeed = 5;

        private Quest quest;
        int interactionAttempt = 0;


        public Quest Quest
        {
            get { return quest; }
            set { quest = value; }
        }

        private Clock lifeCooldown;

        /// <summary>
        /// An command of this class will move the player in the desired direction.
        /// </summary>
        internal class PlayerMover : AbstractCommand
        {
            private Vector2f velocity;

            public PlayerMover(float x, float y)
            {
                velocity = new Vector2f(x, y);
            }

            public override void Execute(Player player)
            {
                player.Velocity = velocity;
            }
        }

        /// <summary>
        /// Applies basic stats to the player. Loads correct texture and applies starting position.
        /// </summary>
        public Player() : base()
        {
            characterType = CharacterID.Player;
            health = 5;
            stamina = 100;
            damage = 1;
            armor = 0;

            lifeCooldown = new Clock();

            WalkLeft = new Texture("Character/PlayerWalkLeft.png");
            WalkRight = new Texture("Character/PlayerWalkRight.png");
            WalkUp = new Texture("Character/PlayerWalkUp.png");
            WalkDown = new Texture("Character/PlayerWalkDown.png");

            WalkLeftAnimation = new Animation(WalkLeft, 9, 1, 32, 32, 100);
            WalkRightAnimation = new Animation(WalkRight, 9, 1, 32, 32, 100);
            WalkUpAnimation = new Animation(WalkUp, 9, 1, 32, 32, 100);
            WalkDownAnimation = new Animation(WalkDown, 9, 1, 32, 32, 100);

            AttackLeft = new Texture("Character/PlayerAttackLeft.png");
            AttackRight = new Texture("Character/PlayerAttackRight.png");
            AttackUp = new Texture("Character/PlayerAttackUp.png");
            AttackDown = new Texture("Character/PlayerAttackDown.png");

            AttackLeftAnimation = new Animation(AttackLeft, 7, 1, 32, 32, 100);
            AttackRightAnimation = new Animation(AttackRight, 7, 1, 32, 32, 100);
            AttackUpAnimation = new Animation(AttackUp, 7, 1, 32, 32, 100);
            AttackDownAnimation = new Animation(AttackDown, 7, 1, 32, 32, 100);


            SetAndApplyPosition(new Vector2f(200, 200));

            //TODO: Testing purpose
            if (quest == null) quest = new Quest();
        }

        /// <summary>
        /// Calls the input method, updates the base and calls the CheckForRoomChange method.
        /// </summary>
        /// <param name="room">
        /// Current room.
        /// </param>
        public override void Update(Room room)
        {
            base.Update(room);

            if (quest != null) quest.Update();

            CheckForEnemyCollision(room.Enemies);
            CheckForItemCollision(room.Items);

            CheckForRoomChange();
        }

        /// <summary>
        /// Handles collision with enemy and attacks.
        /// </summary>
        /// <param name="enemies">List of enemies.</param>
        private void CheckForEnemyCollision(List<EnemyNPC> enemies)
        {
            //TODO: Add Attack Logic
            List<int> savedIndex = new List<int>();

            for(int i = 0; i < enemies.Count; i++)
            {
                if (ToDrawAnimation.Sprite.GetGlobalBounds().Intersects(enemies[i].Bounds))
                {
                    if (lifeCooldown.ElapsedTime.AsSeconds() >= 1f)
                    {
                        health = health - 1;
                        lifeCooldown.Restart();
                    }
                }

                if (enemies[i].Health <= 0)
                {
                    savedIndex.Add(i);
                }
            }

            foreach (int index in savedIndex)
            {
                enemies.RemoveAt(index);
                EnemyEvent();
            }

        }

        /// <summary>
        /// Handles collision with items.
        /// </summary>
        /// <param name="items">List of items.</param>
        private void CheckForItemCollision(List<Item> items)
        {
            List<int> savedIndex = new List<int>();

            for(int i = 0; i < items.Count; i++)
            {
                if(Bounds.Intersects(items[i].Bounds))
                {
                    savedIndex.Add(i);
                }
            }

            foreach(int index in savedIndex)
            {
                items.RemoveAt(index);
                ItemEvent();
            }
        }

        /// <summary>
        /// Checks if the room has to change regarding the player position.
        /// </summary>
        private void CheckForRoomChange()
        {
            if (Position.X < 0 - (animation.Sprite.GetGlobalBounds().Width / 2))
            {
                Position = new Vector2f(Position.X + 1280, Position.Y);
                roomChangeEvent(Direction.LEFT);
            }
            else if (Position.X > 1280 - (animation.Sprite.GetGlobalBounds().Width / 2))
            {
                Position = new Vector2f(Position.X - 1280, Position.Y);
                roomChangeEvent(Direction.RIGHT);
            }
            else if (Position.Y < 0 - (animation.Sprite.GetGlobalBounds().Height / 2))
            {
                Position = new Vector2f(Position.X, Position.Y + 640);
                roomChangeEvent(Direction.UP);
            }
            else if (Position.Y > 640 - (animation.Sprite.GetGlobalBounds().Height / 2))
            {
                Position = new Vector2f(Position.X, Position.Y - 640);
                roomChangeEvent(Direction.DOWN);
            }
        }

        /// <summary>
        /// Checks for user input and enables the player to move.
        /// </summary>
        public void CheckInputs(CommandQueue commandQueue)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.E))
            {
                if (interactionAttempt == 0)
                {
                    interactionAttempt = 1;
                    commandQueue.Push(new Interaction());
                }
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.E))
            {
                interactionAttempt = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                commandQueue.Push(new PlayerMover(MovementSpeed, 0));
                currentAnimationState = AnimationStates.WalkRight;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                commandQueue.Push(new PlayerMover(-MovementSpeed, 0));
                currentAnimationState = AnimationStates.WalkLeft;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                commandQueue.Push(new PlayerMover(0, -MovementSpeed));
                currentAnimationState = AnimationStates.WalkUp;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                commandQueue.Push(new PlayerMover(0, MovementSpeed));
                currentAnimationState = AnimationStates.WalkDown;
            }
        }

        /// <summary>
        /// Event that is raised when the player should receive a new quest.
        /// </summary>
        public void GetNewQuest()
        {
            QuestEvent();
        }       
    }
}
