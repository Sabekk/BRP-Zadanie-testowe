using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Inputs
{
    public class UIInputs : InputsBase, InputBinds.IUIActions
    {
        #region ACTIONS

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

        }

        public void OnBack(InputAction.CallbackContext context)
        {

        }

        public void OnDelete(InputAction.CallbackContext context)
        {

        }

        #endregion
    }
}