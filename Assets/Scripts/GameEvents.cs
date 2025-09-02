using System;

public static class GameEvents
{
    public static Action<EnemyKilledEventArgs> EnemyKilled;
    public static Action<float> OnScoreUpdated;
    public static Action<SoulInformation> OnSoulItemUsed;
}

