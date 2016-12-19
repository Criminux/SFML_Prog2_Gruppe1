using SFML.Window;

namespace SFML_Prog2_Gruppe1.StateMachineSystem
{
    /// <summary>
    /// Serves as a template for all the different game states.
    /// </summary>
    public abstract class State
    {
        public abstract void Initialize();

        public abstract GameStates Update();

        public abstract void Draw();

        public abstract void Dispose();

        public abstract void HandleInput(Keyboard.Key key, bool isPressed);
    }
}
