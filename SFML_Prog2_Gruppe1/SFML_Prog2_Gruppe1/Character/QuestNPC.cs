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
    public class QuestNPC : Character
    {

        /// <summary>
        /// Loads correct texture and applies starting position.
        /// </summary>
        public QuestNPC() : base()
        {
            characterType = CharacterID.QuestNPC;

            Velocity = new Vector2f(0, 0);

            Idle = new Texture("Character/QuestNPCIdle.png");
            IdleAnimation = new Animation(Idle, 10, 1, 32, 32, 100);

            currentAnimationState = AnimationStates.Idle;

            SetAndApplyPosition(position);
        }

        

    }
}
