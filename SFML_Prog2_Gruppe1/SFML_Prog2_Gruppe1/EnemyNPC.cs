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
        const float MovementSpeed = 5;
        const int RandomMovementChangeDelay = 1;
        private Clock movementDelay;
        private Direction movementState;

        public EnemyNPC() : base()
        {
            characterType = CharacterID.EnemyNPC;
            health = 25;
            stamina = 25;
            damage = 10;
            armor = 10;

            characterTexture = new Texture("Character/EnemyNPC.png");
            characterSprite.Texture = characterTexture;

            SetAndApplyPosition(new Vector2f(350, 350));
            movementDelay = new Clock();
        }

        public override void Update(Tile[,] room)
        {
            MoveRandom();

            base.Update(room);
        }

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

            if (movementState == Direction.UP) Velocity = new Vector2f(MovementSpeed, 0);
            if (movementState == Direction.DOWN) Velocity = new Vector2f(0, MovementSpeed);
            if (movementState == Direction.LEFT) Velocity = new Vector2f(-MovementSpeed, 0);
            if (movementState == Direction.RIGHT) Velocity = new Vector2f(0, -MovementSpeed);
        }
    }
}
