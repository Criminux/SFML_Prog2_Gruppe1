﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

using SFML_Prog2_Gruppe1.States;

namespace SFML_Prog2_Gruppe1
{
    public class StateMachine
    {
        private GameStates currentState, previousState, targetState;
        private SplashScreen splashScreen;
        private MainMenu mainMenu;
        private GamePlay gamePlay;
        private PauseMenu pauseMenu;
        private CreditScreen creditScreen;

        public GameStates CurrentState
        {
            get { return currentState; }
        }

        public StateMachine()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the different states.
        /// </summary>
        private void Initialize()
        {
            //Sets the starting state
            currentState = GameStates.SplashScreenState;
            previousState = GameStates.UnspecifiedState;

            //Initializes the different states
            splashScreen = new SplashScreen();
            mainMenu = new MainMenu();
            gamePlay = new GamePlay();
            pauseMenu = new PauseMenu();
            creditScreen = new CreditScreen();
        }

        /// <summary>
        /// Initializes a soecific state.
        /// </summary>
        /// <param name="state">State to be initialized.</param>
        private void InitializeState(State state)
        {
            if (previousState != currentState)
            {
                state.Initialize();
                previousState = currentState;
            }
        }

        private void DisposeState(State state)
        {
            if (targetState != currentState)
            {
                state.Dispose();
                previousState = currentState;
                currentState = targetState;
            }
        }

        /// <summary>
        /// Checks state and updates it.
        /// </summary>
        public void Update()
        {
            switch (currentState)
            {
                case GameStates.SplashScreenState:
                    InitializeState(splashScreen);
                    targetState = splashScreen.Update();
                    DisposeState(splashScreen);
                    break;

                case GameStates.MainMenuState:
                    InitializeState(mainMenu);
                    targetState = mainMenu.Update();
                    DisposeState(mainMenu);
                    break;

                case GameStates.CreditScreenState:
                    InitializeState(creditScreen);
                    targetState = creditScreen.Update();
                    DisposeState(creditScreen);
                    break;

                case GameStates.GamePlayState:
                    InitializeState(gamePlay);
                    targetState = gamePlay.Update();
                    DisposeState(gamePlay);
                    if (targetState == GameStates.CreditScreenState)
                    {
                        gamePlay = new GamePlay();
                    }
                    break;

                case GameStates.QuitState:
                    break;

            }
        }

        /// <summary>
        /// Draws the active state.
        /// </summary>
        public void Draw()
        {
            switch (currentState)
            {
                case GameStates.SplashScreenState:
                    splashScreen.Draw();
                    break;

                case GameStates.MainMenuState:
                    mainMenu.Draw();
                    break;

                case GameStates.GamePlayState:
                    gamePlay.Draw();
                    break;

                case GameStates.CreditScreenState:
                    creditScreen.Draw();
                    break;
            }
        }

        /// <summary>
        /// Delegates the input to active state.
        /// </summary>
        /// <param name="key">Pressed key.</param>
        /// <param name="isPressed">Is the key pressed.</param>
        public void HandleInput(Keyboard.Key key, bool isPressed)
        {
            switch(currentState)
            {
                case GameStates.SplashScreenState:
                    splashScreen.HandleInput(key, isPressed);
                    break;

                case GameStates.MainMenuState:
                    mainMenu.HandleInput(key, isPressed);
                    break;

                case GameStates.GamePlayState:
                    gamePlay.HandleInput(key, isPressed);
                    break;

                case GameStates.CreditScreenState:
                    creditScreen.HandleInput(key, isPressed);
                    break;
            }
        }
    }
}
