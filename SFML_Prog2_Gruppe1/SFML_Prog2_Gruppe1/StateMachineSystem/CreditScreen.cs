using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.StateMachineSystem
{
    public class CreditScreen : State
    {
        private Button backButton;

        private Texture creditsScreen;
        private Sprite creditSprite;

        private SoundBuffer klickButtonBuffer;
        private Sound klickButton;

        private GameStates targetState;

        bool clicked;

        /// <summary>
        /// Constructor for credits screen.
        /// </summary>
        public CreditScreen()
        {
            klickButtonBuffer = new SoundBuffer("StateMachineSystem/Menu/Klack.wav");
            klickButton = new Sound(klickButtonBuffer);

            backButton = new Button(new Vector2f(100, 100), "Back to Menu");
            backButton.Click += backButton_Click;

            targetState = GameStates.PauseMenuState;
            clicked = false;

            creditsScreen = new Texture("StateMachineSystem/Menu/CreditsScreenBackground.png");
            creditSprite = new Sprite(creditsScreen);
        }

        /// <summary>
        /// Clickevent for back button.
        /// </summary>
        /// <param name="sender">sender param.</param>
        /// <param name="e">eventarguments param.</param>
        private void backButton_Click(object sender, System.EventArgs e)
        {
            targetState = GameStates.MainMenuState;
        }

        /// <summary>
        /// Dispose the state.
        /// </summary>
        public override void Dispose()
        {
        }

        /// <summary>
        /// Draws the state.
        /// </summary>
        public override void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(creditSprite);

            backButton.Draw();
        }

        /// <summary>
        /// Handles the input for specific state.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <param name="isPressed">Is the key pressed.</param>
        public override void HandleInput(Keyboard.Key key, bool isPressed)
        {
            if (isPressed && key == Keyboard.Key.Return)
            {
                klickButton.Play();
                clicked = true;
            }
            else clicked = false;

            if (isPressed && key == Keyboard.Key.Escape)
            {
                targetState = GameStates.MainMenuState;
            }
        }

        /// <summary>
        /// Initializes the state.
        /// </summary>
        public override void Initialize()
        {
            targetState = GameStates.CreditScreenState;
            clicked = false;
        }

        /// <summary>
        /// Updates the state.
        /// </summary>
        /// <returns>State for the next frame.</returns>
        public override GameStates Update()
        {
            backButton.Update(true, clicked);

            return targetState;
        }
    }
}
