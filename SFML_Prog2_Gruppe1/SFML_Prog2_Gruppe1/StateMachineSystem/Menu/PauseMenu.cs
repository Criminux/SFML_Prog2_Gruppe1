using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.StateMachineSystem
{
    public class PauseMenu : State
    {
        private Button resumeButton;
        private Button menuButton;

        private Texture menuBackground;
        private Sprite menuSprite;

        private GameStates targetState;

        private SoundBuffer switchButtonBuffer;
        private SoundBuffer klickButtonBuffer;

        private Sound switchButton;
        private Sound klickButton;

        private int currentSelectionIndex;
        private bool clicked;

        public GameStates TargetState
        {
            get { return targetState; }
        }

        public PauseMenu()
        {
            switchButtonBuffer = new SoundBuffer("StateMachineSystem/Menu/Klick.wav");
            switchButton = new Sound(switchButtonBuffer);

            klickButtonBuffer = new SoundBuffer("StateMachineSystem/Menu/Klack.wav");
            klickButton = new Sound(klickButtonBuffer);

            resumeButton = new Button(new Vector2f(500, 300), "Resume");
            menuButton = new Button(new Vector2f(500, 420), "Back to Menu");

            resumeButton.Click += resumeButton_Click;
            menuButton.Click += exitButton_Click;

            targetState = GameStates.PauseMenuState;
            clicked = false;

            menuBackground = new Texture("StateMachineSystem/Menu/PauseMenuBackground.png");
            menuSprite = new Sprite(menuBackground);

            currentSelectionIndex = 0;
        }

        private void resumeButton_Click(object sender, System.EventArgs e)
        {
            targetState = GameStates.GamePlayState;
        }
        private void exitButton_Click(object sender, System.EventArgs e)
        {
            targetState = GameStates.MainMenuState;
        }

        public override void Dispose()
        {
        }

        public override void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(menuSprite);

            //Draw Buttons
            resumeButton.Draw();
            menuButton.Draw();
        }

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
                if (currentSelectionIndex > 1) currentSelectionIndex = 1;
            }

            if (isPressed && key == Keyboard.Key.Return)
            {
                klickButton.Play();
                clicked = true;
            }
            else clicked = false;

            if (isPressed && key == Keyboard.Key.Escape)
            {
                targetState = GameStates.GamePlayState;
            }
        }

        public override void Initialize()
        {
            targetState = GameStates.PauseMenuState;
            clicked = false;
        }

        public override GameStates Update()
        {
            switch (currentSelectionIndex)
            {
                case 0:
                    resumeButton.Update(true, clicked);
                    menuButton.Update(false, clicked);
                    break;
                case 1:
                    resumeButton.Update(false, clicked);
                    menuButton.Update(true, clicked);
                    break;
            }

            return targetState;
        }
    }
}
