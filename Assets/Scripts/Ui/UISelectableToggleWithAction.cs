using UnityEngine;

public class UISelectableToggleWithAction : UISelectable
{
    #region VARIABLES

    [SerializeField] private UIActionToggler _inputAction;

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

    public override bool CanBeSelected()
    {
        return _inputAction.CanBeSelected() && isActiveAndEnabled;
    }

    #endregion
}
