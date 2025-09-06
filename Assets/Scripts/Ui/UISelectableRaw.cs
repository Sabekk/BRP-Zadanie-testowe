using UnityEngine;

public class UISelectableRaw : MonoBehaviour
{
    #region VARIABLES

    #endregion

    #region PROPERTIES

    public bool IsSelected { get; set; }
    protected UiView ParentView { get; set; }

    #endregion

    #region METHODS

    public virtual void SetUiView(UiView parentView)
    {
        ParentView = parentView;
    }

    public virtual void OnSelect()
    {
        IsSelected = true;
        ToggleTransition(true);
    }

    public virtual void OnDeselect()
    {
        IsSelected = false;
        ToggleTransition(false);
    }

    public virtual void ToggleTransition(bool state)
    {

    }

    #endregion
}
