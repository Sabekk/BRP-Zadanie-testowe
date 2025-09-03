using UnityEngine;

public enum GameLocalization
{
    SWAMPS,
    DUNGEON,
    CASTLE,
    CITY,
    TOWER
}

public class GameControlller : MonoSingleton<GameControlller>
{
    [SerializeField] private GameLocalization currentGameLocalization;

    public GameLocalization CurrentGameLocalization
    {
        get => currentGameLocalization;

        set => currentGameLocalization = value;
    }

    private bool _isPaused;

    public bool IsPaused
    {

        get => _isPaused;
        set
        {
            _isPaused = value;
            Time.timeScale = _isPaused ? 0f : 1f;
        }
    }

    public bool IsCurrentLocalization(GameLocalization localization)
    {
        return CurrentGameLocalization == localization;
    }
}