using System;
using UnityEngine;

public class GameStateManager : MonoSingleton<GameStateManager>
{
    #region VARIABLES

    private GameStateType _currentGameState;

    #endregion

    #region PROPERTIES

    public GameStateType CurrentGameState => _currentGameState;
    private GUIController GUIController => GUIController.Instance;

    #endregion

    #region METHODS

    private void Start()
    {
        AttachEvents();
        RefreshCurrentState(true);
    }

    private void OnDestroy()
    {
        DetachEvents();
    }

    private void AttachEvents()
    {
        GameEvents.OnOpenedViewsChanged += HandleOpenedViewsChanged;

    }

    private void DetachEvents()
    {
        GameEvents.OnOpenedViewsChanged -= HandleOpenedViewsChanged;
    }

    private void RefreshCurrentState(bool ignoreCurrentState = false)
    {
        GameStateType newState = CurrentGameState;
        if (GUIController)
        {
            if (GUIController.Instance.AnyViewOpen)
                newState = GameStateType.UI_VIEW;
            else newState = GameStateType.GAMEPLAY;
        }

        if (ignoreCurrentState || CurrentGameState != newState)
        {
            _currentGameState = newState;
            GameEvents.OnGameStateChanged?.Invoke();
        }
    }

    #region HANDLERS

    private void HandleOpenedViewsChanged()
    {
        RefreshCurrentState();
    }

    #endregion

    #endregion
}
