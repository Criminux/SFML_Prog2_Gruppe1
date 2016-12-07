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
        private bool clicked;


        public MainMenu()
        {
            startButton = new Button(new Vector2f(600, 350), "Start");
            exitButton = new Button(new Vector2f(600, 400), "Exit");
            
            startButton.Click += startButton_Click;
            exitButton.Click += exitButton_Click;

            targetState = GameStates.MainMenuState;
            clicked = false;

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
            if ((isPressed && key == Keyboard.Key.W) || (isPressed && key == Keyboard.Key.Up))
            {
                currentSelectionIndex = currentSelectionIndex - 1;
                if (currentSelectionIndex < 0) currentSelectionIndex = 0;
            }
            if ((isPressed && key == Keyboard.Key.S) || (isPressed && key == Keyboard.Key.Down))
            {
                currentSelectionIndex = currentSelectionIndex + 1;
                if (currentSelectionIndex > 1) currentSelectionIndex = 1;
            }

            if (isPressed && key == Keyboard.Key.Return)
            {
                clicked = true;
            }
            else clicked = false;
        }

        public override void Initialize()
        {
        }

        public override GameStates Update()
        {
            switch(currentSelectionIndex)
            {
                case 0:
                    startButton.Update(true, clicked);
                    exitButton.Update(false, clicked);
                    break;
                case 1:
                    startButton.Update(false, clicked);
                    exitButton.Update(true, clicked);
                    break;
            }

            return targetState;
        }
    }
}
