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

        float movementSpeed;
        public float MovementSpeed
        {
            get { return movementSpeed; }
            set
            {
                if (value <= 15) movementSpeed = value;
                else movementSpeed = 15;
            }
        }

        float projectileSpeed = 8;

        public float ProjectileSpeed
        {
            get { return projectileSpeed; }
            set
            {
                if (value <= 20)
                {
                    projectileSpeed = value;
                }
                else
                {
                    movementSpeed = 20;
                }
            }
        }

        private Clock attackTimer;

        public float ShotCoolDown
        {
            get { return shotCoolDown; }
            set
            {
                if (value <= 800)
                {
                    shotCoolDown = value;
                }
                else
                {
                    shotCoolDown = 800;
                }
            }
        }

        private float shotCoolDown;

        private bool questCompleted;
        public bool QuestCompleted
        {
            get { return questCompleted; }
            set { questCompleted = value; }
        }

        private Quest quest;
        private List<Projectile> projectiles;
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
            
            questCompleted = false;

            movementSpeed = 5;

            attackTimer = new Clock();
            shotCoolDown = 1000;

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

            IdleAnimation = new Animation(WalkDown, 7, 1, 32, 32, 100);


            currentAnimationState = AnimationStates.Idle;
            Initialize();

            SetAndApplyPosition(new Vector2f(200, 200));

            //TODO: Testing purpose
            if (quest == null) quest = new Quest();

            projectiles = new List<Projectile>();
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

            for (int i = projectiles.Count -1; i >= 0; i--)
            {
                projectiles[i].Update();

                if (projectiles[i].DestructionTimer.ElapsedTime.AsSeconds() > 1)
                {
                    projectiles.RemoveAt(i);
                }

            }
                

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
            List<int> savedIndex = new List<int>();

            for(int i = enemies.Count - 1; i >= 0; i--)
            {
                if (ToDrawAnimation.Sprite.GetGlobalBounds().Intersects(enemies[i].Bounds))
                {
                    if (lifeCooldown.ElapsedTime.AsSeconds() >= 1f)
                    {

                        health = health - 1;
                        lifeCooldown.Restart();
                        if (enemies[i].CurrentAnimationState == AnimationStates.WalkDown)
                        {
                            enemies[i].CurrentAnimationState = AnimationStates.AttackDown;
                        }
                        if (enemies[i].CurrentAnimationState == AnimationStates.WalkUp)
                        {
                            enemies[i].CurrentAnimationState = AnimationStates.AttackUp;
                        }
                        if (enemies[i].CurrentAnimationState == AnimationStates.WalkLeft)
                        {
                            enemies[i].CurrentAnimationState = AnimationStates.AttackLeft;
                        }
                        if (enemies[i].CurrentAnimationState == AnimationStates.WalkRight)
                        {
                            enemies[i].CurrentAnimationState = AnimationStates.AttackRight;
                        }
                    }
                }

                foreach(Projectile projectile in projectiles)
                {
                    if(projectile.Bounds.Intersects(enemies[i].Bounds))
                    {
                        enemies.RemoveAt(i);
                        EnemyEvent();
                    }
                }
            }
            
        }

        /// <summary>
        /// Handles collision with items.
        /// </summary>
        /// <param name="items">List of items.</param>
        private void CheckForItemCollision(List<Item> items)
        {
            List<int> savedIndex = new List<int>();

            for(int i = items.Count - 1; i >= 0; i--)
            {
                if(Bounds.Intersects(items[i].Bounds))
                {
                    items.RemoveAt(i);
                    ItemEvent();
                }
            }
            
        }

        /// <summary>
        /// Checks if the room has to change regarding the player position.
        /// </summary>
        private void CheckForRoomChange()
        {
            if (Position.X < 0 - (ToDrawAnimation.Sprite.GetGlobalBounds().Width / 2))
            {
                Position = new Vector2f(Position.X + 1280, Position.Y);
                projectiles = new List<Projectile>();
                roomChangeEvent(Direction.LEFT);
            }
            else if (Position.X > 1280 - (ToDrawAnimation.Sprite.GetGlobalBounds().Width / 2))
            {
                Position = new Vector2f(Position.X - 1280, Position.Y);
                roomChangeEvent(Direction.RIGHT);
                projectiles = new List<Projectile>();
            }
            else if (Position.Y < 0 - (ToDrawAnimation.Sprite.GetGlobalBounds().Height / 2))
            {
                Position = new Vector2f(Position.X, Position.Y + 640);
                roomChangeEvent(Direction.UP);
                projectiles = new List<Projectile>();
            }
            else if (Position.Y > 640 - (ToDrawAnimation.Sprite.GetGlobalBounds().Height / 2))
            {
                Position = new Vector2f(Position.X, Position.Y - 640);
                roomChangeEvent(Direction.DOWN);
                projectiles = new List<Projectile>();
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
                commandQueue.Push(new PlayerMover(movementSpeed, 0));
                currentAnimationState = AnimationStates.WalkRight;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                commandQueue.Push(new PlayerMover(-movementSpeed, 0));
                currentAnimationState = AnimationStates.WalkLeft;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                commandQueue.Push(new PlayerMover(0, -movementSpeed));
                currentAnimationState = AnimationStates.WalkUp;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                commandQueue.Push(new PlayerMover(0, movementSpeed));
                currentAnimationState = AnimationStates.WalkDown;
            }


            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                //currentAnimationState = AnimationStates.AttackLeft;
                Attack(commandQueue);
            }
        }

        private AnimationStates Attack(CommandQueue commandQueue)
        {
            if (currentAnimationState == AnimationStates.WalkLeft)
            {
                currentAnimationState = AnimationStates.AttackLeft;
                if (attackTimer.ElapsedTime.AsMilliseconds() > shotCoolDown)
                {
                    projectiles.Add(new Projectile(position, new Vector2f(-projectileSpeed, 0)));
                    attackTimer.Restart();
                }
            }
            else if (currentAnimationState == AnimationStates.WalkUp)
            {
                currentAnimationState = AnimationStates.AttackUp;
                if (attackTimer.ElapsedTime.AsMilliseconds() > shotCoolDown)
                {
                    projectiles.Add(new Projectile(position, new Vector2f(0, -projectileSpeed)));
                    attackTimer.Restart();
                }

            }
            else if (currentAnimationState == AnimationStates.WalkRight)
            {
                currentAnimationState = AnimationStates.AttackRight;
                if (attackTimer.ElapsedTime.AsMilliseconds() > shotCoolDown)
                {
                    projectiles.Add(new Projectile(position, new Vector2f(projectileSpeed, 0)));
                    attackTimer.Restart();
                }
            }
            else if (currentAnimationState == AnimationStates.WalkDown)
            {
                currentAnimationState = AnimationStates.AttackDown;
                if (attackTimer.ElapsedTime.AsMilliseconds() > shotCoolDown)
                {
                    projectiles.Add(new Projectile(position, new Vector2f(0, projectileSpeed)));
                    attackTimer.Restart();
                }

            }


            return currentAnimationState;
        }

        /// <summary>
        /// Event that is raised when the player should receive a new quest.
        /// </summary>
        public void GetNewQuest()
        {
            if (questCompleted == true)
            {
                QuestEvent();
            }

        }

        public override void Draw()
        {
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw();
            }
            base.Draw();
        }
    }
}
