using System;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    #region VARIABLES

    private float _currentScore;

    #endregion

    #region PROPERTIES

    public static ScoreController Instance { get; private set; }
    public float Score
    {
        get => _currentScore;
        set
        {
            if (_currentScore == value)
                return;

            _currentScore = value;
            GameEvents.OnScoreUpdated?.Invoke();
        }
    }

    #endregion

    #region UNITY_METHODS

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        AttachEvents();
    }

    private void OnDisable()
    {
        DetachEvents();
    }

    #endregion

    #region METHODS

    private void Initialize()
    {
        Score = 0;
    }

    private void AttachEvents()
    {
        GameEvents.EnemyKilled += HandleEnemyKilled;
        GameEvents.OnSoulItemUsed += HandleSoulItemUsed;
    }

    private void DetachEvents()
    {
        GameEvents.EnemyKilled -= HandleEnemyKilled;
        GameEvents.OnSoulItemUsed -= HandleSoulItemUsed;
    }

    #region HANDLERS

    private void HandleEnemyKilled(EnemyKilledEventArgs e)
    {
        float reward = e.Enemy.Data.Score;
        if (e.WasKilledByWeakness())
        {
            reward *= e.Enemy.Data.ScoreMultiplerForWeakness;
        }

        Score += reward;
    }

    private void HandleSoulItemUsed(SoulInformation soulInformation)
    {
        Score += soulInformation.soulItem.PointsFromUsing;
    }

    #endregion

    #endregion
}
