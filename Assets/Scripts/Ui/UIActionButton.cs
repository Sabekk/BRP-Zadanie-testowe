using Gameplay.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIActionButton : InputActionRaw<Button>
{
    #region METHODS

    protected override void MakeSelectableAction()
    {
        base.MakeSelectableAction();
        Selectable.onClick?.Invoke();
    }

    #endregion
}
