using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.StateMachineSystem
{
    public class MainMenu : State
    {
        private Button startButton;
        private Button exitButton;
        private Button creditsButton;

        private Texture menuBackground;
        private Sprite menuSprite;

        private GameStates targetState;

        private int currentSelectionIndex;
        private bool clicked;


        public MainMenu()
        {
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
        
        private void startButton_Click(object sender, System.EventArgs e)
        {
            targetState = GameStates.GamePlayState;
        }
        private void creditsButton_Click(object sender, System.EventArgs e)
        {
            targetState = GameStates.CreditScreenState;
        }
        private void exitButton_Click(object sender, System.EventArgs e)
        {
            targetState = GameStates.QuitState;
        }

        public override void Dispose()
        {
        }

        public override void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(menuSprite);

            //Draw Buttons
            startButton.Draw();
            creditsButton.Draw();
            exitButton.Draw();
        }

        public override void HandleInput(Keyboard.Key key, bool isPressed)
        {
            if ((isPressed && key == Keyboard.Key.W) || (isPressed && key == Keyboard.Key.Up))
            {
                currentSelectionIndex = currentSelectionIndex - 1;
                if (currentSelectionIndex < 0) currentSelectionIndex = 0;
            }
            if ((isPressed && key == Keyboard.Key.S) || (isPressed && key == Keyboard.Key.Down))
            {
                currentSelectionIndex = currentSelectionIndex + 1;
                if (currentSelectionIndex > 2) currentSelectionIndex = 2;
            }

            if (isPressed && key == Keyboard.Key.Return)
            {
                clicked = true;
            }
            else clicked = false;

            if (isPressed && key == Keyboard.Key.Escape)
            {
                targetState = GameStates.QuitState;
            }
        }

        public override void Initialize()
        {
            targetState = GameStates.MainMenuState;
            clicked = false;
        }

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
