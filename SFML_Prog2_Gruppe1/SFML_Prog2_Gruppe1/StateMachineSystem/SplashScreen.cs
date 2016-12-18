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
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.StateMachineSystem
{
    public class SplashScreen : State
    {
        Clock clock;

        public SplashScreen()
        {
            Initialize();
        }


        public override void Dispose()
        {
            //throw new NotImplementedException();
        }

        public override void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Clear(Color.Blue);
        }

        public override void HandleInput(Keyboard.Key key, bool isPressed)
        {
        }

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
            if(clock.ElapsedTime.AsSeconds() >= 1f)
            {
                return GameStates.MainMenuState;
            }
            return GameStates.SplashScreenState;
        }
    }
}