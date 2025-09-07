using Gameplay.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class InputActionRaw<T> : UISelectableRaw where T : Selectable
{
    #region VARIABLES

    [SerializeField] private InputActionReference _inputAction;
    [SerializeField] private bool _onlyIfSelected = false;
    [SerializeField] private bool _onlyWhenTopView = true;
    [SerializeField] private Image _buttonActionIcon;
    [SerializeField] private TriggerPhase _trigger = TriggerPhase.PERFORMED;

    private InputAction _actionResolved;

    #endregion

    #region PROPERTIES

    public T Selectable { get; set; }
    private GUIController UIController => GUIController.Instance;
    private InputManager InputManager => InputManager.Instance;

    #endregion

    #region UNITY_METHODS

    private void Awake()
    {
        Selectable = GetComponent<T>();
    }

    private void Start()
    {
        RefreshIcon();
    }

    private void OnEnable()
    {
        ResolveRuntimeAction();
        AttachEvents();
        RefreshIcon();
    }

    private void OnDisable()
    {
        if (_buttonActionIcon)
            _buttonActionIcon.gameObject.SetActive(false);
        DetachEvents();
    }

    #endregion

    #region METHODS

    public override bool CanBeSelected()
    {
        return Selectable != null && Selectable.interactable && Selectable.enabled && isActiveAndEnabled;
    }

    public override void OnSelect()
    {
        base.OnSelect();
        UIHover.Hover(Selectable);
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
        UIHover.Unhover(Selectable);
    }

    public override void ToggleTransition(bool state)
    {
        if (state)
            RefreshIcon();
        else
            _buttonActionIcon.gameObject.SetActive(false);
    }

    protected virtual void MakeSelectableAction()
    {

    }

    private void RefreshIcon()
    {
        if (_buttonActionIcon == null)
            return;

        if (InputManager == null || CanIntegrate() == false)
        {
            _buttonActionIcon.gameObject.SetActive(false);
            return;
        }
        else
            _buttonActionIcon.gameObject.SetActive(true);

        Sprite newInputSprite = InputManager.InpuIconsController.GetActionIcon(_actionResolved);
        if (newInputSprite != null)
        {
            _buttonActionIcon.gameObject.SetActive(true);
            _buttonActionIcon.sprite = newInputSprite;
        }
        else
        {
            _buttonActionIcon.gameObject.SetActive(false);
        }
    }

    private void ResolveRuntimeAction()
    {
        _actionResolved = null;
        if (_inputAction != null && _inputAction.action != null)
        {
            var binds = InputBindsController.Input;
            _actionResolved = binds?.asset?.FindAction(_inputAction.action.id) ?? _inputAction.action;
        }
    }

    private void AttachEvents()
    {
        if (_actionResolved != null)
            switch (_trigger)
            {
                case TriggerPhase.STARTED:
                    _actionResolved.started += HandleInputAction;
                    break;
                case TriggerPhase.PERFORMED:
                    _actionResolved.performed += HandleInputAction;
                    break;
                default:
                    break;
            }

        GameEvents.OnInputDeviceChanged += HandleDeviceChanged;
    }

    private void DetachEvents()
    {
        if (_actionResolved != null)
            switch (_trigger)
            {
                case TriggerPhase.STARTED:
                    _actionResolved.started -= HandleInputAction;
                    break;
                case TriggerPhase.PERFORMED:
                    _actionResolved.performed -= HandleInputAction;
                    break;
                default:
                    break;
            }

        GameEvents.OnInputDeviceChanged -= HandleDeviceChanged;
    }

    private bool CanIntegrate()
    {
        if (_onlyIfSelected && !IsSelected)
            return false;
        if (!isActiveAndEnabled)
            return false;
        if (Selectable == null)
            return false;
        if (!Selectable.interactable)
            return false;
        if (!gameObject.activeInHierarchy)
            return false;

        return true;
    }

    #region HANDLERS

    private void HandleInputAction(InputAction.CallbackContext context)
    {
        if (CanIntegrate() == false)
            return;

        if (_onlyWhenTopView && UIController != null)
        {
            //if (ParentView != null && !ParentView.IsTopOnView)
            if (!UIInputGate.TryConsume(ParentView, _onlyWhenTopView))
                return;
        }


        MakeSelectableAction();
    }

    private void HandleDeviceChanged(InputDeviceType deviceType)
    {
        RefreshIcon();
    }

    #endregion

    #endregion

    #region ENUMS

    public enum TriggerPhase
    {
        STARTED,
        PERFORMED
    }

    #endregion
}
