using Gameplay.Inputs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InputActionButton : UISelectable
{
    #region VARIABLES

    [SerializeField] private InputActionReference _inputAction;
    [SerializeField] private bool _onlyWhenTopView = true;
    [SerializeField] private Image _buttonActionIcon;
    [SerializeField] private TriggerPhase _trigger = TriggerPhase.PERFORMED;

    private UiView _parentView;

    private InputAction _actionResolved;

    #endregion

    #region PROPERTIES

    public Button Button { get; private set; }
    private GUIController UIController => GUIController.Instance;
    private InputManager InputManager => InputManager.Instance;

    #endregion

    #region UNITY_METHODS

    private void Awake()
    {
        Button = GetComponent<Button>();
        _parentView = GetComponentInParent<UiView>(true);
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

    public override void OnSelect()
    {
        base.OnSelect();
        UIButtonHover.Hover(Button);
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
        UIButtonHover.Unhover(Button);
    }

    public override void ToggleTransition(bool state)
    {
        base.ToggleTransition(state);
        if (state)
            RefreshIcon();
        else
            _buttonActionIcon.gameObject.SetActive(false);
    }

    private void RefreshIcon()
    {
        if (_buttonActionIcon == null)
            return;

        if (InputManager == null)
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


    #region HANDLERS

    private void HandleInputAction(InputAction.CallbackContext context)
    {
        if (!isActiveAndEnabled)
            return;
        if (Button == null)
            return;
        if (!Button.interactable)
            return;
        if (!gameObject.activeInHierarchy)
            return;

        if (_onlyWhenTopView && UIController != null)
        {
            if (_parentView != null && UIController.TopView != _parentView)
                return;
        }

        Button.onClick?.Invoke();
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
