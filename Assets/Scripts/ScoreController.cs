using System;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    #region VARIABLES

    private int _currentScore;

    #endregion

    #region PROPERTIES

    private int Score
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            //Event
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

    private void EnemyKilled(IEnemy enemy)
    {

    }

    #endregion
}
