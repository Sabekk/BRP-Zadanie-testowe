using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Inputs
{
    public class GameplayInputs : InputsBase, InputBinds.IGameplayActions
    {
        #region ACTIONS

        public event Action<Vector2> OnNavigate;

        #endregion

        #region CONSTRUCTORS

        public GameplayInputs(InputBinds binds) : base(binds)
        {
            Binds.Gameplay.SetCallbacks(this);
        }

        #endregion

        #region METHODS

        public override void Disable()
        {
            Binds.Gameplay.Disable();
        }

        public override void Enable()
        {
            Binds.Gameplay.Enable();
        }

        public void OnInventory(InputAction.CallbackContext context)
        {

        }

        public void OnNavigation(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnNavigate.Invoke(context.ReadValue<Vector2>());
        }

        public void OnPause(InputAction.CallbackContext context)
        {

        }

        public void OnUseSword(InputAction.CallbackContext context)
        {

        }

        public void OnUseBow(InputAction.CallbackContext context)
        {

        }

        public void OnSelection(InputAction.CallbackContext context)
        {

        }

        #endregion
    }
}