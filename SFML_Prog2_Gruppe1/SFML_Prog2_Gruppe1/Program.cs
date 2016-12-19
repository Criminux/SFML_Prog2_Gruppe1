using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML_Prog2_Gruppe1.Util;
using SFML_Prog2_Gruppe1.StateMachineSystem;
using System;

namespace SFML_Prog2_Gruppe1
{
    /// <summary>
    /// Entry point of the game.
    /// </summary>
    class Program
    {
        static int ApplicationInterval = 17;
        static int refreshAmount;

        static StateMachine stateMachine;

        static Clock clockPerFrame = new Clock();
        static Clock clockPerSecond = new Clock();

        /// <summary>
        /// Main method of the program for initializing and starting the loop.
        /// </summary>
        static void Main()
        {
            ProjectRenderWindow.GetRenderWindowInstance().SetActive();
            stateMachine = new StateMachine();
            clockPerFrame = new Clock();
            ProjectRenderWindow.GetRenderWindowInstance().Closed += (o, a) => ProjectRenderWindow.GetRenderWindowInstance().Close();

            //The following tho lines raise the events as a pointer to the method
            ProjectRenderWindow.GetRenderWindowInstance().KeyPressed += (o, a) => HandleKeyboardInput(a.Code, true);
            ProjectRenderWindow.GetRenderWindowInstance().KeyReleased += (o, a) => HandleKeyboardInput(a.Code, false);

            while (stateMachine.CurrentState != GameStates.QuitState)
            {
                ApplicationLoop();
                System.Threading.Thread.Sleep(ApplicationInterval);
            }
        }

        /// <summary>
        /// Delegates input to the current state.
        /// </summary>
        /// <param name="key">
        /// The pressed key.
        /// </param>
        /// <param name="isPressed">
        /// Enables different key states/actions depending on if pressed or released.
        /// </param>
        private static void HandleKeyboardInput(Keyboard.Key key, bool isPressed)
        {
            stateMachine.HandleInput(key, isPressed);
        }

        /// <summary>
        /// Lets the game loop itself in a constant frame rate.
        /// </summary>
        private static void ApplicationLoop()
        {
            //Update
            stateMachine.Update();
            ProjectRenderWindow.GetRenderWindowInstance().DispatchEvents();

            //Draw
            ProjectRenderWindow.GetRenderWindowInstance().Clear(Color.White);
            stateMachine.Draw();
            ProjectRenderWindow.GetRenderWindowInstance().Display();

            //Frames & Sleep
            ApplicationInterval = MathUtil.Clamp((30 - clockPerFrame.ElapsedTime.AsMilliseconds()), 0, 30);
            refreshAmount += 1;
            if (clockPerSecond.ElapsedTime.AsSeconds() >= 1)
            {
                clockPerSecond.Restart();
                Console.WriteLine(refreshAmount);
                refreshAmount = 0;
            }

            clockPerFrame.Restart();
        }
    }
}
