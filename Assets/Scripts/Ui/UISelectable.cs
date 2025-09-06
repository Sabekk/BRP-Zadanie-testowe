using System;
using UnityEngine;

public abstract class UISelectable : UISelectableRaw
{
    #region VARIABLES

    [SerializeField, Tooltip("Can be null")] private GameObject _selection;

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
    public override void ToggleTransition(bool state)
    {
        base.ToggleTransition(state);
        if (_selection)
            _selection.SetActive(state);
    }

    public UISelectable GetNeighbour(Vector2 direction, UISelectable core=null)
    {
        if (core == null) 
            core = this;
        else if (core == this) 
            return null;

        UISelectable neighbour = null;
        if (direction.normalized.x < 0)
            neighbour = _leftNeighbour;
        if (direction.normalized.x > 0)
            neighbour = _rightNeighbour;
        if (direction.normalized.y > 0)
            neighbour = _topNeighbour;
        if (direction.normalized.y < 0)
            neighbour = _bottomNeighbour;

        if (neighbour == null)
            return null;

        if (neighbour.CanBeSelected())
            return neighbour;

        return neighbour.GetNeighbour(direction, core);
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
