using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UISelectableSlider : UISelectable
{
    #region VARIABLES

    [SerializeField] private Slider _slider;
    [SerializeField] private float changingMultiple = 0.1f;
    #endregion

    #region PROPERTIES

    #endregion

    #region UNITY_METHODS

    private void Awake()
    {
        if (_slider == null)
            _slider = GetComponent<Slider>();
    }

    #endregion

    #region METHODS

    public void ChangeValue(float direction)
    {
        _slider.value += direction * changingMultiple;
    }

    #endregion
}
