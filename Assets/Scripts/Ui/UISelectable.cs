using UnityEngine;

public abstract class UISelectable : MonoBehaviour
{
    #region VARIABLES

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public virtual void OnSelect()
    {
        ToggleTransition(true);
    }

    public virtual void OnDeselect()
    {
        ToggleTransition(false);
    }

    public virtual void ToggleTransition(bool state)
    {

    }

    #endregion
}
