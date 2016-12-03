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

        public event RoomChangeEventHandler roomChangeEvent;

        const float MovementSpeed = 5;

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
            health = 100;
            stamina = 100;
            damage = 1;
            armor = 0;

            characterTexture = new Texture("Character/Player.png");
            characterSprite.Texture = characterTexture;

            SetAndApplyPosition(new Vector2f(200, 200));
        }

        /// <summary>
        /// Calls the input method, updates the base and calls the CheckForRoomChange method.
        /// </summary>
        /// <param name="room">
        /// Current room.
        /// </param>
        public override void Update(Tile[,] room)
        {
            //TODO: Refactor Input

            base.Update(room);

            CheckForRoomChange();
        }

        /// <summary>
        /// Checks if the room has to change regarding the player position.
        /// </summary>
        private void CheckForRoomChange()
        {
            if (Position.X < 0 - (characterSprite.GetGlobalBounds().Width / 2))
            {
                Position = new Vector2f(Position.X + 1280, Position.Y);
                roomChangeEvent(Direction.LEFT);
            }
            else if (Position.X > 1280 - (characterSprite.GetGlobalBounds().Width / 2))
            {
                Position = new Vector2f(Position.X - 1280, Position.Y);
                roomChangeEvent(Direction.RIGHT);
            }
            else if (Position.Y < 0 - (characterSprite.GetGlobalBounds().Height / 2))
            {
                Position = new Vector2f(Position.X, Position.Y + 720);
                roomChangeEvent(Direction.UP);
            }
            else if (Position.Y > 720 - (characterSprite.GetGlobalBounds().Height / 2))
            {
                Position = new Vector2f(Position.X, Position.Y - 720);
                roomChangeEvent(Direction.DOWN);
            }
        }

        /// <summary>
        /// Checks for user input and enables the player to move.
        /// </summary>
        public void CheckInputs(CommandQueue commandQueue)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                commandQueue.Push(new PlayerMover(MovementSpeed, 0));
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                commandQueue.Push(new PlayerMover(-MovementSpeed, 0));
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                commandQueue.Push(new PlayerMover(0, -MovementSpeed));
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                commandQueue.Push(new PlayerMover(0, MovementSpeed));
            }
        }
    }
}
