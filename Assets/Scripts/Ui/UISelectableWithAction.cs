using UnityEngine;

public class UISelectableWithAction : UISelectable
{
    #region VARIABLES

    [SerializeField] private InputActionButton _inputAction;

    #endregion

    #region PROPERTIES

    #endregion

    #region UNITY_METHODS

    private void Start()
    {
        _inputAction.enabled = false;
    }

    #endregion

    #region METHODS

    public override void OnSelect()
    {
        _inputAction.enabled = true;
    }

    public override void OnDeselect()
    {
        _inputAction.enabled = false;
    }

    #endregion
}
