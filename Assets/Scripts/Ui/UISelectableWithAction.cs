using UnityEngine;

public class UISelectableWithAction : UISelectable
{
    #region VARIABLES

    [SerializeField] private InputActionButton _inputAction;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public override void SetUiView(UiView parentView)
    {
        base.SetUiView(parentView);
        _inputAction.SetUiView(parentView);
    }

    public override void OnSelect()
    {
        base.OnSelect();
        _inputAction.OnSelect();
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
        _inputAction.OnDeselect();
    }

    #endregion
}
