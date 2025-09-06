using System;
using UnityEngine;

public abstract class UISelectable : UISelectableRaw
{
    #region VARIABLES

    [Header("Neighbours")]
    [SerializeField] private UISelectable _topNeighbour;
    [SerializeField] private UISelectable _bottomNeighbour;
    [SerializeField] private UISelectable _leftNeighbour;
    [SerializeField] private UISelectable _rightNeighbour;

    private Action<UISelectable> OnSelected;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public UISelectable GetNeighbour(Vector2 direction)
    {
        if (direction.normalized.x < 0)
            return _leftNeighbour;
        if (direction.normalized.x > 0)
            return _rightNeighbour;
        if (direction.normalized.y > 0)
            return _topNeighbour;
        if (direction.normalized.y < 0)
            return _bottomNeighbour;

        return null;
    }

    public void SetUiView(UiView parentView, Action<UISelectable> onSelected)
    {
        SetUiView(parentView);
        OnSelected = onSelected;
    }

    public override void OnSelect()
    {
        base.OnSelect();
        OnSelected?.Invoke(this);
    }

    #endregion
}
