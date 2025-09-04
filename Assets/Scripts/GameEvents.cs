using System;

public static class GameEvents
{
    public static Action<EnemyKilledEventArgs> EnemyKilled;
    public static Action OnScoreUpdated;
    public static Action<SoulInformation> OnSoulItemUsed;
    public static Action<InputDeviceType> OnInputDeviceChanged;
}

