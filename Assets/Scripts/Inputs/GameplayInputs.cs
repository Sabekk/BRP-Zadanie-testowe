using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Inputs
{
    public class GameplayInputs : InputsBase, InputBinds.IGameplayActions
    {
        #region ACTIONS

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

        public void OnMakeAction(InputAction.CallbackContext context)
        {

        }

        public void OnNavigation(InputAction.CallbackContext context)
        {

        }

        public void OnPause(InputAction.CallbackContext context)
        {

        }

        #endregion
    }
}