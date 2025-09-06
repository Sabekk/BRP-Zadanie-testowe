using UnityEngine;

public class OptionsView : UiView
{
    #region VARIABLES

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    protected override void AttachEventsOfTopView()
    {
        base.AttachEventsOfTopView();
    }

    protected override void DetachEventsOfTopView()
    {
        base.DetachEventsOfTopView();
    }

    protected override void HandleUINavigation(Vector2 direction)
    {
        if (direction.normalized.x == 0)
        {
            base.HandleUINavigation(direction);
        }
        else
        {
            if (CurrentSelected != null && CurrentSelected is UISelectableSlider slider)
            {
                slider.ChangeValue(direction.x);
            }
        }

    }

    #endregion
}
