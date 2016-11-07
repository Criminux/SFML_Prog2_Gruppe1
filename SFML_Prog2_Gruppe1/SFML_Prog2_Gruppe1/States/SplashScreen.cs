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

namespace SFML_Prog2_Gruppe1.States
{
    public class SplashScreen : State
    {
        public SplashScreen()
        {

        }


        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Clear(Color.Blue);
        }

        public override void Initialize()
        {
            //throw new NotImplementedException();
        }

        public override GameStates Update()
        {
            //throw new NotImplementedException();
            return GameStates.SplashScreenState;
        }
    }
}
