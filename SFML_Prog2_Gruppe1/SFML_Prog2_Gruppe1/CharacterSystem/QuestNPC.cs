using SFML.System;
using SFML.Graphics;
namespace SFML_Prog2_Gruppe1.CharacterSystem
{
    public class QuestNPC : Character
    {

        /// <summary>
        /// Loads correct texture and applies starting position.
        /// </summary>
        public QuestNPC() : base()
        {

            Velocity = new Vector2f(0, 0);
            Position = new Vector2f(100, 100);

            Idle = new Texture("CharacterSystem/QuestNPCIdle.png");
            IdleAnimation = new Animation(Idle, 10, 1, 32, 32, 100);
            WalkDownAnimation = new Animation(Idle, 10, 1, 32, 32, 100);
            WalkUpAnimation = new Animation(Idle, 10, 1, 32, 32, 100);
            WalkRightAnimation = new Animation(Idle, 10, 1, 32, 32, 100);
            WalkLeftAnimation = new Animation(Idle, 10, 1, 32, 32, 100);

            
            currentAnimationState = AnimationStates.Idle;

            Initialize();

            SetAndApplyPosition(position);
        }
    }
}