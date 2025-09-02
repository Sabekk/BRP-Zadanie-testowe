using System.Text;
using UnityEngine;
using TMPro;
using System;

public class ScoreInformation : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private GameObject _scoreBody;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private const string SCORE_FORMAT = "Score: {0}";
    private StringBuilder _builder = new StringBuilder();

    #endregion

    #region PROPERTIES

    private ScoreController Controller => ScoreController.Instance;

    #endregion

    #region UNITY_METHODS

    private void OnEnable()
    {
        AttachEvents();
        RefreshScore();
    }

    private void OnDisable()
    {
        DetachEvents();
    }

    #endregion

    #region METHODS

    private void RefreshScore()
    {
        if (Controller == null)
        {
            _scoreBody.SetActive(false);
            return;
        }

        if (!_scoreBody.activeInHierarchy)
            _scoreBody.SetActive(true);

        ApplyText(Controller.Score);
    }

    private void AttachEvents()
    {
        GameEvents.OnScoreUpdated += HandleScoreUpdated;
    }

    private void DetachEvents()
    {
        GameEvents.OnScoreUpdated -= HandleScoreUpdated;
    }

    private void ApplyText(params object[] args)
    {
        _builder.Clear();
        _builder.AppendFormat(SCORE_FORMAT, args);
        _scoreText.SetText(_builder);
    }

    #region HANDLERS

    private void HandleScoreUpdated()
    {
        RefreshScore();
    }

    #endregion

    #endregion
}
