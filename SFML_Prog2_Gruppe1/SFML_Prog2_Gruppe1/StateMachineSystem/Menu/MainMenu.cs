using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.StateMachineSystem
{
    /// <summary>
    /// MainMenu as State.
    /// </summary>
    public class MainMenu : State
    {
        private Button startButton;
        private Button exitButton;
        private Button creditsButton;

        private Texture menuBackground;
        private Sprite menuSprite;

        private SoundBuffer switchButtonBuffer;
        private SoundBuffer klickButtonBuffer;

        private Sound switchButton;
        private Sound klickButton;

        private GameStates targetState;

        private int currentSelectionIndex;
        private bool clicked;

        /// <summary>
        /// Constructor for main menu state.
        /// </summary>
        public MainMenu()
        {
            switchButtonBuffer = new SoundBuffer("StateMachineSystem/Menu/Klick.wav");
            switchButton = new Sound(switchButtonBuffer);

            klickButtonBuffer = new SoundBuffer("StateMachineSystem/Menu/Klack.wav");
            klickButton = new Sound(klickButtonBuffer);

            startButton = new Button(new Vector2f(500, 300), "Start");
            creditsButton = new Button(new Vector2f(500, 420), "Credits");
            exitButton = new Button(new Vector2f(500, 540), "Exit");
            
            startButton.Click += startButton_Click;
            creditsButton.Click += creditsButton_Click;
            exitButton.Click += exitButton_Click;

            targetState = GameStates.MainMenuState;
            clicked = false;

            menuBackground = new Texture("StateMachineSystem/Menu/MenuBackground.png");
            menuSprite = new Sprite(menuBackground);

            currentSelectionIndex = 0;
        }
        
        /// <summary>
        /// Clickevent method for start.
        /// </summary>
        /// <param name="sender">Object param.</param>
        /// <param name="e">Event arguments param.</param>
        private void startButton_Click(object sender, System.EventArgs e)
        {
            targetState = GameStates.GamePlayState;
        }

        /// <summary>
        /// Clickevent method for credits.
        /// </summary>
        /// <param name="sender">Object param.</param>
        /// <param name="e">Event arguments param.</param>
        private void creditsButton_Click(object sender, System.EventArgs e)
        {
            targetState = GameStates.CreditScreenState;
        }

        /// <summary>
        /// Clickevent method for exit.
        /// </summary>
        /// <param name="sender">Object param.</param>
        /// <param name="e">Event arguments param.</param>
        private void exitButton_Click(object sender, System.EventArgs e)
        {
            targetState = GameStates.QuitState;
        }

        /// <summary>
        /// Disposing the state.
        /// </summary>
        public override void Dispose()
        {
        }

        /// <summary>
        /// Draws the main menu.
        /// </summary>
        public override void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(menuSprite);

            startButton.Draw();
            creditsButton.Draw();
            exitButton.Draw();
        }

        /// <summary>
        /// Handle input in main menu.
        /// </summary>
        /// <param name="key">Pressed key.</param>
        /// <param name="isPressed">is the key pressed.</param>
        public override void HandleInput(Keyboard.Key key, bool isPressed)
        {
            if ((isPressed && key == Keyboard.Key.W) || (isPressed && key == Keyboard.Key.Up))
            {
                switchButton.Play();
                currentSelectionIndex = currentSelectionIndex - 1;
                if (currentSelectionIndex < 0) currentSelectionIndex = 0;
            }
            if ((isPressed && key == Keyboard.Key.S) || (isPressed && key == Keyboard.Key.Down))
            {
                switchButton.Play();
                currentSelectionIndex = currentSelectionIndex + 1;
                if (currentSelectionIndex > 2) currentSelectionIndex = 2;
            }

            if (isPressed && key == Keyboard.Key.Return)
            {
                klickButton.Play();
                clicked = true;
            }
            else clicked = false;

            if (isPressed && key == Keyboard.Key.Escape)
            {
                targetState = GameStates.QuitState;
            }
        }

        /// <summary>
        /// Initializes the state.
        /// </summary>
        public override void Initialize()
        {
            targetState = GameStates.MainMenuState;
            clicked = false;
        }

        /// <summary>
        /// Update the state.
        /// </summary>
        /// <returns>State for next frame.</returns>
        public override GameStates Update()
        {
            switch(currentSelectionIndex)
            {
                case 0:
                    startButton.Update(true, clicked);
                    creditsButton.Update(false, clicked);
                    exitButton.Update(false, clicked);
                    break;
                case 1:
                    startButton.Update(false, clicked);
                    creditsButton.Update(true, clicked);
                    exitButton.Update(false, clicked);
                    break;
                case 2:
                    startButton.Update(false, clicked);
                    creditsButton.Update(false, clicked);
                    exitButton.Update(true, clicked);
                    break;
            }

            return targetState;
        }
    }
}
