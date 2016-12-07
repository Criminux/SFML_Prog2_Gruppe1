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
using SFML_Prog2_Gruppe1.States.Menu;

namespace SFML_Prog2_Gruppe1.States
{
    public class MainMenu : State
    {
        private Button startButton;
        private Button exitButton;

        private Texture menuBackground;
        private Sprite menuSprite;

        private GameStates targetState;

        private int currentSelectionIndex;

        public MainMenu()
        {
            startButton = new Button(new Vector2f(600, 350), "Start");
            exitButton = new Button(new Vector2f(600, 400), "Exit");
            
            startButton.Click += startButton_Click;
            exitButton.Click += exitButton_Click;

            targetState = GameStates.MainMenuState;

            menuBackground = new Texture("States/Menu/MenuBackground.png");
            menuSprite = new Sprite(menuBackground);

            currentSelectionIndex = 0;
        }
        
        private void startButton_Click(object sender, System.EventArgs e)
        {
            targetState = GameStates.GamePlayState;
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
            exitButton.Draw();
        }

        public override void HandleInput(Keyboard.Key key, bool isPressed)
        {
        }

        public override void Initialize()
        {
        }

        public override GameStates Update()
        {
            //TODO: Check Interact Command

            //Update selectionIndex

            switch(currentSelectionIndex)
            {
                case 0:
                    startButton.Update(true);
                    exitButton.Update(false);
                    break;
                case 1:
                    startButton.Update(false);
                    exitButton.Update(true);
                    break;
            }

            return targetState;
        }
    }
}
