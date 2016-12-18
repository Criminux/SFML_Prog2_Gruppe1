using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.StateMachineSystem
{
    public class CreditScreen : State
    {
        private Button backButton;

        private Texture creditsScreen;
        private Sprite creditSprite;

        private GameStates targetState;

        bool clicked;

        public CreditScreen()
        {
            backButton = new Button(new Vector2f(100, 100), "Back to Menu");
            backButton.Click += backButton_Click;

            targetState = GameStates.PauseMenuState;
            clicked = false;

            creditsScreen = new Texture("StateMachineSystem/Menu/CreditsScreenBackground.png");
            creditSprite = new Sprite(creditsScreen);
        }

        private void backButton_Click(object sender, System.EventArgs e)
        {
            targetState = GameStates.MainMenuState;
        }

        public override void Dispose()
        {
        }

        public override void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(creditSprite);

            backButton.Draw();
        }

        public override void HandleInput(Keyboard.Key key, bool isPressed)
        {
            if (isPressed && key == Keyboard.Key.Return)
            {
                clicked = true;
            }
            else clicked = false;

            if (isPressed && key == Keyboard.Key.Escape)
            {
                targetState = GameStates.MainMenuState;
            }
        }

        public override void Initialize()
        {
            targetState = GameStates.CreditScreenState;
            clicked = false;
        }

        public override GameStates Update()
        {
            backButton.Update(true, clicked);
              
            return targetState;
        }
    }
}
