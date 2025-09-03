using UnityEngine;

namespace Gameplay.Inputs
{
    public class InputManager : MonoSingleton<InputManager>
    {
        #region VARIABLES

        static InputBinds _controll;

        #endregion

        #region PROPERTIES

        public UIInputs UiInputs { get; private set; }
        public GameplayInputs GameplayInputs { get; private set; }
        public static InputBinds Input
        {
            get
            {
                if (_controll == null)
                    _controll = new InputBinds();
                return _controll;
            }
        }


        #endregion

        #region UNITY_METHODS

        private void Start()
        {
            AttachEvents();
        }

        private void OnEnable() => Input.Enable();

        private void OnDisable() => Input.Disable();

        private void OnDestroy()
        {
            DetachEvents();
        }

        #endregion

        #region METHODS

        public void Initialzie()
        {
            UiInputs = new(Input);
            GameplayInputs = new(Input);

            RefreshInputs();
        }


        private void AttachEvents()
        {
            //TODO Add Gameplay State events
        }

        private void DetachEvents()
        {
            //TODO Add Gameplay State events
        }

        private void RefreshInputs()
        {
            //TODO Add Gameplay State switch

            GameplayInputs.Enable();
            UiInputs.Disable();
        }

        #endregion
    }
}