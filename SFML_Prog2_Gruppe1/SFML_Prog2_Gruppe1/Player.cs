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
            //if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right)) { Velocity = new Vector2f(0.5f, 0); }
            //if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))  { Velocity = new Vector2f(-0.5f, 0); }
            //if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))    { Velocity = new Vector2f(0, -0.5f); }
            //if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))  { Velocity = new Vector2f(0, 0.5f); 
            CheckInputs();

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
        void CheckInputs()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                Velocity = new Vector2f(MovementSpeed, 0);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                Velocity = new Vector2f(-MovementSpeed, 0);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                Velocity = new Vector2f(0, -MovementSpeed);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                Velocity = new Vector2f(0, MovementSpeed);
            }

            if ((Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
                && (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left)))
            {
                Velocity = new Vector2f(0, 0);
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
                && (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down)))
            {
                Velocity = new Vector2f(0, 0);
            }


        }
    }
}
