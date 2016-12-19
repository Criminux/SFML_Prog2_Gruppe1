namespace SFML_Prog2_Gruppe1.StateMachineSystem
{
    /// <summary>
    /// Enum which holds the different states of the programm.
    /// </summary>
    public enum GameStates
    {
        UnspecifiedState = 0,
        SplashScreenState = 1,
        MainMenuState = 11,
        PauseMenuState = 12,
        GamePlayState = 21,
        CreditScreenState = 31,
        QuitState = 99
    }
}
