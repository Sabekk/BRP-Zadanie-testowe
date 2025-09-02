using System;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    #region VARIABLES

    private float _currentScore;

    #endregion

    #region PROPERTIES

    private float Score
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            GameEvents.OnScoreUpdated?.Invoke(_currentScore);
        }
    }

    #endregion

    #region UNITY_METHODS

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
        GameEvents.EnemyKilled += EnemyKilled;
    }

    private void DetachEvents()
    {
        GameEvents.EnemyKilled -= EnemyKilled;
    }

    #region HANDLERS

    #endregion

    private void EnemyKilled(EnemyKilledEventArgs e)
    {
        float reward = e.Enemy.Data.Score;
        if (e.WasKilledByWeakness())
        {
            reward *= e.Enemy.Data.ScoreMultiplerForWeakness;
        }

        Score += reward;
    }

    #endregion
}
