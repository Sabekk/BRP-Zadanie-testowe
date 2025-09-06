using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UIActionToggler : InputActionRaw<Toggle>
{
    #region METHODS

    protected override void MakeSelectableAction()
    {
        base.MakeSelectableAction();
        Selectable.isOn = !Selectable.isOn;
    }

    #endregion
}
