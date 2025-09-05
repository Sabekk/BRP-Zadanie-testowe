using UnityEngine;

namespace Gameplay.Inputs
{
    //TODO Remove MonoBehaviour
    public class InputMapController : MonoBehaviour
    {
        #region VARIABLES

        static InputBinds _controll;

        #endregion

        #region PROPERTIES
        public static InputBinds Input
        {
            get
            {
                if (_controll == null)
                    _controll = new InputBinds();
                return _controll;
            }
        }

        public UIInputs UiInputs { get; private set; }
        public GameplayInputs GameplayInputs { get; private set; }

        private GameStateManager GameStateManager => GameStateManager.Instance;

        #endregion

        #region UNITY_METHODS

        private void OnEnable() => Input.Enable();

        private void OnDisable() => Input.Disable();

        #endregion

        #region METHODS

        public void Initialzie()
        {
            UiInputs = new(Input);
            GameplayInputs = new(Input);
        }

        public void LateInitialzie()
        {
            AttachEvents();
            RefreshInputs();
        }

        public void CleanUp()
        {
            DetachEvents();
        }

        private void AttachEvents()
        {
            GameEvents.OnGameStateChanged += HandleGameStateChanged;
        }

        private void DetachEvents()
        {
            GameEvents.OnGameStateChanged -= HandleGameStateChanged;
        }

        private void RefreshInputs()
        {
            if (GameStateManager)
            {
                switch (GameStateManager.CurrentGameState)
                {
                    case GameStateType.GAMEPLAY:
                        GameplayInputs.Enable();
                        UiInputs.Disable();
                        break;
                    case GameStateType.UI_VIEW:
                        GameplayInputs.Disable();
                        UiInputs.Enable();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Debug.LogError("GameStateManager is null. Can't change inputs!");
                GameplayInputs.Enable();
                UiInputs.Disable();
            }
        }

        #region HANDLERS

        private void HandleGameStateChanged()
        {
            RefreshInputs();
        }

        #endregion

        #endregion
    }
}