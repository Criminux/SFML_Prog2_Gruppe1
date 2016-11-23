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
    public class Player : Character
    {

        const float MovementSpeed = 5;

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

        public override void Update(Tile[,] room)
        {
            //TODO: Refactor Input
            //if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right)) { Velocity = new Vector2f(0.5f, 0); }
            //if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left))  { Velocity = new Vector2f(-0.5f, 0); }
            //if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))    { Velocity = new Vector2f(0, -0.5f); }
            //if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))  { Velocity = new Vector2f(0, 0.5f); 
            CheckInputs();

            base.Update(room);
        }

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
