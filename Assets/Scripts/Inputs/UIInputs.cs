using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Inputs
{
    public class UIInputs : InputsBase, InputBinds.IUIActions
    {
        #region ACTIONS

        public event Action OnAcceptInput;
        public event Action OnBackInput;
        public event Action OnDeleteInput;
        public event Action<Vector2> OnUINavigation;

        #endregion

        #region CONSTRUCTORS

        public UIInputs(InputBinds binds) : base(binds)
        {
            Binds.UI.SetCallbacks(this);
        }

        #endregion

        #region METHODS

        public override void Disable()
        {
            Binds.UI.Disable();
        }

        public override void Enable()
        {
            Binds.UI.Enable();
        }

        public void OnAccept(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAcceptInput?.Invoke();
        }

        public void OnBack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnBackInput?.Invoke();
        }

        public void OnDelete(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnDeleteInput?.Invoke();
        }

        public void OnNavigation(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnUINavigation?.Invoke(context.ReadValue<Vector2>());
        }

        #endregion
    }
}