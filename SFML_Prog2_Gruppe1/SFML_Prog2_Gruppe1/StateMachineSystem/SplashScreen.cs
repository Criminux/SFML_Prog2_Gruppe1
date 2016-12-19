using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.StateMachineSystem
{
    /// <summary>
    /// Spash screen state.
    /// </summary>
    public class SplashScreen : State
    {
        Clock clock;
        Texture splashTexture;
        Sprite splashSprite;

        /// <summary>
        /// Constructor for the splash screen.
        /// </summary>
        public SplashScreen()
        {
            splashTexture = new Texture("StateMachineSystem/Splash.png");
            splashSprite = new Sprite(splashTexture);
            Initialize();
        }

        /// <summary>
        /// Disposes the state.
        /// </summary>
        public override void Dispose()
        {
        }

        /// <summary>
        /// Draws the state.
        /// </summary>
        public override void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(splashSprite);
        }

        /// <summary>
        /// Handle input for specific state.
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <param name="isPressed">Is the key pressed.</param>
        public override void HandleInput(Keyboard.Key key, bool isPressed)
        {
        }

        /// <summary>
        /// Initializes the state.
        /// </summary>
        public override void Initialize()
        {
            clock = new Clock();
            clock.Restart();
        }

        /// <summary>
        /// Checks if the time for the splashscreen is up, if so it switches to gameplay state. If not it returns the splashscreen state.
        /// </summary>
        /// <returns>
        /// Returns the correct GameState.
        /// </returns>
        public override GameStates Update()
        {
            if (clock.ElapsedTime.AsSeconds() >= 3f)
            {
                return GameStates.MainMenuState;
            }
            return GameStates.SplashScreenState;
        }
    }
}
