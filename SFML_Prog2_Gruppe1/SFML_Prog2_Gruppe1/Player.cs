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

        public Player() : base()
        {
            characterType = CharacterID.Player;
            health = 100;
            stamina = 100;
            damage = 1;
            armor = 0;

        }

        public override void Update()
        {
            //TODO: Refactor Input
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right)) { SetVelocity(5, 0); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left)) { SetVelocity(-5, 0); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up)) { SetVelocity(0, -5); }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down)) { SetVelocity(0, 5); }

            base.Update();
            //throw new NotImplementedException();
        }

        public override void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(characterSprite);
            //throw new NotImplementedException();
        }

    }
}
