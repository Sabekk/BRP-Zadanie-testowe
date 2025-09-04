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